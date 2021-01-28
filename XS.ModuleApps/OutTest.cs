using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XS.ModuleInterface;

namespace XS.ModuleApps
{
    public partial class OutTest : DockContent, IModules
    {
        public string Title => "外部模块";

        public Image Ico => Properties.Resources.word2;
        public OutTest()
        {
            InitializeComponent();
        }

    }
}
