using System;
using System.Collections.Generic;
using CalcEventDensity.Models;
using CalcEventDensity.Models.Service;
using CalcEventDensity.Services.Base;

namespace CalcEventDensity.Services
{
    public class CalculationService3D : ICalculationService
    {
        /// <summary>
        /// Occurs when to end calculation
        /// </summary>
        public event Action OnCalculationEnd;

        /// <summary>
        /// Container included events and grid points,
        ///  as well as boundary points of map
        /// </summary>
        private readonly PointContainer<IPoint> container;

        private List<IPoint> Events => container.Events;
        private List<IPoint> GridPoints => container.GridPoints;

        private CalculationParameters calcParams;
        
        public CalculationService3D(CalculationParameters calcParams, PointContainer<IPoint> container)
        {
            this.calcParams = calcParams;
            this.container = container;
        }

        /// <summary>
        /// Execution of calculation
        /// </summary>
        public void Calculate()
        {
            if (calcParams.IsGridPoints)
                AddGridPoints();

            CalcCubDensities();
            CalcGlobeDensities();

            OnCalculationEnd?.Invoke();
        }
        
        private void AddGridPoints()
        {
            calcParams.DesicionBoundaryPoints(Events);

            for (double x = calcParams.MinX; x < calcParams.MaxX; x += calcParams.PointRadius)
            {
                for (double y = calcParams.MinY; y < calcParams.MaxY; y += calcParams.PointRadius)
                {
                    for (double z = calcParams.MinZ; z < calcParams.MaxZ; z += calcParams.PointRadius)
                        GridPoints.Add(new Point3D(x, y, z, 0));
                }
            }
        }
        
        private void CalcCubDensities()
        {
            // vertices of the cube
            double minX, maxX, minY, maxY, minZ, maxZ;
            
            for (int i = 0; i < Events.Count; i++)
            {
                // Формируем куб для конкретной точки
                minX = Events[i].X - calcParams.PointRadius;
                maxX = Events[i].X + calcParams.PointRadius;
                minY = Events[i].Y - calcParams.PointRadius;
                maxY = Events[i].Y + calcParams.PointRadius;
                minZ = Events[i].Z - calcParams.PointRadius;
                maxZ = Events[i].Z + calcParams.PointRadius;

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
                        + Math.Pow(Events[i].Z - Events[j].Z, 2) <= calcParams.PointRadius * calcParams.PointRadius) //квадрат радиуса сферы
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
                        + Math.Pow(GridPoints[k].Z - Events[i].Z, 2) <= calcParams.PointRadius * calcParams.PointRadius) //квадрат радиуса сферы
                    {
                        ++GridPoints[k].DensityCirc;
                        GridPoints[k].EnergySumCirc += Events[i].Energy;
                    }
                }
            }
        }
    }
}
