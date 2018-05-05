using System.Collections.Generic;
using CalcEventDensity.Models;

namespace CalcEventDensity
{
    public class PointContainer<T> where T : IPoint
    {
        public List<T> Events { get; set; }
        public List<T> GridPoints { get; set; }

        public static double MinX { get; set; }
        public static double MaxX { get; set; }
        public static double MinY { get; set; }
        public static double MaxY { get; set; }
        public static double MinZ { get; set; }
        public static double MaxZ { get; set; }

        public PointContainer()
        {
            
        }
        public PointContainer(List<T> events, List<T> gridPoints)
        {
            Events = events;
            GridPoints = gridPoints;
        }
    }
}
