using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace KukaDraw.Brain
{
    class SVGParser
    {
        private ArrayList pointArray;
        public SVGParser()
        {
            this.pointArray = new ArrayList();
        }

        public ArrayList GetPointArray()
        {
            return pointArray;
        }

        public void SetPointArray(ArrayList ArrayPoint)
        {
            this.pointArray = ArrayPoint;
        }
        public void addPoint(PointF point)
        {
            this.pointArray.Add(point);
        }
        public void SvgXmlReader(Stream xmlStream)
        {
            var reader = new XmlTextReader(xmlStream);
            reader.ProhibitDtd = false;
            reader.DtdProcessing = DtdProcessing.Ignore;
            reader.XmlResolver = null;

            var doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(reader);

            foreach (XmlElement node in doc.GetElementsByTagName("path"))
            {
                Console.WriteLine(node.GetAttribute("d"));
            }
        }
    }
}
