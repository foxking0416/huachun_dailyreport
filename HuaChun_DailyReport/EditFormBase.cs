using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuaChun_DailyReport
{
    public partial class EditFormBase : IncreaseEditFormBase
    {
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnDelete;

        public EditFormBase()
        {
            InitializeComponent();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearch.Location = new System.Drawing.Point(283, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "搜尋";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.Controls.Add(this.btnSearch);

            this.btnAddEdit.Size = new System.Drawing.Size(110, 23);

            this.btnExit.Size = new System.Drawing.Size(110, 23);
            this.btnExit.Location = new System.Drawing.Point(250, 340);

            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDelete.Location = new System.Drawing.Point(130, 340);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "刪除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.Controls.Add(this.btnDelete);

        }

        protected virtual void LoadInformation(string number)
        { }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
        { }

        protected virtual void btnDelete_Click(object sender, EventArgs e)
        { }
    }
}
