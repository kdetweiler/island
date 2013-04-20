using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void attack(List<Character> entities, Character checker) 
        { 
            int index=-1;
            //have Character have a method that checks if it attacked anything and return an index indicating which entity. -1 if it misses
            if (index != -1) 
            {
                entities[index].takeDamage(checker.strength-entities[index].strength);
            }
        }
    }
}
