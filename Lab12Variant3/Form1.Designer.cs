namespace Lab12Variant3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblFormula = new Label();
            lblD = new Label();
            lblE = new Label();
            lblY = new Label();
            lblZ = new Label();
            txtD = new TextBox();
            txtE = new TextBox();
            txtY = new TextBox();
            txtZ = new TextBox();
            SuspendLayout();
            // 
            // lblFormula
            // 
            lblFormula.AutoSize = true;
            lblFormula.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFormula.Location = new Point(30, 20);
            lblFormula.Name = "lblFormula";
            lblFormula.Size = new Size(400, 28);
            lblFormula.TabIndex = 10;
            lblFormula.Text = "A = -d · z / √|e| + |sin(e) + cos(y)|";
            // 
            // lblD
            // 
            lblD.AutoSize = true;
            lblD.Location = new Point(30, 80);
            lblD.Name = "lblD";
            lblD.Size = new Size(120, 20);
            lblD.TabIndex = 0;
            lblD.Text = "d (ввод):";
            // 
            // txtD
            // 
            txtD.Location = new Point(200, 77);
            txtD.Name = "txtD";
            txtD.Size = new Size(150, 27);
            txtD.TabIndex = 1;
            txtD.Text = "1";
            // 
            // lblE
            // 
            lblE.AutoSize = true;
            lblE.Location = new Point(30, 120);
            lblE.Name = "lblE";
            lblE.Size = new Size(120, 20);
            lblE.TabIndex = 2;
            lblE.Text = "e (ввод):";
            // 
            // txtE
            // 
            txtE.Location = new Point(200, 117);
            txtE.Name = "txtE";
            txtE.Size = new Size(150, 27);
            txtE.TabIndex = 3;
            txtE.Text = "1";
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.Location = new Point(30, 170);
            lblY.Name = "lblY";
            lblY.Size = new Size(160, 20);
            lblY.TabIndex = 4;
            lblY.Text = "y (курсор X, чтение):";
            // 
            // txtY
            // 
            txtY.Location = new Point(200, 167);
            txtY.Name = "txtY";
            txtY.ReadOnly = true;
            txtY.Size = new Size(150, 27);
            txtY.TabIndex = 5;
            txtY.Text = "0";
            // 
            // lblZ
            // 
            lblZ.AutoSize = true;
            lblZ.Location = new Point(30, 210);
            lblZ.Name = "lblZ";
            lblZ.Size = new Size(160, 20);
            lblZ.TabIndex = 6;
            lblZ.Text = "z (курсор Y, чтение):";
            // 
            // txtZ
            // 
            txtZ.Location = new Point(200, 207);
            txtZ.Name = "txtZ";
            txtZ.ReadOnly = true;
            txtZ.Size = new Size(150, 27);
            txtZ.TabIndex = 7;
            txtZ.Text = "0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 260);
            Controls.Add(lblFormula);
            Controls.Add(lblD);
            Controls.Add(txtD);
            Controls.Add(lblE);
            Controls.Add(txtE);
            Controls.Add(lblY);
            Controls.Add(txtY);
            Controls.Add(lblZ);
            Controls.Add(txtZ);
            Name = "Form1";
            Text = "Lab12 Variant3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFormula;
        private Label lblD;
        private Label lblE;
        private Label lblY;
        private Label lblZ;
        private TextBox txtD;
        private TextBox txtE;
        private TextBox txtY;
        private TextBox txtZ;
    }
}
