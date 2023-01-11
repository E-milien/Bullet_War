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
    public class HomeScreen : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Game1 _myGame;
        private SpriteFont _police;
        private MouseState _ms;


        public HomeScreen(Game1 game) : base(game)
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _myGame = game;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContent()
        {
            _police = Content.Load<SpriteFont>("Font");

           
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _ms = Mouse.GetState();

            _myGame._screenDeathOk = false;
            _myGame._screenWinOk = false;
            _myGame._settingOk = false;
            _myGame._shopScreenOk = false;
            _myGame._playScreenOk = false;
            _myGame._pause = false;
            _myGame._homescreenOk = true;
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_myGame._fondHome,new Vector2(0,0),Color.White);

            _spriteBatch.Draw(_myGame._boutonPlay, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 4 / 8-50), Color.White);
            _spriteBatch.DrawString(_police, "Click to start", new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2 + 115, Constantes._HAUTEUR_FENETRE * 4 / 8 - 15), Color.White);

            _spriteBatch.Draw(_myGame._boutonShop, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 5 / 8 - 50), Color.White);
            _spriteBatch.DrawString(_police, "Boutique", new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2 + 140, Constantes._HAUTEUR_FENETRE * 5 / 8 - 15), Color.White);

            _spriteBatch.Draw(_myGame._boutonSettings, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 6 / 8 - 50), Color.White);
            _spriteBatch.DrawString(_police, "Options", new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2 + 140, Constantes._HAUTEUR_FENETRE * 6 / 8 - 15), Color.White);

            _spriteBatch.Draw(_myGame._boutonQuit, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 7 / 8 - 50), Color.White);
            _spriteBatch.DrawString(_police, "Click to quit", new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2 + 120, Constantes._HAUTEUR_FENETRE * 7 / 8 - 15), Color.White);

            _spriteBatch.End();
        }
    }
}