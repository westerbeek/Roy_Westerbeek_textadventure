using System;

namespace Zuul
{
    public class Boulder : Item
    {
        public Boulder(string _name, string _description, int _weight, string _type) : base(_name,_description, _weight, _type)
        {
        }
        public override void Use()
        {
            Console.WriteLine("\nIt's a boulder.\nIt doesnt have a use.\n");
        }
    }
}
