using Hunter.Equipments;
using Hunter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Core
{
    public class Inventory: IStorable
    {
        public List<Item> _inventory;
        
        public static Inventory Empty()
        {
            return new Inventory() { _inventory = new List<Item>() };
        }

        public void Reset()
        {
            _inventory = new List<Item>();
        }

        public void AddItem(Item selected_item)
        {
            _inventory.Add(selected_item);
        }

        public void PickUpItem(List<Item> item_list, Item selected_item)
        {
            _inventory.Add(selected_item);
            RemoveFromMap(item_list, selected_item);
        }

        public void DropItem(List<Item> item_list, Item selected_item)
        {
            _inventory.Remove(selected_item);
            AddToMap(item_list, selected_item);
        }

        public void UseItem(Item selected_item)
        {

        }

        public void EquipItem(Item selected_item)
        {

        }

        public void UnequipItem(Item selected_item)
        {

        }

        private void AddToMap(List<Item> item_list, Item selected_item)
        {

        }

        private void RemoveFromMap(List<Item> item_list, Item selected_item)
        {

        }

        public void DrawEquipped()
        {

        }

        public void DrawInventory()
        {

        }
    }
}
