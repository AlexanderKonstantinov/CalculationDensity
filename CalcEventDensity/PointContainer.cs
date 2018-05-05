using System.Collections.Generic;
using CalcEventDensity.Models;

namespace CalcEventDensity
{
    public class PointContainer<T> where T : IPoint
    {
        public List<T> Events { get; set; }
        public List<T> GridPoints { get; set; }
        
        public PointContainer() { }
    }
}
