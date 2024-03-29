﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadmeEditor.Export.HTML.Template
{
	/// <summary>
	/// ヘッダー，メイン，フッターの3種の構成
	/// </summary>
	public abstract class TemplateComponent : TemplateBase
	{

	}

	public class Header : TemplateComponent
	{
		public static string TestRender(ComponentPackage package)
		{
			return Render(package, Properties.Resources.Header);
		}
	}

	public class Main : TemplateComponent
	{
		public static string TestRender(ComponentPackage package)
		{
			return Render(package, Properties.Resources.Main);
		}
	}

	public class Footer : TemplateComponent
	{
		public static string TestRender(ComponentPackage package)
		{
			return Render(package, Properties.Resources.Footer);
		}
	}

	public class Body : TemplateComponent
	{
		public static string TestRender(ComponentPackage package)
		{
			return Render(package, Properties.Resources.Body);
		}
	}

	public class Head : TemplateComponent
	{
		public static string TestRender(ComponentPackage package)
		{
			return Render(package, Properties.Resources.Head);
		}
	}

	public class HTML : TemplateComponent
	{
		public static string TestRender(ComponentPackage package)
		{
			return Render(package, Properties.Resources.Html);
		}
	}
}