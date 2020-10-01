namespace Path
{
    public class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override int GetHashCode()
        {
            return x ^ y;
        }

        public override bool Equals(object target)
        {
            if (target != null && target.GetType() != typeof(Point))
            {
                return false;
            }

            var point = (Point) target;

            if (ReferenceEquals(null, point))
            {
                return false;
            }

            return x == point.x && y == point.y;
        }

        public bool Equals(Point point)
        {
            if (ReferenceEquals(null, point))
            {
                return false;
            }

            return x == point.x && y == point.y;
        }

        public static bool operator ==(Point a, Point b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (ReferenceEquals(null, a))
            {
                return false;
            }

            if (ReferenceEquals(null, b))
            {
                return false;
            }

            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
    }
}