using Hunter.Equipments;
using Hunter.Interfaces;
using RLNET;
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
        
        // Used when initialising the Player Object
        public static Inventory Empty()
        {
            return new Inventory() { _inventory = new List<Item>() };
        }

        // When a player object already exists this is called, i.e on restart after death
        public void Reset()
        {
            _inventory = new List<Item>();
        }

        // Meant for adding an item to the inventory when it does not require removal from the map
        public void AddItem(Item selected_item)
        {
            _inventory.Add(selected_item);
        }

        // For taking an item off the map floor and adding to inventory
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
            // This kind of item is not created yet
            // This is a placeholder
        }

        public void EquipItem(Item selected_item)
        {
            if(selected_item.IsEquippable)
                selected_item.IsEquipped = true;
        }

        public void UnequipItem(Item selected_item)
        {
            if (selected_item.IsEquippable)
                selected_item.IsEquipped = false;
        }

        private void AddToMap(List<Item> item_list, Item selected_item)
        {
            item_list.Add(selected_item);
        }

        private void RemoveFromMap(List<Item> item_list, Item selected_item)
        {
            item_list.Remove(selected_item);
        }

        public void DrawEquipped(RLConsole equipConsole)
        {
            int y = 1;
            foreach (Item item in _inventory) {
                if (item.IsEquippable && item.IsEquipped)
                {
                    equipConsole.Print(1, y, $"{item.Name} {item.DamageString}", Colors.Text);
                    y += 2;
                }
            }
        }

        public void DrawInventory()
        {

        }
    }
}
