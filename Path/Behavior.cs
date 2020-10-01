using System.Collections.Generic;

namespace Path
{
    public static class Behavior
    {
        public enum DistanceType
        {
            Short,
            Long
        }

        public static List<Point> Find(Grid grid, Point startPos, Point targetPos,
            DistanceType distance = DistanceType.Short, bool ignorePrices = false)
        {
            var points = new List<Point>();
            var nodes = GetPathNodes(grid, startPos, targetPos, distance, ignorePrices);

            if (nodes == null)
            {
                return points;
            }

            foreach (var node in nodes)
            {
                points.Add(new Point(node.gridX, node.gridY));
            }

            return points;
        }

        private static List<Node> GetPathNodes(Grid grid, Point startPos, Point targetPos,
            DistanceType distance = DistanceType.Short, bool ignorePrices = false)
        {
            var startNode = grid.nodes[startPos.x, startPos.y];
            var targetNode = grid.nodes[targetPos.x, targetPos.y];

            var openSet = new List<Node>();
            var closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                var currentNode = openSet[0];

                for (var i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < currentNode.FCost ||
                        openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    return RetracePath(startNode, targetNode);
                }

                foreach (var neighbour in grid.GetNeighbours(currentNode, distance))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    var neighbourCost = currentNode.gCost + GetDistance(currentNode, neighbour) *
                        (ignorePrices ? 1 : (int) (10f * neighbour.price));

                    if (neighbourCost >= neighbour.gCost && openSet.Contains(neighbour))
                    {
                        continue;
                    }

                    neighbour.gCost = neighbourCost;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }

            return null;
        }

        private static List<Node> RetracePath(Node startNode, Node endNode)
        {
            var path = new List<Node>();
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();

            return path;
        }

        private static int GetDistance(Node nodeA, Node nodeB)
        {
            var dstX = System.Math.Abs(nodeA.gridX - nodeB.gridX);
            var dstY = System.Math.Abs(nodeA.gridY - nodeB.gridY);

            return dstX > dstY ? 14 * dstY + 10 * (dstX - dstY) : 14 * dstX + 10 * (dstY - dstX);
        }
    }
}