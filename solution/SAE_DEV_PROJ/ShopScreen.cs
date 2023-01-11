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

        private Texture2D _textureContourHeart;
        private Texture2D _textureCoutourVaisseau;

        private Texture2D _textureButtonMenu;
        private Texture2D _textureButtonMenuPressed;

        private Texture2D _textureTmPayer;
        private Texture2D _textureTmpAnnuler;
        private Texture2D _textureVaisseauTirs;
        private Texture2D _textureVaisseauTirs2;
        private Texture2D _textureTmpMenu;

        private Rectangle _hitboxHeart1;
        private Rectangle _hitboxHeart2;
        private Rectangle _hitboxBoutonPayer;
        private Rectangle _hitboxBoutonAnnuler;
        private Rectangle _hitboxVaisseauTirs;
        private Rectangle _hitboxRafale;

        Rectangle _hitboxSkinVaisseau2;
        
        bool _skinVaisseau2;
        bool _skinVaisseau2tmp;
        bool _skinVaisseau2tmp2;
        private Texture2D _textureSkinVaisseau2;

        Rectangle _hitboxSkinDonald;
        bool _skinDonald;
        bool _skinDonaldtmp;
        bool _skinDonaldtmp2;
        private Texture2D _textureSkinDonald;

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
            
            _hitboxHeart1 = new Rectangle(575 - Constantes._ESPACESHOPBORD, 30,Constantes._TAILLEHEART, Constantes._TAILLEHEART + Constantes._ESPACECONTOURSHOP);
            _hitboxHeart2 = new Rectangle(700 - Constantes._ESPACESHOPBORD, 30, Constantes._TAILLEHEART, Constantes._TAILLEHEART + Constantes._ESPACECONTOURSHOP);
            _hitboxBoutonPayer = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 - Constantes._HAUTEUR_BOUTON - 200, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            _hitboxBoutonAnnuler = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2 - 200, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            
            _hitboxVaisseauTirs = new Rectangle(Constantes._ESPACESHOPBORD, 140, 420, Constantes._HAUTEURVAISSEAUTIRS + Constantes._ESPACECONTOURSHOP);
            _hitboxRafale = new Rectangle(Constantes._ESPACESHOPBORD, 290, 420, Constantes._HAUTEURVAISSEAUTIRS + Constantes._ESPACECONTOURSHOP);
            _hitboxSkinVaisseau2 = new Rectangle(1400, 500, 420, Constantes._HAUTEURVAISSEAUTIRS + Constantes._ESPACECONTOURSHOP);
            _hitboxSkinDonald = new Rectangle(1400, 700, 420, Constantes._HAUTEURVAISSEAUTIRS + Constantes._ESPACECONTOURSHOP);

            base.Initialize();
        }
        public override void LoadContent()
        {
            _textureSkinVaisseau2 = Content.Load<Texture2D>("vaisseau2BIG");
            _textureSkinDonald = Content.Load<Texture2D>("donaldBIG");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _police = Content.Load<SpriteFont>("Font");
            _textureFondWinScreen = Content.Load<Texture2D>("fondWinScreen");
            _textureHeartFill = Content.Load<Texture2D>("heartFill");
            _textureHeartEmpty = Content.Load<Texture2D>("heartEmpty");
            _textureVaisseauTirs = Content.Load<Texture2D>("vaisseauTirs");
            _textureButtonMenu = Content.Load<Texture2D>("boutonM");
            _textureButtonMenuPressed = Content.Load<Texture2D>("boutonM_pressed");
            _textureVaisseauTirs2 = Content.Load<Texture2D>("vaisseauTirs2");
            _textureCoutourVaisseau = Content.Load<Texture2D>("contourShop");
            _textureContourHeart = Content.Load<Texture2D>("contourShop2");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _myGame._screenDeathOk = false;
            _myGame._screenWinOk = false;
            _myGame._homescreenOk = false;
            _myGame._settingOk = false;
            _myGame._playScreenOk = false;

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

            _spriteBatch.Draw(_textureVaisseauTirs2, new Vector2(20, 300), Color.White);
            _spriteBatch.DrawString(_police, "Ameliorer la rafale", new Vector2(25 + Constantes._LARGEURVAISSEAUTIRS, 330), Color.White);

            _spriteBatch.Draw(_textureSkinVaisseau2, new Vector2(1400, 500), Color.White);
            _spriteBatch.DrawString(_police, "skin de vaisseau", new Vector2(1400 + Constantes._LARGEUR_PERSO, 500), Color.White);

            _spriteBatch.Draw(_textureSkinVaisseau2, new Vector2(1400, 700), Color.White);
            _spriteBatch.DrawString(_police, "skin de donald", new Vector2(1400 + Constantes._LARGEUR_PERSO, 700), Color.White);


            if (_hitboxSkinVaisseau2.Contains(_ms.X, _ms.Y) && _heartFillTmp1 == false && _spaceshipPoliceTmp1 == false)
            {
                _skinVaisseau2 = true;
                _skinVaisseau2tmp = true;

            }

            // BOUTON ACHTER
            if (_skinVaisseau2tmp && _heartFillTmp1 == false && _spaceshipPoliceTmp1 == false)
            {
                _spriteBatch.Draw(_textureCoutourVaisseau, new Vector2(1400, 500), Color.White);
                _spriteBatch.DrawString(_police, "skin de vaisseau 50g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 220, 450), Color.White);
            }
            if (_skinVaisseau2 == true && _spaceshipButton == false && _heartFillTmp1 == false)
            {
                _spriteBatch.Draw(_textureTmPayer, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 - Constantes._HAUTEUR_BOUTON - 200), Color.White);
                _spriteBatch.Draw(_textureTmpAnnuler, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2 - 200), Color.White);

                _spriteBatch.DrawString(_police, "Payer", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 800 - Constantes._HAUTEUR_BOUTON / 2 - 15 - 200), Color.White);
                _spriteBatch.DrawString(_police, "Annuler", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 60, 800 + Constantes._HAUTEUR_BOUTON - 15 - 200), Color.White);

                if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonPayer.Contains(_ms.X, _ms.Y))
                {
                    // ARGENT < 50
                    if (_myGame.hero.Money < 50)
                    {
                        _skinVaisseau2 = false;
                        _skinVaisseau2tmp = true;
                    }
                    else
                    {
                        _myGame.hero.Money -= 50;
                        //_myGame._upgradeRafale = true;

                        _skinVaisseau2 = false;
                        _skinVaisseau2tmp = false;
                        _skinVaisseau2tmp2 = false;
                    }
                    if (_skinVaisseau2tmp2)
                    {
                        _spriteBatch.DrawString(_police, "Vous n'avez pas l'argent necessaire, il vous manque " + (50 - _myGame.hero.Money) + " g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 400, 450), Color.White);
                    }
                }
                else
                {
                    _skinVaisseau2 = true;
                    _skinVaisseau2tmp2 = false;
                }
                if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonAnnuler.Contains(_ms.X, _ms.Y))
                {
                    _skinVaisseau2 = false;
                    _skinVaisseau2tmp = false;
                    _skinVaisseau2tmp2 = false;
                }
            }

            if (_hitboxSkinVaisseau2.Contains(_ms.X, _ms.Y) && _heartFillTmp1 == false && _spaceshipPoliceTmp1 == false&& _skinVaisseau2 == false)
            {
                _skinDonald = true;
                _skinDonaldtmp = true;

            }

            if (_skinDonald && _heartFillTmp1 == false && _spaceshipPoliceTmp1 == false)
            {
                _spriteBatch.Draw(_textureCoutourVaisseau, new Vector2(1400, 500), Color.White);
                _spriteBatch.DrawString(_police, "skin de vaisseau 50g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 220, 450), Color.White);
            }
            if (_skinDonald == true && _spaceshipButton == false && _heartFillTmp1 == false)
            {
                _spriteBatch.Draw(_textureTmPayer, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 - Constantes._HAUTEUR_BOUTON - 200), Color.White);
                _spriteBatch.Draw(_textureTmpAnnuler, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2 - 200), Color.White);

                _spriteBatch.DrawString(_police, "Payer", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 800 - Constantes._HAUTEUR_BOUTON / 2 - 15 - 200), Color.White);
                _spriteBatch.DrawString(_police, "Annuler", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 60, 800 + Constantes._HAUTEUR_BOUTON - 15 - 200), Color.White);

                if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonPayer.Contains(_ms.X, _ms.Y))
                {
                    // ARGENT < 75
                    if (_myGame.hero.Money < 75)
                    {
                        _skinDonald = false;
                        _skinDonaldtmp = true;
                    }
                    else
                    {
                        _myGame.hero.Money -= 75;
                        //_myGame._upgradeRafale = true;

                        _skinDonald = false;
                        _skinDonaldtmp = false;
                        _skinDonaldtmp2 = false;
                    }
                    if (_skinVaisseau2tmp2)
                    {
                        _spriteBatch.DrawString(_police, "Vous n'avez pas l'argent necessaire, il vous manque " + (75 - _myGame.hero.Money) + " g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 400, 450), Color.White);
                    }
                }
                else
                {
                    _skinDonald = true;
                    _skinDonaldtmp2 = false;
                }
                if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonAnnuler.Contains(_ms.X, _ms.Y))
                {
                    _skinDonald = false;
                    _skinDonaldtmp = false;
                    _skinDonaldtmp2 = false;
                }
            }


            // AMELIORATION RAFALE 
            if (_hitboxRafale.Contains(_ms.X,_ms.Y) && _heartFillTmp1 == false && _spaceshipPoliceTmp1 == false && _skinVaisseau2 == false)
            {
                _rafalesButton = true;
                _rafalesPoliceTmp1 = true;
            }

            


            if (_rafalesPoliceTmp1 && _heartFillTmp1 == false && _spaceshipPoliceTmp1 == false && _skinVaisseau2 == false)
            {
                _spriteBatch.Draw(_textureCoutourVaisseau, new Vector2(Constantes._ESPACESHOPBORD, 300 - Constantes._ESPACESHOPBORD), Color.White);
                _spriteBatch.DrawString(_police, "Ameliorer la rafale dps x1.5 (100g)", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 220, 450), Color.White);
            }

            if(_rafalesButton == true && _spaceshipButton == false && _heartFillTmp1 == false && _skinVaisseau2 == false)
            {
                _spriteBatch.Draw(_textureTmPayer, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 - Constantes._HAUTEUR_BOUTON - 200), Color.White);
                _spriteBatch.Draw(_textureTmpAnnuler, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2 - 200), Color.White);

                _spriteBatch.DrawString(_police, "Payer", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 800 - Constantes._HAUTEUR_BOUTON / 2 - 15 - 200), Color.White);
                _spriteBatch.DrawString(_police, "Annuler", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 60, 800 + Constantes._HAUTEUR_BOUTON - 15 - 200), Color.White);

                if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonPayer.Contains(_ms.X, _ms.Y))
                {
                    // ARGENT < 100
                    if (_myGame.hero.Money < 100)
                    {
                        _rafalesPoliceTmp1 = false;
                        _rafalesPoliceTmp2 = true;
                    }
                    else
                    {
                        _myGame.hero.Money -= 100;
                        _myGame._upgradeRafale = true;

                        _rafalesButton = false;
                        _rafalesPoliceTmp1 = false;
                        _rafalesPoliceTmp2 = false;
                    }
                    if (_rafalesPoliceTmp2)
                    {
                        _spriteBatch.DrawString(_police, "Vous n'avez pas l'argent necessaire, il vous manque " + (100 - _myGame.hero.Money) + " g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 400, 450), Color.White);
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
            if (_hitboxVaisseauTirs.Contains(_ms.X, _ms.Y) && _heartPoliceTmp1 == false && _rafalesPoliceTmp1 == false && _skinVaisseau2 == false)
            {
                _spaceshipButton = true;
                _spaceshipPoliceTmp1 = true;
            }
            if (_spaceshipPoliceTmp1)
            {
                _spriteBatch.Draw(_textureCoutourVaisseau, new Vector2(Constantes._ESPACESHOPBORD, 140 - Constantes._ESPACESHOPBORD), Color.White);
                _spriteBatch.DrawString(_police, "Debloquer 2 nouveaux tirs (100g)", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 220, 450), Color.White);
            }
            // DESSINE LES TEXTURES DES BOUTONS 
            if (_spaceshipButton && _heartPoliceTmp1 == false && _rafalesPoliceTmp1 == false && _skinVaisseau2 == false)
            {
                _spriteBatch.Draw(_textureTmPayer, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON/2, 800 - Constantes._HAUTEUR_BOUTON -200), Color.White);
                _spriteBatch.Draw(_textureTmpAnnuler, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 800 + Constantes._HAUTEUR_BOUTON / 2 -200), Color.White);

                _spriteBatch.DrawString(_police, "Payer", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 800 - Constantes._HAUTEUR_BOUTON / 2 - 15 - 200), Color.White);
                _spriteBatch.DrawString(_police, "Annuler", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 60, 800 + Constantes._HAUTEUR_BOUTON - 15 -200), Color.White);

                if (_ms.LeftButton == ButtonState.Pressed && _hitboxBoutonPayer.Contains(_ms.X, _ms.Y))
                {
                    // ARGENT < 50
                    if (_myGame.hero.Money < 100)
                    {
                        _spaceshipPoliceTmp1 = false;
                        _spaceshipPoliceTmp2 = true;
                    }
                    else
                    {
                        _myGame.hero.Money -= 100;
                        _myGame._upgradeCote = true;

                        _spaceshipButton = false;
                        _spaceshipPoliceTmp1 = false;
                        _spaceshipPoliceTmp2 = false;
                    }
                    if (_spaceshipPoliceTmp2)
                    {
                        _spriteBatch.DrawString(_police, "Vous n'avez pas l'argent necessaire, il vous manque " + (100 - _myGame.hero.Money) + " g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 400, 450), Color.White);
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
            if (_hitboxHeart1.Contains(_ms.X, _ms.Y) && _spaceshipPoliceTmp1 == false && _rafalesPoliceTmp1 == false && _skinVaisseau2 == false)
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
                if (_heartPoliceTmp1 && _spaceshipPoliceTmp1 == false && _spaceshipPoliceTmp2 == false && _skinVaisseau2 == false)
                {
                    _spriteBatch.Draw(_textureContourHeart, new Vector2(Constantes._ESPACESHOPBORD), Color.White);
                    _spriteBatch.DrawString(_police, "Augmenter sa vie de 20HP (50g)", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 220, 450), Color.White);
                }
                // SI SOURIS PAR DESSUS COEUR ROUGE 
                if (_heartFillTmp1 && _spaceshipPoliceTmp1 == false && _spaceshipButton == false && _skinVaisseau2 == false)
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
                        if (_myGame.hero.Money < 50)
                        {
                            _heartPoliceTmp1 = false;
                            _heartPoliceTmp2 = true;
                        }

                        // EN CAS DE PAIEMENT
                        else
                        {

                            _myGame.hero.Money -= 50;
                            _myGame.hero.PvPerso += 20;

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
                            _spriteBatch.DrawString(_police, "Vous n'avez pas l'argent necessaire, il vous manque " + (50 - _myGame.hero.Money) + " g", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 400, 450), Color.White);
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



