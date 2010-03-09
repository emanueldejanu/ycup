using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChromiumUpdater.Ui.WinForms.Forms;
using ChromiumUpdater.Engine;

namespace ChromiumUpdater.Ui.WinForms
{
    public partial class MainForm : BaseForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (IChromiumUpdateEngine target = ChromiumUpdateEngineFactory.CreateInstance())
            {
                ChromiumRegistryInfo actual;
                actual = target.GetChromiumRegistryInfo();
            }
        }
    }
}
