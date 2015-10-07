using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace KukaDraw.Core
{
    class Log
    {
        private string pathFile;

        public Log(string pathFile)
        {
            this.pathFile = pathFile;
        }
        public void writeLog(List<PointF> data)
        {
            streamWriter(DateTime.Now.ToLongTimeString() + Constants.stringSpace + DateTime.Now.ToLongDateString());
            foreach (PointF pointF in data)
            {
                streamWriter("x : " + pointF.X + " y : " + pointF.Y);
            }
            streamReader();

        }

        private void writeMessage(string logMessage, TextWriter w)
        {
            w.WriteLine(logMessage);
        }

        private void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
        private void streamWriter(string streamer){
            using (StreamWriter w = File.AppendText(this.pathFile))
            {
                writeMessage(streamer, w);
            }

        }
        private void streamReader()
        {
            using (StreamReader r = File.OpenText(this.pathFile))
            {
                DumpLog(r);
            }
        }
    }
}
