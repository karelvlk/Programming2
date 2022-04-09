
namespace WindowsFormsApp_Pexeso
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.heading = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.arrowLabel = new System.Windows.Forms.PictureBox();
            this.opponent = new System.Windows.Forms.Label();
            this.you = new System.Windows.Forms.Label();
            this.youScore = new System.Windows.Forms.Label();
            this.opponentScore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrowLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // heading
            // 
            this.heading.BackColor = System.Drawing.Color.Yellow;
            this.heading.Font = new System.Drawing.Font("Yu Gothic UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.heading.ForeColor = System.Drawing.Color.Fuchsia;
            this.heading.Location = new System.Drawing.Point(364, 15);
            this.heading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.heading.Name = "heading";
            this.heading.Size = new System.Drawing.Size(300, 80);
            this.heading.TabIndex = 0;
            this.heading.Text = "PEXESO";
            this.heading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.Cyan;
            this.startButton.Font = new System.Drawing.Font("Yu Gothic UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.startButton.ForeColor = System.Drawing.Color.DarkOrange;
            this.startButton.Location = new System.Drawing.Point(364, 467);
            this.startButton.Margin = new System.Windows.Forms.Padding(4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(300, 111);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "START GAME";
            this.startButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // numericUpDown
            // 
            this.numericUpDown.BackColor = System.Drawing.Color.OrangeRed;
            this.numericUpDown.Font = new System.Drawing.Font("Yu Gothic UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDown.Location = new System.Drawing.Point(364, 140);
            this.numericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(300, 71);
            this.numericUpDown.TabIndex = 2;
            this.numericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // arrowLabel
            // 
            this.arrowLabel.Image = ((System.Drawing.Image)(resources.GetObject("arrowLabel.Image")));
            this.arrowLabel.Location = new System.Drawing.Point(403, 219);
            this.arrowLabel.Margin = new System.Windows.Forms.Padding(4);
            this.arrowLabel.Name = "arrowLabel";
            this.arrowLabel.Size = new System.Drawing.Size(242, 228);
            this.arrowLabel.TabIndex = 3;
            this.arrowLabel.TabStop = false;
            // 
            // opponent
            // 
            this.opponent.BackColor = System.Drawing.Color.DodgerBlue;
            this.opponent.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.opponent.Location = new System.Drawing.Point(808, 55);
            this.opponent.Name = "opponent";
            this.opponent.Padding = new System.Windows.Forms.Padding(2);
            this.opponent.Size = new System.Drawing.Size(220, 40);
            this.opponent.TabIndex = 4;
            this.opponent.Text = "BOB (opponent)";
            this.opponent.Visible = false;
            // 
            // you
            // 
            this.you.BackColor = System.Drawing.Color.Orange;
            this.you.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.you.Location = new System.Drawing.Point(12, 55);
            this.you.Name = "you";
            this.you.Padding = new System.Windows.Forms.Padding(2);
            this.you.Size = new System.Drawing.Size(220, 40);
            this.you.TabIndex = 5;
            this.you.Text = "YOU";
            this.you.Visible = false;
            // 
            // youScore
            // 
            this.youScore.BackColor = System.Drawing.Color.DarkOrange;
            this.youScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.youScore.Location = new System.Drawing.Point(277, 15);
            this.youScore.Name = "youScore";
            this.youScore.Padding = new System.Windows.Forms.Padding(20);
            this.youScore.Size = new System.Drawing.Size(80, 80);
            this.youScore.TabIndex = 6;
            this.youScore.Text = "0";
            this.youScore.Visible = false;
            // 
            // opponentScore
            // 
            this.opponentScore.BackColor = System.Drawing.Color.RoyalBlue;
            this.opponentScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.opponentScore.Location = new System.Drawing.Point(671, 15);
            this.opponentScore.Name = "opponentScore";
            this.opponentScore.Padding = new System.Windows.Forms.Padding(20);
            this.opponentScore.Size = new System.Drawing.Size(80, 80);
            this.opponentScore.TabIndex = 7;
            this.opponentScore.Text = "0";
            this.opponentScore.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LawnGreen;
            this.ClientSize = new System.Drawing.Size(1059, 1147);
            this.Controls.Add(this.opponentScore);
            this.Controls.Add(this.youScore);
            this.Controls.Add(this.you);
            this.Controls.Add(this.opponent);
            this.Controls.Add(this.arrowLabel);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.heading);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Pexeso";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrowLabel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label heading;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.PictureBox arrowLabel;
        private System.Windows.Forms.Label opponent;
        private System.Windows.Forms.Label you;
        private System.Windows.Forms.Label youScore;
        private System.Windows.Forms.Label opponentScore;
    }
}

