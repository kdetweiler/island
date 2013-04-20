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
    class Items
    {
        public static List<Obj> objList = new List<Obj>();

        public static void Initialize()
        {
            for (int i = 0; i < 64; i++)
            {
                Obj o = new Magic(new Vector2(0,0));
                o.alive = false;
                objList.Add(o);
            }

            objList.Add(new Player(new Vector2(50, 50), "Player1"));
        }

        public static void Reset()
        {
            foreach(Obj o in objList)
            {
                o.alive = false;
            }
        }
    }
}
