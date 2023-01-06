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
        private Texture2D _textureChangerTouche;
        private Texture2D _texturePic1;
        private Texture2D _texturePic2;
        private Texture2D _texturePic3;

        public SettingScreen(Game1 game) : base(game)
        {

            _myGame = game;
          

        }
        public override void Initialize()
        {
            
            base.Initialize();
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _police = Content.Load<SpriteFont>("Font");
            _textureChangerTouche = Content.Load<Texture2D>("Bouton");
            _texturePic1 = Content.Load<Texture2D>("pic1");
            _texturePic2 = Content.Load<Texture2D>("pic2");
            _texturePic3 = Content.Load<Texture2D>("pic3");
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

            _spriteBatch.Draw(_textureChangerTouche, new Vector2(0, 100), Color.White);
            _spriteBatch.Draw(_textureChangerTouche, new Vector2(0, 200), Color.White);
            _spriteBatch.Draw(_textureChangerTouche, new Vector2(0, 300), Color.White);
            _spriteBatch.Draw(_textureChangerTouche, new Vector2(0, 400), Color.White);

            _spriteBatch.DrawString(_police, "Votre touche pour avancer : " + _myGame._forward, new Vector2(0, 108), Color.Black);
            _spriteBatch.DrawString(_police, "Votre touche pour aller a droite : " + _myGame._right, new Vector2(0, 208), Color.Black);
            _spriteBatch.DrawString(_police, "Votre touche pour aller a gauche : " + _myGame._left, new Vector2(0, 308), Color.Black);
            _spriteBatch.DrawString(_police, "Votre touche pour reculer: " + _myGame._behind, new Vector2(0, 408), Color.Black);

            _spriteBatch.Draw(_texturePic1, new Vector2(750, 50), Color.White);
            _spriteBatch.Draw(_texturePic2, new Vector2(1050, 50), Color.White);
            _spriteBatch.Draw(_texturePic3, new Vector2(1350, 50), Color.White);

            _spriteBatch.End();
        }
    }
}



