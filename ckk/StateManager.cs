using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReactiveUI;

namespace ckk
{
    public class StateManager<TReactiveView> where TReactiveView : ViewModelBase<TReactiveView>
    {
        public static StateManager<TReactiveView> Instance => new StateManager<TReactiveView>();

        public static string ApplicationData { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

       
        public string StatePath { get; set; } = Path.Combine(ApplicationData, "ckk");

        private StateManager()
        {
            ConstructPaths();
        }

        public void ConstructPaths()
        {
            if (!Directory.Exists(StatePath))
            {
                Directory.CreateDirectory(StatePath);
            }
        }
        public TReactiveView Get(ViewModelBase<TReactiveView> defaultModel = null)
        {
            ConstructPaths();
            var path = Path.Combine(StatePath, typeof(TReactiveView).Name);
            if (!File.Exists(path) && defaultModel != null)
            {
                Set(defaultModel);
            }
            if (!File.Exists(path))
            {
                throw new Exception($"No file state {path}");
            }

            var bytes = File.ReadAllBytes(path);
            var stringForomBytes = Encoding.ASCII.GetString(bytes);

            var ob = JsonConvert.DeserializeObject<TReactiveView>(stringForomBytes);
            ob.Initjalizing = false;
            return ob;

        }

        public void Set(ViewModelBase<TReactiveView> set)
        {
            ConstructPaths();
            var serialized = JsonConvert.SerializeObject(set);

            var path = Path.Combine(StatePath, typeof(TReactiveView).Name);

            File.WriteAllBytes(path, Encoding.ASCII.GetBytes(serialized));
        }
    }
}
