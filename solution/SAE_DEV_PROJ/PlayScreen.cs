﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;

namespace SAE_DEV_PROJ
{
    public class PlayScreen : GameScreen
    {
        
        private Game1 _myGame;
        private SpriteBatch _spriteBatch;
        private Texture2D _texturePerso;
        internal Bullet[] _tabBulletPerso = new Bullet[200];
        internal Bullet[,] _tabBulletPersoCote = new Bullet[200,2];
        internal Bullet[,] _tabBulletsRandom = new Bullet[8,10];
        internal Bullet[] _tabBullets2 = new Bullet[80];
        internal Bullet[,] _tabBulletsCercle = new Bullet[7,36];
        internal Bullet[] _tabBulletsSpirale = new Bullet[36*10];
        internal Bullet[] _tabBulletsCercleDesax = new Bullet[36*2];
        internal Bullet[] _tabBulletFocus1 = new Bullet[36];
        internal Bullet[] _tabBulletFocus2 = new Bullet[36];
        internal Bullet[] _tabBulletFocus3 = new Bullet[36];
        internal Bullet[] _tabBulletFocus4 = new Bullet[36];
        internal Bullet[] _tabBulletFocus5 = new Bullet[36];
        internal Bullet[] _tabBulletFocus6 = new Bullet[36];
        internal Bullet[] _tabBulletFocus7 = new Bullet[36];
        internal Bullet[] _tabBulletFocus8 = new Bullet[36];
        internal Bullet[] _tabBulletFocus9 = new Bullet[36];
        internal Bullet[] _tabBulletFocus10 = new Bullet[36];
        internal Bullet[] _tabBulletFocus11 = new Bullet[36];
        internal Bullet[] _tabBulletFocus12 = new Bullet[36];
        internal Bullet[] _tabBulletFocus13 = new Bullet[36];
        internal Bullet[] _tabBulletFocus14 = new Bullet[36];
        internal Bullet[] _tabBulletFocus15 = new Bullet[36];
        internal Bullet[] _tabBulletFocus16 = new Bullet[36];
        internal Boss boss1;
        internal Perso hero;
        private double _tmp;
        private bool _redemption;
        public double _chrono;
        public double _chronoPause;
        private int _i1;
        private int _i2;
        private double _var;
        private double _varCercle;
        private bool _ok1;
        private bool _ok2;
        private int _var2;
        private float _angle;
        private double _pvDepart;
        private Vector2 _positionPv;
        private Vector2 _positionPvBoss;
        private Vector2 _positionScore;
        private double _tmp48;

        private int _largeurBarreHp;
        private int _damagePerso;
        public bool _alive=true;
        public bool _bossAlive=true;
        private bool _cheat1;
        private Color _couleur;
        private bool _upgradeCote;
        private bool _upgradeRafale;
        private int _sequenceTir;

        // TEXTURES 
        private Texture2D _textureBulletAllieCote;
        private Texture2D _textureMenu;
        private Texture2D _textureFondPause;
        private Texture2D _textureBoss;
        private Texture2D _textureBullet;
        private Texture2D _textureBulletAllie;
        private Texture2D _textureAttentionPattern5;
        private SpriteFont _police;

        public Texture2D _boutonMenuResume;
        public Texture2D _boutonMenuHome;
        public Texture2D _boutonMenuExit;
        public Texture2D _textureButtonMenu;
        public Texture2D _textureButtonMenuPressed;

        // TEXTURES HP
        private Texture2D _texture_Full;
        private Texture2D _texture_High;
        private Texture2D _texture_Mid;
        private Texture2D _texture_Low;
        private Texture2D _texture_VeryLow;
        private Texture2D _texture_Dead;
        private Texture2D _textureFondTMP;

        // PERSO
        private int _sensPersoX;
        private int _sensPersoY;
        private KeyboardState _keyboardState;

        private Rectangle _hitboxResumeButton;
        private MouseState _ms;

        // SON 
        private SoundEffect _soundShot;
        

