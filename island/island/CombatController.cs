using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace island
{
    class CombatController
    {

        public Player user;

        public CombatController(Player mainCharacter) 
        {
            user = mainCharacter;
        }
    
        public void confirmedHit(Character attacker, Character defender) 
        {
            defender.takeDamage(damageFormula(attacker,defender));
        }

        public static void hit(Character attacker, Character defender) 
        { 

        }

        //this method will not look like this at all. just an idea infrastructure
        public void attack(Player checker, List<NPC> entities) 
        { 
            List<int> hitEnemies = checker.attack(entities);
            for (int k = 0; k < hitEnemies.Count; k++) 
            { 
                int index=hitEnemies[k];
                entities[index].takeDamage(damageFormula(checker, entities[index]));
            }
        }

        public void attack(NPC checker, Player entity) 
        {
            //check to see if NPC attacks Player
            Rectangle playerPosition = entity.rectangle;
        }

        public int damageFormula(Character attacker, Character target) 
        {
            int damage=attacker.strength - target.strength;
            if (damage < 1) damage = 1;
            return damage;
        }
    }
}
