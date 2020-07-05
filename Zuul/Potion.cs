using System;

namespace Zuul
{
    public class Potion : Item
    {
        public Potion(string _name, int _weight, string _type) : base(_name, _weight, _type)
        {
        }
        public void Use()
        {
            Console.WriteLine("You drink the potion!");
        }
    }
}
