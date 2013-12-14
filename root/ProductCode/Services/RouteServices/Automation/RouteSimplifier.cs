using CommonLib.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Services.RouteServices.Automation
{
    public static class RouteSimplifier
    {
        public static IEnumerable<CellIndex> GetRoute(DijkstraNode<CellIndex> node)
        {
            var e = convertToCollection(node).GetEnumerator();

            // Get the first point, but don't yield it
            if (!e.MoveNext()) yield break;
            CellIndex p1 = e.Current;

            // Get the second point, for the loop below
            if (!e.MoveNext())
            {
                yield return p1;
                yield break;
            }
            CellIndex p2 = e.Current;

            // While there are additional points
            while (e.MoveNext())
            {
                CellIndex p3 = e.Current;

                // If p1, p2 and p3 are not arranged in a line, yield p2
                if(!isInline(p1,p2,p3))
                    yield return p2;

                p2 = p3;
            }

            yield return p2;
        }

        private static bool isInline(CellIndex p1, CellIndex p2, CellIndex p3)
        {
            return (p1.X == p2.X && p2.X == p3.X) || (p1.Y == p2.Y && p2.Y == p3.Y);
        }

        private static IEnumerable<CellIndex> convertToCollection(DijkstraNode<CellIndex> node)
        {
            if (node == null)
                yield break;

            foreach (var cell in convertToCollection(node.Parent))
                yield return cell;

            yield return node.Value;
        }
    }
}
