using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadmeEditor.Export.HTML
{
	public class ItemPackage
	{
		private string classIdentifier;
		private Dictionary<string, bool> enableContent = new Dictionary<string,bool>();
		private Dictionary<string, string> variables = new Dictionary<string,string>();

		public ItemPackage(string classIdentifier)
		{
			this.classIdentifier = classIdentifier;
			variables["ClassIdentifier"] = classIdentifier; 
		}

		public string this[string name]
		{
			set { variables[name] = value; }
			get { return variables[name]; }
		}
	}
}
