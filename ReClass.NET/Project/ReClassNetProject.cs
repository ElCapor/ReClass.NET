using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using DeClassNet.Nodes;
using DeClassNet.Util;

namespace DeClassNet.Project
{
	public class ReClassNetProject : IDisposable
	{
		public delegate void ClassesChangedEvent(ClassNode sender);
		public event ClassesChangedEvent ClassAdded;
		public event ClassesChangedEvent ClassRemoved;

		public delegate void EnumsChangedEvent(EnumDescription sender);
		public event EnumsChangedEvent EnumAdded;
		public event EnumsChangedEvent EnumRemoved;

		private readonly List<EnumDescription> enums = new List<EnumDescription>();
		private readonly List<ClassNode> classes = new List<ClassNode>();

		public IReadOnlyList<EnumDescription> Enums => enums;

		public IReadOnlyList<ClassNode> Classes => classes;

		public string Path { get; set; }

		/// <summary>
		/// Key-Value map with custom data for plugins to store project related data.
		/// The preferred key format is {Plugin Name}_{Key Name}.
		/// </summary>
		public CustomDataMap CustomData { get; } = new CustomDataMap();

		/// <summary>
		/// List of data types to use while generating C++ code for nodes.
		/// </summary>
		public CppTypeMapping TypeMapping { get; } = new CppTypeMapping();

		public void Dispose()
		{
			Clear();

			ClassAdded = null;
			ClassRemoved = null;

			EnumAdded = null;
			EnumRemoved = null;
		}

		public void AddClass(ClassNode node)
		{
			Contract.Requires(node != null);

			classes.Add(node);

			node.NodesChanged += NodesChanged_Handler;

			ClassAdded?.Invoke(node);
		}

		public bool ContainsClass(Guid uuid)
		{
			Contract.Requires(uuid != null);

			return classes.Any(c => c.Uuid.Equals(uuid));
		}

		public ClassNode GetClassByUuid(Guid uuid)
		{
			Contract.Requires(uuid != null);

			return classes.First(c => c.Uuid.Equals(uuid));
		}

		private void NodesChanged_Handler(BaseNode sender)
		{
			classes.ForEach(c => c.UpdateOffsets());
		}

		public void Clear()
		{
			var temp = classes.ToList();

			classes.Clear();

			foreach (var node in temp)
			{
				node.NodesChanged -= NodesChanged_Handler;

				ClassRemoved?.Invoke(node);
			}

			var temp2 = enums.ToList();

			enums.Clear();

			foreach (var @enum in temp2)
			{
				EnumRemoved?.Invoke(@enum);
			}
		}

		private IEnumerable<ClassNode> GetClassReferences(ClassNode node)
		{
			Contract.Requires(node != null);

			return classes
				.Where(c => c != node)
				.Where(c => c.Nodes.OfType<BaseWrapperNode>().Any(w => w.ResolveMostInnerNode() == node));
		}

		public void Remove(ClassNode node)
		{
			Contract.Requires(node != null);

			var references = GetClassReferences(node).ToList();
			if (references.Any())
			{
				throw new ClassReferencedException(node, references);
			}

			if (classes.Remove(node))
			{
				node.NodesChanged -= NodesChanged_Handler;

				ClassRemoved?.Invoke(node);
			}
		}

		public void RemoveUnusedClasses()
		{
			var toRemove = classes
				.Except(classes.Where(x => GetClassReferences(x).Any())) // check for references
				.Where(c => c.Nodes.All(n => n is BaseHexNode)) // check if only hex nodes are present
				.ToList();
			foreach (var node in toRemove)
			{
				if (classes.Remove(node))
				{
					ClassRemoved?.Invoke(node);
				}
			}
		}

		public void AddEnum(EnumDescription @enum)
		{
			Contract.Requires(@enum != null);

			enums.Add(@enum);

			EnumAdded?.Invoke(@enum);
		}

		public void RemoveEnum(EnumDescription @enum)
		{
			Contract.Requires(@enum != null);

			var refrences = GetEnumReferences(@enum).ToList();
			if (refrences.Any())
			{
				throw new EnumReferencedException(@enum, refrences.Select(e => e.GetParentClass()).Distinct());
			}

			if (enums.Remove(@enum))
			{
				EnumRemoved?.Invoke(@enum);
			}
		}

		private IEnumerable<EnumNode> GetEnumReferences(EnumDescription @enum)
		{
			Contract.Requires(@enum != null);

			return classes
				.SelectMany(c => c.Nodes.Where(n => n is EnumNode || (n as BaseWrapperNode)?.ResolveMostInnerNode() is EnumNode))
				.Cast<EnumNode>()
				.Where(e => e.Enum == @enum);
		}
	}

	public class ClassReferencedException : Exception
	{
		public ClassNode ClassNode { get; }
		public IEnumerable<ClassNode> References { get; }

		public ClassReferencedException(ClassNode node, IEnumerable<ClassNode> references)
			: base($"The class '{node.Name}' is referenced in other classes.")
		{
			Contract.Requires(node != null);
			Contract.Requires(references != null);
			Contract.Requires(Contract.ForAll(references, c => c != null));

			ClassNode = node;
			References = references;
		}
	}

	public class EnumReferencedException : Exception
	{
		public EnumDescription Enum { get; }
		public IEnumerable<ClassNode> References { get; }

		public EnumReferencedException(EnumDescription @enum, IEnumerable<ClassNode> references)
			: base($"The enum '{@enum.Name}' is referenced in other classes.")
		{
			Contract.Requires(@enum != null);
			Contract.Requires(references != null);
			Contract.Requires(Contract.ForAll(references, c => c != null));

			Enum = @enum;
			References = references;
		}
	}
}
