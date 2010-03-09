using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChromiumUpdater.Engine
{
    public class ChromiumRegistryInfo
    {
        public uint InstallerError { get; set; }
        public uint InstallerResult { get; set; }
        public String InstallerSuccessLaunchCmdLine { get; set; }
        public String LastRun { get; set; }
        public String Name { get; set; }
        public String VersionString { get; set; }
        public Version Version 
        {
            get
            {
                if (String.IsNullOrEmpty(this.VersionString))
                    return null;

                return new System.Version(this.VersionString);
            }
            set
            {
                this.VersionString = value.ToString(4);
            }
        }
    }
}
