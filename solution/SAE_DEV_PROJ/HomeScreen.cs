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
        private Texture2D _texturePlayButton;
        private Texture2D _textureOptionButton;
        private Texture2D _textureLeaveButton;
        private Texture2D _textureShop;
        
        private Texture2D _textureButtonPressed;
        private Texture2D _textureButton;
        public Texture2D _boutonPlay;
        public Texture2D _boutonShop;
        public Texture2D _boutonSettings;
        public Texture2D _boutonQuit;

        public HomeScreen(Game1 game) : base(game)
        {
            _police = Content.Load<SpriteFont>("Font");
            _texturePlayButton = Content.Load<Texture2D>("Jouer");
            _textureOptionButton = Content.Load<Texture2D>("Option");
            _textureLeaveButton = Content.Load<Texture2D>("Leave");
            _textureShop = Content.Load<Texture2D>("Shop");
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
            _textureButton = Content.Load<Texture2D>("boutonM");
            _textureButtonPressed = Content.Load<Texture2D>("boutonM_pressed");
           
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _myGame._homeScreenOpen = true;
            _myGame._actif = true;
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_myGame._fondHome,new Vector2(0,0),Color.White);

            _spriteBatch.Draw(_textureButton, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 4 / 8-50), Color.White);
            _spriteBatch.DrawString(_police, "Click to start", new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2 + 115, Constantes._HAUTEUR_FENETRE * 4 / 8 - 15), Color.White);

            _spriteBatch.Draw(_textureButton, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 5 / 8 - 50), Color.White);
            _spriteBatch.DrawString(_police, "Boutique", new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2 + 140, Constantes._HAUTEUR_FENETRE * 5 / 8 - 15), Color.White);

            _spriteBatch.Draw(_textureButton, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 6 / 8 - 50), Color.White);
            _spriteBatch.DrawString(_police, "Options", new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2 + 140, Constantes._HAUTEUR_FENETRE * 6 / 8 - 15), Color.White);

            _spriteBatch.Draw(_textureButton, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 7 / 8 - 50), Color.White);
            _spriteBatch.DrawString(_police, "Click to quit", new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2 + 120, Constantes._HAUTEUR_FENETRE * 7 / 8 - 15), Color.White);

            _spriteBatch.Draw(_textureShop, new Vector2(50, 50), Color.White);

            _spriteBatch.End();
        }
    }
}