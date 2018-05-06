using PassPilot_v2._0.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassPilot_v2._0.Services
{
    public interface ISiteData
    {
        IEnumerable<Site> GetSites();
        void Add(Site site);
        void Update(EditableSite updated, Site site);
        void Delete(Site site);
    }
}
