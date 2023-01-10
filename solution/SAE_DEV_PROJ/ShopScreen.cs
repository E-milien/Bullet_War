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
        private Texture2D _textureVaisseauTirs;
        private Texture2D _textureTmpMenu;

        private Rectangle _hitboxHeart1;
        private Rectangle _hitboxHeart2;
        private Rectangle _hitboxBoutonPayer;
        private Rectangle _hitboxBoutonAnnuler;
        private Rectangle _hitboxVaisseauTirs;
        private Rectangle _hitboxRafale;

        // COEUR
        bool _heartFillTmp1;
        bool _heartFillTmp2;
        bool _heartPoliceTmp1;
        bool _heartPoliceTmp2;
        bool _heartFillDef1;
        bool _heartFillDef2;
        bool _heartBoutons;

        // VAISSEAU
        bool _spaceshipPoliceTmp1;
        bool _spaceshipPoliceTmp2;
        bool _spaceshipButton;

        // RAFALES
        bool _rafalesPoliceTmp1;
        bool _rafalesPoliceTmp2;
        bool _rafalesButton;

        private MouseState _ms;

        public ShopScreen(Game1 game) : base(game)
        {
            _myGame = game;


        }
        public override void Initialize()
        {
            _hitboxHeart1 = new Rectangle(575,30,Constantes._TAILLEHEART, Constantes._TAILLEHEART);
            _hitboxHeart2 = new Rectangle(700, 30, Constantes._TAILLEHEART, Constantes._TAILLEHEART);
            _hitboxBoutonPayer = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 - Constantes._HAUTEUR_BOUTON - 200, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            _hitboxBoutonAnnuler = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2 - 200, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            _hitboxVaisseauTirs = new Rectangle(20, 140, 410, Constantes._HAUTEURVAISSEAUTIRS + 10);

            _hitboxRafale = new Rectangle(25 + Constantes._LARGEURVAISSEAUTIRS, 300, 410, Constantes._HAUTEURVAISSEAUTIRS + 10);

            _myGame._actif = false;
            _myGame._screenDeathOk = false;
            _myGame._screenWinOk = false;
            _myGame._settingOk = false;
            _myGame._pause = false;
            _myGame._shopScreenOk = true;


            base.Initialize();
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _police = Content.Load<SpriteFont>("Font");
            _textureFondWinScreen = Content.Load<Texture2D>("fondWinScreen");
            _textureHeartFill = Content.Load<Texture2D>("heartFill");
            _textureHeartEmpty = Content.Load<Texture2D>("heartEmpty");
            _textureVaisseauTirs = Content.Load<Texture2D>("vaisseauTirs");
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

            // BOUTON MAIN MENU 
            if (_myGame._hitboxMainMenuShop.Contains(_ms.X, _ms.Y))
            {
                _textureTmpMenu = _textureButtonMenuPressed;
            }
            else
            {
                _textureTmpMenu = _textureButtonMenu;
            }

            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureFondWinScreen, new Vector2(0, 0), Color.White);



            _spriteBatch.Draw(_textureTmpMenu, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2), Color.White);
            _spriteBatch.DrawString(_police, "Back to Main Menu", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 135, 800 + Constantes._HAUTEUR_BOUTON - 20 ), Color.White);
            _spriteBatch.DrawString(_police,"Ameliorations vaisseau",new Vector2(20, 10), Color.Red);

            _spriteBatch.DrawString(_police, "Augmenter le nombre de HP : ", new Vector2(20, 50), Color.White);
            _spriteBatch.Draw(_textureHeartFill, new Vector2(450, 30), Color.White);
            _spriteBatch.Draw(_textureHeartEmpty, new Vector2(575, 30), Color.White);
            _spriteBatch.Draw(_textureHeartEmpty, new Vector2(700, 30), Color.White);

            _spriteBatch.Draw(_textureVaisseauTirs, new Vector2(20, 140), Color.White);
            _spriteBatch.DrawString(_police, "Tirs par salves de 3", new Vector2(25 + Constantes._LARGEURVAISSEAUTIRS, 140 + Constantes._HAUTEURVAISSEAUTIRS/2 - 10), Color.White);

            _spriteBatch.DrawString(_police, "Ameliorer la rafale", new Vector2(25 + Constantes._LARGEURVAISSEAUTIRS, 300), Color.White);

            if(_hitboxRafale.Contains(_ms.X,_ms.Y) && _heartFillTmp1 == false && _spaceshipButton == false)
            {
                _rafalesButton = true;
                _rafalesPoliceTmp1 = true;
            }
            if (_spaceshipPoliceTmp1)
            {
                _spriteBatch.DrawString(_police, "Ameliorer la rafale dps x1.5 (100g)", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 220, 450), Color.White);
            }
            if(_rafalesButton == true && _spaceshipButton == false && _heartFillTmp1 == false)
            {
                _spriteBatch.Draw(_textureTmPayer, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 - Constantes._HAUTEUR_BOUTON - 200), Color.White);
                _spriteBatch.Draw(_textureTmpAnnuler, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2 - 200), Color.White);

                _spriteBatch.DrawString(_police, "Payer", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 800 - Constantes._HAUTEUR_BOUTON / 2 - 15 - 200), Color.White);
                _spriteBatch.DrawString(_police, "Annuler", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 60, 800 + Constantes._HAUTEUR_BOUTON - 15 - 200), Color.White);

                if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonPayer.Contains(_ms.X, _ms.Y))
                {
                    // ARGENT < 50
                    if (_myGame._money < 100)
                    {
                        _rafalesPoliceTmp1 = false;
                        _rafalesPoliceTmp2 = true;
                    }
                    else
                    {
                        _myGame._money -= 100;
                        _myGame._upgradeCote = true;

                        _rafalesButton = false;
                        _rafalesPoliceTmp1 = false;
                        _rafalesPoliceTmp2 = false;
                    }
                    if (_rafalesPoliceTmp2)
                    {
                        _spriteBatch.DrawString(_police, "Vous n'avez pas l'argent necessaire, il vous manque " + (100 - _myGame._money) + " g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 400, 450), Color.White);
                    }
                }
                else
                {
                    _rafalesPoliceTmp1 = true;
                    _rafalesPoliceTmp2 = false;
                }

            }
            if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonAnnuler.Contains(_ms.X, _ms.Y))
            {
                _rafalesButton = false;
                _rafalesPoliceTmp1 = false;
                _rafalesPoliceTmp2 = false;
            }
        




            // VAISSEAU AMELIORATION 3 TIRS
            if (_hitboxVaisseauTirs.Contains(_ms.X, _ms.Y) && _heartPoliceTmp1 == false)
            {
                _spaceshipButton = true;
                _spaceshipPoliceTmp1 = true;
            }
            if (_spaceshipPoliceTmp1)
            {
                _spriteBatch.DrawString(_police, "Debloquer 2 nouveaux tirs (100g)", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 220, 450), Color.White);
            }

            if (_spaceshipButton && _heartPoliceTmp1 == false)
            {
                _spriteBatch.Draw(_textureTmPayer, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON/2, 800 - Constantes._HAUTEUR_BOUTON -200), Color.White);
                _spriteBatch.Draw(_textureTmpAnnuler, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2 -200), Color.White);

                _spriteBatch.DrawString(_police, "Payer", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 800 - Constantes._HAUTEUR_BOUTON / 2 - 15 - 200), Color.White);
                _spriteBatch.DrawString(_police, "Annuler", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 60, 800 + Constantes._HAUTEUR_BOUTON - 15 -200), Color.White);

                if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonPayer.Contains(_ms.X, _ms.Y))
                {
                    // ARGENT < 50
                    if (_myGame._money < 100)
                    {
                        _spaceshipPoliceTmp1 = false;
                        _spaceshipPoliceTmp2 = true;
                    }
                    else
                    {
                        _myGame._money -= 100;
                        _myGame._upgradeCote = true;

                        _spaceshipButton = false;
                        _spaceshipPoliceTmp1 = false;
                        _spaceshipPoliceTmp2 = false;
                    }
                    if (_spaceshipPoliceTmp2)
                    {
                        _spriteBatch.DrawString(_police, "Vous n'avez pas l'argent necessaire, il vous manque " + (100 - _myGame._money) + " g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 400, 450), Color.White);
                    }
                }
                else
                {
                    _spaceshipPoliceTmp1 = true;
                    _spaceshipPoliceTmp2 = false;
                }

            }
            if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonAnnuler.Contains(_ms.X, _ms.Y))
            {
                _spaceshipButton = false;
                _spaceshipPoliceTmp1 = false;
                _spaceshipPoliceTmp2 = false;
            }








            // SI SOURIS PAR DESSUS COEUR ROUGE 1 
            if (_hitboxHeart1.Contains(_ms.X, _ms.Y) && _spaceshipPoliceTmp1 == false && _spaceshipPoliceTmp2 == false)
                {
                    _heartFillTmp1 = true;
                    _heartBoutons = true;

                }
                // REMPLISSAGE COEUR ROUGE 1 
                if (_heartFillTmp1 || _heartFillDef1)
                {
                    _spriteBatch.Draw(_textureHeartFill, new Vector2(575, 30), Color.White);
                }

                // SI SOURIS PAR DESSUS COEUR ROUGE 
                if (_heartPoliceTmp1 && _spaceshipPoliceTmp1 == false && _spaceshipPoliceTmp2 == false)
                {
                    _spriteBatch.DrawString(_police, "Augmenter sa vie de 20HP (50g)", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 220, 450), Color.White);
                }
                // SI SOURIS PAR DESSUS COEUR ROUGE 
                if (_heartFillTmp1 && _spaceshipPoliceTmp1 == false && _spaceshipButton == false)
                {
                    // DESSINE LES TEXTURES 
                    if (_heartBoutons)
                    {
                        _spriteBatch.Draw(_textureTmPayer, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 - Constantes._HAUTEUR_BOUTON - 200), Color.White);
                        _spriteBatch.Draw(_textureTmpAnnuler, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2 - 200), Color.White);

                        _spriteBatch.DrawString(_police, "Payer", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 800 - Constantes._HAUTEUR_BOUTON / 2 - 15 - 200), Color.White);
                        _spriteBatch.DrawString(_police, "Annuler", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 60, 800 + Constantes._HAUTEUR_BOUTON - 15 - 200), Color.White);

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
                            _spriteBatch.DrawString(_police, "Vous n'avez pas l'argent necessaire, il vous manque " + (50 - _myGame._money) + " g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 400, 450), Color.White);
                        }
                    }
                    // AFFICHE A NOUVEAU LE MESSAGE "Augmenter sa vie de 20hp" 
                    else
                    {
                        _heartPoliceTmp2 = false;
                        _heartPoliceTmp1 = true;
                    }
                    // SI JOUEUR APPUIE SUR ANNULER RETIRE TOUT 
                    if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonAnnuler.Contains(_ms.X, _ms.Y))
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



