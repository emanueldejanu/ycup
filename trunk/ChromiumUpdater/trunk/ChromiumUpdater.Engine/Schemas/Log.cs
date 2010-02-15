using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ChromiumUpdater.Engine.Schemas
{
    public partial class Log
    {
        static XmlSerializer _xmlSerializer;
        static Log _emptyLog;

        public static Log Deserialize(Stream s)
        {
            return (Log)Log.GetXmlSerializer().Deserialize(s);
        }

        public static Log Empty
        {
            get
            {
                if (Log._emptyLog == null)
                {
                    lock (typeof(Log))
                    {
                        if (Log._emptyLog == null)
                        {
                            Log l = new Log();
                            Log._emptyLog = l;
                        }
                    }
                }
                return Log._emptyLog;
            }
        }

        protected static XmlSerializer GetXmlSerializer()
        {
            if (Log._xmlSerializer == null)
            {
                lock(typeof(Log))
                {
                    if (Log._xmlSerializer == null)
                    {
                        Log._xmlSerializer = new XmlSerializer(typeof(Log));
                    }
                }
            }
            return Log._xmlSerializer;
        }
    }
}
