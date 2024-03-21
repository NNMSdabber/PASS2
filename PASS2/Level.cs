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
    class Level
    {
        //Declare the current level
        private string level;

        static StreamReader inFile;
        static StreamWriter outFile;

        private Texture2D[,] backgroundImgs;
        private Rectangle[,] backgroundRecs;

        private Texture2D dirtImg;
        private Texture2D grassImg;
        private Texture2D stoneImg;


        public Level(Texture2D dirtImg, Texture2D grassImg, Texture2D stoneImg, string level, Texture2D[,] backgroundImgs, Rectangle[,] backgroundRecs)
        {
            this.dirtImg = dirtImg;
            this.grassImg = grassImg;
            this.stoneImg = stoneImg;
            this.level = level;
            this.backgroundImgs = backgroundImgs;
            this.backgroundRecs = backgroundRecs;

            SetUpLevel();
        }

        public void SetUpLevel()
        {
            try
            {
                string[] data;

                inFile = File.OpenText(level);


                //Loop through each grid row of the map
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(inFile == null);

                    //Set the value of each tile
                    data = inFile.ReadLine().Split(',');

                    //Loop through each grid colomn of the map
                    for (int j = 0; j < 10; j++)
                    {
                        switch (data[j])
                        {
                            case "0":
                                backgroundImgs[i, j] = dirtImg;
                                break;
                            case "1":
                                backgroundImgs[i, j] = grassImg;
                                break;

                            case "2":
                                backgroundImgs[i, j] = stoneImg;
                                break;
                        }

                        backgroundRecs[i, j] = new Rectangle(0+stoneImg.Width*i,0+stoneImg.Width*j,stoneImg.Width,stoneImg.Height);

                    }
                }

                //Close the level file
                inFile.Close();


            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe);

            }
            catch(FormatException fe)
            {
                Console.WriteLine(fe);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DrawLevel(SpriteBatch spriteBatch)
        {
            //Loop through each grid row of the map
            for (int i = 0; i < 10; i++)
            {
                //Loop through each grid colomn of the map
                for (int j = 0; j < 10; j++)
                {
                    spriteBatch.Draw(backgroundImgs[i,j],backgroundRecs[i,j],Color.White);
                }
            }
        }
    }

        

}
