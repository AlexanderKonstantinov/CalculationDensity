using System;
using System.Collections.Generic;
using System.Linq;
using CalcEventDensity.Models;

using static CalcEventDensity.PointContainer<CalcEventDensity.Models.Point2D>;

namespace CalcEventDensity.Services
{
    public class CalculationService3D
    {
        public event Action OnCalculationEnd;

        private List<Point3D> events;
        private List<Point3D> gridPoints;
        private bool isGridPoints;
        /// <summary>
        /// Шаг сетки. Если 0, то точки сетки не добавляются и не рассчитываются
        /// </summary>
        private readonly int gridStep;

        public CalculationService3D(PointContainer<Point3D> container, int gridStep, bool isGridPoints)
        {
            this.isGridPoints = isGridPoints;
            this.gridStep = gridStep;
            events = container.Events;
            gridPoints = container.GridPoints;
        }

        public void Calculate()
        {
            if (isGridPoints)
            {
                DesicionBoundaryPoints();
                AddGridPoints();
            }

            CalcCubDensities();
            CalcGlobeDensities();

            OnCalculationEnd?.Invoke();
        }

        /// <summary>
        /// Определение граничных точек
        /// </summary>
        private void DesicionBoundaryPoints()
        {
            MinX = MaxX = events.First().X;
            MinY = MaxY = events.First().Y;
            MinZ = MaxZ = events.First().Z;

            FindBoundaryPoints(events);
            FindBoundaryPoints(gridPoints);
        }

        private void FindBoundaryPoints(IEnumerable<Point3D> points)
        {
            foreach (var point in points)
            {
                if (point.X < MinX) MinX = point.X;
                if (point.X > MaxX) MaxX = point.X;
                if (point.Y < MinY) MinY = point.Y;
                if (point.Y > MaxY) MaxY = point.Y;
                if (point.Z < MinZ) MinZ = point.Z;
                if (point.Z > MaxZ) MaxZ = point.Z;
            }
        }

        /// <summary>
        /// Добавление точек сетки, где энергия события равна 0 
        /// (сделал так, чтобы не создавать новый тип)
        /// </summary>
        private void AddGridPoints()
        {
            for (double x = MinX; x < MaxX; x += gridStep)
            {
                for (double y = MinY; y < MaxY; y += gridStep)
                {
                    for (double z = MinZ; z < MaxZ; z += gridStep)
                        gridPoints.Add(new Point3D(x, y, z, 0));
                }
            }

            gridPoints = gridPoints.Distinct().ToList();
        }
        
        /// <summary>
        /// Расчет плотности событий в объеме кубов размерами 5x5
        /// </summary>
        private void CalcCubDensities()
        {
            double minX, maxX, minY, maxY, minZ, maxZ;
            
            for (int i = 0; i < events.Count; i++)
            {
                // Формируем куб для конкретной точки
                minX = events[i].X - gridStep;
                maxX = events[i].X + gridStep;
                minY = events[i].Y - gridStep;
                maxY = events[i].Y + gridStep;
                minZ = events[i].Z - gridStep;
                maxZ = events[i].Z + gridStep;

                // Считаем количество событий, произошедших в границах конкретного куба
                // и суммируем их энергию
                for (int j = i + 1; j < events.Count; j++)
                {
                    if (events[j].X >= minX && events[j].X <= maxX &&
                        events[j].Y >= minY && events[j].Y <= maxY &&
                        events[j].Z >= minZ && events[j].Z <= maxZ)
                    {
                        ++events[i].DensityRect;
                        events[i].EnergySumRect += events[j].Energy;

                        ++events[j].DensityRect;
                        events[j].EnergySumRect += events[i].Energy;
                    }
                }

                // Считаем количество точек сетки, принадлежащих конкретному кубу
                // и прибавляем энергию этого события к суммарной энергии точки сетки
                for (int k = 0; k < gridPoints.Count; k++)
                {
                    if (gridPoints[k].X >= minX && gridPoints[k].X <= maxX &&
                        gridPoints[k].Y >= minY && gridPoints[k].Y <= maxY &&
                        gridPoints[k].Z >= minZ && gridPoints[k].Z <= maxZ)
                    {
                        ++gridPoints[k].DensityRect;
                        gridPoints[k].EnergySumRect += events[i].Energy;
                    }
                }
            }
        }

        private void CalcGlobeDensities()
        {
            for (int i = 0; i < events.Count; i++)
            {
                for (int j = i + 1; j < events.Count; j++)
                {
                    if (Math.Pow(events[i].X - events[j].X, 2)
                        + Math.Pow(events[i].Y - events[j].Y, 2)
                        + Math.Pow(events[i].Z - events[j].Z, 2) <= gridStep * gridStep) //квадрат радиуса сферы
                    {
                        ++events[i].DensityCirc;
                        events[i].EnergySumCirc += events[j].Energy;

                        ++events[j].DensityCirc;
                        events[j].EnergySumCirc += events[i].Energy;
                    }
                }

                for (int k = 0; k < gridPoints.Count; k++)
                {
                    if (Math.Pow(gridPoints[k].X - events[i].X, 2)
                        + Math.Pow(gridPoints[k].Y - events[i].Y, 2)
                        + Math.Pow(gridPoints[k].Z - events[i].Z, 2) <= gridStep * gridStep) //квадрат радиуса сферы
                    {
                        ++gridPoints[k].DensityCirc;
                        gridPoints[k].EnergySumCirc += events[i].Energy;
                    }
                }
            }
        }
    }
}
