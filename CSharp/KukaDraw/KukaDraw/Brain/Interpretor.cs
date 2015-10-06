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
        public Orders myOrders;
        private float kukaXMax = 260; // Size of X of the kuka's field
        private float kukaYMax = 185; // Size of Y of the kuka's field

        // Constructor
        public Interpretor()
        {
            this.data = null;
            this.tabPointF = new List<PointF>();
            this.bezier = new Bezier();
            this.myOrders = new Orders();
        }

        // Transform all 
        public void transformCoordinates()
        {
            Boolean first = true;

            float imgXMax = 0;
            float imgYMax = 0;
            float baseX;
            float baseY;
            float myX = 0;
            float myY = 0;
            float scaleOnX;
            float scaleOnY;
            baseX = float.Parse(data[1]);
            baseY = float.Parse(data[2]);

            // Convert to absolute coordinates
            for (int index = 3; index < data.Length; index++)
            {
                if (this.data[index].Equals("m") || this.data[index].Equals("M") ||
                        this.data[index].Equals("c") || this.data[index].Equals("C") || this.data[index].Equals("l") ||
                        this.data[index].Equals("L"))
                {

                }
                else
                {
                    if (first)
                    {
                        myX = float.Parse(data[index]);
                        myX = baseX + myX;
                        if (myX > imgXMax) imgXMax = myX;
                        data[index] = myX.ToString();
                        first = false;
                    }
                    else
                    {
                        myY = float.Parse(data[index]);
                        myY = baseY + myY;
                        if (myY > imgYMax) imgYMax = myY;
                        data[index] = myY.ToString();
                        first = true;
                    }
                }
            }

            scaleOnX = this.kukaXMax / imgXMax;
            scaleOnY = this.kukaYMax / imgYMax;
            first = true;
            // Scale all coordinates to the kuka plan
            for (int index = 0; index < data.Length; index++)
            {
                if (data[index].Equals("m") || data[index].Equals("M") || data[index].Equals("c") ||
                    data[index].Equals("C") || data[index].Equals("l") || data[index].Equals("L"))
                {

                }
                else
                {
                    if (first)
                    {
                        myX = float.Parse(data[index]);
                        myX = myX * scaleOnX;
                        data[index] = myX.ToString();
                        first = false;
                    }
                    else
                    {
                        myY = float.Parse(data[index]);
                        myY = imgYMax - myY;
                        myY = myY * scaleOnY;
                        data[index] = myY.ToString();
                        first = true;
                    }
                }
            }
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
            PointF p1 = new PointF(0, 0);
            this.data = data;
            this.transformCoordinates();
            List<PointF> PointListTmp = new List<PointF>();
            Boolean endMove = true;

            for (int i = 0; i < this.data.Length; i++)
            {
                // M CASE
                if (this.data[i].Equals("M") || this.data[i].Equals("m"))
                {
                    // Take the following point
                    p1 = new PointF(float.Parse(this.data[i + 1]), float.Parse(this.data[i + 2]));
                    i = i + 2;

                    // DEBUG
                    Console.Write("M ");
                }
                // L CASE
                else if (this.data[i].Equals("l") || this.data[i].Equals("L"))
                {

                    endMove = false; // Starting move
                    PointListTmp.Add(p1); // Take last point as a base

                    int j = i;
                    do
                    {
                        p1 = new PointF(float.Parse(this.data[j + 1]), float.Parse(this.data[j + 2]));
                        PointListTmp.Add(p1); // Add the point and use it as a base

                        j = j + 2;
                        if (j < this.data.Length - 1) // Outside array ?
                        {
                            if (this.data[j + 1].Equals("m") || this.data[j + 1].Equals("M") ||
                        this.data[j + 1].Equals("c") || this.data[j + 1].Equals("C") || this.data[j + 1].Equals("l") ||
                        this.data[j + 1].Equals("L")) // Another move ?
                            {
                                endMove = true;
                            }
                            else
                            {
                                endMove = false;
                            }
                        }
                        else
                        {
                            endMove = true;
                        }


                    } while (endMove == false);

                    i = j;

                    myOrders.addOrder(PointListTmp);
                    PointListTmp.Clear();

                    // DEBUG
                    Console.Write("L ");

                }
                // C CASE
                else if (this.data[i].Equals("c") || this.data[i].Equals("C"))
                {
                    endMove = false; // Starting move
                    PointListTmp.Add(p1); // Take last point as a base

                    int j = i;
                    do
                    {
                        p1 = new PointF(float.Parse(this.data[j + 1]), float.Parse(this.data[j + 2]));
                        PointListTmp.Add(p1); // Add the point and use it as a base

                        j = j + 2;
                        if (j < this.data.Length - 1) // Outside array ?
                        {
                            if (this.data[j + 1].Equals("m") || this.data[j + 1].Equals("M") ||
                        this.data[j + 1].Equals("c") || this.data[j + 1].Equals("C") || this.data[j + 1].Equals("l") ||
                        this.data[j + 1].Equals("L")) // Another move ?
                            {
                                endMove = true;
                            }
                            else
                            {
                                endMove = false;
                            }
                        }
                        else
                        {
                            endMove = true;
                        }
                    } while (endMove == false);

                    i = j;

                    List<PointF> PointListTmp2 = new List<PointF>();
                    int nbPasses = (PointListTmp.ToArray().Length - 1) / 3;
                    PointF[] tabPointF = new PointF[4];

                    for (int index1 = 0; index1 < nbPasses; index1++)
                    {
                        tabPointF[0] = PointListTmp[index1 * 3];
                        tabPointF[1] = PointListTmp[index1 * 3 + 1];
                        tabPointF[2] = PointListTmp[index1 * 3 + 2];
                        tabPointF[3] = PointListTmp[index1 * 3 + 3];

                        bezier.GetBezierApproximationR(tabPointF, 4);
                        foreach (PointF ptn in bezier.curveCoordinates)
                        {
                            PointListTmp2.Add(ptn);
                        }

                    }
                    myOrders.addOrder(PointListTmp2.ToArray());

                    // DEBUG
                    Console.Write("C{0} ", PointListTmp.ToArray().Length);

                    PointListTmp.Clear();
                    PointListTmp2.Clear();
                }
            }

            //for (int i = 0; i < this.data.Length - 1; i++)
            //{
            //    if (this.data[i].Equals("m") || this.data[i].Equals("M"))
            //    {
            //        p1 = new PointF(float.Parse(this.data[i + 1]), float.Parse(this.data[i + 2]));
            //        this.tabPointF.Add(p1);
            //        i = i + 2;
            //    }
            //    else if (this.data[i].Equals("c") || this.data[i].Equals("C"))
            //    {
            //        int cpt = 1;
            //        Console.WriteLine("toto");
            //        do
            //        {
            //            PointListTmp.Add(new PointF(float.Parse(this.data[i + cpt]), float.Parse(this.data[i + 1 + cpt])));
            //            cpt = cpt + 2;
            //            Console.WriteLine("toto2");
            //        } while (this.data[i].Equals("m") || this.data[i].Equals("M") || this.data[i].Equals("c") || this.data[i].Equals("C") || this.data[i].Equals("l") || this.data[i].Equals("l") || i >= this.data.Length - 1);
            //        i = i + cpt;
            //        if (i < this.data.Length - 1)
            //        {
            //            if (this.data[i + 1].Equals("c") || this.data[i + 1].Equals("c") || this.data[i + 1].Equals("l") || this.data[i + 1].Equals("L") || this.data[i + 1].Equals("m") || this.data[i + 1].Equals("M"))
            //            {
            //                this.bezier.GetBezierApproximationR(PointListTmp.ToArray(), 20);
            //                this.tabPointF.AddRange(this.bezier.getCurveCoordinates());
            //                p1 = PointListTmp.Last();
            //            }
            //        }
            //        else
            //        {
            //            this.bezier.GetBezierApproximationR(PointListTmp.ToArray(), 20);
            //            this.tabPointF.AddRange(this.bezier.getCurveCoordinates());
            //        }
            //    }
            //    else if (this.data[i].Equals("l") || this.data[i].Equals("L"))
            //    {

            //        tabPointF.Add(new PointF(float.Parse(this.data[i + 1]), float.Parse(this.data[i + 2])));
            //        PointListTmp.Add(new PointF(float.Parse(this.data[i + 3]), float.Parse(this.data[i + 4])));
            //        i = i + 4;
            //    }
            //}
        }
    }
}
