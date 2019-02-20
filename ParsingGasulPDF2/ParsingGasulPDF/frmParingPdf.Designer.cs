namespace ParsingGasulPDF
{
    partial class frmParingPdf
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
            this.label2 = new System.Windows.Forms.Label();
            this._cboPrdt = new System.Windows.Forms.ComboBox();
            this._btnSelDir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this._cbo성별 = new System.Windows.Forms.ComboBox();
            this.btnGO = new System.Windows.Forms.Button();
            this._rhRtn = new System.Windows.Forms.RichTextBox();
            this._cboInputGB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._cbo상품구분 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 34;
            this.label2.Text = "상품";
            // 
            // _cboPrdt
            // 
            this._cboPrdt.DropDownHeight = 120;
            this._cboPrdt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cboPrdt.FormattingEnabled = true;
            this._cboPrdt.IntegralHeight = false;
            this._cboPrdt.Location = new System.Drawing.Point(257, 11);
            this._cboPrdt.Name = "_cboPrdt";
            this._cboPrdt.Size = new System.Drawing.Size(287, 20);
            this._cboPrdt.TabIndex = 33;
            // 
            // _btnSelDir
            // 
            this._btnSelDir.Location = new System.Drawing.Point(751, 41);
            this._btnSelDir.Name = "_btnSelDir";
            this._btnSelDir.Size = new System.Drawing.Size(25, 17);
            this._btnSelDir.TabIndex = 32;
            this._btnSelDir.Text = "...";
            this._btnSelDir.UseVisualStyleBackColor = true;
            this._btnSelDir.Click += new System.EventHandler(this.btn가설파일선택_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "폴더선택 :";
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(74, 43);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(671, 21);
            this.txtDir.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(550, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "성별";
            // 
            // _cbo성별
            // 
            this._cbo성별.DropDownHeight = 120;
            this._cbo성별.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cbo성별.FormattingEnabled = true;
            this._cbo성별.IntegralHeight = false;
            this._cbo성별.Location = new System.Drawing.Point(585, 11);
            this._cbo성별.Name = "_cbo성별";
            this._cbo성별.Size = new System.Drawing.Size(63, 20);
            this._cbo성별.TabIndex = 36;
            this._cbo성별.SelectedIndexChanged += new System.EventHandler(this._cbo성별_SelectedIndexChanged);
            // 
            // btnGO
            // 
            this.btnGO.Location = new System.Drawing.Point(782, 13);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(61, 45);
            this.btnGO.TabIndex = 37;
            this.btnGO.Text = "가설정보추출";
            this.btnGO.UseVisualStyleBackColor = true;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // _rhRtn
            // 
            this._rhRtn.Location = new System.Drawing.Point(12, 70);
            this._rhRtn.Name = "_rhRtn";
            this._rhRtn.Size = new System.Drawing.Size(831, 553);
            this._rhRtn.TabIndex = 38;
            this._rhRtn.Text = "";
            this._rhRtn.TextChanged += new System.EventHandler(this._rhRtn_TextChanged);
            // 
            // _cboInputGB
            // 
            this._cboInputGB.DropDownHeight = 120;
            this._cboInputGB.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cboInputGB.FormattingEnabled = true;
            this._cboInputGB.IntegralHeight = false;
            this._cboInputGB.Location = new System.Drawing.Point(713, 10);
            this._cboInputGB.Name = "_cboInputGB";
            this._cboInputGB.Size = new System.Drawing.Size(63, 20);
            this._cboInputGB.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(654, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "파싱구분";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "상품구분";
            // 
            // _cbo상품구분
            // 
            this._cbo상품구분.DropDownHeight = 120;
            this._cbo상품구분.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cbo상품구분.FormattingEnabled = true;
            this._cbo상품구분.IntegralHeight = false;
            this._cbo상품구분.Location = new System.Drawing.Point(69, 12);
            this._cbo상품구분.Name = "_cbo상품구분";
            this._cbo상품구분.Size = new System.Drawing.Size(145, 20);
            this._cbo상품구분.TabIndex = 41;
            this._cbo상품구분.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // frmParingPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 635);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._cbo상품구분);
            this.Controls.Add(this._cboInputGB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._rhRtn);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this._cbo성별);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._cboPrdt);
            this.Controls.Add(this._btnSelDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDir);
            this.Name = "frmParingPdf";
            this.Text = "frmParingPdf";
            this.Load += new System.EventHandler(this.frmParingPdf_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cboPrdt;
        private System.Windows.Forms.Button _btnSelDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _cbo성별;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.RichTextBox _rhRtn;
        private System.Windows.Forms.ComboBox _cboInputGB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox _cbo상품구분;
    }
}