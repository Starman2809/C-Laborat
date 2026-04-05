using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lab12Variant3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.MouseMove += Form1_MouseMove;
            txtD.TextChanged += InputChanged;
            txtE.TextChanged += InputChanged;

            AttachMouseMoveToChildren(this);
            Calculate();
        }

        private void AttachMouseMoveToChildren(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                c.MouseMove += Form1_MouseMove;
                if (c.HasChildren)
                    AttachMouseMoveToChildren(c);
            }
        }

        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            Point screenPos = Cursor.Position;
            Point clientPos = this.PointToClient(screenPos);

            txtY.Text = clientPos.X.ToString();
            txtZ.Text = clientPos.Y.ToString();

            Calculate();
        }

        private void InputChanged(object? sender, EventArgs e)
        {
            Calculate();
        }

        private static bool TryParseDouble(string text, out double value)
        {
            return double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value)
                || double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        private void Calculate()
        {
            if (!TryParseDouble(txtD.Text, out double d) ||
                !TryParseDouble(txtE.Text, out double ev) ||
                !TryParseDouble(txtY.Text, out double y) ||
                !TryParseDouble(txtZ.Text, out double z))
            {
                this.Text = "ERROR";
                return;
            }

            double sqrtAbsE = Math.Sqrt(Math.Abs(ev));
            if (sqrtAbsE == 0)
            {
                this.Text = "ERROR";
                return;
            }

            double result = -d * z / sqrtAbsE + Math.Abs(Math.Sin(ev) + Math.Cos(y));

            if (double.IsNaN(result) || double.IsInfinity(result))
                this.Text = "ERROR";
            else
                this.Text = "A = " + result.ToString("F4");
        }
    }
}
