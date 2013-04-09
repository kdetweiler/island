using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace island
{
    class CombatController
    {

        Player user;

        public CombatController(Player mainCharacter) 
        {
            user = mainCharacter;
        }
    
        public void confirmHit(Character attacker, Character defender) 
        {
            int dealt = attacker.strength - defender.strength;
            if (dealt < 1) dealt = 1;
            defender.takeDamage(dealt);
        }

        //this method will not look like this at all. just an idea infrastructure
        public void NPCAction(NPC actor) 
        {
            if (actor.hostile())
            {
                //put seek player here
                //if player is in range, attack
                confirmHit(actor, user);
            }
            else 
            { 
                //continue whatever it was doing
            }
        }
    }
}
