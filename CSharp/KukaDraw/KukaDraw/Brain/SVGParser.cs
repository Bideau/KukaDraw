using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.Xml;

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
        public void addPoint(Point point)
        {
            this.pointArray.Add(point);
        }
        public void SvgXmlReader(string PathFile){
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(PathFile,settings);
            reader.Read();
            reader.ReadStartElement("path");
            Console.WriteLine(reader.ReadString());
            reader.ReadEndElement();
        }
    }
}
