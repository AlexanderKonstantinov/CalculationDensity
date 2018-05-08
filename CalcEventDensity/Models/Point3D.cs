
namespace CalcEventDensity.Models
{
    public struct Point3D : IPoint
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public double Energy { get; }

        public double DensityRect { get; set; }
        public double EnergySumRect { get; set; }

        public double DensityCirc { get; set; }
        public double EnergySumCirc { get; set; }

        public Point3D(params double[] data)
        {
            X = data[0];
            Y = data[1];
            Z = data[2];
            Energy = data[3];

            DensityRect = DensityCirc = Energy == 0 ? 0 : 1;

            EnergySumRect = EnergySumCirc = Energy;
        }

        public override bool Equals(object obj)
        {
            Point3D event2 = (Point3D)obj;
            return event2.X == this.X &&
                   event2.Y == this.Y &&
                   event2.Z == this.Z;
        }

        public override int GetHashCode()
            => X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();

        public override string ToString()
            => $"{X};{Y};{Z};{Energy};{DensityRect};{EnergySumRect};{DensityCirc};{EnergySumCirc}";

        public static IPoint GetEvent(params double[] data)
            => new Point3D(data);
    }
}
