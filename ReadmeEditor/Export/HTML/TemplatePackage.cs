using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadmeEditor.Export.HTML
{
	public abstract class PackageBase
	{
		protected Dictionary<string, string> variables = new Dictionary<string,string>();

		public string this[string name]
		{
			set { variables[name] = value; }
			get { return variables[name]; }
		}

		public bool ContainsKey(string name)
		{
			return variables.ContainsKey(name);
		}

		public PackageBase(string classIdentifier)
		{
			variables["ClassIdentifier"] = classIdentifier;
		}
	}

	public class ItemPackage : PackageBase
	{
		public ItemPackage(string classIdentifier) : base(classIdentifier) { }
	}

	public class PartPackage : PackageBase
	{
		public PartPackage(string classIdentifier, string itemTemplate) : base(classIdentifier)
		{
			variables["ItemTemplate"] = itemTemplate;
		}

		public PartPackage(string classIdentifier) : base(classIdentifier) { }
	}

	public class SectionPackage : PackageBase
	{
		private List<SectionPackage> childs;
		public List<SectionPackage> Childs { get { return childs; } }

		public SectionPackage(string classIdentifier, SectionPackage[] childs) : base(classIdentifier) 
		{
			this.childs = new List<SectionPackage>(childs);
		}
	}

	public class ComponentPackage : PackageBase
	{
		public ComponentPackage() : base("") { }
	}
}
