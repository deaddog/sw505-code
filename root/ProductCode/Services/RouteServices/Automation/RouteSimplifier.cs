using CommonLib.DTOs;
using System.Collections.Generic;

namespace Services.RouteServices.Automation
{
    public static class RouteSimplifier
    {
        private IEnumerable<CellIndex> convertToCollection(DijkstraNode<CellIndex> node)
        {
            if (node == null)
                yield break;

            foreach (var cell in convertToCollection(node.Parent))
                yield return cell;

            yield return node.Value;
        }
    }
}
