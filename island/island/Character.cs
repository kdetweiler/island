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

        public int health=100;
        public int maxHealth=100;

        public int strength=10;
        public int defense=5;

        public List<Skill> SkillList=new List<Skill>();

        public static Boolean isAlive;
        public Boolean isHostile;



        public Character()
        {

        }

        public void takeDamage(int damage)
        {
            health -= damage;
        }

        public void healDamage(int healing)
        {
            health += healing;
            if (health > maxHealth) health = maxHealth;
        }

        public static void die() 
        {
            isAlive = false;
            //do a death animation
        }

        public Boolean doesHit(Character target) 
        {
            
            //check target's distance, check your attack range. see if intersects; if so return true
            return false;
        }

        public Boolean lives() { return isAlive; }

        public Boolean hostile() { return isHostile; }

        public virtual int deduceAttackPower() { return strength; }

        public void addSkill(Skill e) 
        {
            SkillList.Add(e);
        }
    }
}
