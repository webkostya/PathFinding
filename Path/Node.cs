namespace Path
{
    public class Node
    {
        public int gridX;
        public int gridY;

        public int gCost;
        public int hCost;

        public float price;
        public bool walkable;

        public Node parent;

        public int FCost => gCost + hCost;

        public Node(float cost, int x, int y)
        {
            gridX = x;
            gridY = y;

            price = cost;
            walkable = cost != 0f;
        }

        public void Update(float cost, int x, int y)
        {
            gridX = x;
            gridY = y;

            price = cost;
            walkable = cost != 0f;
        }
    }
}