namespace HuaChun_DailyReport
{
    partial class QueryFormBase
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
            this.textBoxProjectNo = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.btnProjectSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxProjectNo
            // 
            this.textBoxProjectNo.Enabled = false;
            this.textBoxProjectNo.Location = new System.Drawing.Point(74, 9);
            this.textBoxProjectNo.Name = "textBoxProjectNo";
            this.textBoxProjectNo.ReadOnly = true;
            this.textBoxProjectNo.Size = new System.Drawing.Size(100, 20);
            this.textBoxProjectNo.TabIndex = 0;
            // 
            // textBoxName
            // 
            this.textBoxName.Enabled = false;
            this.textBoxName.Location = new System.Drawing.Point(261, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // btnProjectSelect
            // 
            this.btnProjectSelect.Location = new System.Drawing.Point(180, 10);
            this.btnProjectSelect.Name = "btnProjectSelect";
            this.btnProjectSelect.Size = new System.Drawing.Size(75, 23);
            this.btnProjectSelect.TabIndex = 1;
            this.btnProjectSelect.Text = "...";
            this.btnProjectSelect.UseVisualStyleBackColor = true;
            this.btnProjectSelect.Click += new System.EventHandler(this.btnProjectSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "工程編號";
            // 
            // QueryFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnProjectSelect);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxProjectNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "QueryFormBase";
            this.Text = "QueryFormBase";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxProjectNo;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button btnProjectSelect;
        private System.Windows.Forms.Label label1;
    }
}