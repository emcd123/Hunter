using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Interfaces
{
    public interface IEquipment
    {
        int Attack { get; set; }
        int Defense { get; set; }
        string Name { get; set; }
    }
}
