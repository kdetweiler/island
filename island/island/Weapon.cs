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
            Vector2 spot=target.position;
            for (int k = 0; k < sweetSpots.Length; k++) 
            { 
                
            }
            return false;
        }
    }
}