        public PlayScreen(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {
            _upgradeCote = true;
            _upgradeRafale = false;
            if (_upgradeRafale == true)
                _sequenceTir = 3;
            else
                _sequenceTir = 4;
                
            _couleur = Color.White;
            _cheat1 = false;
            _ok1 = false;
            _ok2 = false;
            _bossAlive = true;
            _alive = true;
            _var = 40;
            _i1 = -1;
            _i2 = -1;
            _chrono = 0;
            _chronoPause = 0;
            _var2 = 2;
            _varCercle = 0;
            _angle = 0f;
            _positionPv = new Vector2(20, 30);
            _positionPvBoss = new Vector2(20, 100);
            _positionScore = new Vector2(20, 200);
            _boutonMenuResume = _textureButtonMenu;
            _boutonMenuHome = _textureButtonMenu;
            _boutonMenuExit = _textureButtonMenu;
            _tmp48 = 0;

            _myGame._money = 100;
            _myGame._hpPerso = 100;

            _largeurBarreHp = 578;
            _hitboxResumeButton = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 300, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            

            // initialisation boss & perso
            boss1 = new Boss(5000, 20, "bossMechant", new Vector2(Constantes._LARGEUR_FENETRE / 2, Constantes._HAUTEUR_FENETRE / 5) - new Vector2(Constantes._LARGEUR_BOSS / 2, 0));
            hero = new Perso(false, _myGame._hpPerso, 5, 0, _myGame._money + 50, "vaisseau", 1, 500, new Vector2(Constantes._LARGEUR_FENETRE/2,Constantes._HAUTEUR_FENETRE*2/3) - new Vector2(Constantes._LARGEUR_PERSO / 2, Constantes._HAUTEUR_PERSO / 2));

            _damagePerso = hero.DamagePerso;
            _pvDepart = hero.PvPerso;

            // Bullets random initialize
            for (int i = 0; i < _tabBulletsRandom.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBulletsRandom.GetLength(1); j++)
                {
                    _tabBulletsRandom[i, j] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bullet");
                }
            }
            // BulletsAlliées initialize
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                _tabBulletPerso[i] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
            }
            // BulletsAlliéesCoté initialize
            if (_upgradeCote == true)
            {
                for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                {
                    for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                    {
                        if (j == 1)
                            _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                        else
                            _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                    }
                }
            }
            // Bullets pattern 2 initialize
            for (int i = 0; i < _tabBullets2.Length; i++)
            {
                _tabBullets2[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bullet");
            }
            // Bullets pattern cercle initialize
            for (int i = 0; i < _tabBulletsCercle.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBulletsCercle.GetLength(1); j++)
                {
                    _tabBulletsCercle[i, j] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bulletCercle");
                }
            }
            //Bullets focus perso init
            for (int i = 0; i < _tabBulletFocus1.Length; i++)
            {
                _tabBulletFocus1[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus2[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus3[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus4[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus5[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus6[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus7[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus8[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus9[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus10[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus11[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus12[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus13[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(Constantes._LARGEUR_FENETRE/3, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus14[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(2 * Constantes._LARGEUR_FENETRE/3, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus15[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(Constantes._LARGEUR_FENETRE / 3, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus16[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(2 * Constantes._LARGEUR_FENETRE / 3, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
            }
            
            InitializeSpirale();
            InitializeCercleDesax();

            base.Initialize();
        }

        public override void LoadContent()
        {
            _police = Content.Load<SpriteFont>("Font");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texturePerso = Content.Load<Texture2D>(hero.SkinPerso);
            _textureBullet = Content.Load<Texture2D>("bullet1");
            _textureBulletAllie = Content.Load<Texture2D>("ballePerso");
            _textureBulletAllieCote = Content.Load<Texture2D>("ballePersoCote");
            _textureBoss = Content.Load<Texture2D>(boss1.SkinBoss);

            _textureFondPause = Content.Load<Texture2D>("pause");
            _textureMenu = Content.Load<Texture2D>("menu");
            _textureButtonMenu = Content.Load<Texture2D>("boutonM");
            _textureButtonMenuPressed = Content.Load<Texture2D>("boutonM_pressed");
            _textureAttentionPattern5 = Content.Load<Texture2D>("attention");

            // barre de vie perso
            _texture_Full = Content.Load<Texture2D>("Full");
            _texture_High = Content.Load<Texture2D>("High");
            _texture_Mid = Content.Load<Texture2D>("Mid");
            _texture_Low = Content.Load<Texture2D>("Low");
            _texture_VeryLow = Content.Load<Texture2D>("VeryLow");
            _texture_Dead = Content.Load<Texture2D>("Dead");

            _soundShot = Content.Load<SoundEffect>("shot");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _ms = Mouse.GetState();
            _myGame._homeScreenOpen = false;
            if (_keyboardState.IsKeyDown(Keys.P) && _keyboardState.IsKeyDown(Keys.I))
                    _couleur = Color.DeepPink;

            _myGame._screenDeathOk = false;
            _myGame._screenWinOk = false;
            _myGame._actif = false;
            _myGame._shopScreenOk = false;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!_myGame._pause)
            {
                if(_chrono>=_tmp48 && !_redemption)
                {
                    _soundShot.Play();
                    _tmp48 = _chrono + 0.1;
                }

                //pattern1 pour les différentes "vague de bullets"
                _chrono += deltaTime;
                _chronoPause = 0;

                //perd du score a cause du temps
                if (_chrono >= _var2)
                {
                    if (hero.Score >= 100 && _bossAlive)
                        hero.Score -= 100;
                    _var2 += 3;
                }

                //tirs alliés

                for (int i = 0; i < _tabBulletPerso.Length; i++)
                {
                    if (_tabBulletPerso[i].BulletPosition.Y > hero.PositionPerso.Y)
                    {
                        if (_tabBulletPerso[i].BulletPosition.X != hero.PositionPerso.X)
                            _tabBulletPerso[i].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, _tabBulletPerso[i].BulletPosition.Y);
                    }
                    _tabBulletPerso[i].BulletPosition -= new Vector2(0, _tabBulletPerso[i].Vitesse * deltaTime);
                }

                //tirs alliés cotés
                if (_upgradeCote == true)
                {
                    for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                    {
                        for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                        {
                            if (_tabBulletPersoCote[i,j].BulletPosition.Y > hero.PositionPerso.Y)
                            {
                                if (_tabBulletPersoCote[i,j].BulletPosition.X != hero.PositionPerso.X)
                                {
                                    if (j == 1)
                                        _tabBulletPersoCote[i,j].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, _tabBulletPersoCote[i,j].BulletPosition.Y);
                                    else
                                        _tabBulletPersoCote[i, j].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, _tabBulletPersoCote[i, j].BulletPosition.Y);
                                }
                            }
                            _tabBulletPersoCote[i,j].BulletPosition -= new Vector2(0, _tabBulletPersoCote[i,j].Vitesse * deltaTime);
                        }
                    }
                }

                //------------------------------------------------------------------------------------------------------------------------------------------------------//
                //------------------------------------------------------         Activation de tous les patterns         -----------------------------------------------//
                //------------------------------------------------------------------------------------------------------------------------------------------------------//

                //active le 3eme patterne (patterncercle)
                if (_chrono > Constantes._DEBUTPAT1 && _chrono < Constantes._FINPAT1)
                {
                    PatternCercle(_angle);
                    if (!_ok2)
                        _varCercle = _chrono;
                    _ok2 = true;
                }

                // active le 1er pattern (bullets random)
                if (_chrono > Constantes._DEBUTPAT4)
                {
                    Pattern1(deltaTime);
                    if (!_ok1)
                        _var = _chrono;
                    _ok1 = true;
                }

                // Pattern cercle desaxé
                if (_chrono > Constantes._DEBUTPAT3)
                    PatternCercleDesax(_angle);

                //lancer pattern2 au bout de 24 sec
                if (_chrono > Constantes._DEBUTPAT2 && _chrono <= Constantes._DEBUTPAT3-1)
                    Pattern2(deltaTime);


                //active le pattern spirale 
                if (_chrono > Constantes._DEBUTPAT5)
                    PatternSpirale(_angle);

                //tous les patterns focus
                UpdatePatternsFocus(Constantes._DEBUTFOCUS1, _tabBulletFocus1, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS2, _tabBulletFocus2, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS3, _tabBulletFocus3, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS4, _tabBulletFocus4, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS5, _tabBulletFocus5, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS6, _tabBulletFocus6, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS7, _tabBulletFocus7, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS8, _tabBulletFocus8, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS9, _tabBulletFocus9, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS10, _tabBulletFocus10, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS11, _tabBulletFocus11, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS12, _tabBulletFocus12, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSDOUBLE1, _tabBulletFocus13, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSDOUBLE1, _tabBulletFocus14, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSDOUBLE2, _tabBulletFocus15, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSDOUBLE2, _tabBulletFocus16, deltaTime);


                DeplacementPerso(deltaTime);
                CollisionBoss();
                Redemption(deltaTime);
                CheckBossDead(boss1);
                BulletAllieReset();
            }
            else
            {
                _chronoPause += deltaTime;
                if(_ms.LeftButton == ButtonState.Pressed && _hitboxResumeButton.Contains(_ms.X, _ms.Y))
                {
                    _myGame._soundButton.Play();
                    _myGame._pause = false;
                }
                if (_hitboxResumeButton.Contains(_ms.X, _ms.Y))
                {
                    _boutonMenuResume = _textureButtonMenuPressed;
                }
                else
                {
                    _boutonMenuResume = _textureButtonMenu;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_myGame._textureFond, new Vector2(0, 0), _couleur);

            

            //Bullets alliées
            if (_redemption == false)
            {
             
                for (int i = 0; i < _tabBulletPerso.Length; i++)
                {
                    if (!(_tabBulletPerso[i].BulletPosition.Y > hero.PositionPerso.Y))
                    {
                        _spriteBatch.Draw(_textureBulletAllie, _tabBulletPerso[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS_PERSO / 2, 0), Color.White);
                    }
                }
            
            }

            //Bullets alliées coté
            if (_upgradeCote == true)
            {
                if (_redemption == false)
                {

                    for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                    {
                        for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                        {
                            if (!(_tabBulletPersoCote[i,j].BulletPosition.Y > hero.PositionPerso.Y))
                                _spriteBatch.Draw(_textureBulletAllieCote, _tabBulletPersoCote[i,j].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS_PERSO_COTE / 2, 0), Color.White);
                        }
                    }
                }
            }

            //Bullets pattern 1 (random)
            for (int z = 0; z <= _i1; z++)
            {
                for (int j = 0; j < _tabBulletsRandom.GetLength(1); j++)
                {
                    _spriteBatch.Draw(_textureBullet, _tabBulletsRandom[z, j].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.White);
                }
            }

            //Bullets pattern2
            if (_chrono >= Constantes._DEBUTPAT2 && _chrono < Constantes._DEBUTPAT3 - 1)
            {
                for (int i = 0; i < _tabBullets2.Length; i++)
                {
                    _spriteBatch.Draw(_textureBullet, _tabBullets2[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.White);
                }
            }

            //Bullets patternCercle
            for (int i = 0; i <= _i2; i++)
            {
                for (int j = 0; j < _tabBulletsCercle.GetLength(1); j++)
                {
                    _spriteBatch.Draw(_textureBullet, _tabBulletsCercle[i, j].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.White);
                }
            }

            //Bullets patternSpiral
            if (_chrono > Constantes._DEBUTPAT5)
            {
                for (int i = 0; i < _tabBulletsSpirale.Length; i++)
                {
                    if (_tabBulletsSpirale[i].PasseOrigine == true)
                        _spriteBatch.Draw(_textureBullet, _tabBulletsSpirale[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
                }
            }

            //Bullets patternCercleDesax
            if (_chrono > Constantes._DEBUTPAT3)
            {
                for (int i = 0; i < _tabBulletsCercleDesax.Length; i++)
                {
                    _spriteBatch.Draw(_textureBullet, _tabBulletsCercleDesax[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS, 0), Color.Yellow);
                }
            }
            //PatternCercleDesax attention //ON TOUCHE PAS SVP
            else if (_chrono > Constantes._DEBUTPAT3 - 1)
                _spriteBatch.Draw(_textureAttentionPattern5,new Vector2(555,180),Color.White);

            //Draw pattern focus

            PatternFocusDraw(Constantes._DEBUTFOCUS1, _tabBulletFocus1);
            PatternFocusDraw(Constantes._DEBUTFOCUS2, _tabBulletFocus2);
            PatternFocusDraw(Constantes._DEBUTFOCUS3, _tabBulletFocus3);
            PatternFocusDraw(Constantes._DEBUTFOCUS4, _tabBulletFocus4);
            PatternFocusDraw(Constantes._DEBUTFOCUS5, _tabBulletFocus5);
            PatternFocusDraw(Constantes._DEBUTFOCUS6, _tabBulletFocus6);
            PatternFocusDraw(Constantes._DEBUTFOCUS7, _tabBulletFocus7);
            PatternFocusDraw(Constantes._DEBUTFOCUS8, _tabBulletFocus8);
            PatternFocusDraw(Constantes._DEBUTFOCUS9, _tabBulletFocus9);
            PatternFocusDraw(Constantes._DEBUTFOCUS10, _tabBulletFocus10);
            PatternFocusDraw(Constantes._DEBUTFOCUS11, _tabBulletFocus11);
            PatternFocusDraw(Constantes._DEBUTFOCUS12, _tabBulletFocus12);
            PatternFocusDraw(Constantes._DEBUTFOCUSDOUBLE1, _tabBulletFocus13);
            PatternFocusDraw(Constantes._DEBUTFOCUSDOUBLE1, _tabBulletFocus14);
            PatternFocusDraw(Constantes._DEBUTFOCUSDOUBLE2, _tabBulletFocus15);
            PatternFocusDraw(Constantes._DEBUTFOCUSDOUBLE2, _tabBulletFocus16);


            _spriteBatch.Draw(_textureBoss, boss1.BossPosition, _couleur);
            _spriteBatch.Draw(_texturePerso, hero.PositionPerso, _couleur);
            _spriteBatch.DrawString(_police, "" + Math.Round(_chrono, 2), new Vector2(Constantes._LARGEUR_FENETRE - 100, 0), _couleur);
            _spriteBatch.DrawString(_police, $"Vie Boss : { boss1.BossHP}", _positionPvBoss, _couleur);
            _spriteBatch.DrawString(_police, $"Score : {hero.Score}", new Vector2(_positionScore.X, _positionScore.Y - 50), _couleur);
            

            //HP
            if (Math.Round((hero.PvPerso / _pvDepart) * 100) > 80)
                _spriteBatch.Draw(_texture_Full, _positionPv, Color.White);

            else if (Math.Round((hero.PvPerso / _pvDepart) * 100) > 60)
                _spriteBatch.Draw(_texture_High, _positionPv, Color.White);

            else if (Math.Round((hero.PvPerso / _pvDepart) * 100) > 40)
                _spriteBatch.Draw(_texture_Mid, _positionPv, Color.White);

            else if (Math.Round((hero.PvPerso / _pvDepart) * 100) > 20)
                _spriteBatch.Draw(_texture_Low, _positionPv, Color.White);

            else if (Math.Round((hero.PvPerso / _pvDepart) * 100) > 0)
                _spriteBatch.Draw(_texture_VeryLow, _positionPv, Color.White);

            else
                _spriteBatch.Draw(_texture_Dead, _positionPv, Color.White);

            _spriteBatch.DrawString(_police, $"{hero.PvPerso} / {_pvDepart}", new Vector2(_positionPv.X + _largeurBarreHp/2 - 40, _positionPv.Y + 20), Color.Black);

            // PAUSE
            if (_myGame._pause)
            {
                _spriteBatch.Draw(_textureFondPause, new Vector2(0, 0), Color.White * 0.8f);
                _spriteBatch.Draw(_textureMenu, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_MENU / 2, Constantes._HAUTEUR_FENETRE / 2 - Constantes._HAUTEUR_MENU / 2), Color.White);
                
                _spriteBatch.Draw(_boutonMenuResume, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON/2, 300), Color.White);
                _spriteBatch.DrawString(_police, "Resume", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 50, 340), Color.White);

                _spriteBatch.Draw(_boutonMenuHome, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 500), Color.White);
                _spriteBatch.DrawString(_police, "Main Menu", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 70, 540), Color.White);

                _spriteBatch.Draw(_boutonMenuExit, new Vector2(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 700), Color.White);
                _spriteBatch.DrawString(_police, "Quit", new Vector2(Constantes._LARGEUR_FENETRE / 2 - 30, 740), Color.White);
                _spriteBatch.DrawString(_police, "Menu Pause", new Vector2(Constantes._LARGEUR_FENETRE/2-90, 200), Color.Black);
            }

            _spriteBatch.End();

        }
        private void DeplacementPerso(float deltaTime)
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(_myGame._left) && !(_keyboardState.IsKeyDown(_myGame._right)) && hero.PositionPerso.X >= 0)
                _sensPersoX = -1;

            else if (_keyboardState.IsKeyDown(_myGame._right) && !(_keyboardState.IsKeyDown(_myGame._left)) && hero.PositionPerso.X <= Constantes._LARGEUR_FENETRE - Constantes._LARGEUR_PERSO)
                _sensPersoX = 1;

            if (_keyboardState.IsKeyDown(_myGame._forward) && !(_keyboardState.IsKeyDown(_myGame._behind)) && hero.PositionPerso.Y >= 0)
                _sensPersoY = -1;

            else if (_keyboardState.IsKeyDown(_myGame._behind) && !(_keyboardState.IsKeyDown(_myGame._forward)) && hero.PositionPerso.Y <= Constantes._HAUTEUR_FENETRE - Constantes._HAUTEUR_PERSO)
                _sensPersoY = 1;

            hero.PositionPerso += new Vector2(_sensPersoX * (int)Math.Round(hero.DeplacementPerso * hero.MultiplicationVitesse, 0) * deltaTime, 0);
            _sensPersoX = 0;

            hero.PositionPerso += new Vector2(0, _sensPersoY * (int)Math.Round(hero.DeplacementPerso * hero.MultiplicationVitesse, 0) * deltaTime);
            _sensPersoY = 0;
        }
        internal bool Collision(bool ok, Bullet[,] _tableau)
        {
            bool tmp = false;
            if (ok == true)
                return false;
            for (int i = 0; i < _tableau.GetLength(0); i++)
            {
                for (int j = 0; j < _tableau.GetLength(1); j++)
                {
                    Rectangle rect1 = new Rectangle((int)_tableau[i, j].BulletPosition.X, (int)_tableau[i, j].BulletPosition.Y, Constantes._LARGEUR_BULLETS, Constantes._HAUTEUR_BULLETS);
                    Rectangle rect2 = new Rectangle((int)hero.PositionPerso.X, (int)hero.PositionPerso.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);
                    if (rect1.Intersects(rect2))
                    {
                        tmp = true;
                    }
                }
            }
            return tmp;
        }
        internal bool Collision(bool ok, Bullet[] _tableau)
        {
            bool tmp = false;
            if (ok == true)
                return false;
            for (int i = 0; i < _tableau.Length; i++)
            {
                Rectangle rect1 = new Rectangle((int)_tableau[i].BulletPosition.X, (int)_tableau[i].BulletPosition.Y, Constantes._LARGEUR_BULLETS, Constantes._HAUTEUR_BULLETS);
                Rectangle rect2 = new Rectangle((int)hero.PositionPerso.X, (int)hero.PositionPerso.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);
                if (rect1.Intersects(rect2))
                {
                    tmp = true;
                }
            }
            return tmp;
        }
        internal bool CollisionSpirale(bool ok, Bullet[] _tableau)
        {
            bool tmp = false;
            if (ok == true)
                return false;
            for (int i = 0; i < _tableau.Length; i++)
            {
                if (!(_tabBulletsSpirale[i].PasseOrigine != true))
                {
                    Rectangle rect1 = new Rectangle((int)_tableau[i].BulletPosition.X, (int)_tableau[i].BulletPosition.Y, Constantes._LARGEUR_BULLETS, Constantes._HAUTEUR_BULLETS);
                    Rectangle rect2 = new Rectangle((int)hero.PositionPerso.X, (int)hero.PositionPerso.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);
                    if (rect1.Intersects(rect2))
                    {
                        tmp = true;
                    }
                }
            }
            return tmp;
        }
        public void CollisionBoss()
        {
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                if (!(_tabBulletPerso[i].BulletPosition.Y > hero.PositionPerso.Y))
                {
                    Rectangle rect1 = new Rectangle((int)_tabBulletPerso[i].BulletPosition.X, (int)_tabBulletPerso[i].BulletPosition.Y, Constantes._LARGEUR_BULLETS_PERSO, Constantes._HAUTEUR_BULLETS_PERSO);
                    Rectangle rect2 = new Rectangle((int)boss1.BossPosition.X, (int)boss1.BossPosition.Y, Constantes._LARGEUR_BOSS, Constantes._HAUTEUR_BOSS);

                    if (rect1.Intersects(rect2))
                    {
                        boss1.BossHP -= hero.DamagePerso;
                        _tabBulletPerso[i].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir);

                        if (_redemption == false && _bossAlive)
                            hero.Score += 10;
                    }
                }
            }
            if (_upgradeCote)
            {
                for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                {
                    for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                    {

                        if (!(_tabBulletPersoCote[i,j].BulletPosition.Y > hero.PositionPerso.Y))
                        {
                            Rectangle rect1 = new Rectangle((int)_tabBulletPersoCote[i,j].BulletPosition.X, (int)_tabBulletPersoCote[i,j].BulletPosition.Y, Constantes._LARGEUR_BULLETS_PERSO, Constantes._HAUTEUR_BULLETS_PERSO);
                            Rectangle rect2 = new Rectangle((int)boss1.BossPosition.X, (int)boss1.BossPosition.Y, Constantes._LARGEUR_BOSS, Constantes._HAUTEUR_BOSS);

                            if (rect1.Intersects(rect2))
                            {
                                if (j == 1)
                                    _tabBulletPersoCote[i, j].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir);
                                else 
                                    _tabBulletPersoCote[i, j].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir);
                                boss1.BossHP -= hero.DamagePerso/5;
                                if (_redemption == false && _bossAlive)
                                    hero.Score += 1;
                            }
                        }
                    }
                }
            }
        }

        // Une fois arrivée en bas , les bullets sont remises en-dessous de la fenêtre
        public void BulletAllieReset()
        {
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                if (_tabBulletPerso[i].BulletPosition.Y <= 0)
                {
                    _tabBulletPerso[i].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir);
                }
            }
            if (_upgradeCote == true)
            {
                for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                {
                    for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                    {
                        if (_tabBulletPersoCote[i,j].BulletPosition.Y <= 0)
                        {
                            if (j == 1)
                                _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                            else
                                _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                        }
                    }
                }
            }
        }

        // 1
        public void Pattern1(float deltaTime)
        {
            if (_chrono >= _var && _i1 < _tabBulletsRandom.GetLength(0) - 1)
            {
                _var += 2;
                _i1++;
            }
            
            
            Random rdn = new Random();
            for (int z = 0; z <= _i1; z++)
            {
                for (int j = 0; j < _tabBulletsRandom.GetLength(1) - 2; j++)
                {
                    _tabBulletsRandom[z, j].BulletPosition += new Vector2(rdn.Next(-50, 50), _tabBulletsRandom[z, j].Vitesse * deltaTime);
                }
            }
            
        }

        // 2
        public void Pattern2(float deltaTime)
        {
            if (_chrono < Constantes._DEBUTPAT3 - 1)
            {

                for (int i = 0; i < _tabBullets2.Length; i++)
                {
                    if (i % 5 == 0)
                        _tabBullets2[i].BulletPosition += new Vector2(i * 2 * deltaTime, i * 2 * deltaTime);
                    else if (i % 5 == 1)
                        _tabBullets2[i].BulletPosition += new Vector2(-i * 2 * deltaTime, i * 2 * deltaTime);
                    else if (i % 5 == 2)
                        _tabBullets2[i].BulletPosition += new Vector2(i * deltaTime, i * 3 * deltaTime);
                    else if (i % 5 == 3)
                        _tabBullets2[i].BulletPosition += new Vector2(-i * deltaTime, i * 3 * deltaTime);
                    else
                        _tabBullets2[i].BulletPosition += new Vector2(0, i * 3 * deltaTime);
                }
            }
            else 
            {
                for (int i = 0; i < _tabBullets2.Length; i++)
                {
                    _tabBullets2[i].BulletPosition = new Vector2(-20, -20);
                }
            }
        }

        // 3
        public void PatternCercle(float angle)
        {
            if (_chrono >= _varCercle && _i2 < _tabBulletsCercle.GetLength(0)-1)
            {
                _varCercle += 3;
                _i2++;
            }
            if (_chrono < Constantes._DEBUTPAT4 -1)
            {
                for (int z = 0; z <= _i2; z++)
                {
                    for (int j = 0; j < _tabBulletsCercle.GetLength(1); j++)
                    {
                        float bulletDirectionX = MathF.Sin(angle * MathF.PI / 180f);
                        float bulletDirectionY = MathF.Cos(angle * MathF.PI / 180f);
                        Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                        _tabBulletsCercle[z, j].BulletPosition += bulletDirection;

                        angle += 10f;
                    }

                }
            }
            else
            {
                for (int z = 0; z <= _i2; z++)
                {
                    for (int j = 0; j < _tabBulletsCercle.GetLength(1); j++)
                    {
                        _tabBulletsCercle[z, j].BulletPosition = new Vector2(-20, -20);
                    }
                }

            }
        }

        // 4
        public void PatternSpirale(float angle)
        {

            for (int i = 0; i < _tabBulletsSpirale.Length; i++)
            {
                if (!(_tabBulletsSpirale[i].BulletPosition.X <= boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2 - 1 || _tabBulletsSpirale[i].BulletPosition.X >= boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2 + 1))
                {
                    if (!(_tabBulletsSpirale[i].BulletPosition.Y <= boss1.BossPosition.Y + Constantes._LARGEUR_BOSS / 2 - 1 || _tabBulletsSpirale[i].BulletPosition.Y >= boss1.BossPosition.Y + Constantes._LARGEUR_BOSS / 2 + 1))
                        _tabBulletsSpirale[i].PasseOrigine = true;
                }

                float bulletDirectionX = MathF.Sin(angle * MathF.PI / 180f);
                float bulletDirectionY = MathF.Cos(angle * MathF.PI / 180f);
                Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                _tabBulletsSpirale[i].BulletPosition += bulletDirection;

                angle += 10f;
            }
        }

        // 5
        public void PatternCercleDesax(float angle)
        {
            if (_chrono > Constantes._DEBUTPAT3)
            {
                for (int i = 0; i < _tabBulletsCercleDesax.Length; i++)
                {
                    float bulletDirectionX = MathF.Sin(angle * MathF.PI / 180f);
                    float bulletDirectionY = MathF.Cos(angle * MathF.PI / 180f);
                    Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);
                    if (i % 2 == 0)
                        _tabBulletsCercleDesax[i].BulletPosition += bulletDirection;
                    else
                        _tabBulletsCercleDesax[i].BulletPosition += new Vector2(-bulletDirectionX,bulletDirectionY);
                    angle += 5f;
                }
            }
        }
        // Pattern focus
        private void PatternFocus(Bullet[] tabBullet, float deltaTime, float angle)
        {
            for (int i = 0; i < tabBullet.Length; i++)
            {
                if (tabBullet[i].PasseOrigine == true)
                {
                    tabBullet[i].BulletPosition += tabBullet[i].BulletDirection;
                }
                else
                {
                    float bulletDirectionX = MathF.Sin(angle * MathF.PI / 180f);
                    float bulletDirectionY = MathF.Cos(angle * MathF.PI / 180f);

                    tabBullet[i].BulletPosition += new Vector2(bulletDirectionX*50, bulletDirectionY*50) ;

                    tabBullet[i].BulletDirection = Vector2.Normalize(hero.PositionPerso - tabBullet[i].BulletPosition) * tabBullet[i].Vitesse * deltaTime;
                    tabBullet[i].PasseOrigine = true;
                    
                    angle += 10f;
                }
            }
        }
        //Pattern focus draw            
        private void PatternFocusDraw(int debut,Bullet[] tabBullet)
        {
            if (_chrono > debut)
            {
                for (int i = 0; i<_tabBulletFocus1.Length; i++)
                {
                    _spriteBatch.Draw(_textureBullet, tabBullet[i].BulletPosition + new Vector2(Constantes._LARGEUR_BULLETS/2,0), Color.Black); 
                }
            }
        }

    // redemption de 2 secondes après être touché
    public void Redemption(float deltaTime)
        {
            if ((Collision(_redemption, _tabBullets2) && _chrono < Constantes._DEBUTPAT3 - 1) || Collision(_redemption, _tabBulletsCercle) || (Collision(_redemption, _tabBulletsRandom) && _chrono > Constantes._DEBUTPAT4) || (Collision(_redemption, _tabBulletsCercleDesax) && _chrono > Constantes._DEBUTPAT3) || (CollisionSpirale(_redemption, _tabBulletsSpirale) && _chrono > Constantes._DEBUTPAT5) && _redemption == false)
            {
                //_alive = true; // pour etre sur
                hero.PvPerso -= (int)boss1.DamageBoss;
                _redemption = true;
            }
            //pareil pour les pattern focus
            else if (Collision(_redemption, _tabBulletFocus1) || Collision(_redemption, _tabBulletFocus2) || Collision(_redemption, _tabBulletFocus3) || Collision(_redemption, _tabBulletFocus4) || Collision(_redemption, _tabBulletFocus5) || Collision(_redemption, _tabBulletFocus6) || Collision(_redemption, _tabBulletFocus7) || Collision(_redemption, _tabBulletFocus8) || Collision(_redemption, _tabBulletFocus9) && _redemption == false)
            {
                hero.PvPerso -= (int)boss1.DamageBoss;
                _redemption = true;
            }
            if (_redemption)
            {
                hero.DamagePerso = 0;
                _tmp += deltaTime;
            }
            if (_tmp >= 2)
            {
                _tmp = 0;
                hero.DamagePerso = _damagePerso;
                _redemption = false;
                //on remet les bullets au dessus du perso
                for (int i = 0; i < _tabBulletPerso.Length; i++)
                {
                    _tabBulletPerso[i] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                }
                for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                {
                    for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                    {
                        if (j == 1)
                            _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                        else
                            _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                    }
                }
            }
            if (hero.PvPerso <= 0)
            {
                _alive = false;
            }
        }

        // si le boss est mort
        internal void CheckBossDead(Boss boss)
        {
            if (boss.BossHP <= 0)
            {
                _bossAlive = false;
                boss.BossHP = 0;
            }
        }

        public void InitializeSpirale()
        {
            // Bullets pattern spirale initialize 
            for (int i = 0; i < _tabBulletsSpirale.Length; i++)
            {
                float bulletDirectionX = MathF.Sin(_angle * MathF.PI / 180f);
                float bulletDirectionY = MathF.Cos(_angle * MathF.PI / 180f);
                Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                _tabBulletsSpirale[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2) - bulletDirection * i * 2, "bulletSpiral", false);
                _angle += 10f;
            }
        }
        public void InitializeCercleDesax()
        {
            // Bullets pattern cercle desaxé initialize
            for (int i = 0; i < _tabBulletsCercleDesax.Length; i++)
            {
                if (i%2 == 0)
                    _tabBulletsCercleDesax[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2 + Constantes._LARGEUR_BOSS / 7 * i, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 7 * i), "bullet");
                else
                    _tabBulletsCercleDesax[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2 - Constantes._LARGEUR_BOSS / 7 * i, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 7 * i), "bullet");
            }
        }
        private void UpdatePatternsFocus(int debut, Bullet[] tab, float deltaTime)
        {
            if (_chrono > debut)
                PatternFocus(tab, deltaTime, _angle);
        }

    }
}

