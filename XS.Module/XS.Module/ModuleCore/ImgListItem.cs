using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace XS.Module.ModuleBll
{
    
    public class ImgListItem
    {
        public DockContent DockContentObj { get; set; }
        public string Name { get; set; }

        public Image Ico { get; set; }

    }
    public class ImgListItemBll
    {
        private ImageList imageList = new ImageList();
        private List<ImgListItem> Items = new List<ImgListItem>();
        private System.Windows.Forms.ListView lv;
        public ImgListItemBll(ListView _lv)
        {
            lv = _lv;
        }

        public void Add(string sName, Image icoImg, DockContent dc)
        {

            Items.Add(new ImgListItem() { Name = sName, Ico = icoImg, DockContentObj = dc });
            imageList.Images.Add(icoImg);
            imageList.ImageSize = new Size(32, 32);// 设置行高 20 //分别是宽和高  
        }
        public Action<ImgListItem> OnMouseDoubleClick { get; set; }
        private void lv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!Equals(OnMouseDoubleClick, null))
            {
                if (lv.SelectedItems.Count > 0)
                {
                    int index = (int)lv.SelectedItems[0].Tag;
                    ImgListItem dcImgListItem = Items[index];
                    OnMouseDoubleClick(dcImgListItem);
                }

            }
        }
        public void Bind()
        {
            lv.View = View.LargeIcon;
            lv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_MouseDoubleClick);
            lv.LargeImageList = imageList;
            lv.BeginUpdate();

            for (int i = 0; i < Items.Count; i++)
            {
                ImgListItem lli = Items[i];
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                lvi.Text = lli.Name;
                lvi.Tag = i;
                lv.Items.Add(lvi);
            }
            lv.EndUpdate();
        }

    }
}
