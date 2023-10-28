using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;
using System.Text;

namespace GKProject1
{
    public partial class Form1 : Form
    {
        private Bitmap canvas;
        private int circleRadius = 5;
        private List<Polygon> polygons = new List<Polygon>() {};
        private Polygon? actualPolygon;
        Pen pen = new Pen(Color.Gray, 1);
        Pen testPen = new Pen(Color.Black, 1);
        Brush brush = new SolidBrush(Color.DarkBlue);
        Brush letterBrushH = new SolidBrush(Color.Red);
        Brush letterBrushV = new SolidBrush(Color.DarkGreen);
        Font drawFont = new Font("Arial", 7);
        Point lastLeftMouseClick;
        Graphics g;
        Point pOut;
        private bool isDragging = false;
        private bool isEdgeDragging = false;
        private Point lastMousePosition;
        Polygon selectedPolygon;
        Point startPointToAddVertex, endPointToAddVertex;
        Point startPointToDragEdge, endPointToDragEdge;
        int indSt = -1, indEn = -1;
        int indOfPOintToMove = -1;
        int offsetValue=3;
        public Form1()
        {
            InitializeComponent();
            createPredifinedPolygons();
            InitializeCanvas();
        }
        private void InitializeCanvas()
        {
            canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = canvas;
            g = Graphics.FromImage(canvas);

           
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
        }
        private void createPredifinedPolygons() {
            if (polygons.Count()!=0) return;
            Polygon p1 = new Polygon(new Point(100, 100));
            p1.addPoint(new Point(100, 200));
            p1.addPoint(new Point(200, 100));
            p1.addPoint(new Point(101, 101));
            p1.makeEdgeVertical(new Edge(new Point(100, 100), new Point(100, 200)));
            polygons.Add(p1);

            Polygon p2 = new Polygon(new Point(300, 250));
            p2.addPoint(new Point(300, 350));
            p2.addPoint(new Point(400, 250));
            p2.addPoint(new Point(301, 251));
            p2.makeEdgeHorizontal(new Edge(new Point(400, 250), new Point(300, 250)));

            polygons.Add(p2);
        }
        private void UncheckBresenhams() {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            UncheckBresenhams();
            if (e.Button == MouseButtons.Left)
            {
                lastMousePosition = e.Location;
                foreach (Polygon polygon in polygons)
                {
                    if (polygon.IsPointInPolygon(lastMousePosition))
                    {
                        selectedPolygon = polygon;
                        if (selectedPolygon.isPointInPoints(e.Location, out Point pp)) { 
                            isDragging = true;
                            break;
                        }
                        (bool isOnEdge, Point st, Point en) = selectedPolygon.isPointOnEdges(e.Location);
                        if (isOnEdge)
                        {
                            startPointToDragEdge = st;
                            endPointToDragEdge = en;
                            isEdgeDragging = true;
                        }
                        else {
                            isDragging = true;
                        }
                        break;
                    }
                }

                if (!isDragging && !isEdgeDragging)
                {
                    if (actualPolygon == null)
                    {
                        Polygon p = new Polygon(new Point(e.X, e.Y));
                        actualPolygon = p;
                    }
                    else
                    {
                        actualPolygon.addPoint(new Point(e.X, e.Y));
                        if (actualPolygon.isFinished())
                        {
                            polygons.Add(actualPolygon);
                            actualPolygon = null;
                        }
                    }
                    drawActualPolygon();
                    drawPolygons();
                    pictureBox.Refresh();

                }
            }
            if (e.Button == MouseButtons.Right){
                lastMousePosition = e.Location;

                foreach (Polygon polygon in polygons)
                {
                    if (polygon.IsPointInPolygon(lastMousePosition))
                    {
                        selectedPolygon = polygon;
                        (bool onEdge, Point st, Point en) = selectedPolygon.isPointOnEdges(lastMousePosition);
                        if (selectedPolygon.isPointInPoints(new Point(lastMousePosition.X, lastMousePosition.Y), out pOut))
                        {
                            contextMenuVertexDeletion.Show(this, e.Location);
                            lastLeftMouseClick = e.Location;
                        }
                        else if (onEdge) {
                            contextMenuAddVertex.Show(this, e.Location);
                            lastLeftMouseClick = e.Location;
                            startPointToAddVertex = st;
                            endPointToAddVertex = en;
                        } else
                        {
                            contextMenuDeletion.Show(this, e.Location);
                            lastLeftMouseClick = e.Location;
                        }
                        break;
                    }
                }

                drawPolygons();
                pictureBox.Refresh();
            }
        }
        private void drawActualPolygon() {
            if (actualPolygon == null) return;
            var points = actualPolygon.getPoints().ToArray();
            g.FillEllipse(brush, points[0].X - circleRadius, points[0].Y - circleRadius, 2 * circleRadius, 2 * circleRadius);
            for (int i = 1; i < points.Count(); i++)
            {
                g.FillEllipse(brush, points[i].X - circleRadius, points[i].Y - circleRadius, 2 * circleRadius, 2 * circleRadius);
                g.DrawLine(pen, points[i - 1], points[i]);
            }
        }
        private void drawOffsets()
        {
            Pen Pinkpen = new Pen(Color.LightPink, 2 * offsetValue);
            Brush Pinkbrush = new SolidBrush(Color.LightPink);
            foreach (Polygon polygon in polygons)
            {
                var points = polygon.getPoints().ToArray();
                g.FillEllipse(Pinkbrush, points[0].X - offsetValue, points[0].Y - offsetValue, 2 * offsetValue, 2 * offsetValue);
                for (int i = 1; i < points.Count(); i++)
                {
                    g.FillEllipse(Pinkbrush, points[i].X - offsetValue, points[i].Y - offsetValue, 2 * offsetValue, 2 * offsetValue);
                    g.DrawLine(Pinkpen, points[i - 1], points[i]);
                }
                g.DrawLine(Pinkpen, points[0], points[points.Count() - 1]);
            }

            foreach (Polygon polygon in polygons)
            {
                var points = polygon.getPoints().ToArray();
                g.FillPolygon(Brushes.White, points);
            }
        }
        private void drawPolygons() {
            drawOffsets();
            foreach (Polygon polygon in polygons)
            {
                var points = polygon.getPoints().ToArray();
                g.FillEllipse(brush, points[0].X - circleRadius, points[0].Y - circleRadius, 2 * circleRadius, 2 * circleRadius);
                for (int i = 1; i < points.Count(); i++)
                {
                    g.FillEllipse(brush, points[i].X - circleRadius, points[i].Y - circleRadius, 2 * circleRadius, 2 * circleRadius);
                    g.DrawLine(pen, points[i-1], points[i]);
                }
                g.DrawLine(pen, points[0], points[points.Count()-1]);
                var H_pos = polygon.getHpos();
                foreach (Point position in H_pos) {
                    g.DrawString("H", drawFont, letterBrushH, position);
                }
                var V_pos = polygon.getVpos();
                foreach (Point position in V_pos) {
                    g.DrawString("V", drawFont, letterBrushV, position);
                }
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Polygon> polygonsToRemove = new List<Polygon>() { };
            foreach (Polygon polygon in polygons)
            {
                if (polygon.IsPointInPolygon(lastLeftMouseClick)) {
                    polygonsToRemove.Add(polygon);
                    break;
                }
            }

            foreach (Polygon polygon in polygonsToRemove)
            {
                polygons.Remove(polygon);
            }
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentMousePoint = e.Location;
            if (isDragging && selectedPolygon != null)
            {
                if (selectedPolygon.isPointInPoints(new Point(lastMousePosition.X, lastMousePosition.Y), out pOut))
                {
                    int deltaX = e.X - lastMousePosition.X;
                    int deltaY = e.Y - lastMousePosition.Y;
                    List<Point> points = selectedPolygon.getPoints();
                    if (indOfPOintToMove == -1) {

                        indOfPOintToMove = points.IndexOf(pOut);

                    }
                    ((bool isNearV, int indv), (bool isNearH, int indH)) = selectedPolygon.isNearVorH(indOfPOintToMove);
                    if (!isNearV && !isNearH)
                    {
                        points[indOfPOintToMove] = new Point(points[indOfPOintToMove].X + deltaX, points[indOfPOintToMove].Y + deltaY);
                    }
                    else if (isNearH && !isNearV)
                    {

                        ((bool isNearV2, int indv2), (bool isNearH2, int indH2)) = selectedPolygon.isNearVorH(indH);
                        if (isNearV2)
                        {
                            points[indOfPOintToMove] = new Point(points[indOfPOintToMove].X + deltaX, points[indOfPOintToMove].Y + deltaY);
                            points[indH] = new Point(points[indH].X, points[indH].Y + deltaY);
                        }
                        else
                        {
                            points[indOfPOintToMove] = new Point(points[indOfPOintToMove].X + deltaX, points[indOfPOintToMove].Y + deltaY);
                            points[indH] = new Point(points[indH].X + deltaX, points[indH].Y + deltaY);
                        }
                    }
                    else if (isNearV && !isNearH)
                    {
                        ((bool isNearV2, int indv2), (bool isNearH2, int indH2)) = selectedPolygon.isNearVorH(indv);
                        if (isNearV2)
                        {
                            points[indOfPOintToMove] = new Point(points[indOfPOintToMove].X + deltaX, points[indOfPOintToMove].Y + deltaY);
                            points[indv] = new Point(points[indv].X + deltaX, points[indv].Y);
                        }
                        else
                        {
                            points[indOfPOintToMove] = new Point(points[indOfPOintToMove].X + deltaX, points[indOfPOintToMove].Y + deltaY);
                            points[indv] = new Point(points[indv].X + deltaX, points[indv].Y + deltaY);
                        }
                    }
                    else {
                        points[indOfPOintToMove] = new Point(points[indOfPOintToMove].X + deltaX, points[indOfPOintToMove].Y + deltaY);
                        points[indH] = new Point(points[indH].X, points[indH].Y+deltaY);
                        points[indv] = new Point(points[indv].X + deltaX, points[indv].Y);
                    }
                    selectedPolygon.setPoints(points.ToArray());
                    lastMousePosition = e.Location;
                    g.Clear(Color.White);
                    drawPolygons();
                    pictureBox.Refresh();
                }
                else
                {
                    int deltaX = e.X - lastMousePosition.X;
                    int deltaY = e.Y - lastMousePosition.Y;
                    Point[] points = selectedPolygon.getPoints().ToArray();
                    for (int i = 0; i < points.Length; i++)
                    {
                        points[i] = new Point(points[i].X + deltaX, points[i].Y + deltaY);
                    }
                    selectedPolygon.setPoints(points);
                    lastMousePosition = e.Location;
                    g.Clear(Color.White);
                    drawPolygons();
                    pictureBox.Refresh();
                }
            }

            if (isEdgeDragging && selectedPolygon != null)
            {
                int deltaX = e.X - lastMousePosition.X;
                int deltaY = e.Y - lastMousePosition.Y;
                List<Point> points = selectedPolygon.getPoints();
                if (indSt == -1 && indEn == -1)
                {
                    indSt = points.IndexOf(startPointToDragEdge);
                    indEn = points.IndexOf(endPointToDragEdge);
                }
                ((bool isStartNearV, int indv), (bool isStartNearH, int indH)) = selectedPolygon.isNearVorH(indSt);
                ((bool isEndNearV, int indve), (bool isEndNearH, int indHe)) = selectedPolygon.isNearVorH(indEn);

                if (isStartNearH && isStartNearV)
                {
                    if (isEndNearH)
                    {
                        points[indSt] = new Point(points[indSt].X, points[indSt].Y + deltaY);
                        points[indEn] = new Point(points[indEn].X, points[indEn].Y + deltaY);
                    }
                    else if (isEndNearV) {
                        points[indSt] = new Point(points[indSt].X + deltaX, points[indSt].Y);
                        points[indEn] = new Point(points[indEn].X + deltaX, points[indEn].Y);
                    }
                } else if (isEndNearH && isEndNearV) {
                    if (isStartNearV) {
                        points[indSt] = new Point(points[indSt].X + deltaX, points[indSt].Y);
                        points[indEn] = new Point(points[indEn].X + deltaX, points[indEn].Y);
                    } else if (isStartNearH) {

                        points[indSt] = new Point(points[indSt].X, points[indSt].Y + deltaY);
                        points[indEn] = new Point(points[indEn].X, points[indEn].Y + deltaY);
                    }
                }
                else
                {
                    if (isStartNearH)
                    {
                        if (isEndNearH)
                        {
                            
                            ((bool isNearV2, int indv2), (bool isNearH2, int indH2)) = selectedPolygon.isNearVorH(indH);
                            
                            if (indHe == indH2)
                            {
                                points[indSt] = new Point(points[indSt].X + deltaX, points[indSt].Y + deltaY);
                                points[indEn] = new Point(points[indEn].X + deltaX, points[indEn].Y + deltaY);
                            }
                            else {
                                points[indSt] = new Point(points[indSt].X + deltaX, points[indSt].Y);
                                points[indEn] = new Point(points[indEn].X + deltaX, points[indEn].Y);
                            }

                        }
                        else if (isEndNearV)
                        {
                        }
                        else
                        {
                            points[indSt] = new Point(points[indSt].X + deltaX, points[indSt].Y);
                            points[indEn] = new Point(points[indEn].X + deltaX, points[indEn].Y + deltaY);
                        }
                    }
                    else if (isStartNearV)
                    {
                        if (isEndNearV)
                        {
                            ((bool isNearV2, int indv2), (bool isNearH2, int indH2)) = selectedPolygon.isNearVorH(indv);
                            if (indve == indv2)
                            {
                                points[indSt] = new Point(points[indSt].X + deltaX, points[indSt].Y + deltaY);
                                points[indEn] = new Point(points[indEn].X + deltaX, points[indEn].Y + deltaY);
                            }
                            else {

                                points[indSt] = new Point(points[indSt].X, points[indSt].Y + deltaY);
                                points[indEn] = new Point(points[indEn].X, points[indEn].Y + deltaY);
                            }

                        }
                        else if (isEndNearH)
                        {
                        }
                        else
                        {
                            points[indSt] = new Point(points[indSt].X, points[indSt].Y + deltaY);
                            points[indEn] = new Point(points[indEn].X + deltaX, points[indEn].Y + deltaY);
                        }
                    }
                    else
                    {
                        if (isEndNearH)
                        {
                            points[indSt] = new Point(points[indSt].X + deltaX, points[indSt].Y + deltaY);
                            points[indEn] = new Point(points[indEn].X + deltaX, points[indEn].Y);
                        }
                        else if (isEndNearV)
                        {
                            points[indSt] = new Point(points[indSt].X + deltaX, points[indSt].Y + deltaY);
                            points[indEn] = new Point(points[indEn].X, points[indEn].Y + deltaY);
                        }
                        else
                        {
                            points[indSt] = new Point(points[indSt].X + deltaX, points[indSt].Y + deltaY);
                            points[indEn] = new Point(points[indEn].X + deltaX, points[indEn].Y + deltaY);
                        }
                    }
                }
                startPointToDragEdge.X += deltaX;
                startPointToDragEdge.Y += deltaY;
                endPointToDragEdge.X += deltaX;
                endPointToDragEdge.Y += deltaY;
                selectedPolygon.setPoints(points.ToArray());
                lastMousePosition = e.Location;
                g.Clear(Color.White);
                drawPolygons();
                pictureBox.Refresh();
            }

            if (actualPolygon != null) {
                g.Clear(Color.White);
                drawPolygons();
                drawActualPolygon();
                g.DrawLine(testPen, lastMousePosition, currentMousePoint);
                pictureBox.Refresh();
            }
        }
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            isEdgeDragging = false;
            selectedPolygon = null;
            indSt = indEn = -1;
            indOfPOintToMove = -1;
        }
        private void krawędźPoziomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Polygon polygon in polygons)
            {
                if (polygon.IsPointInPolygon(lastMousePosition))
                {
                    selectedPolygon = polygon;
                    break;
                }
            }
            (bool isonedge, Point st, Point en) = selectedPolygon.isPointOnEdges(lastLeftMouseClick);
            bool couldBeHorizontal = selectedPolygon.couldMakeEdgeHorizontal(new Edge(st,en));
            if (couldBeHorizontal) selectedPolygon.makeEdgeHorizontal(new Edge(st, en));
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();

        }
        private void cofnijPoziomośćToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Polygon polygon in polygons)
            {
                if (polygon.IsPointInPolygon(lastMousePosition))
                {
                    selectedPolygon = polygon;
                    break;
                }
            }
            (bool isonedge, Point st, Point en) = selectedPolygon.isPointOnEdges(lastLeftMouseClick);
            bool isHoriz = selectedPolygon.isThisEdgeHorizontal(new Edge(st, en));
            if (isHoriz) selectedPolygon.makeNoHorizontal(new Edge(st, en));
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
        }
        private void krawędźPionowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Polygon polygon in polygons)
            {
                if (polygon.IsPointInPolygon(lastMousePosition))
                {
                    selectedPolygon = polygon;
                    break;
                }
            }
            (bool isonedge, Point st, Point en) = selectedPolygon.isPointOnEdges(lastLeftMouseClick);
            bool couldBeVertical = selectedPolygon.couldMakeEdgeVertical(new Edge(st, en));
            if (couldBeVertical) selectedPolygon.makeEdgeVertical(new Edge(st, en));
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
        }
        private void cofnijPionowośćToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Polygon polygon in polygons)
            {
                if (polygon.IsPointInPolygon(lastMousePosition))
                {
                    selectedPolygon = polygon;
                    break;
                }
            }
            (bool isonedge, Point st, Point en) = selectedPolygon.isPointOnEdges(lastLeftMouseClick);
            bool isVert = selectedPolygon.isThisEdgeVertical(new Edge(st, en));
            if (isVert) selectedPolygon.makeNoVertical(new Edge(st, en));
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
        }
        private void deleteVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Polygon polygon in polygons)
            {
                if (polygon.IsPointInPolygon(lastMousePosition))
                {
                    selectedPolygon = polygon;
                    break;
                }
            }
            List<Point> points = selectedPolygon.getPoints();
            int index = points.IndexOf(pOut);
            selectedPolygon.resetNeighboursVHSettings(index, true);
            points.Remove(pOut);
            selectedPolygon.setPoints(points.ToArray());
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
        }
        private void addVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Polygon polygon in polygons)
            {
                if (polygon.isPointInPoints(endPointToAddVertex, out pOut))
                {
                    selectedPolygon = polygon;
                    break;
                }
            }
            var points = selectedPolygon.getPoints();
            int index = points.IndexOf(pOut);
            selectedPolygon.resetNeighboursVHSettings(index);
            if (index == 0)
            {
                points.Add(new Point((points[0].X + points[points.Count() - 1].X) / 2, (points[0].Y + points[points.Count() - 1].Y) / 2));
            }
            else
            {
                points.Insert(index, new Point((startPointToAddVertex.X + endPointToAddVertex.X) / 2, (startPointToAddVertex.Y + endPointToAddVertex.Y) / 2));
            }
            selectedPolygon.setPoints(points.ToArray());
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
        }
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            //string inputText = textBox1.Text;
            //int number;
            //if (int.TryParse(inputText, out number))
            //{
            //    if (number < 0) { MessageBox.Show("Offset must be positive"); } else {
            //        offsetValue = number;
            //        InitializeCanvas();
            //        g.Clear(Color.White);
            //        drawPolygons();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Invalid input. Please enter a valid number.");
            //}
        }
        private void BresenhamsLine(int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = (x0 < x1) ? 1 : -1;
            int sy = (y0 < y1) ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                canvas.SetPixel(x0,y0, Color.Gray);
                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            drawOffsets();
            foreach (Polygon polygon in polygons)
            {
                var points = polygon.getPoints().ToArray();
                g.FillEllipse(brush, points[0].X - circleRadius, points[0].Y - circleRadius, 2 * circleRadius, 2 * circleRadius);
                for (int i = 1; i < points.Count(); i++)
                {
                    g.FillEllipse(brush, points[i].X - circleRadius, points[i].Y - circleRadius, 2 * circleRadius, 2 * circleRadius);
                    BresenhamsLine(points[i - 1].X, points[i - 1].Y, points[i].X, points[i].Y);

                }
                BresenhamsLine(points[0].X, points[0].Y, points[points.Count() - 1].X, points[points.Count() - 1].Y);
                var H_pos = polygon.getHpos();
                foreach (Point position in H_pos)
                {
                    g.DrawString("H", drawFont, letterBrushH, position);
                }
                var V_pos = polygon.getVpos();
                foreach (Point position in V_pos)
                {
                    g.DrawString("V", drawFont, letterBrushV, position);
                }
            }
            pictureBox.Refresh();
        }
        private int IPart(float x)
        {
            return (int)x;
        }

        private int Round(float x)
        {
            return (int)(x + 0.5f);
        }

        private float FracPart(float x)
        {
            return x - (int)x;
        }

        private int RFracPart(float x)
        {
            return Round(255 * FracPart(x));
        }

        private void DrawPixel(int x, int y, float brightness)
        {
            if (x >= 0 && x < this.Width && y >= 0 && y < this.Height)
            {
                Color color = Color.FromArgb((int)(brightness), 0, 0, 0);
                
                SolidBrush brush = new SolidBrush(color);
                g.FillRectangle(brush, x, y, 1, 1);
            }
        }

        private void WULine(int x0, int y0, int x1, int y1) {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                int temp = x0;
                x0 = y0;
                y0 = temp;
                temp = x1;
                x1 = y1;
                y1 = temp;
            }
            if (x0 > x1)
            {
                int temp = x0;
                x0 = x1;
                x1 = temp;
                temp = y0;
                y0 = y1;
                y1 = temp;
            }
            int dx = x1 - x0;
            int dy = y1 - y0;
            float gradient = (float)dy / dx;

            int xEnd = Round(x0);
            float yEnd = y0 + gradient * (xEnd - x0);
            int xGap = RFracPart(x0 + 0.5f);
            int xPixel1 = xEnd;

            int yPixel1 = IPart(yEnd);
            if (steep)
            {
                DrawPixel(yPixel1, xPixel1, RFracPart(yEnd) * xGap);
                DrawPixel(yPixel1 + 1, xPixel1, FracPart(yEnd) * xGap);
            }
            else
            {
                DrawPixel(xPixel1, yPixel1, RFracPart(yEnd) * xGap);
                DrawPixel(xPixel1, yPixel1 + 1, FracPart(yEnd) * xGap);
            }
            float intery = yEnd + gradient;
            xEnd = Round(x1);
            yEnd = y1 + gradient * (xEnd - x1);
            xGap = RFracPart(x1 + 0.5f);
            int xPixel2 = xEnd;
            int yPixel2 = IPart(yEnd);
            if (steep)
            {
                DrawPixel(yPixel2, xPixel2, RFracPart(yEnd) * xGap);
                DrawPixel(yPixel2 + 1, xPixel2, FracPart(yEnd) * xGap);
            }
            else
            {
                DrawPixel(xPixel2, yPixel2, RFracPart(yEnd) * xGap);
                DrawPixel(xPixel2, yPixel2 + 1, FracPart(yEnd) * xGap);
            }
            for (int x = xPixel1 + 1; x <= xPixel2 - 1; x++)
            {
                if (steep)
                {
                    DrawPixel(IPart(intery), x, RFracPart(intery));
                    DrawPixel(IPart(intery) + 1, x, FracPart(intery));
                }
                else
                {
                    DrawPixel(x, IPart(intery), RFracPart(intery));
                    DrawPixel(x, IPart(intery) + 1, FracPart(intery));
                }
                intery += gradient;
            }
        }
        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WriteFile.txt");
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                int index = 1;
                foreach (Polygon p in polygons)
                {
                    StringBuilder polygonSb = new StringBuilder("\nPolygon ");
                    polygonSb.Append(index);
                    polygonSb.Append(":\n");
                    var points = p.getPoints();
                    StringBuilder tmpSb = new StringBuilder("Verteces: ");
                    for (int i = 0; i < points.Count(); ++i)
                    {
                        tmpSb.Append("[");
                        tmpSb.Append(i);
                        tmpSb.Append("] = (");
                        tmpSb.Append(points[i].X);
                        tmpSb.Append(",");
                        tmpSb.Append(points[i].Y);
                        tmpSb.Append("), ");
                    }
                    tmpSb.Append("\n");
                    polygonSb.Append(tmpSb.ToString());

                    tmpSb.Clear();
                    var HPos = p.getListOfHPOsitions();
                    tmpSb.Append("Edges between those indexes of verteces are horizontal: ");
                    foreach (var item in HPos)
                    {
                        tmpSb.Append("(");
                        tmpSb.Append(item.Item1);
                        tmpSb.Append(",");
                        tmpSb.Append(item.Item2);
                        tmpSb.Append(")");
                    }
                    tmpSb.Append("\n");
                    polygonSb.Append(tmpSb.ToString());

                    tmpSb.Clear();
                    var VPos = p.getListOfVPOsitions();
                    tmpSb.Append("Edges between those indexes of verteces are vertical: ");
                    foreach (var item in VPos)
                    {
                        tmpSb.Append("(");
                        tmpSb.Append(item.Item1);
                        tmpSb.Append(",");
                        tmpSb.Append(item.Item2);
                        tmpSb.Append(")");
                    }
                    tmpSb.Append("\n");
                    polygonSb.Append(tmpSb.ToString());

                    ++index;
                outputFile.Write(polygonSb.ToString());
                }
                outputFile.Close();
            }
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            drawOffsets();
            foreach (Polygon polygon in polygons)
            {
                var points = polygon.getPoints().ToArray();
                g.FillEllipse(brush, points[0].X - circleRadius, points[0].Y - circleRadius, 2 * circleRadius, 2 * circleRadius);
                for (int i = 1; i < points.Count(); i++)
                {
                    g.FillEllipse(brush, points[i].X - circleRadius, points[i].Y - circleRadius, 2 * circleRadius, 2 * circleRadius);
                    if (points[i - 1].X == points[i].X || points[i - 1].Y == points[i].Y) {
                        g.DrawLine(pen, points[i - 1], points[i]);
                    } else {
                        WULine(points[i - 1].X, points[i - 1].Y, points[i].X, points[i].Y);
                    }

                }
                if (points[0].X == points[points.Count()-1].X || points[0].Y == points[points.Count()-1].Y)
                {
                    g.DrawLine(pen, points[0], points[points.Count()-1]);
                }
                else
                {
                    WULine(points[0].X, points[0].Y, points[points.Count() - 1].X, points[points.Count() - 1].Y);
                }
                var H_pos = polygon.getHpos();
                foreach (Point position in H_pos)
                {
                    g.DrawString("H", drawFont, letterBrushH, position);
                }
                var V_pos = polygon.getVpos();
                foreach (Point position in V_pos)
                {
                    g.DrawString("V", drawFont, letterBrushV, position);
                }
            }
            pictureBox.Refresh();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = canvas;
            g = Graphics.FromImage(canvas);
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            drawPolygons();
            pictureBox.Refresh();
            //UncheckBresenhams();
        }

        private void trackBar1_ValueChanged_1(object sender, EventArgs e)
        {
            offsetValue = trackBar1.Value;
            InitializeCanvas();
            g.Clear(Color.White);
            drawPolygons();

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
        }
        private void canvas_Click(object sender, EventArgs e)
        {
        }
        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}

