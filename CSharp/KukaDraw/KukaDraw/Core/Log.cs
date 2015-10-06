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
        public Log(List<PointF> tata)
        {
            foreach (PointF item in tata)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    toto("x : " + item.X + " y : " + item.Y, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    DumpLog(r);
                }
            }
            
        }

        public static void toto(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}
