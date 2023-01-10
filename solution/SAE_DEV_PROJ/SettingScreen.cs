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
        private Texture2D _textureLeaveButtonPressed;
        private Texture2D _textureChangerTouche;
        private Texture2D _texturePic1;
        private Texture2D _texturePic2;
        private Texture2D _texturePic3;
        private Texture2D _texturePic4;
        private Texture2D _texturePic5;
        private Texture2D _texturePic6;
        private Texture2D _texturePic7;
        private Texture2D _texturePic8;
        private Texture2D _textureContour;
        private MouseState _ms;
        private Texture2D _textureFondPause;
        private Texture2D _bindKey;
        private Texture2D _bruitOn;
        private Texture2D _bruitOff;

        public SettingScreen(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            Rectangle contourPic = new Rectangle(740, 45, 10, 5);
            base.Initialize();
        }
        public override void LoadContent()
        {
            _bindKey = Content.Load<Texture2D>("bindKey");
            _textureFondPause = Content.Load<Texture2D>("pause");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _police = Content.Load<SpriteFont>("Font");
            _textureChangerTouche = Content.Load<Texture2D>("Bouton");
            _textureLeaveButton = Content.Load<Texture2D>("boutonM");
            _textureLeaveButtonPressed = Content.Load<Texture2D>("boutonM_pressed");

            _texturePic1 = Content.Load<Texture2D>("pic1");
            _texturePic2 = Content.Load<Texture2D>("pic2");
            _texturePic3 = Content.Load<Texture2D>("pic3");
            _texturePic4 = Content.Load<Texture2D>("pic4");
            _texturePic5 = Content.Load<Texture2D>("pic5");
            _texturePic6 = Content.Load<Texture2D>("pic6");
            _texturePic7 = Content.Load<Texture2D>("pic7");
            _texturePic8 = Content.Load<Texture2D>("pic8");
            _textureContour = Content.Load<Texture2D>("contour");
            _bruitOn = Content.Load<Texture2D>("Bruit");
            _bruitOff = Content.Load<Texture2D>("pasBruit");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
            _myGame._actif = false;
            _myGame._settingOk = true;
            _myGame._homeScreenOpen = true;
            _myGame._shopScreenOk = false;
            
            _ms = Mouse.GetState();
        }
        public override void Draw(GameTime gameTime)
        {
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_myGame._fondSettings, new Vector2(0, 0), Color.White);
            if (_myGame._sonOff)
            {
                _spriteBatch.Draw(_bruitOff, new Vector2(100, 520), Color.White);
            }
            else
            {
                _spriteBatch.Draw(_bruitOn, new Vector2(100, 520), Color.White);
            }
            if (_myGame.hitboxOptionButton.Contains(_ms.X, _ms.Y))
            {
                _spriteBatch.Draw(_textureLeaveButtonPressed, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 6 / 8 - 50), Color.White);
            }
            else
            {
                _spriteBatch.Draw(_textureLeaveButton, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 6 / 8 - 50), Color.White);
            }
            _spriteBatch.DrawString(_police, "Back to Main Menu", new Vector2(825, Constantes._HAUTEUR_FENETRE * 6 / 8 - 15), Color.White);

            if (_myGame.hitboxSettingButtonZ.Contains(_ms.X, _ms.Y))
                _spriteBatch.Draw(_textureChangerTouche, new Vector2(0, 100), Color.White);
            if (_myGame.hitboxSettingButtonD.Contains(_ms.X, _ms.Y))
                _spriteBatch.Draw(_textureChangerTouche, new Vector2(0, 200), Color.White);
            if (_myGame.hitboxSettingButtonQ.Contains(_ms.X, _ms.Y))
                _spriteBatch.Draw(_textureChangerTouche, new Vector2(0, 300), Color.White);
            if (_myGame.hitboxSettingButtonS.Contains(_ms.X, _ms.Y))
                _spriteBatch.Draw(_textureChangerTouche, new Vector2(0, 400), Color.White);

            _spriteBatch.DrawString(_police, "Votre touche pour avancer : " + _myGame._forward, new Vector2(0, 108), Color.Black);
            _spriteBatch.DrawString(_police, "Votre touche pour aller a droite : " + _myGame._right, new Vector2(0, 208), Color.Black);
            _spriteBatch.DrawString(_police, "Votre touche pour aller a gauche : " + _myGame._left, new Vector2(0, 308), Color.Black);
            _spriteBatch.DrawString(_police, "Votre touche pour reculer: " + _myGame._behind, new Vector2(0, 408), Color.Black);

            _spriteBatch.Draw(_textureContour, new Vector2(_myGame.coordXcontourFond-5, _myGame.coordYcontourFond-3), Color.White);

            _spriteBatch.Draw(_texturePic1, new Vector2(750, 50), Color.White);
            _spriteBatch.Draw(_texturePic2, new Vector2(1050, 50), Color.White);
            _spriteBatch.Draw(_texturePic3, new Vector2(1350, 50), Color.White);
            _spriteBatch.Draw(_texturePic4, new Vector2(1650, 50), Color.White);

            _spriteBatch.Draw(_texturePic5, new Vector2(750, 240), Color.White);
            _spriteBatch.Draw(_texturePic6, new Vector2(1050, 240), Color.White);
            _spriteBatch.Draw(_texturePic7, new Vector2(1350, 240), Color.White);
            _spriteBatch.Draw(_texturePic8, new Vector2(1650, 240), Color.White);

            
            

            if (_myGame._tmpZ == true || _myGame._tmpD == true || _myGame._tmpQ == true || _myGame._tmpS == true)
            {
                _myGame._keyUpdating = true;
                _spriteBatch.Draw(_textureFondPause, new Vector2(0, 0), Color.White * 0.8f);
                _spriteBatch.Draw(_bindKey, new Vector2(Constantes._LARGEUR_FENETRE/2-500, Constantes._HAUTEUR_FENETRE/2-280), Color.White);
                _spriteBatch.DrawString(_police, "Touchez sur une touche pour l'assigner", new Vector2(600, 450), Color.Black);
            }
            else
            {
                _myGame._keyUpdating = false;
            }
            if(_myGame._touche == true)
            {
                _spriteBatch.DrawString(_police, _myGame._toucheAssignee + " est deja assignee, cliquez sur une autre...", new Vector2(600, 600), Color.Red);
            }
            _spriteBatch.End();
        }
    }
}



