using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using KukaDraw.Core;

namespace KukaDraw.Brain
{
    class Optimize
    {
        public Optimize()
        {
        }

        public List<PointF> drawingOptimize(List<PointF> tabListPointF)
        {
            PointF p1 = tabListPointF[0];
            PointF p2 = tabListPointF[1];

            List<PointF> tmpListePointF = new List<PointF>();

            for (int i = 1; i < tabListPointF.Count - 1; i++)
            {
                if (CompareLengthOfToPointF(p1,p2,Constants.minSizeLine))
                {
                    p2 = tabListPointF[i+1];
                }else{
                    tmpListePointF.Add(p1);
                    tmpListePointF.Add(p2);
                    p1 = p2;
                    p2 = tabListPointF[i+1];
                }
            }
            return tmpListePointF;
        }
        private bool CompareLengthOfToPointF(PointF p1, PointF p2, float distanceMin)
        {
            float distance = 0.0f;
            distance = (float)Math.Sqrt((Math.Pow((double)((p2.X - p1.X) + (p2.Y - p1.Y)),(double)2)));

            if (distance < distanceMin)
            {
                return true;
            }
            return false;
        }
    }
}
