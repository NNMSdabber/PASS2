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
    class Villager : Mob
    {
        public Villager(Rectangle destRec, Texture2D mobImg, int scorePerMob, int health, int maxSpeed) : base(destRec, mobImg, scorePerMob, health, maxSpeed)
        {

        }
    }
}
