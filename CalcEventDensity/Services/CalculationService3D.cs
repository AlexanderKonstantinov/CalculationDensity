using System;
using System.Collections.Generic;
using System.Linq;
using CalcEventDensity.Models;
using CalcEventDensity.Services.Base;
using static CalcEventDensity.PointContainer<CalcEventDensity.Models.Point3D>;

namespace CalcEventDensity.Services
{
    public class CalculationService3D : ICalculationService
    {
        public event Action OnCalculationEnd;

        private readonly PointContainer<IPoint> container;

        private List<IPoint> Events
        {
            get => container.Events;
            set => container.Events = value;
        }
        private List<IPoint> GridPoints
        {
            get => container.GridPoints;
            set => container.GridPoints = value;
        }

        private bool isGridPoints;
        /// <summary>
        /// Шаг сетки. Если 0, то точки сетки не добавляются и не рассчитываются
        /// </summary>
        private readonly int gridStep;

        public CalculationService3D(int gridStep, bool isGridPoints, PointContainer<IPoint> container)
        {
            this.isGridPoints = isGridPoints;
            this.gridStep = gridStep;

            this.container = container;
        }

        public void Calculate(ref string pathToNewFile)
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
            MinX = MaxX = Events.First().X;
            MinY = MaxY = Events.First().Y;
            MinZ = MaxZ = Events.First().Z;

            FindBoundaryPoints(Events);
            FindBoundaryPoints(GridPoints);
        }

        private void FindBoundaryPoints(IEnumerable<IPoint> points)
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
                        GridPoints.Add(new Point3D(x, y, z, 0));
                }
            }

            GridPoints = GridPoints.Distinct().ToList();
        }
        
        /// <summary>
        /// Расчет плотности событий в объеме кубов размерами 5x5
        /// </summary>
        private void CalcCubDensities()
        {
            double minX, maxX, minY, maxY, minZ, maxZ;
            
            for (int i = 0; i < Events.Count; i++)
            {
                // Формируем куб для конкретной точки
                minX = Events[i].X - gridStep;
                maxX = Events[i].X + gridStep;
                minY = Events[i].Y - gridStep;
                maxY = Events[i].Y + gridStep;
                minZ = Events[i].Z - gridStep;
                maxZ = Events[i].Z + gridStep;

                // Считаем количество событий, произошедших в границах конкретного куба
                // и суммируем их энергию
                for (int j = i + 1; j < Events.Count; j++)
                {
                    if (Events[j].X >= minX && Events[j].X <= maxX &&
                        Events[j].Y >= minY && Events[j].Y <= maxY &&
                        Events[j].Z >= minZ && Events[j].Z <= maxZ)
                    {
                        ++Events[i].DensityRect;
                        Events[i].EnergySumRect += Events[j].Energy;

                        ++Events[j].DensityRect;
                        Events[j].EnergySumRect += Events[i].Energy;
                    }
                }

                // Считаем количество точек сетки, принадлежащих конкретному кубу
                // и прибавляем энергию этого события к суммарной энергии точки сетки
                for (int k = 0; k < GridPoints.Count; k++)
                {
                    if (GridPoints[k].X >= minX && GridPoints[k].X <= maxX &&
                        GridPoints[k].Y >= minY && GridPoints[k].Y <= maxY &&
                        GridPoints[k].Z >= minZ && GridPoints[k].Z <= maxZ)
                    {
                        ++GridPoints[k].DensityRect;
                        GridPoints[k].EnergySumRect += Events[i].Energy;
                    }
                }
            }
        }

        private void CalcGlobeDensities()
        {
            for (int i = 0; i < Events.Count; i++)
            {
                for (int j = i + 1; j < Events.Count; j++)
                {
                    if (Math.Pow(Events[i].X - Events[j].X, 2)
                        + Math.Pow(Events[i].Y - Events[j].Y, 2)
                        + Math.Pow(Events[i].Z - Events[j].Z, 2) <= gridStep * gridStep) //квадрат радиуса сферы
                    {
                        ++Events[i].DensityCirc;
                        Events[i].EnergySumCirc += Events[j].Energy;

                        ++Events[j].DensityCirc;
                        Events[j].EnergySumCirc += Events[i].Energy;
                    }
                }

                for (int k = 0; k < GridPoints.Count; k++)
                {
                    if (Math.Pow(GridPoints[k].X - Events[i].X, 2)
                        + Math.Pow(GridPoints[k].Y - Events[i].Y, 2)
                        + Math.Pow(GridPoints[k].Z - Events[i].Z, 2) <= gridStep * gridStep) //квадрат радиуса сферы
                    {
                        ++GridPoints[k].DensityCirc;
                        GridPoints[k].EnergySumCirc += Events[i].Energy;
                    }
                }
            }
        }
    }
}
