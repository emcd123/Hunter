using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Interfaces
{
    public interface IEquipment
    {
        bool IsEquippable { get; set; }
        bool IsEquipped { get; set; }
    }
}
