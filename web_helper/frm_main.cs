using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace web_helper
{

    public partial class frm_main : Form
    {
        Form current_form = new Form();
        public frm_main()
        {
            InitializeComponent();
        }

        public void SetMdiBackColor()
        {
            MdiClient ctlMDI = new MdiClient();
            int i = 0;
            for (i = 0; i <= this.Controls.Count; i++)
            {
                try
                {
                    ctlMDI = (MdiClient)this.Controls[i];
                    ctlMDI.BackColor = this.BackColor;
                }
                catch (Exception error)
                {

                }
            }
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            SetMdiBackColor();
        }

        public void set_window(Form frm)
        {
            foreach (Control control in panel_container.Controls)
            {
                if (control.Text == frm.Text)
                {
                    control.BringToFront();
                    current_form = (Form)frm;
                    frm.Dispose();
                    return;
                }
            }

            frm.TopLevel = false;
            frm.Dock = System.Windows.Forms.DockStyle.Fill;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.TopMost = true;
            this.panel_container.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
            current_form = (Form)frm;

            TreeNode node = new TreeNode();
            node.Text = frm.Text; 
            tree_menu.Nodes.Add(node);
        }

        private void mongoDBOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMongoDBOperate frm = new FrmMongoDBOperate();
            set_window((Form)frm);
        }
        private void mongoDBWebDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_mongo_doc_find frm = new frm_mongo_doc_find();
            frm.frm_main = this;
            set_window((Form)frm); 
        }
        private void htmlAnalyseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmHtmlAnalyse frm = new FrmHtmlAnalyse();
            set_window((Form)frm);
        }
        private void templateManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_template_manage frm = new frm_template_manage();
            frm.frm_main = this;
            set_window((Form)frm);
        }
        private void templateOperateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTemplateOperate frm = new FrmTemplateOperate();
            set_window((Form)frm);
        }
        private void downAllFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_html_download frm = new frm_html_download();
            set_window((Form)frm); 
        }
        private void logMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_tool_log_monitor frm = new frm_tool_log_monitor();
            set_window((Form)frm);
        }
        private void setFixedUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_url_fix_set frm = new frm_url_fix_set();
            set_window((Form)frm); 
        }
        private void selectFixedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_url_fix_pick frm = new frm_url_fix_pick();
            set_window((Form)frm);
        }
        private void codeConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_code_console frm = new frm_code_console();
            set_window((Form)frm);
        }
        private void tree_menu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string name = e.Node.Text;

            foreach (Control control in panel_container.Controls)
            {
                if (control.Text == name)
                {
                    control.BringToFront();
                    current_form = (Form)control;
                }
            }
        }

        private void tool_close_form_Click(object sender, EventArgs e)
        {
            if (tree_menu.Nodes.Count > 0)
            {
                TreeNode node_delete = new TreeNode();
                foreach (TreeNode node in tree_menu.Nodes)
                {
                    if (node.Text == current_form.Text)
                    {
                        node_delete = node;
                    }
                }

                Control control_delete = new Control();
                foreach (Control control in panel_container.Controls)
                {
                    if (control.Text == node_delete.Text)
                    {
                        control_delete = control;
                    }
                }

                tree_menu.Nodes.Remove(node_delete);
                panel_container.Controls.Remove(control_delete);
            }
        }

        public void open_frm_template_list(string template_id)
        {
            FrmTemplateOperate frm = new FrmTemplateOperate();
            frm.type = "update";
            frm.template_id = template_id;
            frm.Text = "[T]" + template_id + "";
            
            set_window(frm);
        }
        public void open_frm_doc_find_view(string doc_id)
        {
            FrmDocFindView frm = new FrmDocFindView();
            frm.doc_id = doc_id;
            frm.Text = "[D]" + doc_id + "";
            set_window(frm);
        }

        private void requestSocketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRequest_Socket frm = new FrmRequest_Socket();
            set_window((Form)frm);
        } 
        private void viewHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHexConvert frm = new FrmHexConvert();
            set_window((Form)frm);
        }

        private void autoFiexeURLPickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_url_fix_pick_timing frm = new frm_url_fix_pick_timing();
            set_window((Form)frm);
        }

        private void autoUrlPickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_url_free_pick frm = new frm_url_free_pick();
            set_window((Form)frm);
        } 
    }

}
