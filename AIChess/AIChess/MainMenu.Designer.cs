
namespace AIChess
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.newGameBox = new System.Windows.Forms.PictureBox();
            this.difficultyBox = new System.Windows.Forms.PictureBox();
            this.exitBox = new System.Windows.Forms.PictureBox();
            this.githubBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.newGameBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.difficultyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.githubBox)).BeginInit();
            this.SuspendLayout();
            // 
            // newGameBox
            // 
            this.newGameBox.Image = ((System.Drawing.Image)(resources.GetObject("newGameBox.Image")));
            this.newGameBox.Location = new System.Drawing.Point(56, 75);
            this.newGameBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newGameBox.Name = "newGameBox";
            this.newGameBox.Size = new System.Drawing.Size(493, 145);
            this.newGameBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.newGameBox.TabIndex = 0;
            this.newGameBox.TabStop = false;
            this.newGameBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.newGameBox_MouseDown);
            this.newGameBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.newGameBox_MouseUp);
            // 
            // difficultyBox
            // 
            this.difficultyBox.Image = ((System.Drawing.Image)(resources.GetObject("difficultyBox.Image")));
            this.difficultyBox.Location = new System.Drawing.Point(56, 226);
            this.difficultyBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.difficultyBox.Name = "difficultyBox";
            this.difficultyBox.Size = new System.Drawing.Size(244, 145);
            this.difficultyBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.difficultyBox.TabIndex = 1;
            this.difficultyBox.TabStop = false;
            this.difficultyBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.difficultyBox_MouseDown);
            this.difficultyBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.difficultyBox_MouseUp);
            // 
            // exitBox
            // 
            this.exitBox.Image = ((System.Drawing.Image)(resources.GetObject("exitBox.Image")));
            this.exitBox.Location = new System.Drawing.Point(307, 226);
            this.exitBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.exitBox.Name = "exitBox";
            this.exitBox.Size = new System.Drawing.Size(244, 145);
            this.exitBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.exitBox.TabIndex = 2;
            this.exitBox.TabStop = false;
            this.exitBox.Click += new System.EventHandler(this.exitBox_Click);
            // 
            // githubBox
            // 
            this.githubBox.Image = ((System.Drawing.Image)(resources.GetObject("githubBox.Image")));
            this.githubBox.Location = new System.Drawing.Point(556, 75);
            this.githubBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.githubBox.Name = "githubBox";
            this.githubBox.Size = new System.Drawing.Size(244, 297);
            this.githubBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.githubBox.TabIndex = 3;
            this.githubBox.TabStop = false;
            this.githubBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.githubBox_MouseUp);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1019, 460);
            this.Controls.Add(this.githubBox);
            this.Controls.Add(this.exitBox);
            this.Controls.Add(this.difficultyBox);
            this.Controls.Add(this.newGameBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(873, 497);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            ((System.ComponentModel.ISupportInitialize)(this.newGameBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.difficultyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.githubBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox newGameBox;
        private System.Windows.Forms.PictureBox difficultyBox;
        private System.Windows.Forms.PictureBox exitBox;
        private System.Windows.Forms.PictureBox githubBox;
    }
}