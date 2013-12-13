using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RouteServices.Automation
{
    public class DijkstraSearch<T>
    {
        private List<DijkstraNode<T>> S;
        private List<DijkstraNode<T>> Q;
        private Dictionary<T, DijkstraNode<T>> nodes;

        public void Search(Func<T, T, uint> w, T s, Func<T, IEnumerable<T>> adj)
        {
            InitializeSingleSource(s);

            while (Q.Count > 0)
            {
                var u = ExtractMin(Q);
                S.Add(u);
                foreach (var v in Adj(u, adj))
                    Relax(u, v, w);
            }
        }

        private void InitializeSingleSource(T s)
        {
            S = new List<DijkstraNode<T>>();
            Q = new List<DijkstraNode<T>>();
            nodes = new Dictionary<T, DijkstraNode<T>>();

            DijkstraNode<T> start = DiscoverNode(s);
            start.Weight = 0;
        }
        private DijkstraNode<T> ExtractMin(List<DijkstraNode<T>> list)
        {
            return (from node in list orderby node.Weight ascending select node).First();
        }
        private IEnumerable<DijkstraNode<T>> Adj(DijkstraNode<T> element, Func<T, IEnumerable<T>> adj)
        {
            foreach (var e in adj(element.Value))
            {
                if (nodes.ContainsKey(e))
                    yield return nodes[e];
                else
                    yield return DiscoverNode(e);
            }
        }
        private void Relax(DijkstraNode<T> u, DijkstraNode<T> v, Func<T, T, uint> w)
        {
            if (v.Weight > u.Weight + w(u.Value, v.Value))
            {
                v.Weight = u.Weight + w(u.Value, v.Value);
                v.Parent = u;
            }
        }

        protected DijkstraNode<T> DiscoverNode(T value)
        {
            DijkstraNode<T> node = new DijkstraNode<T>(value);
            Q.Add(node);
            nodes.Add(value, node);
            return node;
        }
    }
}
