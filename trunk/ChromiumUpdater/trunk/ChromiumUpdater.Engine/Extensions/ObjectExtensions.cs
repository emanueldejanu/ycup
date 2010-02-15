using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChromiumUpdater.Engine.Extensions
{
    public static class ObjectExtensions
    {
        public static T ExecuteInLock<T>(Func<T> code, ref T value) where T: class
        {
            if (value == null)
            {
                lock (typeof(T))
                {
                    if (value == null)
                    {
                        value = code();
                    }
                }
            }
            return value;
        }
    }
}
