using System;

namespace Services.RouteServices
{
    public class RouterFactory
    {
        private static RouterFactory instance;

        public static RouterFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new RouterFactory();

                return instance;
            }
        }

        private RouterFactory()
        {
        }
    }
}
