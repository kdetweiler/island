using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace island
{
    class Weapon
    {
        public String name;
        public Sensor range;

        public int[] sweetSpots;
        

        public Weapon(String n, Sensor r, int[] sweet) 
        {
            name = n;
            range = r;
            sweetSpots = sweet;
        }

        public Boolean inSweetSpot(NPC target) 
        {

            for (int k = 0; k < sweetSpots.Length; k++) 
            { 
                
            }
            return false;
        }
    }
}
