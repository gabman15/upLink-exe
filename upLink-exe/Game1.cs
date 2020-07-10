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

        private List<Component> _components;
        private GameObject background;

        private Player _player;

        public static int ScreenHeight;

        public static int ScreenWidth;

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
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;

            rand = rand = new Random();

            ScreenHeight = graphics.PreferredBackBufferHeight;

            ScreenWidth = graphics.PreferredBackBufferWidth;

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

            background = new GameObject(Content.Load<Texture2D>("Background"));

            _player = new Player(Content.Load<Texture2D>("Player"));

            _components = new List<Component>()
      {
         _player,
         new GameObject(Content.Load<Texture2D>("NPC")),

      };
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

            var kstate = Keyboard.GetState();

            //THIS IS A TEST FOR SCREEN SHAKE
            if (kstate.IsKeyDown(Keys.F))
            {
                shake_timer = 5;
                shake_amount = 30;
            }

            foreach (var component in _components)
                component.Update(gameTime);

            if (shake_timer > 0)
            {
                shake_timer--;
            }

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

            spriteBatch.Begin(SpriteSortMode.Texture, null, null, null, null, null, Matrix.CreateTranslation(shakeX, shakeY, 0));

            background.Draw(gameTime, spriteBatch);

            foreach (var component in _components) {
                //Console.WriteLine("PRINT TEST " + component);
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
