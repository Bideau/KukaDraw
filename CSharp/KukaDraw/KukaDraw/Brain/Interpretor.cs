using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;

namespace KukaDraw.Brain
{
    class Interpretor
    {
        private string[] data;
        private List<PointF> tabPointF;
        private Bezier bezier;

        public Interpretor()
        {
            this.data = null;
            this.tabPointF = new List<PointF>();
            this.bezier = new Bezier();
        }

        public List<PointF> getTabPointF()
        {
            return this.tabPointF;
        }

        public string[] getData()
        {
            return this.data;
        }

        public void setData(string[] data)
        {
            this.data = data;
        }


        public void interpretation(string[] data)
        {
            PointF p1;
            this.data = data;
            List<PointF> PointListTmp = new List<PointF>();

            for (int i = 0; i < this.data.Length - 1; i++)
            {
                if (this.data[i].Equals("m") || this.data[i].Equals("M"))
                {
                    p1 = new PointF(float.Parse(this.data[i + 1]), float.Parse(this.data[i + 2]));
                    this.tabPointF.Add(p1);
                    i = i + 2;
                }
                else if (this.data[i].Equals("c") || this.data[i].Equals("C"))
                {
                    int cpt = 1;
                    Console.WriteLine("toto");
                    do
                    {
                        PointListTmp.Add(new PointF(float.Parse(this.data[i+cpt]),float.Parse(this.data[i+1+cpt])));
                        cpt = cpt+2;
                        Console.WriteLine("toto2");
                    } while (this.data[i].Equals("m") || this.data[i].Equals("M") || this.data[i].Equals("c") || this.data[i].Equals("C") || this.data[i].Equals("l") || this.data[i].Equals("l") || i >= this.data.Length-1);
                    i = i + cpt;
                    if(i < this.data.Length-1)
                    {
                        if(this.data[i+1].Equals("c")||this.data[i+1].Equals("c")||this.data[i+1].Equals("l")||this.data[i+1].Equals("L")||this.data[i+1].Equals("m")||this.data[i+1].Equals("M"))
                        {
                            this.bezier.GetBezierApproximationR(PointListTmp.ToArray(), 20);
                            this.tabPointF.AddRange(this.bezier.getCurveCoordinates());
                            p1 = PointListTmp.Last();
                        }
                    }else
                    {
                        this.bezier.GetBezierApproximationR(PointListTmp.ToArray(), 20);
                        this.tabPointF.AddRange(this.bezier.getCurveCoordinates());
                    }
                }else if (this.data[i].Equals("l") || this.data[i].Equals("L")){

                    tabPointF.Add(new PointF(float.Parse(this.data[i + 1]), float.Parse(this.data[i + 2])));
                }
            }

        }
    }
}
