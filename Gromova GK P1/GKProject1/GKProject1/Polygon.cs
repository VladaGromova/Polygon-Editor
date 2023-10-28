namespace GKProject1
{
    class Edge {
        private Point start, end;
        private bool vertical = false;
        private bool horizontal = false;
        public Edge(Point st, Point en) {
            start = st;
            end = en;
        }
        public Point getStart() { return start; }
        public Point getEnd() { return end; }
        public void setStart(Point st) { start = st; }
        public void setEnd(Point en) { end = en; }
        public bool isVertical() { return vertical; }
        public bool isHorizontal() { return horizontal; }
        public void makeVertical() { 
            vertical = true;
            Point tmpSt = new Point((start.X + end.X) / 2, start.Y);
            Point tmpEn = new Point((start.X + end.X) / 2, end.Y);
            start = tmpSt;
            end = tmpEn;
        }
        public void makeHorizontal() { 
            horizontal = true;
            Point tmpSt = new Point(start.X, (start.Y + end.Y) / 2);
            Point tmpEn = new Point(end.X, (start.Y + end.Y) / 2);
            start = tmpSt;
            end = tmpEn;
        }
        public void noVertical() {
            vertical = false;
        }
        public void noHorizontal() { 
            horizontal=false;
        }
    }
    internal class Polygon
    {
        private List<(int,int)> H_positions; // indexes of points in List of points
        private List<(int, int)> V_positions;
        private List<Point> points;
        private List<Edge> edges;
        private bool finished = false;
        private int eps = 10;
        private int circleRadius = 8;
        public Polygon(Point p) { 
            points = new List<Point>() { };
            edges = new List<Edge>() { };
            H_positions = new List<(int, int)>() { };
            V_positions = new List<(int, int)>() { };
            points.Add(p);
        }

        public List<(int, int)> getListOfHPOsitions() {
            return H_positions;
        }
        public List<(int, int)> getListOfVPOsitions()
        {
            return V_positions;
        }
        public List<Point> getHpos() { 
            List<Point> res = new List<Point>() { };
            foreach (var (i1, i2) in H_positions)
            {
                res.Add(new Point((points[i1].X + points[i2].X) / 2, (points[i1].Y + points[i2].Y) / 2));
            }
            return res;
        }
        public List<Point> getVpos() {
            List<Point> res = new List<Point>() { };
            foreach (var (i1, i2) in V_positions)
            {
                res.Add(new Point((points[i1].X + points[i2].X) / 2, (points[i1].Y + points[i2].Y) / 2));
            }
            return res;
        }
        private void makeEdges() {
            edges.Clear();
            Edge e;
            HashSet<int> endingsOfH = new HashSet<int>() { };
            foreach (var item in H_positions)
            {
                endingsOfH.Add(item.Item2);
            }
            HashSet<int> endingsOfV = new HashSet<int>() { };
            foreach (var item in V_positions)
            {
                endingsOfV.Add(item.Item2);
            }
            for (int i = 1; i < points.Count(); i++)
            {
                e = new Edge(points[i - 1], points[i]);
                if (endingsOfH.Contains(i)) e.makeHorizontal();
                if (endingsOfV.Contains(i)) e.makeVertical();
                edges.Add(e);
            }
            e = new Edge(points[points.Count() - 1], points[0]);
            if (endingsOfH.Contains(0)) { 
                e.makeHorizontal();
                if (H_positions.Contains((points.Count() - 2, 0))) {
                    H_positions.Remove((points.Count() - 2, 0));
                    H_positions.Add((points.Count() - 1, 0));
                }
            }
            if (endingsOfV.Contains(0)) { e.makeVertical();
                if (V_positions.Contains((points.Count() - 2, 0)))
                {
                    V_positions.Remove((points.Count() - 2, 0));
                    V_positions.Add((points.Count() - 1, 0));
                }
            }
            edges.Add(e);
        }
        public void addPoint(Point p) {
            if (Math.Abs(points.First().X - p.X) <= eps && Math.Abs(points.First().Y - p.Y) <= eps)
            {
                finished = true;
                makeEdges();
            }
            else
            {
                points.Add(p);
            }
        }
        public List<Point> getPoints() { return points; }
        public void setPoints(Point[] ps) {
            points.Clear();
            foreach (Point point in ps)
            {
                points.Add(point);
            }
            makeEdges();
        }
        public bool isFinished() { return finished; }
        public Point prev() { return points.Last(); }
        public Point firstPoint() { return points.First(); }
        public bool IsPointInPolygon(Point point)
        {
            var polygon = points.ToArray();
            int numVertices = polygon.Length;
            bool inside = false;
            for (int i = 0, j = numVertices - 1; i < numVertices; j = i++)
            {
                if ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y) &&
                    (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    inside = !inside;
                }
            }
            return inside;
        }
        public bool isPointInPoints(Point point, out Point pOut) {
            foreach (Point p in points)
            {
                if (point.X <= p.X + circleRadius && point.X >= p.X - circleRadius &&
                    point.Y <= p.Y + circleRadius && point.Y >= p.Y - circleRadius) { 
                    pOut = p;
                    return true; }
            }
            pOut = new Point(0,0);
            return false;
        }
        public (bool, Point, Point) isPointOnEdges(Point point) {
            for (int i = 0; i < edges.Count(); i++)
            {
                if (IsPointInSegment(point, edges[i].getStart(), edges[i].getEnd())) return (true, edges[i].getStart(), edges[i].getEnd());
            }
            return (false, new Point(0, 0), new Point(0, 0));
        }
        public void resetNeighboursVHSettings(int indOfPoint, bool both=false) {
            if (indOfPoint != 0) {
                if (isThisEdgeHorizontal(new Edge(points[indOfPoint - 1], points[indOfPoint]))) {
                    makeNoHorizontal(new Edge(points[indOfPoint - 1], points[indOfPoint]));
                }
                if (isThisEdgeVertical(new Edge(points[indOfPoint - 1], points[indOfPoint])))
                {
                    makeNoVertical(new Edge(points[indOfPoint - 1], points[indOfPoint]));
                }
            } else{
                if (isThisEdgeHorizontal(new Edge(points[points.Count() - 1], points[indOfPoint])))
                {
                    makeNoHorizontal(new Edge(points[points.Count() - 1], points[indOfPoint]));
                }
                if (isThisEdgeVertical(new Edge(points[points.Count() - 1], points[indOfPoint])))
                {
                    makeNoVertical(new Edge(points[points.Count() - 1], points[indOfPoint]));
                }
            }
            if (both) {
                if (indOfPoint != points.Count() - 1)
                {
                    if (isThisEdgeHorizontal(new Edge(points[indOfPoint], points[indOfPoint+1])))
                    {
                        makeNoHorizontal(new Edge(points[indOfPoint], points[indOfPoint+1]));
                    }
                    if (isThisEdgeVertical(new Edge(points[indOfPoint], points[indOfPoint+1])))
                    {
                        makeNoVertical(new Edge(points[indOfPoint], points[indOfPoint+1]));
                    }
                }
                else {
                    if (isThisEdgeHorizontal(new Edge(points[indOfPoint], points[0])))
                    {
                        makeNoHorizontal(new Edge(points[indOfPoint], points[0]));
                    }
                    if (isThisEdgeVertical(new Edge(points[indOfPoint], points[0])))
                    {
                        makeNoVertical(new Edge(points[indOfPoint], points[0]));
                    }
                }
            }
        }
        private int getIndexOfEdge(Edge e) { 
            for (int i = 0; i < edges.Count(); i++)
            {
                if (edges[i].getStart() == e.getStart() && edges[i].getEnd() == e.getEnd()) return i;
            }
            return -1;
        }
        public Edge nextEgde(Edge e) {
            if (getIndexOfEdge(e) == -1) {
                Edge newEdge = new Edge(e.getEnd(), e.getStart());
                e = newEdge;
            }
            if (getIndexOfEdge(e) == edges.Count() - 1) return edges[0];
            return edges[getIndexOfEdge(e) + 1];
        }
        public Edge previousEdge(Edge e) {
            if (getIndexOfEdge(e) == -1)
            {
                Edge newEdge = new Edge(e.getEnd(), e.getStart());
                e = newEdge;
            }
            if (getIndexOfEdge(e) == 0) return edges[edges.Count() - 1];
            return edges[getIndexOfEdge(e) - 1];
        }
        public ((bool, int), (bool, int)) isNearVorH(int index) {
            bool isH=false, isV=false;
            int indH = -1, indV=-1;
            foreach (var p in V_positions)
            {
                if (p.Item1 == index)
                {
                    isV = true;
                    indV = p.Item2;
                    break;
                }
                if (p.Item2 == index) {
                    isV = true;
                    indV = p.Item1;
                    break;
                }
            }
            foreach (var p in H_positions) {
                if (p.Item1 == index)
                {
                    isH = true;
                    indH = p.Item2;
                    break;
                }
                if (p.Item2 == index) { 
                    isH =true;
                    indH=p.Item1;
                    break;
                }
            }
            return ((isV, indV), (isH, indH));
        }
        public bool couldMakeEdgeHorizontal(Edge e) {
            return !previousEdge(e).isHorizontal() && !nextEgde(e).isHorizontal();
        }
        public bool couldMakeEdgeVertical(Edge e) { 
            return !previousEdge(e).isVertical() && !nextEgde(e).isVertical();
        }
        public bool isThisEdgeHorizontal(Edge e) {
            if (getIndexOfEdge(e) == -1)
            {
                Edge newEdge = new Edge(e.getEnd(), e.getStart());
                e = newEdge;
            }
            int ind = getIndexOfEdge(e);
            return edges[ind].isHorizontal();
        }
        public bool isThisEdgeVertical(Edge e) {
            if (getIndexOfEdge(e) == -1)
            {
                Edge newEdge = new Edge(e.getEnd(), e.getStart());
                e = newEdge;
            }
            int ind = getIndexOfEdge(e);
            return edges[ind].isVertical();
        }
        public void makeEdgeHorizontal(Edge e) {
            if (getIndexOfEdge(e) == -1)
            {
                Edge newEdge = new Edge(e.getEnd(), e.getStart());
                e = newEdge;
            }
            int indOfSt = points.IndexOf(e.getStart());
            int indOfEn = points.IndexOf(e.getEnd());
            int indexOfEdge = getIndexOfEdge(e);
            int indexOfPreviousEdge = getIndexOfEdge(previousEdge(e));
            int indexOfNextEdge = getIndexOfEdge(nextEgde(e));
            e.makeHorizontal();
            edges[indexOfPreviousEdge].setEnd(e.getStart());
            edges[indexOfEdge] = new Edge(e.getStart(), e.getEnd());
            edges[indexOfEdge].makeHorizontal();
            edges[indexOfNextEdge].setStart(e.getEnd());
            points[indOfSt] = e.getStart();
            points[indOfEn] = e.getEnd();
            H_positions.Add((indOfSt, indOfEn));
        }
        public void makeEdgeVertical(Edge e) {
            if (getIndexOfEdge(e) == -1)
            {
                Edge newEdge = new Edge(e.getEnd(), e.getStart());
                e = newEdge;
            }
            int indOfSt = points.IndexOf(e.getStart());
            int indOfEn = points.IndexOf(e.getEnd());
            int indexOfEdge = getIndexOfEdge(e);
            int indexOfPreviousEdge = getIndexOfEdge(previousEdge(e));
            int indexOfNextEdge = getIndexOfEdge(nextEgde(e));
            e.makeVertical();
            edges[indexOfPreviousEdge].setEnd(e.getStart());
            edges[indexOfEdge] = new Edge(e.getStart(), e.getEnd());
            edges[indexOfEdge].makeVertical();
            edges[indexOfNextEdge].setStart(e.getEnd());
            points[indOfSt] = e.getStart();
            points[indOfEn] = e.getEnd();
            V_positions.Add((indOfSt, indOfEn));
        }
        public void makeNoVertical(Edge e)
        {
            if (getIndexOfEdge(e) == -1)
            {
                Edge newEdge = new Edge(e.getEnd(), e.getStart());
                e = newEdge;
            }
            int indOfSt = points.IndexOf(e.getStart());
            int indOfEn = points.IndexOf(e.getEnd());
            int indexOfEdge = getIndexOfEdge(e);
            edges[indexOfEdge].noVertical();
            V_positions.Remove((indOfSt, indOfEn));
        }
        public void makeNoHorizontal(Edge e) {
            if (getIndexOfEdge(e) == -1)
            {
                Edge newEdge = new Edge(e.getEnd(), e.getStart());
                e = newEdge;
            }
            int indOfSt = points.IndexOf(e.getStart());
            int indOfEn = points.IndexOf(e.getEnd());
            int indexOfEdge = getIndexOfEdge(e);
            edges[indexOfEdge].noHorizontal();
            H_positions.Remove((indOfSt, indOfEn));
        }
        private bool IsPointInSegment(Point point, Point segmentStart, Point segmentEnd)
        {
            double dx = segmentEnd.X - segmentStart.X;
            double dy = segmentEnd.Y - segmentStart.Y;
            double length = Math.Sqrt(dx * dx + dy * dy);
            double a = (point.X - segmentStart.X) * (segmentEnd.X - segmentStart.X) + (point.Y - segmentStart.Y) * (segmentEnd.Y - segmentStart.Y);
            a /= length * length;
            double nearestX = segmentStart.X + a * (segmentEnd.X - segmentStart.X);
            double nearestY = segmentStart.Y + a * (segmentEnd.Y - segmentStart.Y);
            double distance = Math.Sqrt((point.X - nearestX) * (point.X - nearestX) + (point.Y - nearestY) * (point.Y - nearestY));
            return distance <= eps;
        }
    }
}
