using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoDataIntegration.Types
{
    public class GeoDataModel
    {
        public int Identity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MinLat { get; set; }
        public string MaxLat { get; set; }
        public string MinLon { get; set; }
        public string MaxLon { get; set; }
        public string AreaDescription { get; set; }
        public string ProjValue { get; set; }
        public string DeprecatedOrDiscontinued { get; set; }
    }
}
