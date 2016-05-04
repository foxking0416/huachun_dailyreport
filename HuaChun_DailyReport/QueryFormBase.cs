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
    public partial class QueryFormBase : Form
    {
        public QueryFormBase()
        {
            InitializeComponent();
        }

        private void btnProjectSelect_Click(object sender, EventArgs e)
        {
            ProjectSearchForm form = new ProjectSearchForm(this);
            form.ShowDialog();
        }

        public virtual void LoadProjectInfo(string projectNo)
        { }
    }
}
