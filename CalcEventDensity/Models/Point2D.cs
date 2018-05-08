
namespace CalcEventDensity.Models
{
    public struct Point2D : IPoint
    {
        public double X { get; }
        public double Y { get; }
        public double Z => 0;

        public double Energy { get; }

        public double DensityRect { get; set; }
        public double EnergySumRect { get; set; }

        public double DensityCirc { get; set; }
        public double EnergySumCirc { get; set; }

        public Point2D(params double[] data)
        {
            X = data[0];
            Y = data[1];
            Energy = data[2];

            DensityCirc = DensityRect = Energy == 0 ? 0 : 1;

            EnergySumRect = EnergySumCirc = Energy;
        }

        public override bool Equals(object obj)
        {
            Point2D point2 = (Point2D)obj;
            return point2.X == this.X &&
                    point2.Y == this.Y;
        }

        public override int GetHashCode()
            => X.GetHashCode() + Y.GetHashCode();

        public override string ToString()
            => $"{X};{Y};{Energy};{DensityRect};{EnergySumRect};{DensityCirc};{EnergySumCirc}";

        public static IPoint GetEvent(params double[] data)
            => new Point2D(data);
    }
}
