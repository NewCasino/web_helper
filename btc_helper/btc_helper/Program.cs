using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace btc_helper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (Environment.UserName == "CY120467") SQLServerHelper.str_con = "Data Source=.;Initial Catalog=btce;Integrated Security=True";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_single_btcchina());
        }
    }
}
