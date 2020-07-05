using System;
using System.Collections.Generic;

namespace Zuul
{
    public class Inventory
    {
        public List<Item> slots = new List<Item>();
        public int max_weight = 0;
        public bool overweight;

        public void InstanceAdd(Item item)
        {
            
                slots.Add(item);
              
        }

       
        public void checkweight()
        {
            int weight = 0;
            for (int i = slots.Count - 1; i >= 0; i--)
            {
                weight += slots[i].weight;
            }
            if(weight > max_weight)
            {
                overweight = true;
                Console.WriteLine("\nYou are carrying too much!\nYou should 'drop' something.\n");
            }
            else
            {
                overweight = false;
            }
        }
        public Item look4Item(string itemname)
        {
            for (int i = slots.Count - 1; i >= 0; i--)
            {

                if (itemname == slots[i].name && slots[i] != null)
                {
                    return slots[i];
                }
                else
                {
                    Console.WriteLine("something went wrong with" + slots[i].name);

                }
            }
            return null;
        }
        public Item findemptyslot()
        {
            for (int i = slots.Count - 1; i >= 0; i--)
            {
                if (slots[i].name == null || slots[i].name == "")
                {
                    return slots[i];
                }
            }
            return null;
        }
        public void Switchitems(Inventory otherinv, string itemname)
        {
            otherinv.slots.Add(look4Item(itemname));
            this.slots.Remove(look4Item(itemname));
            otherinv.checkweight();
            checkweight();
        }
    
    }
}
