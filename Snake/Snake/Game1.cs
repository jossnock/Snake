using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Vector2 ballPosition;
        float ballSpeed;
        int directionFacing; // 0 = East, 1 = North, 2 = West, 3 = South
        bool gameOver;

        Vector2 centrePosition;
        Texture2D gameOverTexture;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.PreferredBackBufferWidth = 1600;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 150f;
            directionFacing = 0;

            gameOver = false;
            centrePosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");
            gameOverTexture = Content.Load<Texture2D>("game_over_PNG56");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            
            var kstate = Keyboard.GetState();

            // Setting directionFacing:
            if (gameOver == false)
            {
                if (kstate.IsKeyDown(Keys.W))
                {
                    directionFacing = 1;
                }
                else if (kstate.IsKeyDown(Keys.S))
                {
                    directionFacing = 3;
                }
                else if (kstate.IsKeyDown(Keys.A))
                {
                    directionFacing = 2;
                }
                else if (kstate.IsKeyDown(Keys.D))
                {
                    directionFacing = 0;
                }
            }

            // Moving the snake depending on directionFacing (so snake always moves):
            switch (directionFacing)
            {
                case 0:
                    ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case 1:
                    ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case 2:
                    ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case 3:
                    ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }

            // directionFacing: 0 = East, 1 = North, 2 = West, 3 = South




            // Collisions with edges of screen:
            if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
            {
                ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
                gameOver = true;
            }
            else if (ballPosition.X < ballTexture.Width / 2)
            {
                ballPosition.X = ballTexture.Width / 2;
                gameOver = true;
            }
            
            if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
            {
                ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
                gameOver = true;
            }
            else if (ballPosition.Y < ballTexture.Height / 2)
            {
                ballPosition.Y = ballTexture.Height / 2;
                gameOver = true;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                ballTexture,
                ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
             );

            if (gameOver == true)
            {
                _spriteBatch.Draw(
                    gameOverTexture,
                    centrePosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(gameOverTexture.Width / 2, gameOverTexture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                 );
            }

            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
