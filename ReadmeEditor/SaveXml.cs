using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace ReadmeEditor
{
	public class SaveXml
	{
		public static void Save(AttributePackage pack, Stream stream)
		{
			XmlTextWriter w = new XmlTextWriter(stream, Encoding.UTF8);
			w.Formatting = Formatting.Indented;

			w.WriteStartDocument(true);
			{
				w.WriteStartElement("ReadmeEditor");
				{
					WriteInformation(w, pack);
					WriteElement(w, "Caption", pack.Caption);
					WriteHowto(w, pack);
					WriteDerivate(w, pack);
					WriteRemark(w, pack);
					WriteLicense(w, pack);
					WriteUpdate(w, pack);
				}
				w.WriteEndElement();
			}
			w.WriteEndDocument();
			w.Close();
		}

		private static void WriteInformation(XmlTextWriter w, AttributePackage pack)
		{
			w.WriteStartElement("Information");
			{
				WriteElement(w, "Author", pack.Author);
				WriteElement(w, "Email", pack.Email);
				WriteElement(w, "Title", pack.Title);
				WriteElement(w, "CreatedAt", pack.CreatedAt);
				WriteElement(w, "Version", pack.Version);
				WriteElement(w, "OpenPass", pack.OpenPass);
			}
			w.WriteEndElement();
		}

		private static void WriteHowto(XmlTextWriter w, AttributePackage pack)
		{
			WriteItems(w, pack.Howto, "Howto", new string[] { "Title", "Section", "Caption" });
		}

		private static void WriteDerivate(XmlTextWriter w, AttributePackage pack)
		{
			WriteItems(w, pack.Derivate, "Derivate", new string[] { "Title", "Link", "Comment" });
		}

		private static void WriteLicense(XmlTextWriter w, AttributePackage pack)
		{
			WriteItems(w, pack.License, "License", new string[] { "Type" });
		}

		private static void WriteUpdate(XmlTextWriter w, AttributePackage pack)
		{
			WriteItems(w, pack.Update, "Update", new string[] { "Title", "Section", "Caption" });
		}

		private static void WriteRemark(XmlTextWriter w, AttributePackage pack)
		{
			WriteItems(w, pack.Remark, "Remark", new string[] { "Title", "Caption" });
		}

		private static void WriteItems(XmlTextWriter w, TextPackage[] pack, string startNode, string[] elems)
		{
			w.WriteStartElement(startNode);
			{
				foreach (var node in pack)
					WriteItem(w, node, "Item", elems);
			}
			w.WriteEndElement();
		}

		private static void WriteItem(XmlTextWriter w, TextPackage node, string nodeName, string[] elems)
		{
			w.WriteStartElement(nodeName);
			foreach (var elem in elems)
				WriteElement(w, node, elem);
			w.WriteEndElement();
		}

		private static void WriteElement(XmlTextWriter w, TextPackage pack, string element)
		{
			w.WriteStartElement(element);
			w.WriteString(pack[element]);
			w.WriteEndElement();
		}

		private static void WriteElement(XmlTextWriter w, string element, string text)
		{
			w.WriteStartElement(element);
			w.WriteString(text);
			w.WriteEndElement();
		}
	}
}
