namespace ParsingGasulPDF
{
    partial class MDIMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.가설문자추출ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.가설보험료추출ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.보험료확인ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.플랜별보험료확인ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.가설연령별보험료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.ToolStripMenuItem3});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(974, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(67, 20);
            this.toolStripMenuItem1.Text = "상품관리";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.가설문자추출ToolStripMenuItem,
            this.가설보험료추출ToolStripMenuItem,
            this.보험료확인ToolStripMenuItem,
            this.가설연령별보험료ToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(82, 20);
            this.toolStripMenuItem2.Text = "Parsing관리";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // 가설문자추출ToolStripMenuItem
            // 
            this.가설문자추출ToolStripMenuItem.Name = "가설문자추출ToolStripMenuItem";
            this.가설문자추출ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.가설문자추출ToolStripMenuItem.Text = "가설문자추출";
            this.가설문자추출ToolStripMenuItem.Click += new System.EventHandler(this.가설문자추출ToolStripMenuItem_Click);
            // 
            // 가설보험료추출ToolStripMenuItem
            // 
            this.가설보험료추출ToolStripMenuItem.Name = "가설보험료추출ToolStripMenuItem";
            this.가설보험료추출ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.가설보험료추출ToolStripMenuItem.Text = "가설보험료추출";
            this.가설보험료추출ToolStripMenuItem.Click += new System.EventHandler(this.가설보험료추출ToolStripMenuItem_Click);
            // 
            // 보험료확인ToolStripMenuItem
            // 
            this.보험료확인ToolStripMenuItem.Name = "보험료확인ToolStripMenuItem";
            this.보험료확인ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.보험료확인ToolStripMenuItem.Text = "가설보험료확인";
            this.보험료확인ToolStripMenuItem.Click += new System.EventHandler(this.보험료확인ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem3
            // 
            this.ToolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.플랜별보험료확인ToolStripMenuItem});
            this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
            this.ToolStripMenuItem3.Size = new System.Drawing.Size(67, 20);
            this.ToolStripMenuItem3.Text = "플랜관리";
            this.ToolStripMenuItem3.Click += new System.EventHandler(this.보험료보기ToolStripMenuItem_Click);
            // 
            // 플랜별보험료확인ToolStripMenuItem
            // 
            this.플랜별보험료확인ToolStripMenuItem.Name = "플랜별보험료확인ToolStripMenuItem";
            this.플랜별보험료확인ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.플랜별보험료확인ToolStripMenuItem.Text = "플랜별보험료확인";
            this.플랜별보험료확인ToolStripMenuItem.Click += new System.EventHandler(this.플랜별보험료확인ToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 533);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(974, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel.Text = "상태";
            // 
            // 가설연령별보험료ToolStripMenuItem
            // 
            this.가설연령별보험료ToolStripMenuItem.Name = "가설연령별보험료ToolStripMenuItem";
            this.가설연령별보험료ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.가설연령별보험료ToolStripMenuItem.Text = "가설연령별보험료";
            this.가설연령별보험료ToolStripMenuItem.Click += new System.EventHandler(this.가설연령별보험료ToolStripMenuItem_Click);
            // 
            // MDIMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 555);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDIMain";
            this.Text = "MDIMain";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 가설문자추출ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 가설보험료추출ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 보험료확인ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 플랜별보험료확인ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 가설연령별보험료ToolStripMenuItem;
    }
}



