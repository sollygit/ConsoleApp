using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApp.Common
{
    public static class UtilityXml
    {
        public static IEnumerable<string> GetFolders(string xml, char startingLetter)
        {
            var doc = XDocument.Parse(xml);
            var folders = doc.Descendants("folder");
            var query = from f in folders
                        where f.Attribute("name").Value.StartsWith(startingLetter.ToString())
                        select f.Attribute("name").Value;

            return query;
        }
    }
}
