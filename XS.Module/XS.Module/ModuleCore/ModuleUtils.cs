using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XS.Core.FSO;
using XS.ModuleInterface;

namespace XS.Module.ModuleCore
{
    public class ModuleUtils
    {

        public static Dictionary<string, IModules> LoadModules()
        {
            Dictionary<string, IModules> dic = new Dictionary<string, IModules>();
            List<IModules> lst = getModuleList();
            foreach (var model in lst)
            {
                dic.Add(model.Title, model);
            }
            return dic;

        }
        private static List<IModules> getModuleList()
        {
            List<Assembly> assModule = new List<Assembly>();
            try
            {
                FileInfo[] moduleDlls = FObject.GetFileListByType(string.Concat(Application.StartupPath, "\\modules\\"), "dll", true); //获取所有Dll
                foreach (FileInfo fileInfo in moduleDlls)
                {
                    Assembly asm = Assembly.LoadFrom(fileInfo.FullName);
                    assModule.Add(asm);
                }

            }
            catch (Exception ex)
            {
                XS.Core.Log.ErrorLog.Error($"扩展模块加载发生异常:【{ex.Message}】");
            }
            List<Assembly> assAll = new List<Assembly>();
            try
            {
                Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
                assAll = assModule.Union(asms).ToList();//Concat保留重复项,Union去重

            }
            catch (Exception ex)
            {
                XS.Core.Log.ErrorLog.Error($"默认模块加载发生异常:【{ex.Message}】");
            }

            return LoadInterface<IModules>(assAll);
        }

        private static List<T> LoadInterface<T>(List<Assembly> asms) where T : class
        {
            List<T> lstModules = new List<T>();
            foreach (var asm in asms)
            {
                var tTypes = asm.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(T))).ToArray();
                foreach (var type in tTypes)
                {
                    if (type.IsClass)
                    {
                        try
                        {
                            T md = Activator.CreateInstance(type) as T;
                            if (!Equals(md, null))
                            {
                                lstModules.Add(md);
                            }
                        }
                        catch (Exception ex)
                        {
                            XS.Core.Log.ErrorLog.Error($"模块类型转换发生异常:【{ex.Message}】");
                        }
                    }
                }
            }

            return lstModules;
        }
    }
}
