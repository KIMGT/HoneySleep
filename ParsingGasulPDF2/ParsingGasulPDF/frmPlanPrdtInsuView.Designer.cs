namespace ParsingGasulPDF
{
    partial class frmPlanPrdtInsuView
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
            this._cbo플랜 = new System.Windows.Forms.ComboBox();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._cbo성별 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._cboPrdt = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 59;
            this.label5.Text = "플랜";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // _cbo플랜
            // 
            this._cbo플랜.DropDownHeight = 120;
            this._cbo플랜.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._cbo플랜.FormattingEnabled = true;
            this._cbo플랜.IntegralHeight = false;
            this._cbo플랜.Location = new System.Drawing.Point(47, 15);
            this._cbo플랜.Name = "_cbo플랜";
            this._cbo플랜.Size = new System.Drawing.Size(145, 20);
            this._cbo플랜.TabIndex = 58;
            this._cbo플랜.SelectedIndexChanged += new System.EventHandler(this._cbo플랜_SelectedIndexChanged);
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(47, 38);
            this.txtSum.Name = "txtSum";
            this.txtSum.Size = new System.Drawing.Size(828, 21);
            this.txtSum.TabIndex = 56;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(645, 12);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(43, 21);
            this.txtAge.TabIndex = 55;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(912, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 67);
            this.btnOK.TabIndex = 54;
            this.btnOK.Text = "검색";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(616, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 53;
            this.label1.Text = "연령";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(529, 15);
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
            this._cbo성별.Location = new System.Drawing.Point(558, 13);
            this._cbo성별.Name = "_cbo성별";
            this._cbo성별.Size = new System.Drawing.Size(58, 20);
            this._cbo성별.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 16);
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
            this._cboPrdt.Location = new System.Drawing.Point(239, 13);
            this._cboPrdt.Name = "_cboPrdt";
            this._cboPrdt.Size = new System.Drawing.Size(287, 20);
            this._cboPrdt.TabIndex = 49;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(974, 609);
            this.dataGridView1.TabIndex = 60;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 12);
            this.label6.TabIndex = 57;
            // 
            // frmPlanPrdtInsuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 696);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._cbo플랜);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._cbo성별);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._cboPrdt);
            this.Name = "frmPlanPrdtInsuView";
            this.Text = "플랜별보험료확인1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox _cbo플랜;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _cbo성별;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cboPrdt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
    }
}