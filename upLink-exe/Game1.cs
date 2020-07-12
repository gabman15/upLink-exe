using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using upLink_exe.GameObjects;

namespace upLink_exe
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        private Random rand;
        private int shake_timer;
        private int shake_amount;

        Room currentRoom;
        public int currentLevel;
        public int lives;
        

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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;

            rand = rand = new Random();

            LoadLevel(1);
            currentLevel = 1;
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

            AssetManager.Content = Content;

            AssetManager.LoadTexture("ball", "sprites\\ball", 1);
            AssetManager.LoadTexture("beach", "sprites\\beach", 1);
            AssetManager.LoadTexture("monaco", "sprites\\monaco", 1);
            AssetManager.LoadTexture("turtleFlare", "sprites\\turtleFlare", 1);
            AssetManager.LoadTexture("turtle", "sprites\\turtle", 1);
            AssetManager.LoadTexture("yara", "sprites\\yaraSprite", 23);
            AssetManager.LoadTexture("labFloor", "sprites\\labFloorTile",1);
            AssetManager.LoadTexture("labBench", "sprites\\labBench", 1);
            AssetManager.LoadTexture("redWire", "sprites\\redWire", 1);
            AssetManager.LoadTexture("blueWire", "sprites\\blueWire", 1);
            AssetManager.LoadTexture("greenWire", "sprites\\greenWire", 1);
            AssetManager.LoadTexture("orangeWire", "sprites\\orangeWire", 1);
            AssetManager.LoadTexture("redTerminal", "sprites\\redTerminal", 1);
            AssetManager.LoadTexture("blueTerminal", "sprites\\blueTerminal", 1);
            AssetManager.LoadTexture("greenTerminal", "sprites\\greenTerminal", 1);
            AssetManager.LoadTexture("orangeTerminal", "sprites\\orangeTerminal", 1);
            AssetManager.LoadTexture("wall", "sprites\\wall", 1);
            AssetManager.LoadTexture("grass", "sprites\\grass", 1);
            AssetManager.LoadTexture("tree", "sprites\\tree", 1);
            AssetManager.LoadTexture("rock", "sprites\\rock", 1);
            AssetManager.LoadTexture("saw", "sprites\\saw", 3);
            AssetManager.LoadTexture("door", "sprites\\door", 1);
            AssetManager.LoadTexture("post", "sprites\\post", 1);

            AssetManager.LoadSound("forestTheme", "sounds\\TreesForTheForest");
            AssetManager.LoadSound("overtureTheme", "sounds\\Overture");
            AssetManager.LoadSound("gameAfootTheme", "sounds\\GameIsAfoot");
            AssetManager.LoadSound("escapeTheme", "sounds\\Escape");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        public void LoadLevel(int levelnum)
        {
            currentLevel = levelnum;
            currentRoom?.Destroy();
            currentRoom = new Room(this);
            
            currentRoom.Load("test.txt");
            //currentRoom.Load("level" + levelnum + ".txt");
        }

        public void NextLevel()
        {
            LoadLevel(currentLevel + 1);
        }

        public void RestartLevel()
        {
            LoadLevel(currentLevel);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            var kstate = Keyboard.GetState();

            //THIS IS A TEST FOR SCREEN SHAKE
            if (kstate.IsKeyDown(Keys.F))
            {
                shake_timer = 5;
                shake_amount = 30;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kstate.IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.R))
                RestartLevel();
            currentRoom.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            //SCREEN SHAKE 
            float shakeX = 0;
            float shakeY = 0;

            if (shake_timer > 0)
            {
                float radius = rand.Next(1, shake_amount);
                int angle = rand.Next(0, 360);
                shakeX = (float)Math.Cos(angle) * radius;
                shakeY = (float)Math.Sin(angle) * radius;
            }
            
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, Matrix.CreateTranslation(shakeX, shakeY, 0));

            currentRoom.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
