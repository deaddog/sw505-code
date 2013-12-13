using System;

namespace Services.RouteServices.Automation
{
    public class DijkstraNode<T>
    {
        private uint weight;
        private DijkstraNode<T> parent;
        private T value;

        public DijkstraNode(T value)
        {
            this.weight = uint.MaxValue;
            this.parent = null;
            this.value = value;
        }

        public uint Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        public DijkstraNode<T> Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public T Value
        {
            get { return value; }
        }
    }
}
