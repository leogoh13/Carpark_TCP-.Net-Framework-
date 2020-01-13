using System;
using System.Xml;

namespace Carpark_TCP
{
    public class XML
    {
        string filename;
        public XML(string filename)
        {
            this.filename = filename;
        }
        public string GetStrXML(string root, string parent, string var)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/" + root + "/" + parent);

            foreach (XmlNode node in nodeList)
                return node.SelectSingleNode(var).InnerText;

            return "";
        }
        public string GetStrXML(string xpath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            return xmlDoc.SelectSingleNode(xpath).InnerText;
        }
        public int GetIntXML(string root, string parent, string var)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/" + root + "/" + parent);

            foreach (XmlNode node in nodeList)
                return Int32.Parse(node.SelectSingleNode(var).InnerText);

            return 0;
        }
        public int GetIntXML(string xpath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            return Int32.Parse(xmlDoc.SelectSingleNode(xpath).InnerText);
        }
        public string GetSingleNode(XmlNodeList nodeList, string var)
        {
            foreach (XmlNode node in nodeList)
            {
                return node.SelectSingleNode(var).InnerText;
            }
            return "";
        }
        public XmlNodeList GetNodelist(string xpath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            return xmlDoc.SelectNodes(xpath);
        }
    }
}



