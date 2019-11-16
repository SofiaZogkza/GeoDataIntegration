using GeoDataIntegration.Interfaces;
using System;
using System.Collections.Generic;
using GeoDataIntegration.Types;

namespace GeoDataIntegration.Services
{
    public class GeoDataFileManager : IGeoDataFileManager
    {
        public Response DeserializeGeoDataFile()
        {
            // Read file as an object
            var text = Types.Properties.Resources.input;

            // Split the file per related object. (Either 3 or 4 lines depending on the {DeprecatedOrDiscontinued} value existence.)
            string[] data = text.Split(new[] { "\r\n\r\n", "\r\r", "\n\n" }, StringSplitOptions.None);

            // Split Data
            var splitItemsPerObject = new List<GeoDataModel>();
            foreach (var d in data)
            {
                splitItemsPerObject.Add(SplitMapData(d));
            }

            return new Response(){ Items = splitItemsPerObject};
        }

        private GeoDataModel SplitMapData(string data)
        {
            // Split each object per line.
            string[] obj = data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // Mapping
            var mapObject = new GeoDataModel();
            foreach (var o in obj)
            {
                if (o.StartsWith("<"))
                {
                    string[] line = o.Split(new[] { "<", "> ", " <>" }, StringSplitOptions.None);
                    MapIdentityAndProjValue(mapObject, line);
                }
                else if (o.StartsWith("# area"))
                {
                    string[] line = o.Split(new[] { "# area: (lat: ", ") - (lon: ", ", ", ") [", "]" }, StringSplitOptions.None);
                    MapAreaLine(mapObject, line);
                }
                else if ((o.StartsWith("# DEPRECATED")) || (o.Contains("# DISCONTINUED")))
                {
                    mapObject.DeprecatedOrDiscontinued = o;
                }
                else
                {
                    MapNameAndDescription(mapObject, o);
                }
            }
            return mapObject;
        }

        private void MapIdentityAndProjValue(GeoDataModel mapObject, string[] line)
        {
            mapObject.Identity = Convert.ToInt32(line[1]);
            mapObject.ProjValue = line[2];
        }

        private void MapAreaLine(GeoDataModel mapObject, string[] line)
        {
            mapObject.MinLat = line[1];
            mapObject.MaxLat = line[2];
            mapObject.MinLon = line[3];
            mapObject.MaxLon = line[4];
            mapObject.AreaDescription = line[5];
        }

        private void MapNameAndDescription(GeoDataModel mapObject, string o)
        {
            var smallerChunks = o.Split(new[] { "# ", " " }, StringSplitOptions.None);
            mapObject.Name = smallerChunks[1];

            var firstIndexOf = o.IndexOf(' ');
            var secondIndexOf = o.IndexOf(' ', firstIndexOf + 1);
            var constructDescription = o.Substring(secondIndexOf + 2);
            mapObject.Description = constructDescription.Remove(constructDescription.Length - 1);
        }
    }
}
