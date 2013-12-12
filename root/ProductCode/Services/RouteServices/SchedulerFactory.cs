using System;

namespace Services.RouteServices
{
    public class SchedulerFactory
    {
        private static SchedulerFactory instance;

        public static SchedulerFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new SchedulerFactory();

                return instance;
            }
        }

        private SchedulerFactory()
        {
        }

        public IScheduler GetGUIScheduler()
        {
            return new GUIScheduler();
        }

        public IScheduler GetAutomatedScheduler()
        {
            throw new NotImplementedException();
        }
    }
}
