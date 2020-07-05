using System;

namespace Zuul
{
    public class Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public int weight { get; set; }
        public string type { get; set; }
        public bool used;

        public Item(string _name, string _description, int _weight, string _type)
        {
            this.name = _name;
            this.description = _description;
            this.weight = _weight;
            this.type = _type;
        }
        public virtual void Use()
        {
            Console.WriteLine("Using all the uses that use me.\nYou shouldn't be seeing me");
        }
    }
}
