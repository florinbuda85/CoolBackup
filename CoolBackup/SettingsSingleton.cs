using CoolBackup.Containers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CoolBackup
{
    public class SettingsSingleton
    {
        private const String FILE_NAME = "settings.json";

        private SettingsSingleton()
        {
        }

        private static SettingsSingleton _instance = null;
        private SettingsContainer container;

        public static SettingsSingleton getInstance()
        {
            if (_instance == null)
            {
                _instance = new SettingsSingleton();
            }
            return _instance;
        }

        public SettingsContainer getSettings()
        {
            try
            {
                string jsonContent = File.ReadAllText(FILE_NAME);
                container = Newtonsoft.Json.JsonConvert.DeserializeObject<SettingsContainer>(jsonContent);
            }
            catch (Exception)
            {
                container = new SettingsContainer();
            }
            return container;
        }

        public void saveContainer()
        {
            try
            {
                var curentState = Newtonsoft.Json.JsonConvert.SerializeObject(container, Formatting.Indented);
                File.WriteAllText(FILE_NAME, curentState, Encoding.UTF8);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error when saving settings" + e.Message);
            }
        }
    }
}