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

        private static CombatController instance;

        private CombatController() { }

        public static CombatController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CombatController();
                }
                return instance;
            }
        }

        public Player user;

        //don't call this anymore!
        //public CombatController(Player mainCharacter) 
        //{
        //    user = mainCharacter;
        //}


        //Call this instead to set the main character
        public void setMainCharacter(Player mainCharacter)
        {
            user = mainCharacter;
        }

        //Way you access CombatController 
        //CombatController combat = CombatController.Instance;
        ////Then you could call the setMainCharacter function defined above
        //combat.setMainCharacter(player);


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
            for (int k = 0; k < entities.Count; k++)
            {
                entities[k].takeDamage(damageFormula(checker, entities[k]));
            }
        }

        public void attack(NPC checker, Player entity) 
        {
            //check to see if NPC attacks Player
            if (checker.withinRange) 
            {
                confirmedHit(checker, entity);
            }
        }

        public int damageFormula(Character attacker, Character target) 
        {
            int damage=attacker.strength - target.strength;
            if (damage < 1) damage = 1;
            return damage;
        }
    }
}
