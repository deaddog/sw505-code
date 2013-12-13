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
            return new Services.RouteServices.UserInput.GUIScheduler();
        }

        public IScheduler GetAutomatedScheduler()
        {
            return new Services.RouteServices.Automation.AutomatedScheduler();
        }
    }
}
