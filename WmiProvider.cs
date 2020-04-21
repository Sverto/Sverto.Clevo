using System;
using System.Linq;
using System.Management;

namespace Sverto.Clevo
{
    public class WmiProvider : IDisposable
    {
        protected ManagementObjectSearcher _WmiSearcher;
        protected ManagementObject _Wmi;

        public WmiProvider(string path, string query)
        {
            _WmiSearcher = new ManagementObjectSearcher(path, query);
            _Wmi = _WmiSearcher.Get().OfType<ManagementObject>().First();
        }

        public object RunMethod(string method)
        {
            ManagementBaseObject outParams = _Wmi.InvokeMethod(method, null, null);
            return outParams["Data"];
        }

        public void RunMethod(string method, object[] args)
        {
            _Wmi.InvokeMethod(method, args);
        }

        public void Dispose()
        {
            _Wmi?.Dispose();
            _WmiSearcher?.Dispose();
        }

    }
}
