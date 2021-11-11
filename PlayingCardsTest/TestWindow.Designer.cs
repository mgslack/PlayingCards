namespace PlayingCardsTest
{
    partial class TestWindow
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
            this.showResults = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numDecks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbJokers = new System.Windows.Forms.RadioButton();
            this.rbPinochle = new System.Windows.Forms.RadioButton();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.image = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.SuspendLayout();
            // 
            // showResults
            // 
            this.showResults.Location = new System.Drawing.Point(12, 12);
            this.showResults.Multiline = true;
            this.showResults.Name = "showResults";
            this.showResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.showResults.Size = new System.Drawing.Size(467, 320);
            this.showResults.TabIndex = 0;
            this.showResults.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Number Of Decks:";
            // 
            // numDecks
            // 
            this.numDecks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.numDecks.FormattingEnabled = true;
            this.numDecks.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.numDecks.Location = new System.Drawing.Point(113, 341);
            this.numDecks.Name = "numDecks";
            this.numDecks.Size = new System.Drawing.Size(39, 21);
            this.numDecks.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 344);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Options:";
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Checked = true;
            this.rbNone.Location = new System.Drawing.Point(226, 342);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(51, 17);
            this.rbNone.TabIndex = 4;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "N&one";
            this.rbNone.UseVisualStyleBackColor = true;
            // 
            // rbJokers
            // 
            this.rbJokers.AutoSize = true;
            this.rbJokers.Location = new System.Drawing.Point(283, 342);
            this.rbJokers.Name = "rbJokers";
            this.rbJokers.Size = new System.Drawing.Size(94, 17);
            this.rbJokers.TabIndex = 5;
            this.rbJokers.Text = "&Include Jokers";
            this.rbJokers.UseVisualStyleBackColor = true;
            // 
            // rbPinochle
            // 
            this.rbPinochle.AutoSize = true;
            this.rbPinochle.Location = new System.Drawing.Point(383, 342);
            this.rbPinochle.Name = "rbPinochle";
            this.rbPinochle.Size = new System.Drawing.Size(95, 17);
            this.rbPinochle.TabIndex = 6;
            this.rbPinochle.Text = "&Pinochle Deck";
            this.rbPinochle.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(15, 370);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 7;
            this.btnCreate.Text = "&Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnShuffle
            // 
            this.btnShuffle.Enabled = false;
            this.btnShuffle.Location = new System.Drawing.Point(96, 370);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(75, 23);
            this.btnShuffle.TabIndex = 8;
            this.btnShuffle.Text = "&Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(258, 370);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnShow
            // 
            this.btnShow.Enabled = false;
            this.btnShow.Location = new System.Drawing.Point(177, 370);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 9;
            this.btnShow.Text = "S&how Card";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // image
            // 
            this.image.Location = new System.Drawing.Point(494, 12);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(100, 100);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.image.TabIndex = 11;
            this.image.TabStop = false;
            // 
            // TestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 405);
            this.Controls.Add(this.image);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnShuffle);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.rbPinochle);
            this.Controls.Add(this.rbJokers);
            this.Controls.Add(this.rbNone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numDecks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showResults);
            this.Name = "TestWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PlayingCards Test";
            this.Load += new System.EventHandler(this.TestWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox showResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox numDecks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.RadioButton rbJokers;
        private System.Windows.Forms.RadioButton rbPinochle;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.PictureBox image;
    }
}

