using System;
using System.Collections.Generic;
using System.Linq;
using CalcEventDensity.Services.Base;
using CalcEventDensity.Models;
using CalcEventDensity.Models.Service;

namespace CalcEventDensity.Services
{
    public class CalculationService2D : ICalculationService
    {
        /// <summary>
        /// Container included events and grid points,
        ///  as well as boundary points of map
        /// </summary>
        private readonly PointContainer<IPoint> container;

        private List<IPoint> Events => container.Events;
        private List<IPoint> GridPoints => container.GridPoints;

        private CalculationParameters calcParams;

        public CalculationService2D(CalculationParameters calcParams, PointContainer<IPoint> container)
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
            {
                calcParams.DesicionBoundaryPoints(Events);

                container.GridPoints = new List<IPoint>();
                AddGridPoints();

                container.GridPoints.Capacity = container.GridPoints.Count;
            }

            CalcRectDensities();
            CalcCircDensities();
        }

        private void AddGridPoints()
        {
            for (double x = calcParams.MinX; x <= calcParams.MaxX; x += calcParams.PointRadius)
            {
                for (double y = calcParams.MinY; y <= calcParams.MaxY; y += calcParams.PointRadius)
                    GridPoints.Add(new Point2D(x, y, 0));
            }
        }

        private void CalcRectDensities()
        {
            // vertices of the rectangle
            double minX, maxX, minY, maxY;

            for (int i = 0; i < Events.Count; i++)
            {
                minX = Events[i].X - calcParams.PointRadius;
                maxX = Events[i].X + calcParams.PointRadius;
                minY = Events[i].Y - calcParams.PointRadius;
                maxY = Events[i].Y + calcParams.PointRadius;

                for (int j = i + 1; j < Events.Count; j++)
                {
                    if (Events[j].X >= minX && Events[j].X <= maxX &&
                        Events[j].Y >= minY && Events[j].Y <= maxY)
                    {
                        ++Events[i].DensityRect;
                        Events[i].EnergySumRect += Events[j].Energy;

                        ++Events[j].DensityRect;
                        Events[j].EnergySumRect += Events[i].Energy;
                    }
                }

                for (int k = 0; k < GridPoints.Count; k++)
                {
                    if (GridPoints[k].X >= minX && GridPoints[k].X <= maxX &&
                        GridPoints[k].Y >= minY && GridPoints[k].Y <= maxY)
                    {
                        ++GridPoints[k].DensityRect;
                        GridPoints[k].EnergySumRect += Events[i].Energy;
                    }
                }
            }
        }

        private void CalcCircDensities()
        {
            for (int i = 0; i < Events.Count; i++)
            {
                for (int j = i + 1; j < Events.Count; j++)
                {
                    if (Math.Pow(Events[i].X - Events[j].X, 2)
                        + Math.Pow(Events[i].Y - Events[j].Y, 2) <= calcParams.PointRadius * calcParams.PointRadius) //квадрат радиуса окружности
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
                        + Math.Pow(GridPoints[k].Y - Events[i].Y, 2) <= calcParams.PointRadius * calcParams.PointRadius) //квадрат радиуса окружности
                    {
                        ++GridPoints[k].DensityCirc;
                        GridPoints[k].EnergySumCirc += Events[i].Energy;
                    }
                }
            }
        }
    }
}
