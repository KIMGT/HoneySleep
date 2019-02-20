namespace ParsingGasulPDF
{
    partial class frmParsingInfo
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
            this.btnView2 = new System.Windows.Forms.Button();
            this.btnView1 = new System.Windows.Forms.Button();
            this.btn가설파일선택 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._cboCompy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnView2
            // 
            this.btnView2.Location = new System.Drawing.Point(124, 68);
            this.btnView2.Name = "btnView2";
            this.btnView2.Size = new System.Drawing.Size(67, 23);
            this.btnView2.TabIndex = 27;
            this.btnView2.Text = "담보내용보기";
            this.btnView2.UseVisualStyleBackColor = true;
            this.btnView2.Click += new System.EventHandler(this.btnView2_Click);
            // 
            // btnView1
            // 
            this.btnView1.Location = new System.Drawing.Point(27, 68);
            this.btnView1.Name = "btnView1";
            this.btnView1.Size = new System.Drawing.Size(89, 23);
            this.btnView1.TabIndex = 26;
            this.btnView1.Text = "전체내용보기";
            this.btnView1.UseVisualStyleBackColor = true;
            this.btnView1.Click += new System.EventHandler(this.btnView1_Click);
            // 
            // btn가설파일선택
            // 
            this.btn가설파일선택.Location = new System.Drawing.Point(666, 41);
            this.btn가설파일선택.Name = "btn가설파일선택";
            this.btn가설파일선택.Size = new System.Drawing.Size(50, 23);
            this.btn가설파일선택.TabIndex = 19;
            this.btn가설파일선택.Text = "...";
            this.btn가설파일선택.UseVisualStyleBackColor = true;
            this.btn가설파일선택.Click += new System.EventHandler(this.btn가설파일선택_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "가설파일명 :";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(104, 40);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(557, 21);
            this.txtFileName.TabIndex = 17;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // _cboCompy
            // 
            this._cboCompy.DropDownHeight = 120;
            this._cboCompy.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cboCompy.FormattingEnabled = true;
            this._cboCompy.IntegralHeight = false;
            this._cboCompy.Location = new System.Drawing.Point(104, 10);
            this._cboCompy.Name = "_cboCompy";
            this._cboCompy.Size = new System.Drawing.Size(255, 20);
            this._cboCompy.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "회사명";
            // 
            // frmParsingInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 95);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._cboCompy);
            this.Controls.Add(this.btnView2);
            this.Controls.Add(this.btnView1);
            this.Controls.Add(this.btn가설파일선택);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFileName);
            this.Name = "frmParsingInfo";
            this.Text = "가설데이터추출기본정보";
            this.Load += new System.EventHandler(this.frmParsingInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnView2;
        private System.Windows.Forms.Button btnView1;
        private System.Windows.Forms.Button btn가설파일선택;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox _cboCompy;
        private System.Windows.Forms.Label label2;
    }
}