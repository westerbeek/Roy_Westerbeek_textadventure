﻿using System;

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
            inv.max_weight = 5;
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
