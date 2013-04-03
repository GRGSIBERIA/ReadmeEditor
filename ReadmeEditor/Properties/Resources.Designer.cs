﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.18034
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReadmeEditor.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ReadmeEditor.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///                   &lt;section&gt;
        ///                    &lt;h2&gt;$ChapterTitle$&lt;/h2&gt;
        ///                    &lt;div&gt;
        ///                        $Content$
        ///                    &lt;/div&gt;
        ///                &lt;/section&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Caption {
            get {
                return ResourceManager.GetString("Caption", resourceCulture);
            }
        }
        
        /// <summary>
        ///                   &lt;section class=&quot;$ClassIdentifier$&quot;&gt;
        ///                    &lt;h2&gt;$Title$&lt;/h2&gt;
        ///                    &lt;div class=&quot;link&quot;&gt;$Link$&lt;/div&gt;
        ///                    &lt;div class=&quot;comment&quot;&gt;
        ///                        $Comment$
        ///                    &lt;/div&gt;
        ///                &lt;/section&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DerivateItem {
            get {
                return ResourceManager.GetString("DerivateItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///           &lt;footer&gt;
        ///            &lt;div id=&quot;copyright&quot;&gt;
        ///                Copyright $Year$ $Author$ All Rights Reserved.&lt;br /&gt;
        ///            &lt;/div&gt;
        ///            &lt;div id=&quot;generated&quot;&gt;
        ///                Generated by ReadmeEditor V$GenVersion$.&lt;br /&gt;
        ///            &lt;/div&gt;
        ///        &lt;/footer&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Footer {
            get {
                return ResourceManager.GetString("Footer", resourceCulture);
            }
        }
        
        /// <summary>
        ///           &lt;header&gt;
        ///            &lt;h1&gt;$Title$&lt;/h1&gt;
        ///        &lt;/header&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Header {
            get {
                return ResourceManager.GetString("Header", resourceCulture);
            }
        }
        
        /// <summary>
        ///           &lt;div id=&quot;main&quot;&gt;
        ///            $MainContent$
        ///        &lt;/div&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Main {
            get {
                return ResourceManager.GetString("Main", resourceCulture);
            }
        }
        
        /// <summary>
        ///           &lt;meta charset=&quot;UTF-8&quot; /&gt;
        ///        &lt;meta name=&quot;author&quot; content=&quot;$Author$&quot; /&gt;
        ///        &lt;meta http-equiv=&quot;generator&quot; content=&quot;ReadmeEditor V$GenVersion$&quot; /&gt;
        ///        &lt;meta name=&quot;keywords&quot; content=&quot;$Tags$&quot; /&gt;
        ///        &lt;link rel=&quot;Stylesheet&quot; href=&quot;./style.css&quot; /&gt;
        ///        &lt;title&gt;$Title$&lt;/title&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string MetaHead {
            get {
                return ResourceManager.GetString("MetaHead", resourceCulture);
            }
        }
        
        /// <summary>
        ///               &lt;!-- もくじ --&gt;
        ///            &lt;nav&gt;
        ///                &lt;h2&gt;$IndexTitle$&lt;/h2&gt;
        ///                &lt;ol&gt;
        ///                    &lt;li&gt;&lt;a href=&quot;#caption&quot;&gt;$CaptionTitle$&lt;/a&gt;&lt;/li&gt;
        ///                    &lt;li&gt;&lt;a href=&quot;#howto&quot;&gt;$HowtoTitle$&lt;/a&gt;&lt;/li&gt;
        ///                    &lt;li&gt;&lt;a href=&quot;#derivate&quot;&gt;$DerivateTitle$&lt;/a&gt;&lt;/li&gt;
        ///                    &lt;li&gt;&lt;a href=&quot;#remark&quot;&gt;$RemarkTitle$&lt;/a&gt;&lt;/li&gt;
        ///                    &lt;li&gt;&lt;a href=&quot;#license&quot;&gt;$LicenseTitle$&lt;/a&gt;&lt;/li&gt;
        ///                    &lt;li&gt;&lt;a href=&quot;#update&quot;&gt;$UpdateTitle$&lt;/a&gt;&lt;/li&gt;
        ///     [残りの文字列は切り詰められました]&quot;; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NavigationPart {
            get {
                return ResourceManager.GetString("NavigationPart", resourceCulture);
            }
        }
        
        /// <summary>
        ///                       &lt;section&gt;
        ///                        &lt;h3&gt;$Title$&lt;/h3&gt;
        ///                        &lt;div class=&quot;$ClassIdentifier$-content&quot;&gt;
        ///                            $Content$
        ///                        &lt;/div&gt;
        ///                    &lt;/section&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SectionItem {
            get {
                return ResourceManager.GetString("SectionItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///                   &lt;hr&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Separate {
            get {
                return ResourceManager.GetString("Separate", resourceCulture);
            }
        }
        
        /// <summary>
        ///                   &lt;section&gt;
        ///                    &lt;h3&gt;$Title$&lt;/h3&gt;
        ///                    &lt;div&gt;
        ///                        $Caption$
        ///                    &lt;/div&gt;
        ///                &lt;/section&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string StandardItem {
            get {
                return ResourceManager.GetString("StandardItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///               &lt;article id=&quot;$ClassIdentifier$&quot;&gt;
        ///                &lt;h2&gt;$ChapterTitle$&lt;/h2&gt;
        ///
        ///                $Contents$
        ///            &lt;/article&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string StandardPart {
            get {
                return ResourceManager.GetString("StandardPart", resourceCulture);
            }
        }
    }
}
