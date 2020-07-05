using System;

namespace Zuul
{
    public class Potion : Item
    {
        public Potion(string _name, string _description, int _weight, string _type) : base(_name,_description, _weight, _type)
        {
        }
        public override void Use()
        {
            Console.WriteLine("\nYou drink the potion! \nIt turns out it was just water,\nStill refreshing though! you gain 25 health!");
        }
    }
}
