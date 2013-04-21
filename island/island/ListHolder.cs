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
    class ListHolder
    {
        private static ListHolder instance;

        private ListHolder() { }

        public static ListHolder Instance 
        {
            get 
            {
                if (instance == null) 
                {
                    instance = new ListHolder();
                }
                return instance;
            }
        }

        public Game1 game;
        public Player player;

        public List<NPC> NPCList;
        public List<Wall> WallList;

        public void setGame(Game1 NewGame) 
        {
            game = NewGame;
        }

        public void setPlayer(Player NewPlayer) 
        {
            player=NewPlayer;
        }

        public void setNPCList(List<NPC> NewNPCList) 
        {
            NPCList = NewNPCList;
        }

        public void setWallList(List<Wall> NewWallList) 
        {
            WallList = NewWallList;
        }

        public Player getPlayer() { return player; }
        public Game1 getGame() { return game; }
        public List<NPC> getNPCList() { return NPCList; }
        public List<Wall> getWallList() { return WallList; }

        public void addNPC(NPC NPCToAdd) 
        {
            NPCList.Add(NPCToAdd);
        }


    }
}
