//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Furlough.Resources.Services.Mail {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class User {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal User() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Furlough.Resources.Services.Mail.User", typeof(User).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;div style=&quot;&quot;&gt;Dear {0},&lt;br/&gt;An account for managing your vacation days was created for you.&lt;br/&gt;&lt;i&gt;Username:&lt;/i&gt; &lt;b&gt;{1}&lt;/b&gt;&lt;br/&gt;&lt;i&gt;Password:&lt;/i&gt; &lt;b&gt;{2}&lt;/b&gt;&lt;br/&gt;&lt;br/&gt;&lt;/div&gt;&lt;div&gt;&lt;p&gt;It is intended you change this password after your first login.&lt;/p&gt;&lt;br/&gt;&lt;p&gt;This email was intended for {3}&lt;/p&gt;&lt;/div&gt;.
        /// </summary>
        public static string createdBody {
            get {
                return ResourceManager.GetString("createdBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your new account!.
        /// </summary>
        public static string createdSubject {
            get {
                return ResourceManager.GetString("createdSubject", resourceCulture);
            }
        }
    }
}
