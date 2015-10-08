using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using KukaDraw.Core;

namespace KukaDraw.Brain
{
    class Interpretor
    {
        private string[] data;
        private Bezier bezier;
        public Orders myOrders;
        private float kukaXMax = Constants.canvasDsX;
        private float kukaYMax = Constants.canvasDsY;
        private Optimize optimize;

        // Constructor
        public Interpretor()
        {
            this.data = null;
            this.bezier = new Bezier();
            this.myOrders = new Orders();
            this.optimize = new Optimize();
        }

        // Adapt all coordinates to the kukaPlan
        public void transformCoordinates()
        {
            Boolean first = true;

            float imgXMax = 0;
            float imgYMax = 0;
            float myX = 0;
            float myY = 0;
            float scaleOnX;
            float scaleOnY;

            this.coordinatesToAbsoluteOnes();

            // Get maxX and maxY
            for (int index = 0; index < data.Length; index++)
            {
                if (this.data[index].Equals("m") || this.data[index].Equals("M") ||
                        this.data[index].Equals("c") || this.data[index].Equals("C") || this.data[index].Equals("l") ||
                        this.data[index].Equals("L") || this.data[index].Equals("y") || this.data[index].Equals("Y"))
                {
                }
                else
                {
                    if (first)
                    {
                        myX = float.Parse(data[index]);
                        if (myX > imgXMax) imgXMax = myX;
                        first = false;
                    }
                    else
                    {
                        myY = float.Parse(data[index]);
                        if (myY > imgYMax) imgYMax = myY;
                        first = true;
                    }
                }
            }

            scaleOnX = this.kukaXMax / imgXMax + 50;
            scaleOnY = this.kukaYMax / imgYMax + 50;
            first = true;


            // Scale all coordinates to the kuka plan
            for (int index = 0; index < data.Length; index++)
            {
                if (data[index].Equals("m") || data[index].Equals("M") || data[index].Equals("c") ||
                    data[index].Equals("C") || data[index].Equals("l") || data[index].Equals("L") ||
                    this.data[index].Equals("y") || this.data[index].Equals("Y"))
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

        public void coordinatesToAbsoluteOnes()
        {

            Boolean endMove = true;
            float baseX;
            float baseY;

            baseX = 0;
            baseY = 0;

            for (int i = 0; i < data.Length; i++)
            {
                // M CASE
                if (this.data[i].Equals("M") || this.data[i].Equals("m"))
                {
                    // Adapt coordinates
                    this.data[i + 1] = (float.Parse(this.data[i + 1]) + baseX).ToString();
                    this.data[i + 2] = (float.Parse(this.data[i + 2]) + baseY).ToString();

                    // Get new base point
                    baseX = float.Parse(this.data[i + 1]);
                    baseY = float.Parse(this.data[i + 2]);

                    // DEBUG
                    //Console.Write("M ");
                }

                // L CASE
                else if (this.data[i].Equals("l") || this.data[i].Equals("L"))
                {
                    do
                    {

                        // Adapt coordinates
                        this.data[i + 1] = (float.Parse(this.data[i + 1]) + baseX).ToString();
                        this.data[i + 2] = (float.Parse(this.data[i + 2]) + baseY).ToString();

                        // Get new base point
                        baseX = float.Parse(this.data[i + 1]);
                        baseY = float.Parse(this.data[i + 2]);

                        // Move on the array
                        i = i + 2;

                        if (i < this.data.Length - 1) // Outside array ?
                        {
                            if (this.data[i + 1].Equals("m") || this.data[i + 1].Equals("M") || this.data[i + 1].Equals("Y") ||
                        this.data[i + 1].Equals("c") || this.data[i + 1].Equals("C") || this.data[i + 1].Equals("l") ||
                        this.data[i + 1].Equals("L") || this.data[i + 1].Equals("y")) // Another move ?
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

                    // DEBUG
                    //Console.Write("L ");

                }

                // C CASE
                else if (this.data[i].Equals("c") || this.data[i].Equals("C"))
                {
                    float tmpBaseX = 0;
                    float tmpBaseY = 0;
                    do
                    {

                        // Adapt coordinates
                        this.data[i + 1] = (float.Parse(this.data[i + 1]) + baseX).ToString();
                        this.data[i + 2] = (float.Parse(this.data[i + 2]) + baseY).ToString();

                        // Get new temporary base point
                        tmpBaseX = float.Parse(this.data[i + 1]);
                        tmpBaseY = float.Parse(this.data[i + 2]);

                        // Move on the array
                        i = i + 2;

                        if (i < this.data.Length - 1) // Outside array ?
                        {
                            if (this.data[i + 1].Equals("m") || this.data[i + 1].Equals("M") || this.data[i + 1].Equals("Y") ||
                        this.data[i + 1].Equals("c") || this.data[i + 1].Equals("C") || this.data[i + 1].Equals("l") ||
                        this.data[i + 1].Equals("L") || this.data[i + 1].Equals("y")) // Another move ?
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

                    baseX = tmpBaseX;
                    baseY = tmpBaseY;
                }

                // Y CASE
                else if (this.data[i].Equals("y") || this.data[i].Equals("Y"))
                {
                    // New path. Initialization of base point.
                    baseX = 0;
                    baseY = 0;
                }
            }
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
                    //Console.Write("M ");
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
                            if (this.data[j + 1].Equals("m") || this.data[j + 1].Equals("M") || this.data[j + 1].Equals("Y") || this.data[j + 1].Equals("y") ||
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
                    PointListTmp = optimize.drawingOptimize(PointListTmp);
                    myOrders.addOrder(PointListTmp);
                    PointListTmp.Clear();

                    // DEBUG
                    //Console.Write("L ");

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
                            if (this.data[j + 1].Equals("m") || this.data[j + 1].Equals("M") || this.data[j + 1].Equals("Y") || this.data[j + 1].Equals("y") ||
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
                    PointListTmp2 = optimize.drawingOptimize(PointListTmp2);
                    myOrders.addOrder(PointListTmp2.ToArray());

                    // DEBUG
                    //Console.Write("C{0} ", PointListTmp.ToArray().Length);

                    PointListTmp.Clear();
                    PointListTmp2.Clear();
                }
            }
        }
    }
}
