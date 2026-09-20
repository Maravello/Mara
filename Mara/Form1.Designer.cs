namespace Mara
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            button1 = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(279, 44);
            label1.Name = "label1";
            label1.Size = new Size(0, 25);
            label1.TabIndex = 0;
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(207, 385);
            button1.Name = "button1";
            button1.Size = new Size(231, 34);
            button1.TabIndex = 1;
            button1.Text = "Parler";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button1.MouseClick += button1_MouseClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(349, 179);
            label2.Name = "label2";
            label2.Size = new Size(0, 25);
            label2.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(12, 438);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(635, 200);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(128, 12);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(389, 76);
            textBox2.TabIndex = 4;
            textBox2.Text = "Bien le boujour Yahaya";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = SystemColors.ControlDark;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Location = new Point(50, 94);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(539, 269);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 670);
            Controls.Add(pictureBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private PictureBox pictureBox1;
    }
}
