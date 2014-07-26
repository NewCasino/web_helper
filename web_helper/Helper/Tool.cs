using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;


class Tool
{
    public static string get_format_str(DataTable dt)
    { 
        StringBuilder sb = new StringBuilder();
        foreach (DataColumn column in dt.Columns)
        {
            sb.Append(column.ColumnName.PR(20));
        }
        sb.Append(Environment.NewLine);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                sb.Append(dt.Rows[i][j].PR(20));
            }
            sb.Append(Environment.NewLine);
        }

        return sb.ToString();
    }
}
