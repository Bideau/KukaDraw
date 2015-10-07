using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KukaDraw.Brain
{
    class Bezier
    {
        public PointF[] curveCoordinates;

        public PointF[] getCurveCoordinates()
        {
            return this.curveCoordinates;
        }

        public void GetBezierApproximationR(PointF[] controlPoints, int outputSegmentCount)
        {
            curveCoordinates = new PointF[outputSegmentCount + 1];
            for (int i = 0; i <= outputSegmentCount; i++)
            {
                float t = (float)i / outputSegmentCount;
                curveCoordinates[i] = GetBezierPointR(t, controlPoints, 0, controlPoints.Length);
            }

        }

        private PointF GetBezierPointR(float t, PointF[] controlPoints, int index, int count)
        {
            if (count == 1)
                return controlPoints[index];
            PointF P0 = GetBezierPointR(t, controlPoints, index, count - 1);
            PointF P1 = GetBezierPointR(t, controlPoints, index + 1, count - 1);
            return new PointF((1 - t) * P0.X + t * P1.X, (1 - t) * P0.Y + t * P1.Y);
        }

        public void GetBezierApproximation(PointF[] controlPoints, int outputSegmentCount)
        {
            curveCoordinates = new PointF[outputSegmentCount + 1];
            for (int i = 0; i <= outputSegmentCount; i++)
            {
                float t = (float)i / outputSegmentCount;
                curveCoordinates[i] = GetBezierPoint(t, controlPoints);
            }

        }
        private PointF GetBezierPoint(float t, PointF[] controlPoints)
        {
            double x = controlPoints[0].X * Math.Pow((1 - t), 3) + 3 * controlPoints[1].X * t * Math.Pow((1 - t), 2) + 3 * controlPoints[2].X * Math.Pow(t, 2) * (1 - t) + controlPoints[3].X * Math.Pow(t, 3);
            double y = controlPoints[0].Y * Math.Pow((1 - t), 3) + 3 * controlPoints[1].Y * t * Math.Pow((1 - t), 2) + 3 * controlPoints[2].Y * Math.Pow(t, 2) * (1 - t) + controlPoints[3].Y * Math.Pow(t, 3);

            return new PointF((float)x, (float)y);
        }
    }
}
