using System.Collections.Generic;
using System.Linq;

namespace CalcEventDensity.Models.Service
{
    public class CalculationParameters
    {
        #region Boundary points of map
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
        public double MinZ { get; set; }
        public double MaxZ { get; set; } 
        #endregion

        /// <summary>
        /// It characterizes the precision of calculation
        /// and space between grid points
        /// </summary>
        public readonly int PointRadius;

        /// <summary>
        /// Whether it is necessary to calculate additional grid points 
        /// </summary>
        public readonly bool IsGridPoints;

        public CalculationParameters(int pointRadius, bool isGridPoints)
        {
            this.PointRadius = pointRadius;
            this.IsGridPoints = isGridPoints;
        }

        /// <summary>
        /// Desicion boundary grid points
        /// </summary>
        public void DesicionBoundaryPoints(IEnumerable<IPoint> events)
        {
            MinX = MaxX = events.First().X;
            MinY = MaxY = events.First().Y;
            MinZ = MaxZ = events.First().Z;

            foreach (var point in events)
            {
                if (point.X < MinX) MinX = point.X;
                if (point.X > MaxX) MaxX = point.X;
                if (point.Y < MinY) MinY = point.Y;
                if (point.Y > MaxY) MaxY = point.Y;

                if (point.Z < MinZ) MinZ = point.Z;
                if (point.Z > MaxZ) MaxZ = point.Z;
            }
        }
    }
}
