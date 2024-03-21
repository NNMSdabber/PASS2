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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Store the game states
        public const byte MENU_STATE = 0;
        public const byte STAT_STATE = 1;
        public const byte GAME_STATE = 2;
        public const byte INFORMATION_STATE = 3;
        public const byte RESULTS_STATE = 4;
        public const byte SHOP_STATE = 5;
        public const byte EXIT_STATE = 6;

        //Store the max number of buffs
        private const int MAX_BUFFS = 4;

        //Store the number of levels in the game
        public const int MAX_LEVELS = 5;

        //Store graphics and spritebatch
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //use FileIO to store stats
        static StreamReader inFile;
        static StreamWriter outFile;

        //Variable for rng
        Random rng = new Random();

        //Store the screen width and height size
        int screenWidth;
        int screenHeight;

        //Store the current and previous mouse and keyboard state
        MouseState mouse;
        MouseState prevMouse;
        KeyboardState kb;
        KeyboardState prevKb;

        //Store the game state
        public static int gameState { get; set; }

        //Store the ground images
        Texture2D dirtImg;
        Texture2D grassImg;
        Texture2D stoneImg;

        //Store the mob images
        Texture2D villagerImg;
        Texture2D creeperImg;
        Texture2D skeletonImg;
        Texture2D pillagerImg;
        Texture2D endermanImg;
        Texture2D shieldImg;

        //Store the players image
        Texture2D playerImg;

        //Stores the freeze player image and rectangle
        Texture2D freezeEndermanImg;
        Rectangle freezeEndermanRect;

        //Store the images of the arrows
        Texture2D pArrowImg;
        Texture2D sArrowImg;

        //Store the image for button
        Texture2D buttonImg;

        //Stores the buff images for the game and shop in an array
        Texture2D[] buffShopImgs = new Texture2D[MAX_BUFFS];
        Texture2D[] buffGameImgs = new Texture2D[MAX_BUFFS];

        //Store the title images
        Texture2D menuTitle;
        Texture2D statsTitle;
        Texture2D shopTitle;

        //Store the background image
        Texture2D gameBackground;

        //Store the image for darkening the screen
        Texture2D darkImg;

        //Store the mobs within a list
        List<Mob> mobs = new List<Mob>();

        //Stores the levels within an array
        Level[] level = new Level[MAX_LEVELS];

        //Stores the current level
        int curLevel = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Create the screen width and height

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 640;

            //Set the mouse to visible
            IsMouseVisible = true;

            //Applys the graphics changes
            graphics.ApplyChanges();

            //Define the screen width and height
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the mob images
            villagerImg = Content.Load<Texture2D>("MinecraftImages/Sized/Villager_64");
            creeperImg = Content.Load<Texture2D>("MinecraftImages/Sized/Creeper_64");
            skeletonImg = Content.Load<Texture2D>("MinecraftImages/Sized/Skeleton_64");
            pillagerImg = Content.Load<Texture2D>("MinecraftImages/Sized/Pillager_64");
            endermanImg = Content.Load<Texture2D>("MinecraftImages/Sized/Enderman_64");
            shieldImg = Content.Load<Texture2D>("MinecraftImages/Sized/Shield_48");

            //Load the backround image
            dirtImg = Content.Load<Texture2D>("MinecraftImages/Sized/Dirt_64");
            grassImg = Content.Load<Texture2D>("MinecraftImages/Sized/Grass1_64");
            stoneImg = Content.Load<Texture2D>("MinecraftImages/Sized/Cobblestone_64");

            //Load the enderman image and the rectangle
            freezeEndermanImg = Content.Load<Texture2D>("MinecraftImages/Sized/Enderman_64");
            freezeEndermanRect = new Rectangle(freezeEndermanImg.Width / 2, freezeEndermanImg.Height / 2, freezeEndermanImg.Width * 9, freezeEndermanImg.Height * 9);

            //Load steve img 
            playerImg = Content.Load<Texture2D>("MinecraftImages/Sized/Steve_64");

            //Load the dark image
            darkImg = Content.Load<Texture2D>("MinecraftImages/Sized/BlankPixel");

            //Load the buttom image
            buttonImg = Content.Load<Texture2D>("MinecraftImages/Sized/Button");

            //Load the titles
            shopTitle = Content.Load<Texture2D>("MinecraftImages/Sized/ShopTitle");
            menuTitle = Content.Load<Texture2D>("MinecraftImages/Sized/Title");
            statsTitle = Content.Load<Texture2D>("MinecraftImages/Sized/StatsTitle");

            //Load the background
            gameBackground = Content.Load<Texture2D>("MinecraftImages/Sized/MenuBG3");

            //Load the arrow image for the player and skeleton
            pArrowImg = Content.Load<Texture2D>("MinecraftImages/Sized/ArrowUp");
            sArrowImg = Content.Load<Texture2D>("MinecraftImages/Sized/ArrowDown");

            //Load the buff images for the shop
            buffShopImgs[0] = Content.Load<Texture2D>("MinecraftImages/Sized/ShopSpeedBoost_300");
            buffShopImgs[1] = Content.Load<Texture2D>("MinecraftImages/Sized/ShopDamageBoost_300");
            buffShopImgs[2] = Content.Load<Texture2D>("MinecraftImages/Sized/ShopFireRateBoost_300");
            buffShopImgs[3] = Content.Load<Texture2D>("MinecraftImages/Sized/ShopPointsBoost_300");

            //Load the buff images for the game
            buffGameImgs[0] = Content.Load<Texture2D>("MinecraftImages/Sized/IconSpeed_32");
            buffGameImgs[1] = Content.Load<Texture2D>("MinecraftImages/Sized/IconDamage_32");
            buffGameImgs[2] = Content.Load<Texture2D>("MinecraftImages/Sized/IconFireRate_32");
            buffGameImgs[3] = Content.Load<Texture2D>("MinecraftImages/Sized/IconPoints_32");

            // TODO: use this.Content to load your game content here

            //mobs.Add(new Villager(new Rectangle(0 - villagerImg.Width, rng.Next(0, screenHeight - (villagerImg.Height * 2)), villagerImg.Width, villagerImg.Height), villagerImg, 10, 1, 10));
            mobs.Add(new Villager(new Rectangle(200, 200, villagerImg.Width, villagerImg.Height), villagerImg, 10, 1, 10));
            gameState = MENU_STATE;


            level[0] = new Level(dirtImg, grassImg, stoneImg, "Level1.txt",new Texture2D[10,10],new Rectangle[10,10]);
           // level[1] = new Level(dirtImg, grassImg, stoneImg, "Level2.txt",new Texture2D[10, 10], new Rectangle[10, 10]);
           // level[2] = new Level(dirtImg, grassImg, stoneImg, "Level3.txt", new Texture2D[10, 10], new Rectangle[10, 10]);
           // level[3] = new Level(dirtImg, grassImg, stoneImg, "Level4.txt", new Texture2D[10, 10], new Rectangle[10, 10]);
           // level[4] = new Level(dirtImg, grassImg, stoneImg, "Level5.txt", new Texture2D[10, 10], new Rectangle[10, 10]);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            //Draws the morbius
            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].DrawMob(spriteBatch);

            }

            //for (int i = 0; i < level.Length; i++)
            //{
              //  level[i].DrawLevel(spriteBatch);
            //}
            level[0].DrawLevel(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
