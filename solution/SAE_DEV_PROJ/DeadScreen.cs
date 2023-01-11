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

        private Texture2D _textureDeadScreen;
        private Texture2D _textureyouAreDead;
        private Texture2D _textureButtonMenu;
        private Texture2D _textureButtonMenuPressed;
        private MouseState _ms;

        private int _largeuryouAreDead = 600;

        public DeadScreen(Game1 game) : base(game)
        {
            _police = Content.Load<SpriteFont>("Font");

            _textureDeadScreen = Content.Load<Texture2D>("deadScreen");
            _textureyouAreDead = Content.Load<Texture2D>("youaredead");

            _textureButtonMenu = Content.Load<Texture2D>("boutonM");
            _textureButtonMenuPressed = Content.Load<Texture2D>("boutonM_pressed");

            _myGame = game;
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _ms = Mouse.GetState();
            _myGame._actif = false;
        }
        public override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();

            // FONDS 
            _spriteBatch.Draw(_textureDeadScreen, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_textureyouAreDead, new Vector2(Constantes._LARGEUR_FENETRE / 2 - _largeuryouAreDead / 2 - 170, 50), Color.White);

            // TEXTURES SI SOURIS PAR DESSUS 
            if (_myGame._hitboxBoutonMReplay.Contains(_ms.X, _ms.Y))
            {
                _spriteBatch.Draw(_textureButtonMenuPressed, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 300), Color.White);
            }
            else
            {
                _spriteBatch.Draw(_textureButtonMenu, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 300), Color.White);
            }
            if (_myGame._hitboxMenuButton.Contains(_ms.X, _ms.Y))
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

            // TEXTE 
            _spriteBatch.DrawString(_police, "Replay", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 340), Color.White);
            _spriteBatch.DrawString(_police, "Main Menu", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 70, 540), Color.White);
            _spriteBatch.DrawString(_police, "Quit", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 20, 740), Color.White);

            _spriteBatch.End();
        }
    }
}

