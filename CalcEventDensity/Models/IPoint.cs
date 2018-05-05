
namespace CalcEventDensity.Models
{
    public interface IPoint
    {
        double Energy { get; }

        double X { get; }
        double Y { get; }
        double Z { get; }

        double DensityRect { get; set; }
        double EnergySumRect { get; set; }

        double DensityCirc { get; set; }
        double EnergySumCirc { get; set; }
    }

}
