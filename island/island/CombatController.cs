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
            int dealt = attacker.strength - defender.strength;
            if (dealt < 1) dealt = 1;
            defender.takeDamage(dealt);
        }

        public static void hit(Character attacker, Character defender) 
        { 

        }

        //this method will not look like this at all. just an idea infrastructure
        public void attack(Player checker, List<Character> entities) 
        { 
            int index=-1;
            //have Character have a method that checks if it attacked anything and return an index indicating which entity. -1 if it misses
            if (index != -1) 
            {
                entities[index].takeDamage(checker.strength-entities[index].strength);
            }
        }

        public void attack(NPC checker, Player entity) 
        {
            //check to see if NPC attacks Player
            Rectangle playerPosition = entity.rectangle;
        }
    }
}
