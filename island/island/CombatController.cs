using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace island
{
    class CombatController
    {
        public CombatController() { }
    
        public void confirmHit(Character attacker, Character defender) 
        {
            int dealt = attacker.strength - defender.strength;
            if (dealt < 1) dealt = 1;
            defender.takeDamage(dealt);
        }
    }
}
