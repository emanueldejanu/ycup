using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChromiumUpdater.Engine
{
    public enum ProxyType
    {
        None= 0,
        FromSystem,
        Custom,
    }

    public class ChromiumUpdateEngineConfiguration
    {
        public ProxyType WebProxyType { get; set; }
        public String WebProxyAddress { get; set; }
    }
}
