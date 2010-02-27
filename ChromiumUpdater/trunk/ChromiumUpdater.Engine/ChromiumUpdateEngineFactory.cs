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
            return new ChromiumUpdateEngine() as IChromiumUpdateEngine;
        }
    }
}
