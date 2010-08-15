using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ChromiumUpdater.Engine.Schemas
{
    public partial class changelogs
    {
        static XmlSerializer _xmlSerializer;
        static changelogs _emptyLog;

        public static changelogs Deserialize(Stream s)
        {
            return (changelogs)changelogs.GetXmlSerializer().Deserialize(s);
        }

        public static changelogs Empty
        {
            get
            {
                if (changelogs._emptyLog == null)
                {
                    lock (typeof(changelogs))
                    {
                        if (changelogs._emptyLog == null)
                        {
                            changelogs l = new changelogs();
                            changelogs._emptyLog = l;
                        }
                    }
                }
                return changelogs._emptyLog;
            }
        }

        protected static XmlSerializer GetXmlSerializer()
        {
            if (changelogs._xmlSerializer == null)
            {
                lock (typeof(changelogs))
                {
                    if (changelogs._xmlSerializer == null)
                    {
                        changelogs._xmlSerializer = new XmlSerializer(typeof(changelogs));
                    }
                }
            }
            return changelogs._xmlSerializer;
        }
  
        public String ConcatenatedText
        {
            get
            {
                if (this.Text == null)
                    return String.Empty;

                if (this.Text.Count == 0)
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
