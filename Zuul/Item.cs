using System;

namespace Zuul
{
    public class Item
    {
        public string name;
        public int weight;
        public string type;

        public Item(string _name, int _weight, string _type)
        {
            this.name = _name;
            this.weight = _weight;
            this.type = _type;
        }
    }
}
