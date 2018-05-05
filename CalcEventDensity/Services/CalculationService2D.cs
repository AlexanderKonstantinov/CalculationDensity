using CalcEventDensity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CalcEventDensity.Services.Base;
using static CalcEventDensity.PointContainer<CalcEventDensity.Models.Point2D>;

namespace CalcEventDensity.Services
{
    public class CalculationService2D
    {
        private List<Point2D> events;
        private List<Point2D> gridPoints;
        private bool isGridPoints;

        /// <summary>
        /// Шаг сетки. Если 0, то точки сетки не добавляются и не рассчитываются
        /// </summary>
        private readonly int gridStep;

        public CalculationService2D(PointContainer<Point2D> container, int gridStep, bool isGridPoints)
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

            CalcRectDensities();
            CalcCircDensities();
        }

        /// <summary>
        /// Определение граничных точек
        /// </summary>
        private void DesicionBoundaryPoints()
        {
            MinX = MaxX = events.First().X;
            MinY = MaxY = events.First().Y;

            FindBoundaryPoints(events);
            FindBoundaryPoints(gridPoints);
        }

        private void FindBoundaryPoints(IEnumerable<Point2D> points)
        {
            foreach (var point in points)
            {
                if (point.X < MinX) MinX = point.X;
                if (point.X > MaxX) MaxX = point.X;
                if (point.Y < MinY) MinY = point.Y;
                if (point.Y > MaxY) MaxY = point.Y;
            }
        }

        private void AddGridPoints()
        {
            for (double x = MinX; x < MaxX; x += 5)
            {
                for (double y = MinY; y < MaxY; y += 5)
                    gridPoints.Add(new Point2D(x, y, 0));
            }
            gridPoints = gridPoints.Distinct().ToList();
        }

        private void CalcRectDensities()
        {
            // Граничные координаты вершин куба
            double minX, maxX, minY, maxY;

            for (int i = 0; i < events.Count; i++)
            {
                minX = events[i].X - gridStep;
                maxX = events[i].X + gridStep;
                minY = events[i].Y - gridStep;
                maxY = events[i].Y + gridStep;

                for (int j = i + 1; j < events.Count; j++)
                {
                    if (events[j].X >= minX && events[j].X <= maxX &&
                        events[j].Y >= minY && events[j].Y <= maxY)
                    {
                        ++events[i].DensityRect;
                        events[i].EnergySumRect += events[j].Energy;

                        ++events[j].DensityRect;
                        events[j].EnergySumRect += events[i].Energy;
                    }
                }

                for (int k = 0; k < gridPoints.Count; k++)
                {
                    if (gridPoints[k].X >= minX && gridPoints[k].X <= maxX &&
                        gridPoints[k].Y >= minY && gridPoints[k].Y <= maxY)
                    {
                        ++gridPoints[k].DensityRect;
                        gridPoints[k].EnergySumRect += events[i].Energy;
                    }
                }
            }
        }

        private void CalcCircDensities()
        {
            for (int i = 0; i < events.Count; i++)
            {
                for (int j = i + 1; j < events.Count; j++)
                {
                    if (Math.Pow(events[i].X - events[j].X, 2)
                        + Math.Pow(events[i].Y - events[j].Y, 2) <= gridStep * gridStep) //квадрат радиуса окружности
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
                        + Math.Pow(gridPoints[k].Y - events[i].Y, 2) <= gridStep * gridStep) //квадрат радиуса окружности
                    {
                        ++gridPoints[k].DensityCirc;
                        gridPoints[k].EnergySumCirc += events[i].Energy;
                    }
                }
            }
        }
    }
}
