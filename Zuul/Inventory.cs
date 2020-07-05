using System;
using System.Collections.Generic;

namespace Zuul
{
    public class Inventory
    {
        public List<Item> slots = new List<Item>();
        public int max_weight = 0;


        public void InstanceAdd(Item item)
        {
            
                slots.Add(item);
              
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
            otherinv.findemptyslot().name = look4Item(itemname).name;
            emptyitem(look4Item(itemname));
            


        }
        public void emptyitem(Item item2empty)
        {
            item2empty.name = "";
            item2empty.weight = 0;
            item2empty.type = "";
        }
    }
}
