using System.Collections.Generic;

namespace Path
{
    public class Grid
    {
        public Node[,] nodes;

        private int _gridSizeX;
        private int _gridSizeY;

        public Grid(float[,] costs)
        {
            var width = costs.GetLength(0);
            var height = costs.GetLength(1);

            CreateNodes(width, height);

            for (var x = 0; x < _gridSizeX; x++)
            {
                for (var y = 0; y < _gridSizeY; y++)
                {
                    nodes[x, y] = new Node(costs[x, y], x, y);
                }
            }
        }

        public Grid(bool[,] tiles)
        {
            var width = tiles.GetLength(0);
            var height = tiles.GetLength(1);

            CreateNodes(width, height);

            for (var x = 0; x < _gridSizeX; x++)
            {
                for (var y = 0; y < _gridSizeY; y++)
                {
                    nodes[x, y] = new Node(tiles[x, y] ? 1f : 0f, x, y);
                }
            }
        }

        private void CreateNodes(int width, int height)
        {
            _gridSizeX = width;
            _gridSizeY = height;

            nodes = new Node[_gridSizeX, _gridSizeY];
        }

        public void Update(float[,] tiles)
        {
            var width = tiles.GetLength(0);
            var height = tiles.GetLength(1);

            if (nodes == null || _gridSizeX != width || _gridSizeY != height)
            {
                CreateNodes(width, height);
            }

            for (var x = 0; x < _gridSizeX; x++)
            {
                for (var y = 0; y < _gridSizeY; y++)
                {
                    nodes[x, y].Update(tiles[x, y], x, y);
                }
            }
        }

        public void Update(bool[,] tiles)
        {
            var width = tiles.GetLength(0);
            var height = tiles.GetLength(1);

            if (nodes == null || _gridSizeX != width || _gridSizeY != height)
            {
                CreateNodes(width, height);
            }

            for (var x = 0; x < _gridSizeX; x++)
            {
                for (var y = 0; y < _gridSizeY; y++)
                {
                    nodes[x, y].Update(tiles[x, y] ? 1f : 0f, x, y);
                }
            }
        }

        public IEnumerable<Node> GetNeighbours(Node node, Behavior.DistanceType distanceType)
        {
            var neighbours = new List<Node>();

            int x;
            int y;

            switch (distanceType)
            {
                case Behavior.DistanceType.Long:

                {
                    y = 0;

                    for (x = -1; x <= 1; ++x)
                    {
                        AddNodeNeighbour(x, y, node, neighbours);
                    }

                    x = 0;

                    for (y = -1; y <= 1; ++y)
                    {
                        AddNodeNeighbour(x, y, node, neighbours);
                    }

                    break;
                }

                case Behavior.DistanceType.Short:
                {
                    for (x = -1; x <= 1; x++)
                    {
                        for (y = -1; y <= 1; y++)
                        {
                            AddNodeNeighbour(x, y, node, neighbours);
                        }
                    }

                    break;
                }
                default: return neighbours;
            }

            return neighbours;
        }

        private void AddNodeNeighbour(int x, int y, Node node, ICollection<Node> neighbours)
        {
            if (x == 0 && y == 0)
            {
                return;
            }

            var checkX = node.gridX + x;
            var checkY = node.gridY + y;

            if (checkX < 0 || checkX >= _gridSizeX || checkY < 0 || checkY >= _gridSizeY)
            {
                return;
            }

            neighbours.Add(nodes[checkX, checkY]);
        }
    }
}