﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleNotServer.Lang {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class langres {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal langres() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("ConsoleNotServer.Lang.langres", typeof(langres).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string Server_Started {
            get {
                return ResourceManager.GetString("Server_Started", resourceCulture);
            }
        }
        
        internal static string Server_Error {
            get {
                return ResourceManager.GetString("Server_Error", resourceCulture);
            }
        }
        
        internal static string Server_Wrong_IP {
            get {
                return ResourceManager.GetString("Server_Wrong_IP", resourceCulture);
            }
        }
        
        internal static string Program_No_Arguments {
            get {
                return ResourceManager.GetString("Program_No_Arguments", resourceCulture);
            }
        }
        
        internal static string Program_Help {
            get {
                return ResourceManager.GetString("Program_Help", resourceCulture);
            }
        }
        
        internal static string Program_Only_Numbers {
            get {
                return ResourceManager.GetString("Program_Only_Numbers", resourceCulture);
            }
        }
        
        internal static string Error_Exception {
            get {
                return ResourceManager.GetString("Error_Exception", resourceCulture);
            }
        }
    }
}
