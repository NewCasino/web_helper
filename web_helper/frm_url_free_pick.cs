using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace web_helper
{
    public partial class frm_url_free_pick : Form
    {
        public frm_url_free_pick()
        {
            InitializeComponent();
        }

        private void btn_pick_Click(object sender, EventArgs e)
        {
            if (cb_local.Checked)
            {
                this.txt_result.Text = AutoPickHelper.select_data_from_local(this.txt_local.Text);
            }
            else
            {
                this.txt_result.Text = AutoPickHelper.select_data_from_url(this.txt_url.Text);
            }
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        private void btn_pick_to_tree_Click(object sender, EventArgs e)
        {
            this.tree_result.Nodes.Clear();
            if (cb_local.Checked)
            {
                AutoPickHelper.show_tree_from_local(this.txt_local.Text, ref this.tree_result);
            }
            else
            {
                AutoPickHelper.show_tree_from_url(this.txt_url.Text, ref this.tree_result);
            }
            this.tree_result.ExpandAll();
        }

        private void btn_pick_to_table_Click(object sender, EventArgs e)
        {
            TreeTableAndList result = new TreeTableAndList();
            if (cb_local.Checked)
            {
                result = AutoPickHelper.get_tree_table_from_local(this.txt_local.Text);
            }
            else
            {
                result = AutoPickHelper.get_tree_table_from_url(this.txt_url.Text);
            }

            this.dgv_tree.DataSource = result.table;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.list.Count; i++)
            {
                sb.Append(i.ToString() + "["+((ArrayList)result.list[i]).Count +"]:  ");
                foreach (int item in (ArrayList)result.list[i])
                {
                    sb.Append(item.ToString() + "  ");
                }
                sb.Append(Environment.NewLine);
            }
            this.txt_result.Text = sb.ToString();
        } 
    }
}
