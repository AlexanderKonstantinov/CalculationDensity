using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using CalcEventDensity.Models;
using Microsoft.Win32;

namespace CalcEventDensity.Services
{
    public static class ReadDataService
    {
        private static bool isCoordinateZ;
        public static FileInfo PathToInitialFile;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns>Был ли выбран файл и успешно считаны заголовки его столбцов</returns>
        public static bool OpenFile(Dimension dimension)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Выберите файл",
                Filter = "(разделитель \";\")|*.csv;*.txt", 
            };

            if (fd.ShowDialog() == true)
            {
                try
                {
                    using (var sr = new StreamReader(fd.FileName))
                    {
                        string[] columnHeaders = sr.ReadLine().Split(';');
                        isCoordinateZ = columnHeaders.Contains("Z");

                        string[] columnHeadersCheck = {"X", "Y", "Energy"};

                        foreach (var columnHeader in columnHeadersCheck)
                            if (!columnHeaders.Contains(columnHeader))
                                throw new Exception("В файле должны быть следующие заголовки столбцов: \"X\", \"Y\", \"Energy\" ");

                        if (dimension == Dimension.D3 && !isCoordinateZ)
                            throw new Exception("Вы выбрали двумерный режим расчета, но файл не содержит столбца с заголовком \"Z\"");
                    }

                    PathToInitialFile = new FileInfo(fd.FileName);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return false;
        }

        public static bool ReadData(List<IPoint> events, List<IPoint> gridPoints, Dimension dimension)
        {
            try
            {
                using (var sr = new StreamReader(PathToInitialFile.FullName))
                {
                    double[] temp;

                    sr.ReadLine(); // Пропуск заголовка

                    while (!sr.EndOfStream)
                    {
                        IPoint point;

                        if (dimension == Dimension.D2)
                        {
                            temp = isCoordinateZ
                                ? GetDoubleArrayWithSkip(sr.ReadLine())
                                : GetDoubleArrayWithoutSkip(sr.ReadLine());
                            point = Point2D.GetEvent(temp);
                        }
                        else
                        {
                            temp = GetDoubleArrayWithoutSkip(sr.ReadLine());
                            point = Point3D.GetEvent(temp);
                        }

                        if (point.Energy != 0)
                            events.Add(point);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось считать данные!");
                return false;
            }
        }

        /// <summary>
        /// Преобразует строку в массив вещественных чисел 2-й точности
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Возвращает массив элементов 2D-события</returns>
        static double[] GetDoubleArrayWithoutSkip(string row)
            => row.Split(';', '\t', ' ')
                .Select(c => Convert.ToDouble(c)).ToArray();

        /// <summary>
        /// Преобразует строку в массив вещественных чисел 2-й точности
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Возвращает массив элементов 3D-события</returns>
        static double[] GetDoubleArrayWithSkip(string row)
        {
            double[] array = row.Split(';', '\t', ' ').Select(c => Convert.ToDouble(c)).ToArray();

            return new[] {array[0], array[1], array[3]};
        }
    }
}
