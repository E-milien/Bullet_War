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
    public class ShopScreen : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Game1 _myGame;
        private SpriteFont _police;
        private Texture2D _textureFondWinScreen;
        private Texture2D _textureHeartFill;
        private Texture2D _textureHeartEmpty;

        private Rectangle _hitboxHeart1;
        private Rectangle _hitboxHeart2;

        private MouseState _ms;

        public ShopScreen(Game1 game) : base(game)
        {
            _myGame = game;

        }
        public override void Initialize()
        {
            Rectangle _hitboxHeart1 = new Rectangle(575,30,Constantes._TAILLEHEART, Constantes._TAILLEHEART);
            Rectangle _hitboxHeart2 = new Rectangle(700, 30, Constantes._TAILLEHEART, Constantes._TAILLEHEART);

            base.Initialize();
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _police = Content.Load<SpriteFont>("Font");
            _textureFondWinScreen = Content.Load<Texture2D>("fondWinScreen");
            _textureHeartFill = Content.Load<Texture2D>("heartFill");
            _textureHeartEmpty = Content.Load<Texture2D>("heartEmpty");


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _myGame._actif = false;
            _myGame._shopScreenOk = true;
        }
        public override void Draw(GameTime gameTime)
        {
            _ms = Mouse.GetState();

            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureFondWinScreen, new Vector2(0, 0), Color.White);

            _spriteBatch.DrawString(_police,"Ameliorations vaisseau",new Vector2(20, 10), Color.Red);
            _spriteBatch.DrawString(_police, "Augmenter le nombre de HP : ", new Vector2(10, 50), Color.White);
            _spriteBatch.Draw(_textureHeartFill, new Vector2(450, 30), Color.White);

            _spriteBatch.Draw(_textureHeartEmpty, new Vector2(575, 30), Color.White);
            _spriteBatch.Draw(_textureHeartEmpty, new Vector2(700, 30), Color.White);

            if(_hitboxHeart1.Contains(_ms.X, _ms.Y))
            {
                _spriteBatch.Draw(_textureHeartFill, new Vector2(575, 30), Color.White);
                Console.WriteLine("test");
            }

            _spriteBatch.End();
        }
    }
}



