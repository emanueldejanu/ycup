// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.0.0.19662
//    <NameSpace>ChromiumUpdater.Engine.Schemas</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>True</UseBaseClass><GenerateCloneMethod>True</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net35</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><AutomaticProperties>True</AutomaticProperties><DisableDebug>False</DisableDebug><CustomUsings></CustomUsings>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace ChromiumUpdater.Engine.Schemas {
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;
    
    
    #region Base entity class
    public partial class EntityBase<T>
     {
        
        #region Clone method
        /// <summary>
        /// Create a clone of this T object
        /// </summary>
        public virtual T Clone() {
            return ((T)(this.MemberwiseClone()));
        }
        #endregion
    }
    #endregion
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.0.0.19663")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class changelogs : EntityBase<changelogs> {
        
        private List<changelogsLogentry> logField;
        
        private List<string> textField;
        
        [System.Xml.Serialization.XmlArrayItemAttribute("logentry", IsNullable=false)]
        public List<changelogsLogentry> log {
            get {
                if ((this.logField == null)) {
                    this.logField = new List<changelogsLogentry>();
                }
                return this.logField;
            }
            set {
                this.logField = value;
            }
        }
        
        [System.Xml.Serialization.XmlTextAttribute()]
        public List<string> Text {
            get {
                if ((this.textField == null)) {
                    this.textField = new List<string>();
                }
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "3.0.0.19663")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class changelogsLogentry : EntityBase<changelogsLogentry> {
        
    public string author {get; set;}

    public System.DateTime date {get; set;}

    public string msg {get; set;}

    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort revision {get; set;}

    }
}