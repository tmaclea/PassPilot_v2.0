using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PassPilot_v2._0.Models;

namespace PassPilot_v2._0.Services
{
    //TODO: Make this class a singleton
    //also, dependancy injection
    public class JsonSiteData : ISiteData
    {
        List<Site> _sites = new List<Site>();
        //json cannot deserialize into a list of objects
        //need an object that contains a list
        DeserializedSites jsonSitesObject = new DeserializedSites();

        //should probably decouple this later
        string FilePath = Directory.GetCurrentDirectory() + "\\SiteData.json";
        public JsonSiteData()
        {
            //create the file if it doesn't exist
            if(!File.Exists(FilePath)) { File.Create(FilePath); }
            //retrieve data from json file
            using (StreamReader sr = new StreamReader(FilePath))
            {
                if (sr.Peek() != -1)
                {
                    string jsonString = sr.ReadToEnd();
                    jsonSitesObject = JsonConvert.DeserializeObject<DeserializedSites>(jsonString);
                    _sites = jsonSitesObject.SiteList;
                }
            }
        }

        public void Add(Site site)
        {
            _sites.Add(site);
            Save();
        }

        public IEnumerable<Site> GetSites()
        {
            return _sites.OrderBy(s => s.SiteName).ToArray();
        }

        public void Update(EditableSite updated, Site site)
        {
            //TODO: make sure that SiteName is unique to every site or this is bound to fail
            var siteToUpdate = _sites.FirstOrDefault(s => s.SiteName == site.SiteName);
            if (siteToUpdate != null)
            {
                siteToUpdate.Username = updated.Username;
                siteToUpdate.SiteName = updated.SiteName;
                siteToUpdate.SiteURL = updated.SiteURL;
                siteToUpdate.PasswordLength = updated.PasswordLength;
                siteToUpdate.AllowedSymbols = updated.AllowedSymbols;
                var configGetter = new Dictionary<string, bool>();
                siteToUpdate.CharacterConfig = updated.CharacterConfig.CharSettingsToDict();

                if(siteToUpdate.Password != updated.Password)
                {
                    if (siteToUpdate.Password != "" && siteToUpdate.Password != null)
                        siteToUpdate.PasswordHistory.Add(siteToUpdate.Password);

                    siteToUpdate.Password = updated.Password;
                    siteToUpdate.LastChanged = DateTime.Now;
                }
            }

            Save();
        }

        public void Delete(Site siteToDelete)
        {
            _sites.Remove(siteToDelete);
            Save();
        }

        public void Save()
        {
            jsonSitesObject.SiteList = _sites;
            var jsonList = JsonConvert.SerializeObject(jsonSitesObject);
            File.WriteAllText(FilePath, jsonList);
        }



        private class DeserializedSites
        {
            public List<Site> SiteList { get; set; }
        }
    }
}
