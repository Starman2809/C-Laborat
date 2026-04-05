using System.Drawing.Drawing2D;

namespace Lab17
{
    public partial class Form1 : Form
    {
        private const float Units = 70f;
        private PointF origin;

        private Color region1Color = Color.FromArgb(180, 140, 150, 220);
        private Color region2Color = Color.FromArgb(200, 50, 50, 50);

        private int hoveredRegion;

        private readonly List<DraggableLabel> labels = new();
        private DraggableLabel? draggedLabel;
        private PointF dragOffset;

        private readonly System.Windows.Forms.Timer animTimer = new();
        private bool animating;
        private float animPhase;

        private Button btnAnim = null!;
        private readonly ColorDialog colorDlg = new();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ClientSize = new Size(850, 700);
            Text = "Лабораторная 17 – GDI+ (Вариант 3)";
            BackColor = Color.White;
            origin = new PointF(280, 440);

            CreateUI();
            CreateLabels();

            animTimer.Interval = 40;
            animTimer.Tick += (_, _) => { animPhase += 0.08f; Invalidate(); };

            Paint += OnPaint;
            MouseMove += OnMouseMove;
            MouseDown += OnMouseDown;
            MouseUp += OnMouseUp;
        }

        private PointF F(float mx, float my) =>
            new(origin.X + mx * Units, origin.Y - my * Units);

        private RectangleF EllipseRect(float cx, float cy, float r)
        {
            var c = F(cx, cy);
            float pr = r * Units;
            return new(c.X - pr, c.Y - pr, 2 * pr, 2 * pr);
        }

        private RectangleF MathRect(float x1, float y1, float x2, float y2)
        {
            var tl = F(x1, y2);
            var br = F(x2, y1);
            return new(tl.X, tl.Y, br.X - tl.X, br.Y - tl.Y);
        }

        private GraphicsPath CirclePath(float cx, float cy, float r)
        {
            var p = new GraphicsPath();
            p.AddEllipse(EllipseRect(cx, cy, r));
            return p;
        }

        #region UI Controls

        private void CreateUI()
        {
            MakeBtn("Цвет области 1", 12, () =>
            {
                colorDlg.Color = region1Color;
                if (colorDlg.ShowDialog() == DialogResult.OK)
                {
                    region1Color = Color.FromArgb(160, colorDlg.Color);
                    Invalidate();
                }
            });

            MakeBtn("Цвет области 2", 155, () =>
            {
                colorDlg.Color = region2Color;
                if (colorDlg.ShowDialog() == DialogResult.OK)
                {
                    region2Color = Color.FromArgb(160, colorDlg.Color);
                    Invalidate();
                }
            });

            btnAnim = MakeBtn("▶ Анимация", 298, () =>
            {
                animating = !animating;
                if (animating)
                {
                    animTimer.Start();
                    btnAnim.Text = "⏹ Стоп";
                }
                else
                {
                    animTimer.Stop();
                    animPhase = 0;
                    btnAnim.Text = "▶ Анимация";
                    Invalidate();
                }
            });
        }

        private Button MakeBtn(string text, int x, Action click)
        {
            var b = new Button
            {
                Text = text,
                Location = new Point(x, 8),
                Size = new Size(133, 32),
                FlatStyle = FlatStyle.System
            };
            b.Click += (_, _) => click();
            Controls.Add(b);
            return b;
        }

        #endregion

        #region Labels

        private void CreateLabels()
        {
            labels.AddRange(new DraggableLabel[]
            {
                new("x",  F(4.5f,  -0.35f)),
                new("y",  F(-0.3f,  3.6f)),
                new("0",  F(-0.4f, -0.4f)),
                new("2",  F(1.85f, -0.4f)),
                new("4",  F(3.85f, -0.4f)),
                new("-2", F(-2.25f, -0.4f)),
                new("3",  F(-0.5f,  3.1f)),
                new("2",  F(-0.5f,  2.1f)),
                new("-2", F(-0.6f, -1.9f)),
            });
        }

        #endregion

        #region Drawing

        private void OnPaint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            using var c1 = CirclePath(0, 0, 2);
            using var c2 = CirclePath(2, 0, 2);
            var rc = MathRect(0, 0, 4, 3);
            var belowAxis = MathRect(-5, -5, 5, 0);

            // Region 1: intersection of circles, only below x-axis
            using var r1 = new Region(c1);
            r1.Intersect(c2);
            using var belowPath = new GraphicsPath();
            belowPath.AddRectangle(belowAxis);
            r1.Intersect(belowPath);

            // Region 2: left half of rectangle (x: 0..2, y: 0..3) above both circles
            using var rp = new GraphicsPath();
            rp.AddRectangle(MathRect(0, 0, 2, 3));
            using var r2 = new Region(rp);
            r2.Exclude(c1);
            r2.Exclude(c2);

            using var b1 = new SolidBrush(FillColor(1, region1Color));
            using var b2 = new SolidBrush(FillColor(2, region2Color));
            g.FillRegion(b2, r2);
            g.FillRegion(b1, r1);

