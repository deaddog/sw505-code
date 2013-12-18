using Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace SystemInterface.GUI.Controls
{
    public class GridLogger
    {
        private string filepath;

        public GridLogger(string filepath)
        {
            this.filepath = filepath;
            FileInfo f = new FileInfo(filepath);
            using (var fs = f.Create()) { }
        }

        public void Log(OccupancyGrid grid)
        {
            using (BinaryWriter writer = new BinaryWriter(new FileStream(filepath, FileMode.Append)))
            {
                writer.Write(grid.X);
                writer.Write(grid.Y);
                writer.Write(grid.CellSize);
                writer.Write(grid.Columns);
                writer.Write(grid.Rows);
                for (int x = 0; x < grid.Columns; x++)
                    for (int y = 0; y < grid.Rows; y++)
                        writer.Write(grid[x, y]);
            }
        }

        public static OccupancyGrid[] LoadGrids(string filepath)
        {
            List<OccupancyGrid> grids = new List<OccupancyGrid>();

            using (BinaryReader reader = new BinaryReader(new FileStream(filepath, FileMode.Open)))
            {
                float xOff = reader.ReadSingle(), yOff = reader.ReadSingle();
                float size = reader.ReadSingle();

                int columns = reader.ReadInt32(), rows = reader.ReadInt32();

                double[,] array = new double[columns, rows];
                for (int x = 0; x < columns; x++)
                    for (int y = 0; y < rows; y++)
                        array[x, y] = reader.ReadDouble();

                grids.Add(new OccupancyGrid(array, size, xOff, yOff));
            }

            return grids.ToArray();
        }
    }
}
