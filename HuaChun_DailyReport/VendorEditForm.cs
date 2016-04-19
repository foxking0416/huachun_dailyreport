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
    public partial class VendorEditForm : VendorIncreaseForm
    {
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSearch;
        private string[] vendors;
        private int selectIndex = 0;
        private int vendorCount;

        public VendorEditForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            this.Text = "廠商編輯作業";
            //this.btnClear.Visible = false;
            this.btnClear.Text = "刪除";
            this.textBoxVendor_No.Enabled = false;
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();

            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(201, 17);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = "上一個";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(280, 17);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一個";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(360, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "搜尋";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.groupBox1.Controls.Add(this.btnLast);
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnSearch);

            vendors = SQL.Read1DArrayNoCondition_SQL_Data("vendor_no", "vendor");
            vendorCount = vendors.Length;
            if (vendorCount != 0)
                this.btnClear.Enabled = true;
            else
                this.btnClear.Enabled = false;
            LoadInformation(vendors[selectIndex]);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            selectIndex--;
            if (selectIndex < 0)
                selectIndex = vendorCount - 1;

            LoadInformation(vendors[selectIndex]);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            selectIndex++;
            if (selectIndex >= vendorCount)
                selectIndex = 0;

            LoadInformation(vendors[selectIndex]);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            VendorSearchForm searchform = new VendorSearchForm(this);
            searchform.Show();
        }

        public void LoadInformation(string vendor_no)
        {
            this.textBoxVendor_No.Text    = SQL.Read_SQL_data("vendor_no", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxVendor_Name.Text  = SQL.Read_SQL_data("vendor_name", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxVendor_Abbre.Text = SQL.Read_SQL_data("vendor_abbre", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxContact1.Text     = SQL.Read_SQL_data("contact1", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxPhone1.Text       = SQL.Read_SQL_data("phone1", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxFax.Text          = SQL.Read_SQL_data("fax", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxContact2.Text     = SQL.Read_SQL_data("contact2", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxPhone2.Text       = SQL.Read_SQL_data("phone2", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxCell.Text         = SQL.Read_SQL_data("cell", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxEmail.Text        = SQL.Read_SQL_data("email", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxCode2.Text        = SQL.Read_SQL_data("code2", "vendor", "vendor_no = '" + vendor_no + "'");
            this.comboBoxCity.SelectedItem = SQL.Read_SQL_data("address_city", "vendor", "vendor_no = '" + vendor_no + "'");
            this.comboBoxDistrict.SelectedItem = SQL.Read_SQL_data("address_district", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxAddress.Text      = SQL.Read_SQL_data("address_road", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxID.Text           = SQL.Read_SQL_data("id", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxTaxTitle.Text     = SQL.Read_SQL_data("taxtitle", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxBusinessItem.Text = SQL.Read_SQL_data("businessitems", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxOther.Text        = SQL.Read_SQL_data("other", "vendor", "vendor_no = '" + vendor_no + "'");
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            label19.Visible = false;
            label20.Visible = false;

            if (textBoxVendor_Name.Text == string.Empty)
                label19.Visible = true;

            if (textBoxVendor_Name.Text == string.Empty)
                return;

            if (textBoxEmail.Text != string.Empty)
            {
                if (!textBoxEmail.Text.Contains("@"))
                {
                    label20.Visible = true;
                    return;
                }
            }

            DialogResult result = MessageBox.Show("確定要修改廠商資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                SQL.Set_SQL_data("vendor_name", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxVendor_Name.Text);
                SQL.Set_SQL_data("vendor_abbre", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxVendor_Abbre.Text);
                SQL.Set_SQL_data("contact1", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxContact1.Text);
                SQL.Set_SQL_data("phone1", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxPhone1.Text);
                SQL.Set_SQL_data("fax", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxFax.Text);
                SQL.Set_SQL_data("contact2", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxContact2.Text);
                SQL.Set_SQL_data("phone2", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxPhone2.Text);
                SQL.Set_SQL_data("cell", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxCell.Text);
                SQL.Set_SQL_data("email", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", "'" + this.textBoxEmail.Text + "'");
                SQL.Set_SQL_data("code2", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxCode2.Text);
                SQL.Set_SQL_data("address_city", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.comboBoxCity.SelectedItem.ToString());
                SQL.Set_SQL_data("address_district", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.comboBoxDistrict.SelectedItem.ToString());
                SQL.Set_SQL_data("address_road", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxAddress.Text);
                SQL.Set_SQL_data("id", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxID.Text);
                SQL.Set_SQL_data("taxtitle", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxTaxTitle.Text);
                SQL.Set_SQL_data("businessitems", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxBusinessItem.Text);
                SQL.Set_SQL_data("other", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxOther.Text);
            }   
        }

        protected override void btnClear_Click(object sender, EventArgs e) 
        {
            DialogResult result = MessageBox.Show("確定要刪除廠商資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                SQL.NoHistoryDelete_SQL("vendor", "vendor_no = '" + this.textBoxVendor_No.Text + "'");

                vendors = SQL.Read1DArrayNoCondition_SQL_Data("vendor_no", "vendor");
                vendorCount = vendors.Length;
                --selectIndex;
                if (selectIndex >= 0)
                    LoadInformation(vendors[selectIndex]);
                else
                {
                    this.btnClear.Enabled = false;
                    selectIndex = 0;
                    textBoxVendor_No.Clear();
                    textBoxVendor_Name.Clear();
                    textBoxVendor_Abbre.Clear();
                    textBoxContact1.Clear();
                    textBoxContact2.Clear();
                    textBoxPhone1.Clear();
                    textBoxPhone2.Clear();
                    textBoxCell.Clear();
                    textBoxEmail.Clear();
                    textBoxFax.Clear();
                    textBoxCode2.Clear();
                    textBoxAddress.Clear();
                    textBoxID.Clear();
                    textBoxTaxTitle.Clear();
                    textBoxBusinessItem.Clear();
                    textBoxOther.Clear();
                }
            }
        }
    }
}
