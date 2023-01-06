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
    public class SettingScreen : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Game1 _myGame;
        private SpriteFont _police;
        private Texture2D _textureLeaveButton;

        public SettingScreen(Game1 game) : base(game)
        {
            _police = Content.Load<SpriteFont>("Font");
            _myGame = game;

        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureLeaveButton = Content.Load<Texture2D>("Leave");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _myGame._actif = false;
            _myGame._settingOk = true;
            
        }
        public override void Draw(GameTime gameTime)
        {

            _myGame.GraphicsDevice.Clear(Color.Yellow); 
            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureLeaveButton, new Vector2(500, 600), Color.White);
            _spriteBatch.DrawString(_police, "Main menu", new Vector2(900, 660), Color.White);
            _spriteBatch.DrawString(_police, "Votre touche pour avancer : ", new Vector2(0, 100), Color.Black);
            _spriteBatch.End();
        }
    }
}



