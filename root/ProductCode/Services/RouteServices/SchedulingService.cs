﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Interfaces;
using Data;

namespace Services.RouteServices
{
    public class SchedulingService
    {
        private static SchedulingService instance;
        public static SchedulingService Instance
        {
            get
            {
                if (instance == null)
                    instance = new SchedulingService();
                return instance;
            }
        }

        private Queue<Queue<ICoordinate>> points;

        private SchedulingService()
        {
            this.points = new Queue<Queue<ICoordinate>>();
        }

        public IEnumerable<ICoordinate> GetRoute(OccupancyGrid grid)
        {
            if (points.Count > 0)
            {
                var queue = points.Dequeue();
                if (queue.Count > 0)
                    return ReturnElements(queue);
                else
                    return GetRoute(grid);
            }
            else
            {
                LoadPoints(grid);
                return GetRoute(grid);
            }
        }

        private IEnumerable<ICoordinate> ReturnElements(Queue<ICoordinate> queue)
        {
            foreach (ICoordinate item in queue)
                yield return item;
        }

        private void LoadPoints(OccupancyGrid grid)
        {
            List<ICoordinate> coordinates = new List<ICoordinate>();
            using (SchedulingForm form = new SchedulingForm(grid))
            {
                form.ShowDialog();
                coordinates.Add(form.Point);
            }
            Queue<ICoordinate> queue = new Queue<ICoordinate>();
            queue.Enqueue(coordinates[0]);
            points.Enqueue(queue);
        }
    }
}
