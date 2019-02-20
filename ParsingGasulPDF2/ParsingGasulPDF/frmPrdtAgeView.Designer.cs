namespace ParsingGasulPDF
{
    partial class frmPrdtAgeView
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
            this.label5 = new System.Windows.Forms.Label();
            this._cbo상품구분 = new System.Windows.Forms.ComboBox();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._cbo성별 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._cboPrdt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._cboInsu = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 60;
            this.label5.Text = "상품구분";
            // 
            // _cbo상품구분
            // 
            this._cbo상품구분.DropDownHeight = 120;
            this._cbo상품구분.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cbo상품구분.FormattingEnabled = true;
            this._cbo상품구분.IntegralHeight = false;
            this._cbo상품구분.Location = new System.Drawing.Point(70, 25);
            this._cbo상품구분.Name = "_cbo상품구분";
            this._cbo상품구분.Size = new System.Drawing.Size(77, 20);
            this._cbo상품구분.TabIndex = 59;
            this._cbo상품구분.SelectedIndexChanged += new System.EventHandler(this._cbo상품구분_SelectedIndexChanged);
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(640, 25);
            this.txtSum.Name = "txtSum";
            this.txtSum.Size = new System.Drawing.Size(125, 21);
            this.txtSum.TabIndex = 57;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(921, 588);
            this.dataGridView1.TabIndex = 55;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(791, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(131, 53);
            this.btnOK.TabIndex = 54;
            this.btnOK.Text = "검색";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(494, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 52;
            this.label3.Text = "성별";
            // 
            // _cbo성별
            // 
            this._cbo성별.DropDownHeight = 120;
            this._cbo성별.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cbo성별.FormattingEnabled = true;
            this._cbo성별.IntegralHeight = false;
            this._cbo성별.Location = new System.Drawing.Point(529, 26);
            this._cbo성별.Name = "_cbo성별";
            this._cbo성별.Size = new System.Drawing.Size(82, 20);
            this._cbo성별.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "상품";
            // 
            // _cboPrdt
            // 
            this._cboPrdt.DropDownHeight = 120;
            this._cboPrdt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cboPrdt.FormattingEnabled = true;
            this._cboPrdt.IntegralHeight = false;
            this._cboPrdt.Location = new System.Drawing.Point(190, 14);
            this._cboPrdt.Name = "_cboPrdt";
            this._cboPrdt.Size = new System.Drawing.Size(287, 20);
            this._cboPrdt.TabIndex = 49;
            this._cboPrdt.SelectedIndexChanged += new System.EventHandler(this._cboPrdt_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 62;
            this.label1.Text = "담보";
            // 
            // _cboInsu
            // 
            this._cboInsu.DropDownHeight = 120;
            this._cboInsu.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cboInsu.FormattingEnabled = true;
            this._cboInsu.IntegralHeight = false;
            this._cboInsu.Location = new System.Drawing.Point(190, 40);
            this._cboInsu.Name = "_cboInsu";
            this._cboInsu.Size = new System.Drawing.Size(287, 20);
            this._cboInsu.TabIndex = 61;
            // 
            // frmPrdtAgeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 658);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cboInsu);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._cbo상품구분);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._cbo성별);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._cboPrdt);
            this.Name = "frmPrdtAgeView";
            this.Text = "frmPrdtAgeView";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox _cbo상품구분;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _cbo성별;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cboPrdt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _cboInsu;
    }
}