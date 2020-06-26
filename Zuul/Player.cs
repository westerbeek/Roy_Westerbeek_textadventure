using System;

namespace Zuul
{
    public class Player
    {
        public Inventory inv;
        public int health;
        private int maxhealth;
        public Room currentRoom;
        public bool dead;

        public void setstats()
        {
            maxhealth = 100;
            health = maxhealth;
            inv = new Inventory();
            inv.slots = new Item[5];
            inv.slots[0] = new Item();
            inv.slots[1] = new Item();
            inv.slots[2] = new Item();
            inv.slots[3] = new Item();
            inv.slots[4] = new Item();
        }

        public void heal(int _amountheal)
        {
            health += _amountheal;

            if (health >= maxhealth)
            {
                health = maxhealth;
            }
        }

        public void damage(int _amountdmg)
        {
            health -= _amountdmg;
            isAlive();
        }
        public void isAlive()
        {
            if (health <= 0)
            {
                dead = true;
            }

        }
    }
}
