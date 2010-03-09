using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChromiumUpdater.Engine
{
    public class ChromiumRegistryInfo
    {
        public int InstallerError { get; set; }
        public int InstallerResult { get; set; }
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

        public override string ToString()
        {
            return String.Format("{0} {1}", this.Name, this.VersionString);
        }
    }
}
