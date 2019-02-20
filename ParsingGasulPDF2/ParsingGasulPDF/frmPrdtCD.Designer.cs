namespace ParsingGasulPDF
{
    partial class frmPrdtCD
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
            this.grdPrdtCD = new System.Windows.Forms.DataGridView();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this._cbo상품구분 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._cboCompy = new System.Windows.Forms.ComboBox();
            this.prdt_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prdt_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.use_yn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdPrdtCD)).BeginInit();
            this.SuspendLayout();
            // 
            // grdPrdtCD
            // 
            this.grdPrdtCD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPrdtCD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.prdt_cd,
            this.prdt_name,
            this.use_yn});
            this.grdPrdtCD.Location = new System.Drawing.Point(12, 56);
            this.grdPrdtCD.Name = "grdPrdtCD";
            this.grdPrdtCD.RowTemplate.Height = 23;
            this.grdPrdtCD.Size = new System.Drawing.Size(763, 532);
            this.grdPrdtCD.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(758, 10);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(114, 40);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "검색";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 62;
            this.label5.Text = "상품구분";
            // 
            // _cbo상품구분
            // 
            this._cbo상품구분.DropDownHeight = 120;
            this._cbo상품구분.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cbo상품구분.FormattingEnabled = true;
            this._cbo상품구분.IntegralHeight = false;
            this._cbo상품구분.Location = new System.Drawing.Point(83, 14);
            this._cbo상품구분.Name = "_cbo상품구분";
            this._cbo상품구분.Size = new System.Drawing.Size(77, 20);
            this._cbo상품구분.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 64;
            this.label2.Text = "회사명";
            // 
            // _cboCompy
            // 
            this._cboCompy.DropDownHeight = 120;
            this._cboCompy.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cboCompy.FormattingEnabled = true;
            this._cboCompy.IntegralHeight = false;
            this._cboCompy.Location = new System.Drawing.Point(232, 14);
            this._cboCompy.Name = "_cboCompy";
            this._cboCompy.Size = new System.Drawing.Size(255, 20);
            this._cboCompy.TabIndex = 63;
            // 
            // prdt_cd
            // 
            this.prdt_cd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.prdt_cd.HeaderText = "상품코드";
            this.prdt_cd.MinimumWidth = 100;
            this.prdt_cd.Name = "prdt_cd";
            // 
            // prdt_name
            // 
            this.prdt_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.prdt_name.HeaderText = "상품명";
            this.prdt_name.MinimumWidth = 300;
            this.prdt_name.Name = "prdt_name";
            this.prdt_name.Width = 300;
            // 
            // use_yn
            // 
            this.use_yn.HeaderText = "사용여부";
            this.use_yn.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.use_yn.Name = "use_yn";
            // 
            // frmPrdtCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 663);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._cboCompy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._cbo상품구분);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.grdPrdtCD);
            this.Name = "frmPrdtCD";
            this.Text = "frmPrdtCD";
            ((System.ComponentModel.ISupportInitialize)(this.grdPrdtCD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdPrdtCD;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox _cbo상품구분;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cboCompy;
        private System.Windows.Forms.DataGridViewTextBoxColumn prdt_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn prdt_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn use_yn;
    }
}