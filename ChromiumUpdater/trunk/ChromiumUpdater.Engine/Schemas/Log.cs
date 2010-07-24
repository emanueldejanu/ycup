using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ChromiumUpdater.Engine.Schemas
{
    public partial class ChangeLog
    {
        static XmlSerializer _xmlSerializer;
        static ChangeLog _emptyLog;

        public static ChangeLog Deserialize(Stream s)
        {
            return (ChangeLog)ChangeLog.GetXmlSerializer().Deserialize(s);
        }

        public static ChangeLog Empty
        {
            get
            {
                if (ChangeLog._emptyLog == null)
                {
                    lock (typeof(ChangeLog))
                    {
                        if (ChangeLog._emptyLog == null)
                        {
                            ChangeLog l = new ChangeLog();
                            ChangeLog._emptyLog = l;
                        }
                    }
                }
                return ChangeLog._emptyLog;
            }
        }

        protected static XmlSerializer GetXmlSerializer()
        {
            if (ChangeLog._xmlSerializer == null)
            {
                lock (typeof(ChangeLog))
                {
                    if (ChangeLog._xmlSerializer == null)
                    {
                        ChangeLog._xmlSerializer = new XmlSerializer(typeof(ChangeLog));
                    }
                }
            }
            return ChangeLog._xmlSerializer;
        }

        public String ConcatenatedText
        {
            get
            {
                if (this.textField == null)
                    return String.Empty;

                if (this.textField.Count == 0)
                    return String.Empty;

                StringBuilder sb = new StringBuilder();
                foreach (var item in this.textField)
                {
                    sb.Append(item);
                }
                return sb.ToString();
            }
        }
    }
}
