using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _Common
{
    public class ReadXMLContent
    {
        public static List<_Model.LineConfig> GetConfigFile(string filePath)
        {
            List<_Model.LineConfig> list = new List<_Model.LineConfig>();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(filePath);
            XmlNodeList nodes = xdoc.SelectSingleNode("Config").ChildNodes;

            foreach (XmlNode node in nodes)
            {
                _Model.LineConfig _Model = new _Model.LineConfig();
                XmlNodeList xmlFeeds = ReadXML_MV.GetChildNodes(node);

                _Model.LineNo = Convert.ToInt32(ReadXML_MV.ReadAttrValue(node, "No"));
                foreach (XmlNode nodeFeed in xmlFeeds)
                {
                    if (nodeFeed.Name == Common.GetPropertyName<_Model.LineConfig>(x => x.FilePathResponse))
                    {
                        _Model.FilePathResponse = ReadXML_MV.ReadAttrValue(nodeFeed, "path");
                    }
                    else if (nodeFeed.Name == Common.GetPropertyName<_Model.LineConfig>(x => x.FilePathRequest))
                    {
                        _Model.FilePathRequest = ReadXML_MV.ReadAttrValue(nodeFeed, "path");
                    }
                }
                list.Add(_Model);
            }
            return list;
        }
    }
}
