
namespace CalcEventDensity.Models
{
    public interface IPoint
    {
        double Energy { get; }

        double X { get; }
        double Y { get; }
        double Z { get; }

        /// <summary>
        /// Количество событий, входящих в квадрат (куб)
        /// </summary>
        double DensityRect { get; set; }

        /// <summary>
        /// Энергия событий, входящих в квадрат (куб)
        /// </summary>
        double EnergySumRect { get; set; }

        // <summary>
        /// Количество событий, входящих в круг (шар)
        /// </summary>
        double DensityCirc { get; set; }

        /// <summary>
        /// Энергия событий, входящих в круг (шар)
        /// </summary>
        double EnergySumCirc { get; set; }
    }

}
