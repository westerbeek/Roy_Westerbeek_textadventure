using System;

namespace Zuul
{
    public class Key : Item
    {
        public Key(string _name, string _description, int _weight, string _type) : base(_name,_description, _weight, _type)
        {
        }
        public override void Use()
        {
            Console.WriteLine("\nyou used the key to open the door to the roof!\n");
        }
    }
}
