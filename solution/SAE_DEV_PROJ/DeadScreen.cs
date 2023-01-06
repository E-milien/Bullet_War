using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace SAE_DEV_PROJ
{
    public class DeadScreen : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Game1 _myGame;
        private SpriteFont _police;
        private Texture2D _textureRePlayButton;
        private Texture2D _textureOptionButton;
        private Texture2D _textureLeaveButton;

        public DeadScreen(Game1 game) : base(game)
        {
            _police = Content.Load<SpriteFont>("Font");

            _textureRePlayButton = Content.Load<Texture2D>("Jouer");
            _textureOptionButton = Content.Load<Texture2D>("Option");
            _textureLeaveButton = Content.Load<Texture2D>("Leave");

            _myGame = game;
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _myGame._actif = false;
        }
        public override void Draw(GameTime gameTime)
        {

            _myGame.GraphicsDevice.Clear(Color.DarkRed);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureRePlayButton, new Vector2(500, 200), Color.White);
            _spriteBatch.DrawString(_police, "Click to start", new Vector2(900, 260), Color.White);

            _spriteBatch.Draw(_textureOptionButton, new Vector2(500, 400), Color.White);
            _spriteBatch.DrawString(_police, "Main menu", new Vector2(910, 460), Color.White);

            _spriteBatch.Draw(_textureLeaveButton, new Vector2(500, 600), Color.White);
            _spriteBatch.DrawString(_police, "Click to quit", new Vector2(900, 660), Color.White);
            _spriteBatch.End();
        }
    }
}

