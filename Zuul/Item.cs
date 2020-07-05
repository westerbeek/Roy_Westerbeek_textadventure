using System;

namespace Zuul
{
    public class Item
    {
        public string name { get; set; }
        public int weight { get; set; }
        public string type { get; set; }

        public Item(string _name, int _weight, string _type)
        {
            this.name = _name;
            this.weight = _weight;
            this.type = _type;
        }
    }
}
