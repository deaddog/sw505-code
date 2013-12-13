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

        public void Search(Func<T, T, uint> w, T s)
        {
            InitializeSingleSource(s);

            while (Q.Count > 0)
            {
                var u = ExtractMin(Q);
                S.Add(u);
                foreach (var v in Adj(u))
                    Relax(u, v, w);
            }
        }

        private void InitializeSingleSource(T s)
        {
            S = new List<DijkstraNode<T>>();
            Q = new List<DijkstraNode<T>>();

            Q.Add(new DijkstraNode<T>(s) { Weight = 0 });
        }
        private DijkstraNode<T> ExtractMin(List<DijkstraNode<T>> list)
        {
            return (from node in list orderby node.Weight ascending select node).First();
        }
        private IEnumerable<DijkstraNode<T>> Adj(DijkstraNode<T> element)
        {
            throw new NotImplementedException();
        }
        private void Relax(DijkstraNode<T> u, DijkstraNode<T> v, Func<T, T, uint> w)
        {
            if (v.Weight > u.Weight + w(u.Value, v.Value))
            {
                v.Weight = u.Weight + w(u.Value, v.Value);
                v.Parent = u;
            }
        }
    }
}
