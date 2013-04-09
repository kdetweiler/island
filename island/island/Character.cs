using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace island
{
    class Character : Entity
    {

        public int health;
        public int maxHealth;

        public int strength;
        public int defense;

        public Boolean isAlive;

        public Boolean isHostile;

        public Character()
        {

        }

        public void takeDamage(int damage)
        {
            health -= damage;
            if (isHostile == false) isHostile = true;
            if (health < 1) isAlive = false;
        }

        public void healDamage(int healing)
        {
            health += healing;
            if (health > maxHealth) health = maxHealth;
        }

        public void attack() { }

        public Boolean lives() { return isAlive; }

        public Boolean hostile() { return isHostile; }
    }
}
