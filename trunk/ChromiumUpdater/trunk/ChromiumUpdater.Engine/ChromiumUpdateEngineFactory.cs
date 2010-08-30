using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChromiumUpdater.Engine
{
    public static class ChromiumUpdateEngineFactory
    {
        public static IChromiumUpdateEngine CreateInstance()
        {

            return new ChromiumUpdateEngine(new ChromiumUpdateEngineConfiguration() { WebProxyType = ProxyType.None }) as IChromiumUpdateEngine;
        }

        public static IChromiumUpdateEngine CreateInstance(ChromiumUpdateEngineConfiguration configuration)
        {
            return new ChromiumUpdateEngine(configuration) as IChromiumUpdateEngine;
        }
    }
}
