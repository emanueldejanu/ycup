using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace ChromiumUpdater.Engine.ProcessManagement
{
    public class ProcessLauncher : IDisposable
    {
        private StringBuilder _processConsoleOutput = new StringBuilder(512);
        private Process _process = new Process();
        private TextWriter _error = null;
        private TextWriter _out = null;

        public ProcessLauncher()
        {
            _process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceived);
            _process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
            _error = new StringWriter(_processConsoleOutput);
            _out = new StringWriter(_processConsoleOutput);
            _process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
        }

        public bool HasExited
        {
            get
            {
                return this._process.HasExited;
            }
        }

        public int? ExitCode
        {
            get
            {
                if (this._process.HasExited)
                    return this._process.ExitCode;

                return null;
            }
        }


        public bool LoadUserProfile
        {
            get { return _process.StartInfo.LoadUserProfile; }
            set { _process.StartInfo.LoadUserProfile = value; }
        }

        public String StandardOutput
        {
            get
            {
                return _processConsoleOutput.ToString();
            }
        }

        public String Password
        {
            get
            {
                IntPtr ptr = Marshal.SecureStringToBSTR(this._process.StartInfo.Password);
                try
                {
                    String p = Marshal.PtrToStringUni(ptr);
                    return p;
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                    {
                        Marshal.FreeBSTR(ptr);
                    }
                }
            }
            set
            {
                String v = value;
                unsafe
                {
                    fixed (char* p = v.ToCharArray())
                    {
                        SecureString s = new SecureString(p, v.Length);
                        _process.StartInfo.Password = s;
                    }
                }
            }
        }

        public String Login
        {
            get { return _process.StartInfo.UserName; }
            set { _process.StartInfo.UserName = value; }
        }

        public String WorkingDirectory
        {
            get { return _process.StartInfo.WorkingDirectory; }
            set { _process.StartInfo.WorkingDirectory = value; }
        }

        public string FileName
        {
            get
            {
                return _process.StartInfo.FileName;
            }
            set
            {
                _process.StartInfo.FileName = value;
            }
        }


        public string Arguments
        {
            get
            {
                return _process.StartInfo.Arguments;
            }
            set
            {
                _process.StartInfo.Arguments = value;
            }
        }


        public void SetArguments(string[] args, bool analyzeSpaceCharacters)
        {
            StringBuilder sb = new StringBuilder();
            int c = 0;
            bool containsSpace = false;
            foreach (string val in args)
            {
                if (c > 0)
                {
                    sb.Append(' ');
                }
                if (analyzeSpaceCharacters)
                    containsSpace = (val.IndexOf(' ') != -1);

                if (containsSpace)
                {
                    sb.Append('\"');
                }

                sb.Append(val);

                if (containsSpace)
                {
                    sb.Append('\"');
                }
                c++;
            }
            Arguments = sb.ToString();
        }



        protected virtual void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                _out.WriteLine(e.Data);
            }
        }


        protected virtual void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                _error.WriteLine(e.Data);
            }
        }


        public Process Process
        {
            get
            {
                return _process;
            }
            set
            {
                _process = value;
            }
        }


        public ProcessStartInfo StartInfo
        {
            get
            {
                return _process.StartInfo;
            }
        }

        public void Kill()
        {
            this._process.Kill();
        }

        public void Start()
        {
            this.Start(true, null);
        }

        public bool Start(bool waitForExit, TimeSpan? ts)
        {
            // Initialize the asynchronous stuff 
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.CreateNoWindow = true;

            _process.StartInfo.FileName = Environment.ExpandEnvironmentVariables(_process.StartInfo.FileName);

            _process.Start();

            _process.BeginErrorReadLine();
            _process.BeginOutputReadLine();

            if (waitForExit)
            {
                if (ts.HasValue)
                {
                    bool b = _process.WaitForExit((int)ts.Value.TotalMilliseconds);
                    return b;
                }
                else
                {
                    _process.WaitForExit();
                    return true;
                }
            }
            return false;
        }


        public TextWriter Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
            }
        }


        public TextWriter Out
        {
            get
            {
                return _out;
            }
            set
            {
                _out = value;
            }
        }

        public void Dispose()
        {
            (this as IDisposable).Dispose();
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this._process != null)
            {
                try
                {
                    if (!this._process.HasExited)
                    {
                        if (!this._process.WaitForExit(5000))
                        {
                            this._process.Kill();
                        }
                    }
                }
                catch (InvalidOperationException)
                { }
                this._process.Dispose();
                this._process = null;
            }
        }

        #endregion
    }
}