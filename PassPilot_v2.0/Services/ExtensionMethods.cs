using PassPilot_v2._0.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassPilot_v2._0.Services
{
    public static class ExtensionMethods
    {
        public static ObservableCollection<char?> ToNullableCharArray(this string str)
        {
            var CharList = new ObservableCollection<char?>();
            var CharArray = str.ToCharArray();

            foreach(char character in CharArray)
            {
                CharList.Add(character);
            }

            return CharList;
        }

        public static Dictionary<string, bool> CharSettingsToDict(
            this ObservableCollection<CharSetting> settings)
        {
            var settingDict = new Dictionary<string, bool>();
            foreach(var setting in settings)
            {
                settingDict.Add(setting.Label, setting.IsEnabled);
            }

            return settingDict;
        }

        public static ObservableCollection<CharSetting> DictToCharSettings(this Dictionary<string, bool> settings)
        {
            var charSettings = new ObservableCollection<CharSetting>();
            foreach(var setting in settings)
            {
                charSettings.Add(new CharSetting(setting.Key, setting.Value));
            }

            return charSettings;
        }
    }
}
