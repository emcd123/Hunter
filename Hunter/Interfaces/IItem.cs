using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Interfaces
{
    public interface I_Item
    {
        string DamageString { get; set; }
        int Attack { get; set; }
        int Awareness { get; set; }
        int Defense { get; set; }
        int Gold { get; set; }
        int Health { get; set; }
        string Name { get; set; }
        int Speed { get; set; }
    }
}
