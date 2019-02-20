namespace ParsingGasulPDF
{
    partial class frmPrdtView
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
            this._cbo성별 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._cboPrdt = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._cbo상품구분 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // _cbo성별
            // 
            this._cbo성별.DropDownHeight = 120;
            this._cbo성별.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cbo성별.FormattingEnabled = true;
            this._cbo성별.IntegralHeight = false;
            this._cbo성별.Location = new System.Drawing.Point(523, 18);
            this._cbo성별.Name = "_cbo성별";
            this._cbo성별.Size = new System.Drawing.Size(82, 20);
            this._cbo성별.TabIndex = 39;
            this._cbo성별.SelectedIndexChanged += new System.EventHandler(this._cbo성별_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "상품";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // _cboPrdt
            // 
            this._cboPrdt.DropDownHeight = 120;
            this._cboPrdt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cboPrdt.FormattingEnabled = true;
            this._cboPrdt.IntegralHeight = false;
            this._cboPrdt.Location = new System.Drawing.Point(195, 18);
            this._cboPrdt.Name = "_cboPrdt";
            this._cboPrdt.Size = new System.Drawing.Size(287, 20);
            this._cboPrdt.TabIndex = 37;
            this._cboPrdt.SelectedIndexChanged += new System.EventHandler(this._cboPrdt_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(488, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "성별";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(611, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "연령";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(861, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 37);
            this.btnOK.TabIndex = 42;
            this.btnOK.Text = "검색";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(921, 573);
            this.dataGridView1.TabIndex = 43;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(646, 17);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(43, 21);
            this.txtAge.TabIndex = 44;
            this.txtAge.TextChanged += new System.EventHandler(this.txtAge_TextChanged);
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(730, 17);
            this.txtSum.Name = "txtSum";
            this.txtSum.Size = new System.Drawing.Size(125, 21);
            this.txtSum.TabIndex = 45;
            this.txtSum.TextChanged += new System.EventHandler(this.txtSum_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(695, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "합계";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "상품구분";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // _cbo상품구분
            // 
            this._cbo상품구분.DropDownHeight = 120;
            this._cbo상품구분.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cbo상품구분.FormattingEnabled = true;
            this._cbo상품구분.IntegralHeight = false;
            this._cbo상품구분.Location = new System.Drawing.Point(71, 20);
            this._cbo상품구분.Name = "_cbo상품구분";
            this._cbo상품구분.Size = new System.Drawing.Size(77, 20);
            this._cbo상품구분.TabIndex = 47;
            this._cbo상품구분.SelectedIndexChanged += new System.EventHandler(this._cbo상품구분_SelectedIndexChanged);
            // 
            // frmPrdtView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 642);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._cbo상품구분);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._cbo성별);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._cboPrdt);
            this.Name = "frmPrdtView";
            this.Text = "상품별보험료확인";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _cbo성별;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cboPrdt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox _cbo상품구분;
    }
}