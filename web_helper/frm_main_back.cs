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
    public partial class frm_main_back : Form
    {
        public frm_main_back()
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

        private void mongoDBOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMongoDBOperate frm = new FrmMongoDBOperate();
            frm.MdiParent = this;
            frm.Show();
        }

        private void htmlAnalyseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmHtmlAnalyse frm = new FrmHtmlAnalyse();
            frm.Show();
        }

        private void templateManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_template_manage frm = new frm_template_manage();
            frm.Show();
        }

        private void templateOperateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTemplateOperate frm = new FrmTemplateOperate();
            frm.Show();
        } 

        private void downAllFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_html_download frm = new frm_html_download();
            frm.Show();
        }

        private void logMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_tool_log_monitor frm = new frm_tool_log_monitor();
            frm.Show();
        }

        private void setFixedUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_url_fix_set frm = new frm_url_fix_set();
            frm.Show();

        }

        private void selectFixedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_url_fix_pick_timing frm = new frm_url_fix_pick_timing();
            frm.Show();
        }

        private void codeConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_code_console frm = new frm_code_console();
            frm.Show();
        }

        private void mongoDBWebDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_mongo_doc_find frm = new frm_mongo_doc_find();
            frm.Show();
        }

 
 
 
    }

}
