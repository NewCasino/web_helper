using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class wsConfigHelper
{
    public string sid { get; set; }
    public List<string> upgrades { get; set; }
    public int pingInterval { get; set; }
    public int pingTimeout { get; set; }
}
