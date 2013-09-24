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

namespace TestGame
{
    class Ball : Microsoft.Xna.Framework.Game
    {
        public Vector2 position;
        public Texture2D texture;
        public int width, height;
        public float speed;
        public bool movingDownLeft, movingDownRight, movingUpLeft, movingUpRight;


        public Ball()
        {
            Content.RootDirectory = ("Content");
            speed = 6.0f;
            width = 20;
            height = 20;
            movingDownLeft = true;
            movingDownRight = false;
            movingUpLeft = false;
            movingUpRight = false;
            texture = null;
            position = Vector2.Zero;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update()
        {
            if (movingUpLeft)
            {
                position.Y -= speed;
                position.X -= speed;
            }

            if (movingDownLeft)
            {
                position.Y += speed;
                position.X -= speed;
            }

            if (movingUpRight)
            {
                position.Y -= speed;
                position.X += speed;
            }

            if (movingDownRight)
            {
                position.Y += speed;
                position.X += speed;
            }

            if (movingUpLeft && position.Y <= 0 + 25)
            {
                movingUpLeft = false;
                movingDownLeft = true;
            }

            else if (movingDownLeft && position.X <= 0)
            {
                movingDownLeft = false;
                movingDownRight = true;
            }

            else if (movingUpLeft && position.X <= 0)
            {
                movingUpLeft = false;
                movingUpRight = true;
            }

            else if (movingDownLeft && position.Y >= 768 - 45)
            {
                movingUpLeft = true;
                movingDownLeft = false;
            }

            else if (movingDownRight && position.X >= 1024 - width)
            {
                movingDownLeft = true;
                movingDownRight = false;
            }

            else if (movingUpRight && position.Y <= 0 + 25)
            {
                movingDownRight = true;
                movingUpRight = false;
            }

            else if (movingDownRight && position.Y >= 768 - 45)
            {
                movingUpRight = true;
                movingDownRight = false;
            }

            else if (movingUpRight && position.X >= 1024 - width)
            {
                movingUpLeft = true;
                movingUpRight = false;
            }
        }
    }
}
