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

namespace XS.Module
{
    public partial class Main : Form
    {
        #region DockContent 操作

        /// <summary>
        /// 获取一个DockContent对象
        /// </summary>
        /// <param name="text">标签名称</param>
        /// <returns></returns>
        private DockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (form.Text == text)
                    {
                        return form as DockContent;
                    }
                }
                return null;
            }
            else
            {
                foreach (DockContent content in dockPanel.Documents)
                {
                    if (content.DockHandler.TabText == text)
                    {
                        return content;
                    }
                }

                return null;
            }
        }
        /// <summary>
        /// 显示一个标签窗体，程序会根据标题来寻找，是否已经加载过相同标题的窗体，如果有，就直接激活焦点
        /// </summary>
        /// <param name="Title">窗体显示名称，如果</param>
        /// <param name="formType">要加载的标签窗体，这个要自己写类，继承于 DockContent</param>
        /// <param name="dockState">窗体的显示方式，一般为左，右等等</param>
        public void ShowContent(string Title, DockContent formType, DockState dockState = DockState.Document)
        {
             
               DockContent frm = FindDocument(Title);
            if (frm == null)
            {
                formType.DockHandler.TabText = Title;
                formType.Show(this.dockPanel, dockState);
            }
            else
            {
                frm.Show(this.dockPanel, dockState);
                frm.BringToFront();
            }
        }
        #endregion

        public Main()
        {
            InitializeComponent();

            this.IsMdiContainer = true; //把主窗体设置为多窗体的容器

            //设置左栏宽
            dockPanel.DockLeftPortion = 120;

            this.ShowContent("功能模块", new ModuleList(this), DockState.DockLeft);

            this.ShowContent("首页", new Index());

        }

        private void 首页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowContent("主页", new Index(), DockState.DockLeft);
        }

        private void 工具栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowContent("工具栏", new ModuleList(this), DockState.DockLeft);
        }
    }
}
