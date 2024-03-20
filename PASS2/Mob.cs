using GameUtility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PASS2
{
    class Mob
    {
        //Store img and rectangle
        protected Rectangle destRec;
        protected Texture2D mobImg;

        //Store health and score
        protected int scorePerMob;
        protected int health;

        //Store speed of mob
        protected int speed;

        public Mob(Rectangle destRec,Texture2D mobImg,int scorePerMob, int health, int speed)
        {
            this.destRec = destRec;
            this.mobImg = mobImg;
            this.scorePerMob = scorePerMob;
            this.health = health;
            this.speed = speed;

        }

        public virtual void DrawMob(SpriteBatch spriteBatch)
        {
            //Draws the mob on screen
            spriteBatch.Draw(mobImg, destRec, Color.White);
        }
    }
}
