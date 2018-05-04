using System.IO;
using System.Linq;
using CalcEventDensity.Models;

namespace CalcEventDensity.Services
{
    public static class WriteDataService<T> where T : IPoint
    {
        private static string dimension;
        private static string columnHeaders;

        public static string WriteFile(FileInfo file, PointContainer<T> container)
        {
            if (container.Events.First() is Point2D)
            {
                dimension = "_2D";
                columnHeaders = "X;Y;Energy;DensityRect;EnergySumRect;DensityCirc;EnergySumCirc";
            }
            else
            {
                dimension = "_3D";
                columnHeaders = "X;Y;Z;Energy;DensityCub;EnergySummCub;DensityGlobe;EnergySummGlobe";
            }

            string path = file.FullName.Replace(file.Extension, $"{dimension}.csv");

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(columnHeaders);

                foreach (IPoint point in container.GridPoints)
                    sw.WriteLine(point);

                foreach (IPoint point in container.Events)
                    sw.WriteLine(point);
            }

            return path;
        }
    }
}
