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
        private Func<T, IEnumerable<T>> adjecent;

        public static DijkstraNode<T>[] Search(Func<T, IEnumerable<T>> adjecent, Func<T, T, uint> w, T s)
        {
            DijkstraSearch<T> searcher = new DijkstraSearch<T>(adjecent);
            return searcher.Search(w, s);
        }

        private DijkstraSearch(Func<T, IEnumerable<T>> adjecent)
        {
            this.adjecent = adjecent;
        }

        public DijkstraNode<T>[] Search(Func<T, T, uint> w, T s)
        {
            InitializeSingleSource(s);

            while (Q.Count > 0)
            {
                var u = ExtractMin(Q);
                S.Add(u);
                foreach (var v in Adj(u))
                    Relax(u, v, w);
            }

            // Return the result of the algorithm above by.
            return S.ToArray();
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
        private IEnumerable<DijkstraNode<T>> Adj(DijkstraNode<T> element)
        {
            foreach (var e in adjecent(element.Value))
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
