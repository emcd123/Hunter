using Hunter.Equipments;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Interfaces
{
    public interface IStorable
    {
        void PickUpItem(List<Item> item_list, Item selected_item);
        void DropItem(List<Item> item_list, Item selected_item);
        void UseItem(Item selected_item);
        void EquipItem(Item selected_item);
        void UnequipItem(Item selected_item);
        //void AddToMap(List<Item> inventory, List<Item> item_list);
        //void RemoveFromMap(List<Item> inventory, List<Item> item_list);
        void DrawEquipped(RLConsole equipConsole);
        void DrawInventory();
    }
}
