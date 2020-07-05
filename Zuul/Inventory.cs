using System;

namespace Zuul
{
    public class Inventory
    {
        public Item[] slots= new Item[3];

        public int max_weight = 0;

        public Item look4Item(string itemname)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (itemname == slots[i].name)
                {
                    return slots[i];
                }
            }
            return null;
        }
        public Item findemptyslot()
        {
            for (int i = 0; i < slots.Length; i++)
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