            using var pen = new Pen(Color.Black, 2f);
            g.DrawEllipse(pen, EllipseRect(0, 0, 2));
            g.DrawEllipse(pen, EllipseRect(2, 0, 2));
            g.DrawRectangle(pen, rc.X, rc.Y, rc.Width, rc.Height);
            g.DrawLine(pen, F(2, -2.8f), F(2, 3));

            DrawAxes(g);
            DrawLabels(g);
        }

        private Color FillColor(int id, Color c)
        {
            if (hoveredRegion != id)
                return c;
            if (!animating)
                return Brighten(c, 70);
            float t = 0.5f + 0.5f * MathF.Sin(animPhase * 3f);
            return Color.FromArgb(c.A,
                Clamp8(c.R + (int)(90 * t)),
                Clamp8(c.G + (int)(90 * t)),
                Clamp8(c.B + (int)(90 * t)));
        }

        private static Color Brighten(Color c, int d) =>
            Color.FromArgb(c.A, Clamp8(c.R + d), Clamp8(c.G + d), Clamp8(c.B + d));

        private static int Clamp8(int v) => Math.Clamp(v, 0, 255);

        private void DrawAxes(Graphics g)
        {
            using var arrowPen = new Pen(Color.Black, 1.5f)
            {
                CustomEndCap = new AdjustableArrowCap(5, 5)
            };
            g.DrawLine(arrowPen, F(-2.8f, 0), F(4.8f, 0));
            g.DrawLine(arrowPen, F(0, -2.8f), F(0, 3.8f));

            using var tick = new Pen(Color.Black, 1f);
            float d = 4;
            foreach (float x in new[] { -2f, 2f, 4f })
            {
                var p = F(x, 0);
                g.DrawLine(tick, p.X, p.Y - d, p.X, p.Y + d);
            }
            foreach (float y in new[] { -2f, 2f, 3f })
            {
                var p = F(0, y);
                g.DrawLine(tick, p.X - d, p.Y, p.X + d, p.Y);
            }
        }

        private void DrawLabels(Graphics g)
        {
            float sz = 11f;
            if (animating)
                sz *= 1f + 0.3f * MathF.Sin(animPhase);

            using var font = new Font("Segoe UI", Math.Max(6, sz), FontStyle.Bold);

            Color textColor;
            if (animating)
            {
                int r = Clamp8((int)(40 + 80 * (0.5 + 0.5 * MathF.Sin(animPhase * 1.4f))));
                int gr = Clamp8((int)(40 + 50 * (0.5 + 0.5 * MathF.Cos(animPhase * 1.1f))));
                int b = Clamp8((int)(80 + 60 * (0.5 + 0.5 * MathF.Sin(animPhase * 0.8f))));
                textColor = Color.FromArgb(255, r, gr, b);
            }
            else
            {
                textColor = Color.Black;
            }

            using var brush = new SolidBrush(textColor);

            foreach (var lbl in labels)
            {
                var s = g.MeasureString(lbl.Text, font);
                lbl.Bounds = new RectangleF(lbl.Position.X, lbl.Position.Y, s.Width, s.Height);
                g.DrawString(lbl.Text, font, brush, lbl.Position);
            }
        }

        #endregion

        #region Mouse interaction

        private void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (draggedLabel != null)
            {
                draggedLabel.Position = new PointF(e.X - dragOffset.X, e.Y - dragOffset.Y);
                Invalidate();
                return;
            }

            int h = HitTest(e.Location);
            if (h != hoveredRegion)
            {
                hoveredRegion = h;
                Invalidate();
            }

            Cursor = labels.Any(l => l.Bounds.Contains(e.Location))
                ? Cursors.Hand
                : Cursors.Default;
        }

        private int HitTest(Point pt)
        {
            using var c1 = CirclePath(0, 0, 2);
            using var c2 = CirclePath(2, 0, 2);

            using var r1 = new Region(c1);
            r1.Intersect(c2);
            using var belowPath = new GraphicsPath();
            belowPath.AddRectangle(MathRect(-5, -5, 5, 0));
            r1.Intersect(belowPath);

            using var rp = new GraphicsPath();
            rp.AddRectangle(MathRect(0, 0, 2, 3));
            using var r2 = new Region(rp);
            r2.Exclude(c1);
            r2.Exclude(c2);

            using var g = CreateGraphics();
            if (r1.IsVisible(pt, g)) return 1;
            if (r2.IsVisible(pt, g)) return 2;
            return 0;
        }

        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            for (int i = labels.Count - 1; i >= 0; i--)
            {
                if (!labels[i].Bounds.Contains(e.Location)) continue;
                draggedLabel = labels[i];
                dragOffset = new PointF(e.X - labels[i].Position.X, e.Y - labels[i].Position.Y);
                Cursor = Cursors.SizeAll;
                return;
            }
        }

        private void OnMouseUp(object? sender, MouseEventArgs e)
        {
            if (draggedLabel != null)
            {
                draggedLabel = null;
                Cursor = Cursors.Default;
            }
        }

        #endregion
    }

    public sealed class DraggableLabel
    {
        public string Text { get; }
        public PointF Position { get; set; }
        public RectangleF Bounds { get; set; }

        public DraggableLabel(string text, PointF pos)
        {
            Text = text;
            Position = pos;
        }
    }
}
