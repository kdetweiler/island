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
        public int range;

        public int delay = 100;

        public int[] sweetSpots;
        

        public Weapon(String n, int r, int[] sweet) 
        {
            name = n;
            range = r;
            sweetSpots = sweet;
        }

        public Boolean inSweetSpot(NPC target) 
        {
            
            return false;
        }
    }
}
