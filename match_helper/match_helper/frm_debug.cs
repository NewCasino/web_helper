using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


public partial class frm_debug : Form
{
    public frm_debug()
    {
        InitializeComponent();
    }
    public frm_debug(string msg)
    {
        InitializeComponent();
        this.txt_result.Text = msg;
        this.txt_mark.Focus();
        this.txt_mark.SelectAll();
        this.txt_mark.Text = "";
         
    }
    //private void txt_result_TextChanged(object sender, EventArgs e)
    //{
    //    this.txt_result.SelectionStart = this.txt_result.TextLength;
    //    this.txt_result.ScrollToCaret();
    //}
}
