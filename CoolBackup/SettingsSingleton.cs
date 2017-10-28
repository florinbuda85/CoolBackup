using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBackup
{
    public class SettingsSingleton
    {

        private SettingsSingleton() { }

        private static SettingsSingleton _instance = null;

        public static SettingsSingleton getInstance()
        {
            if (_instance == null)
            {
                _instance = new SettingsSingleton();
            }
            return _instance;
        }

        /****/

    }

}