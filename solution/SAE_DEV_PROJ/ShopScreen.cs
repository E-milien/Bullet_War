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

        private Texture2D _textureButtonMenu;
        private Texture2D _textureButtonMenuPressed;
        private Texture2D _textureTmPayer;
        private Texture2D _textureTmpAnnuler;
        //private Texture2D _texture

        private Rectangle _hitboxHeart1;
        private Rectangle _hitboxHeart2;
        private Rectangle _hitboxBoutonPayer;
        private Rectangle _hitboxBoutonAnnuler;

        bool _heartFillTmp1;
        bool _heartFillTmp2;
        bool _heartPoliceTmp1;
        bool _heartPoliceTmp2;

        bool _heartFillDef1;
        bool _heartFillDef2;
        bool _heartBoutons;

        private MouseState _ms;

        public ShopScreen(Game1 game) : base(game)
        {
            _myGame = game;

        }
        public override void Initialize()
        {
            _hitboxHeart1 = new Rectangle(575,30,Constantes._TAILLEHEART, Constantes._TAILLEHEART);
            _hitboxHeart2 = new Rectangle(700, 30, Constantes._TAILLEHEART, Constantes._TAILLEHEART);
            _hitboxBoutonPayer = new Rectangle(Constantes._LARGEUR_FENETRE / 2 + 100, 800 - Constantes._HAUTEUR_BOUTON, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            _hitboxBoutonAnnuler = new Rectangle(Constantes._LARGEUR_FENETRE / 2 + 100, 800 + Constantes._HAUTEUR_BOUTON / 2, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);

            base.Initialize();
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _police = Content.Load<SpriteFont>("Font");
            _textureFondWinScreen = Content.Load<Texture2D>("fondWinScreen");
            _textureHeartFill = Content.Load<Texture2D>("heartFill");
            _textureHeartEmpty = Content.Load<Texture2D>("heartEmpty");

            _textureButtonMenu = Content.Load<Texture2D>("boutonM");
            _textureButtonMenuPressed = Content.Load<Texture2D>("boutonM_pressed");


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _myGame._actif = false;
            _myGame._shopScreenOk = true;

            if (_hitboxBoutonPayer.Contains(_ms.X, _ms.Y))
            {
                _textureTmPayer = _textureButtonMenuPressed;
            }
            else
            {
                _textureTmPayer = _textureButtonMenu;
            }
            if(_hitboxBoutonAnnuler.Contains(_ms.X, _ms.Y))
            {
                _textureTmpAnnuler = _textureButtonMenuPressed;
            }
            else
            {
                _textureTmpAnnuler = _textureButtonMenu;
            }

        }
        public override void Draw(GameTime gameTime)
        {
            _ms = Mouse.GetState();

            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureFondWinScreen, new Vector2(0, 0), Color.White);

            _spriteBatch.DrawString(_police,"Ameliorations vaisseau",new Vector2(20, 10), Color.Red);
            _spriteBatch.DrawString(_police, "Augmenter le nombre de HP : ", new Vector2(20, 50), Color.White);
            _spriteBatch.Draw(_textureHeartFill, new Vector2(450, 30), Color.White);

            _spriteBatch.Draw(_textureHeartEmpty, new Vector2(575, 30), Color.White);
            _spriteBatch.Draw(_textureHeartEmpty, new Vector2(700, 30), Color.White);
 


            // SI SOURIS PAR DESSUS COEUR ROUGE 1 
            if (_hitboxHeart1.Contains(_ms.X, _ms.Y))
            {
                _heartFillTmp1 = true;
                // SI SOURIS CLIQUE
                if (_ms.LeftButton == ButtonState.Pressed)
                {
                    _heartPoliceTmp1 = true;
                    _heartBoutons = true;
                }
            }
            // REMPLISSAGE COEUR ROUGE 1 
            if (_heartFillTmp1 || _heartFillDef1)
            {
                _spriteBatch.Draw(_textureHeartFill, new Vector2(575, 30), Color.White);
            }

            // SI SOURIS PAR DESSUS COEUR ROUGE 
            if (_heartPoliceTmp1)
            {
                _spriteBatch.DrawString(_police, "Augmenter sa vie de 20HP (50g), cliquer sur le coeur", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 440, 650), Color.White);
            }
            // SI SOURIS PAR DESSUS COEUR ROUGE 
            if (_heartFillTmp1)
            {
                // SI COEUR CLIQUÉ DESSINE LES TEXTURES 
                if (_heartBoutons)
                {
                _spriteBatch.Draw(_textureTmPayer, new Vector2(Constantes._LARGEUR_FENETRE / 2 + 100, 800 - Constantes._HAUTEUR_BOUTON), Color.White);
                _spriteBatch.Draw(_textureTmpAnnuler, new Vector2(Constantes._LARGEUR_FENETRE / 2 + 100, 800 + Constantes._HAUTEUR_BOUTON / 2), Color.White);

                _spriteBatch.DrawString(_police, "Payer", new Vector2(Constantes._LARGEUR_FENETRE / 2 + 100 + Constantes._LARGEUR_BOUTON / 2 - 40, 800 - Constantes._HAUTEUR_BOUTON / 2 - 15), Color.White);
                _spriteBatch.DrawString(_police, "Annuler", new Vector2(Constantes._LARGEUR_FENETRE / 2 + 100 + Constantes._LARGEUR_BOUTON / 2 - 40, 800 + Constantes._HAUTEUR_BOUTON - 15), Color.White);

                }
                // SI CLIQUE SUR BOUTON PAYER 
                if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonPayer.Contains(_ms.X, _ms.Y))
                {
                    // ARGENT < 50
                    if (_myGame._money < 50)
                    {
                        _heartPoliceTmp1 = false;
                        _heartPoliceTmp2 = true;
                    }

                    // EN CAS DE PAIEMENT
                    else
                    {

                        _myGame._money -= 50;
                        _myGame._hpPerso += 20;

                        // CHANGEMENT HITBOX POUR LE DERNIER COEUR
                        _hitboxHeart1 = _hitboxHeart2;

                        _heartPoliceTmp1 = false;
                        _heartFillTmp1 = false;
                        _heartFillDef1 = true;

                        // SI DERNIER COEUR ACHETE L'AFFICHER DEFINITIVEMENT
                        if (_heartFillTmp2)
                        {
                            _heartFillDef2 = true;
                        }
                    }

                    // SI ARGENT < 50 
                    if (_heartPoliceTmp2)
                    {
                        _spriteBatch.DrawString(_police, "Vous n'avez pas l'argent necessaire, il vous manque " + (50 - _myGame._money) + " g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 400, 650), Color.White);
                    }
                }
                // AFFICHE A NOUVEAU LE MESSAGE "Augmenter sa vie de 20hp" 
                else
                {
                    _heartPoliceTmp2 = false;
                    _heartPoliceTmp1 = true;
                }
                // SI JOUEUR APPUIE SUR ANNULER RETIRE TOUT 
                if(_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonAnnuler.Contains(_ms.X, _ms.Y))
                {
                    _heartPoliceTmp1 = false;
                    _heartPoliceTmp2 = false;
                    _heartFillTmp1 = false;
                    _heartFillTmp2 = false;

                    _heartBoutons = false;
                }
            }
            // SI 2 COEURS REMPLIS /3 ET SOURIS PASSE PAR DESSUS LE TROISIEME COEUR, AFFICHE CELUI-CI
            if (_heartFillDef1 && _hitboxHeart2.Contains(_ms.X, _ms.Y))
            {
                _heartFillTmp2 = true;
            }
            // SI LE COEUR 3 EST AFFICHE ON L'AFFICHE
            if (_heartFillTmp2 || _heartFillDef2)
            {
                _spriteBatch.Draw(_textureHeartFill, new Vector2(700, 30), Color.White);
            }
            

            
                
            

            

            _spriteBatch.End();
        }
    }
}



