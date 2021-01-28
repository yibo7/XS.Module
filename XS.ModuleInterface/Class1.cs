using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace XS.ModuleInterface
{
    public interface IModules
    {
        string Title { get; }
        Image Ico { get; }
    }
}
