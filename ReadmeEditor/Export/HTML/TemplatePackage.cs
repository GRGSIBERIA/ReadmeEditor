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
	}

	public class ItemPackage : PackageBase
	{
		public ItemPackage(string classIdentifier, string itemTemplate)
		{
			variables["ClassIdentifier"] = classIdentifier;
			variables["ItemTemplate"] = itemTemplate;
		}
	}

	public class PartPackage : PackageBase
	{
		private string classIdentifier;

		public PartPackage(string classIdentifier)
		{
			this.classIdentifier = classIdentifier;
		}
	}
}
