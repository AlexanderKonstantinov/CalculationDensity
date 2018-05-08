using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcEventDensity.Models.Service
{
    public class CalculationParameters
    {
        #region Boundary points of map
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }
        public int MinZ { get; set; }
        public int MaxZ { get; set; } 
        #endregion

        /// <summary>
        /// It characterizes the precision of calculation
        /// and space between grid points
        /// </summary>
        public readonly int PointRadius;

        /// <summary>
        /// Whether it is necessary to calculate additional grid points 
        /// </summary>
        public bool IsGridPoints { get; }
        
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
            double MinXd = events.First().X,
                   MaxXd = events.First().X,
                   MinYd = events.First().Y,
                   MaxYd = events.First().Y,
                   MinZd = events.First().Z,
                   MaxZd = events.First().Z;

            foreach (var point in events)
            {
                if (point.X < MinXd) MinXd = point.X;
                if (point.X > MaxXd) MaxXd = point.X;
                if (point.Y < MinYd) MinYd = point.Y;
                if (point.Y > MaxYd) MaxYd = point.Y;

                if (point.Z < MinZd) MinZd = point.Z;
                if (point.Z > MaxZd) MaxZd = point.Z;
            }

            MinX = (int) MinXd;
            MaxX = (int) MaxXd;
            MinY = (int) MinYd;
            MaxY = (int) MaxYd;
            MinZ = (int) MinZd;
            MaxZ = (int) MaxZd;
        }
    }
}
