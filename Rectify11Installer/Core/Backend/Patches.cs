using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Rectify11Installer.Core
{
    #region Parser
    public class PatchesParser
    {
        #region Public Methods
        public static Patches GetAll()
        {
            XmlSerializer ser = new(typeof(Patches));
            Patches patches;
            using (var reader = new StringReader(Rectify11Installer.Properties.Resources.rectify11xml))
            {
                patches = (Patches)ser.Deserialize(reader);
            }
            return patches;
        }
        #endregion
    }
    #endregion
    #region Deserializer Class

    [GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Patches
    {

        private PatchesPatch[] itemsField;
        [XmlElementAttribute("Patch", Form = XmlSchemaForm.Unqualified)]
        public PatchesPatch[] Items
        {
            get => this.itemsField;
            set => this.itemsField = value;
        }
    }

    [GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class PatchesPatch
    {
        private string muiField;
        private string maskField;
        private string x86Field;
        private string hardlinkTargetField;
        private string disableOnSafeModeField;
        private string IgnoreField;
        private string MinVersionField;
        private string MaxVersionField;

        [XmlAttributeAttribute()]
        public string Mui
        {
            get => this.muiField;
            set => this.muiField = value;
        }

        [XmlAttributeAttribute()]
        public string mask
        {
            get => this.maskField;
            set => this.maskField = value;
        }

        [XmlAttributeAttribute()]
        public string x86
        {
            get => this.x86Field;
            set => this.x86Field = value;
        }

        [XmlAttributeAttribute()]
        public string HardlinkTarget
        {
            get => this.hardlinkTargetField;
            set => this.hardlinkTargetField = value;
        }

        [XmlAttributeAttribute()]
        public string DisableOnSafeMode
        {
            get => this.disableOnSafeModeField;
            set => this.disableOnSafeModeField = value;
        }

        [XmlAttributeAttribute()]
        public string Ignore
        {
            get => this.IgnoreField;
            set => this.IgnoreField = value;
        }

        [XmlAttributeAttribute()]
        public string MinVersion
        {
            get => this.MinVersionField;
            set => this.MinVersionField = value;
        }

        [XmlAttributeAttribute()]
        public string MaxVersion
        {
            get => this.MaxVersionField;
            set => this.MaxVersionField = value;
        }
    }
    #endregion
}