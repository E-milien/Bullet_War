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
    public class WinScreen : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Game1 _myGame;
        private SpriteFont _police;

        private Texture2D _textureRePlayButton;
        private Texture2D _textureOptionButton;
        private Texture2D _textureLeaveButton;
        private Texture2D _textureButtonMenu;
        private Texture2D _textureButtonMenuPressed;
        private Texture2D _textureFondWinScreen;
        private Texture2D _textureWin;

        private MouseState _ms;

        private int _largeurImage = 1000;

        public WinScreen(Game1 game) : base(game)
        {
            _police = Content.Load<SpriteFont>("Font");
            _myGame = game;

        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureRePlayButton = Content.Load<Texture2D>("Jouer");
            _textureOptionButton = Content.Load<Texture2D>("Option");
            _textureLeaveButton = Content.Load<Texture2D>("Leave");

            _textureButtonMenu = Content.Load<Texture2D>("boutonM");
            _textureButtonMenuPressed = Content.Load<Texture2D>("boutonM_pressed");
            _textureFondWinScreen = Content.Load<Texture2D>("fondWinScreen");
            _textureWin = Content.Load<Texture2D>("win");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _myGame._actif = false;

        }
        public override void Draw(GameTime gameTime)
        {
            _ms = Mouse.GetState();
            _spriteBatch.Begin();
        
            _spriteBatch.Draw(_textureFondWinScreen, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_textureWin, new Vector2(Constantes._LARGEUR_FENETRE / 2 - _largeurImage / 2, 0), Color.White);

            // TEXTURES SI SOURIS PAR DESSUS 
            if (_myGame._hitboxReplayWinScreen.Contains(_ms.X, _ms.Y))
            {
                _spriteBatch.Draw(_textureButtonMenuPressed, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 300), Color.White);
            }
            else
            {
                _spriteBatch.Draw(_textureButtonMenu, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 300), Color.White);
            }
            if (_myGame._hitboxMainMenuWinScreen.Contains(_ms.X, _ms.Y))
            {
                _spriteBatch.Draw(_textureButtonMenuPressed, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 500), Color.White);
            }
            else
            {
                _spriteBatch.Draw(_textureButtonMenu, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 500), Color.White);
            }
            if (_myGame._hitboxExitButton.Contains(_ms.X, _ms.Y))
            {
                _spriteBatch.Draw(_textureButtonMenuPressed, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 700), Color.White);
            }
            else
            {
                _spriteBatch.Draw(_textureButtonMenu, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 700), Color.White);
            }

            // TEXTES 
            _spriteBatch.DrawString(_police, "Replay", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 340), Color.White);
            _spriteBatch.DrawString(_police, "Main Menu", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 70, 540), Color.White);
            _spriteBatch.DrawString(_police, "Quit", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 20, 740), Color.White);


            _spriteBatch.End();
        }
    }
}



