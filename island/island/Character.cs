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

        public List<Skill> SkillList=new List<Skill>();

        public static Boolean isAlive;
        public Boolean isHostile;



        public Character()
        {

        }

        public void takeDamage(int damage)
        {
            health -= damage;
            if (isHostile == false) isHostile = true;
            if (health < 1) die();
        }

        public void healDamage(int healing)
        {
            health += healing;
            if (health > maxHealth) health = maxHealth;
        }

        public override static void die() 
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
