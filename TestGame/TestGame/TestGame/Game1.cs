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

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        Texture2D topBumper;
        Texture2D bottomBumper;

        Vector2 tBumperPos;
        Vector2 bBumperPos;


        Paddle p1 = new Paddle();
        Paddle p2 = new Paddle();
        Ball ball = new Ball();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
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

            p1.texture = Content.Load<Texture2D>("player1paddle");
            p2.texture = Content.Load<Texture2D>("player2paddle");
            topBumper = Content.Load<Texture2D>("topbumper");
            bottomBumper = Content.Load<Texture2D>("bottomBumper");
            ball.texture = Content.Load<Texture2D>("ball");

            p1.position.X = 50;
            p1.position.Y = graphics.GraphicsDevice.Viewport.Height / 2 - (p1.height / 2);
            p2.position.X = graphics.GraphicsDevice.Viewport.Width - 50 - p2.width;
            p2.position.Y = graphics.GraphicsDevice.Viewport.Height / 2 - (p2.height / 2);

            ball.position.Y = (768 / 2) - ball.height / 2;
            ball.position.X = (1024 / 2) - ball.width / 2;

            tBumperPos.Y = 0;
            tBumperPos.X = 0;

            bBumperPos.Y = 768 - 25;
            bBumperPos.X = 0;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            PlayerInput();
            p1.Update();
            p2.Update();
            ball.Update();

            if (CollidingWithPaddle1())
            {
                if (ball.movingDownLeft)
                {
                    ball.movingDownLeft = false;
                    ball.movingDownRight = true;
                }

                else if (ball.movingUpLeft)
                {
                    ball.movingUpLeft = false;
                    ball.movingDownRight = true;
                }

            }

            if (CollidingWithPaddle2())
            {
                if (ball.movingDownRight)
                {
                    ball.movingDownLeft = true;
                    ball.movingDownRight = false;
                }

                else if (ball.movingUpRight)
                {
                    ball.movingUpLeft = true;
                    ball.movingDownRight = false;
                }

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            p1.Draw(spriteBatch);
            p2.Draw(spriteBatch);

            spriteBatch.Draw(topBumper, tBumperPos, Color.White);
            spriteBatch.Draw(bottomBumper, bBumperPos, Color.White);

            ball.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void PlayerInput()
        {
            if (Keyboard.GetState(p1.pNumber).IsKeyDown(Keys.W))
            {
                p1.position.Y -= p1.speed;
            }

            else if (Keyboard.GetState(p1.pNumber).IsKeyDown(Keys.S))
            {
                p1.position.Y += p1.speed;
            }

            if (Keyboard.GetState(p2.pNumber).IsKeyDown(Keys.Up))
            {
                p2.position.Y -= p2.speed;
            }

            else if (Keyboard.GetState(p2.pNumber).IsKeyDown(Keys.Down))
            {
                p2.position.Y += p2.speed;
            }

        }

        public bool CollidingWithPaddle1()
        {
            if (ball.position.Y >= p1.position.Y && ball.position.X > p1.position.X && ball.position.X < (p1.position.X + p1.width) && ball.position.Y < (p1.position.Y + p1.height))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public bool CollidingWithPaddle2()
        {
            if (ball.position.Y >= p2.position.Y && ball.position.X > p2.position.X && ball.position.X < (p2.position.X + p2.width) && ball.position.Y < (p2.position.Y + p2.height))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
