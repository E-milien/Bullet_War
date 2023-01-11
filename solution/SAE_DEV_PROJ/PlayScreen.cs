using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Linq;

namespace SAE_DEV_PROJ
{
    public class PlayScreen : GameScreen
    {
        // VARAIBLES A RENOMMER (un vrai frère)
        private int _i1;
        private int _i2;
        private int _i3;
        private double _tmp48;
        private double _tmpC;
        private double _tmpC2;
        private double _tmp;
        private bool _ok1;
        private bool _ok2;
        private bool _ok3;
        private int _var2;
        private double _var;
        
        private Game1 _myGame;
        private SpriteBatch _spriteBatch;
        private Texture2D _texturePerso;

        //Bullets du perso allié
        internal Bullet[] _tabBulletPerso = new Bullet[200];
        internal Bullet[,] _tabBulletPersoCote = new Bullet[200,2];
        //Pattern random
        internal Bullet[,] _tabBulletsRandom = new Bullet[8,10];
        //Patterns fourchette
        internal Bullet[] _tabBulletsFourchette1 = new Bullet[80];
        internal Bullet[] _tabBulletsFourchette2 = new Bullet[80];
        //Pattern Cercle
        internal Bullet[,] _tabBulletsCercle = new Bullet[7,36];
        //Patterns spirale
        internal Bullet[] _tabBulletsSpirale1 = new Bullet[36 * 10];
        internal Bullet[] _tabBulletsSpirale2 = new Bullet[36 * 8];
        //Patterns desaxés
        internal Bullet[] _tabBulletsCercleDesax1 = new Bullet[36 * 2];
        internal Bullet[] _tabBulletsCercleDesax2 = new Bullet[36 * 4];

        //Dernier pattern 
        internal Bullet[] _tabBulletsSpiraleFinal = new Bullet[36 * 10];
        internal Bullet[] _tabBulletsFourchetteFinal = new Bullet[80];
        internal Bullet[,] _tabBulletsCercleFinal = new Bullet[7, 36];
        internal Bullet[] _tabBulletsCercleDesaxFinal = new Bullet[36 * 2];

        //Création de tous les patterns qui focusent le personnage
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
        internal Bullet[] _tabBulletFocus17 = new Bullet[36];
        internal Bullet[] _tabBulletFocus18 = new Bullet[36];
        internal Bullet[] _tabBulletFocus19 = new Bullet[36];
        internal Bullet[] _tabBulletFocus20 = new Bullet[36];


        //quand on se fait toucher
        private bool _redemption;
        public double _chrono;
        public double _chronoPause;
        private double _varCercle;
        private float _angle;
        private double _pvDepart;
        private Vector2 _positionPv;
        private Vector2 _positionPvBoss;
        private Vector2 _positionScore;
        private Vector2 _positionCoin;

        //relance et ré-initialiaze les patterns focus une 2e fois, allant 2 fois plus vite
        private bool _allFocusUsed;
        private int _allFocusUsedAdd;
        private int _allFocusUsedMult;


        private int _largeurBarreHp;
        private int _damagePerso;
        public bool _alive=true;
        public bool _bossAlive=true;
        private Color _couleur;
        private Color _couleurPerso;
        private double _tmpVie;

        private int _sequenceTir;

        // TEXTURES 
        private Texture2D _textureBulletAllieCote;
        private Texture2D _textureMenu;
        private Texture2D _textureFondPause;
        private Texture2D _textureBoss;
        private Texture2D _textureBullet1;
        private Texture2D _textureBulletAllie;
        private Texture2D _textureAttentionPattern5;
        private SpriteFont _police;
        private Texture2D _textureCoin;
        public Texture2D _textureCoin2;

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

        // PERSO
        private int _sensPersoX;
        private int _sensPersoY;
        private KeyboardState _keyboardState;

        private Rectangle _hitboxResumeButton;
        private MouseState _ms;

        private int _compteurScoreReset;

        public PlayScreen(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {
            //upgrade qui augmente la fréquence de tir
            if (_myGame._upgradeRafale == true)
                _sequenceTir = 3;

            else
                _sequenceTir = 4;

            _positionCoin = new Vector2(-50, -50);
            _couleur = Color.White;
            _couleurPerso = Color.White;
            _bossAlive = true;
            _alive = true;
            _chrono = 0;
            _chronoPause = 0;
            _varCercle = 0;
            _angle = 0f;
            _positionPv = new Vector2(20, 30);
            _positionPvBoss = new Vector2(20, 100);
            _positionScore = new Vector2(20, 200);
            _boutonMenuResume = _textureButtonMenu;
            _boutonMenuHome = _textureButtonMenu;
            _boutonMenuExit = _textureButtonMenu;
            _compteurScoreReset = 0;
            _tmpVie = Constantes._PATTERNFINALFIN;
            _tmp48 = 0;
            _var = 40;
            _i1 = -1;
            _i2 = -1;
            _var2 = 2;
            _ok1 = false;
            _ok2 = false;
            _tmpC = 6;

            _allFocusUsed = false;
            _allFocusUsedAdd = 0;
            _allFocusUsedMult = 2;

            // SETUP PERSO

            _myGame._score = 0;
            _myGame._hpBoss = Constantes._HPBOSS;
            _myGame._hpPerso = Constantes._HPERSO;
            

            _myGame.boss1 = new Boss(_myGame._hpBoss, 20, "bossMechant", new Vector2(Constantes._LARGEUR_FENETRE / 2, Constantes._HAUTEUR_FENETRE / 5) - new Vector2(Constantes._LARGEUR_BOSS / 2, 0));
            _myGame.hero = new Perso(false, _myGame._hpPerso, 5, _myGame._score, "vaisseau", 1, 500, new Vector2(Constantes._LARGEUR_FENETRE / 2, Constantes._HAUTEUR_FENETRE * 2 / 3) - new Vector2(Constantes._LARGEUR_PERSO / 2, Constantes._HAUTEUR_PERSO / 2));

            _largeurBarreHp = 578;
            _hitboxResumeButton = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 300, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);

            _damagePerso = _myGame.hero.DamagePerso;
            _pvDepart = _myGame.hero.PvPerso;

            // Bullets random initialize
            for (int i = 0; i < _tabBulletsRandom.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBulletsRandom.GetLength(1); j++)
                {
                    _tabBulletsRandom[i, j] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bullet");
                }
            }
            // BulletsAlliées initialize
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                _tabBulletPerso[i] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
            }
            // BulletsAlliéesCoté initialize
            if (_myGame._upgradeCote == true)
            {
                for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                {
                    for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                    {
                        if (j == 1)
                            _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                        else
                            _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                    }
                }
            }
            // Bullets pattern fourchette initialize
            InitializePatternFourchette(_tabBulletsFourchette1);
            InitializePatternFourchette(_tabBulletsFourchette2);
            InitializePatternFourchette(_tabBulletsFourchetteFinal);

            // Bullets pattern cercle initialize
            InitializePatternCercle(_tabBulletsCercle);
            InitializePatternCercle(_tabBulletsCercleFinal);

            //Bullets focus perso init
            TabBulletFocusInitialize();
            
            //Spirales
            InitializeSpirale(_tabBulletsSpirale1,false);
            InitializeSpirale(_tabBulletsSpirale2,false);
            InitializeSpirale(_tabBulletsSpiraleFinal,true);

            //Cercles desax
            InitializeCercleDesax(_tabBulletsCercleDesax1);
            InitializeCercleDesax(_tabBulletsCercleDesax2);
            InitializeCercleDesax(_tabBulletsCercleDesaxFinal);

            base.Initialize();
        }

        public override void LoadContent()
        {
            _police = Content.Load<SpriteFont>("Font");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            if (_myGame._skinD)
                _texturePerso = Content.Load<Texture2D>("donald");
            else if (_myGame._skinV)
                _texturePerso = Content.Load<Texture2D>("vaisseau2");
            else if (_myGame._skinO)
                _texturePerso = Content.Load<Texture2D>(_myGame.hero.SkinPerso);
            else
                _texturePerso = Content.Load<Texture2D>(_myGame.hero.SkinPerso);
            _textureBullet1 = Content.Load<Texture2D>("bullet4");
            _textureBulletAllie = Content.Load<Texture2D>("ballePerso");
            _textureBulletAllieCote = Content.Load<Texture2D>("ballePersoCote");
            _textureBoss = Content.Load<Texture2D>(_myGame.boss1.SkinBoss);
            _textureCoin = Content.Load<Texture2D>("Coin");
            _textureFondPause = Content.Load<Texture2D>("pause");
            _textureMenu = Content.Load<Texture2D>("menu");
            _textureButtonMenu = Content.Load<Texture2D>("boutonM");
            _textureButtonMenuPressed = Content.Load<Texture2D>("boutonM_pressed");
            _textureAttentionPattern5 = Content.Load<Texture2D>("attention");
            _textureCoin2 = Content.Load<Texture2D>("coin2");

            // barre de vie perso
            _texture_Full = Content.Load<Texture2D>("Full");
            _texture_High = Content.Load<Texture2D>("High");
            _texture_Mid = Content.Load<Texture2D>("Mid");
            _texture_Low = Content.Load<Texture2D>("Low");
            _texture_VeryLow = Content.Load<Texture2D>("VeryLow");
            _texture_Dead = Content.Load<Texture2D>("Dead");



            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
            if((_myGame.hero.Score - (1000 * _compteurScoreReset)) >= 1000)
            {
                _compteurScoreReset++;
                _myGame._money += Constantes._GAIN_PAR_COIN;
            }

            _ms = Mouse.GetState();
            if (_keyboardState.IsKeyDown(Keys.P) && _keyboardState.IsKeyDown(Keys.I))
                    _couleur = Color.DeepPink;

            _myGame._homescreenOk = false;
            _myGame._screenDeathOk = false;
            _myGame._screenWinOk = false;
            _myGame._homescreenOk = false;
            _myGame._shopScreenOk = false;
            _myGame._playScreenOk = true;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!_myGame._pause)
            {
                if (_chrono>_tmpVie)
                {
                    _tmpVie = _chrono + 2;
                    _myGame.hero.PvPerso -= 20;
                }
                if(_chrono>=_tmp48 && !_redemption && _myGame.hero.PvPerso > 0)
                {
                    _myGame._soundShot.Play();
                    _tmp48 = _chrono + 0.1;
                }

                //pattern1 pour les différentes "vague de bullets"
                _chrono += deltaTime;
                _chronoPause = 0;

                //perd du score a cause du temps
                if (_chrono >= _var2)
                {
                    if (_myGame.hero.Score >= 100 && _bossAlive && _myGame.hero.PvPerso > 0)
                        _myGame.hero.Score -= 100;
                    _var2 += 3;
                }

                //tirs alliés

                for (int i = 0; i < _tabBulletPerso.Length; i++)
                {
                    if (_tabBulletPerso[i].BulletPosition.Y > _myGame.hero.PositionPerso.Y)
                    {
                        if (_tabBulletPerso[i].BulletPosition.X != _myGame.hero.PositionPerso.X)
                            _tabBulletPerso[i].BulletPosition = new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, _tabBulletPerso[i].BulletPosition.Y);
                    }
                    _tabBulletPerso[i].BulletPosition -= new Vector2(0, _tabBulletPerso[i].Vitesse * deltaTime);
                }

                //tirs alliés cotés
                if (_myGame._upgradeCote == true)
                {
                    for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                    {
                        for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                        {
                            if (_tabBulletPersoCote[i,j].BulletPosition.Y > _myGame.hero.PositionPerso.Y)
                            {
                                if (_tabBulletPersoCote[i,j].BulletPosition.X != _myGame.hero.PositionPerso.X)
                                {
                                    if (j == 1)
                                        _tabBulletPersoCote[i,j].BulletPosition = new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, _tabBulletPersoCote[i,j].BulletPosition.Y);
                                    else
                                        _tabBulletPersoCote[i, j].BulletPosition = new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, _tabBulletPersoCote[i, j].BulletPosition.Y);
                                }
                            }
                            _tabBulletPersoCote[i,j].BulletPosition -= new Vector2(0, _tabBulletPersoCote[i,j].Vitesse * deltaTime);
                        }
                    }
                }

                // COINS
                if (_chrono >= _tmpC)
                {
                    _tmpC = _chrono + 8;
                    _tmpC2 = _chrono + 3;
                    Random rnd = new Random();
                    _positionCoin = new Vector2(rnd.Next(0, Constantes._LARGEUR_FENETRE - 200), rnd.Next(0, Constantes._HAUTEUR_FENETRE - 200));
                }
                else if (_chrono>=_tmpC2)
                {
                    _positionCoin=new Vector2(-50, -50);
                }
                if (CollisionCoin(_redemption))
                {
                    _positionCoin = new Vector2(-50, -50);
                    _myGame._money += 5;
                    Console.WriteLine(_myGame._money);
                }

                //------------------------------------------------------------------------------------------------------------------------------------------------------//
                //------------------------------------------------------         Activation de tous les patterns         -----------------------------------------------//
                //------------------------------------------------------------------------------------------------------------------------------------------------------//

                //active le 1er pattern (patterncercle)
                if (_chrono > Constantes._DEBUTPAT1 && _chrono < Constantes._FINPAT1)
                {
                    PatternCercle(Constantes._DEBUTPAT4 - 1,_tabBulletsCercle,_i2,_angle);
                    if (!_ok2)
                        _varCercle = _chrono;
                    _ok2 = true;
                }

                //lancer pattern fourchette (2e pattern)
                PatternFourchette(Constantes._DEBUTPAT2,Constantes._DEBUTPAT3 + 1, _tabBulletsFourchette1,deltaTime);

                // Pattern cercle desaxé (3e pattern)
                if (_chrono > Constantes._DEBUTPAT3)
                    PatternCercleDesax(_tabBulletsCercleDesax1,_angle);

                // active le 4e pattern (bullets random)
                if (_chrono > Constantes._DEBUTPAT4)
                {
                    Pattern1(deltaTime);
                    if (!_ok1)
                        _var = _chrono;
                    _ok1 = true;
                }

                //active le pattern spirale (5e pattern)
                PatternSpirale(Constantes._DEBUTPAT5, _tabBulletsSpirale1, _angle);

                //active le 6e pattern (remake du pattern cercle désaxé)
                if (_chrono > Constantes._DEBUTPAT6)
                    PatternCercleDesax(_tabBulletsCercleDesax2,_angle);

                //7e pattern, retour fourchette avec le reste du pattern d'avant 
                PatternFourchette(Constantes._DEBUTPAT7, Constantes._FINPAT7, _tabBulletsFourchette2, deltaTime);

                //8e pattern, retour de la spirale
                PatternSpirale(Constantes._DEBUTPAT8, _tabBulletsSpirale2, _angle);

                //PATTERN FINAL BOOM OMG IMPOSSIBLE QUE PASA
                if (_chrono > Constantes._PATTERNFINAL)
                {
                    PatternCercle(Constantes._PATTERNFINALFIN,_tabBulletsCercleFinal,_i3,_angle);
                    if (!_ok3)
                        _varCercle = _chrono;
                    _ok3 = true;
                    PatternCercleDesax(_tabBulletsCercleDesaxFinal, _angle);
                }
                PatternFourchette(Constantes._PATTERNFINAL, Constantes._PATTERNFINALFIN, _tabBulletsFourchetteFinal, deltaTime);
                PatternSpirale(Constantes._PATTERNFINAL, _tabBulletsSpiraleFinal, _angle);

                //tous les patterns focus
                UpdatePatternsFocus(Constantes._DEBUTFOCUS1 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus1, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS2 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus2, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS3 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus3, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS4 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus4, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS5 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus5, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS6 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus6, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS7 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus7, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS8 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus8, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS9 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus9, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS10 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus10, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS11 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus11, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUS12 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus12, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSDOUBLE1 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus13, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSDOUBLE1 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus14, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSDOUBLE2 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus15, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSDOUBLE2 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus16, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSSPIR1 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus17, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSSPIR2 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus18, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSSPIR3 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus19, deltaTime);
                UpdatePatternsFocus(Constantes._DEBUTFOCUSSPIR4 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus20, deltaTime);


                DeplacementPerso(deltaTime);
                CollisionBoss();
                Redemption(deltaTime);
                CheckBossDead(_myGame.boss1);
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
                    if (!(_tabBulletPerso[i].BulletPosition.Y > _myGame.hero.PositionPerso.Y))
                    {
                        _spriteBatch.Draw(_textureBulletAllie, _tabBulletPerso[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS_PERSO / 2, 0), Color.White);
                    }
                }
            
            }

            //Bullets alliées coté
            if (_myGame._upgradeCote == true)
            {
                if (_redemption == false)
                {

                    for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                    {
                        for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                        {
                            if (!(_tabBulletPersoCote[i,j].BulletPosition.Y > _myGame.hero.PositionPerso.Y))
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
                    _spriteBatch.Draw(_textureBullet1, _tabBulletsRandom[z, j].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.LawnGreen);
                }
            }

            //Bullets pattern2 (fourchette)
            PatternFourchetteDraw(Constantes._DEBUTPAT2, Constantes._DEBUTPAT3 + 1, _tabBulletsFourchette1);
            PatternFourchetteDraw(Constantes._DEBUTPAT7, Constantes._FINPAT7, _tabBulletsFourchette2);
            PatternFourchetteDraw(Constantes._PATTERNFINAL, Constantes._PATTERNFINALFIN, _tabBulletsFourchetteFinal);

            //Bullets patternCercle
            PatternCercleDraw(Constantes._DEBUTPAT1, _i2, _tabBulletsCercle);
            PatternCercleDraw(Constantes._PATTERNFINAL, _i3, _tabBulletsCercleFinal);

            //Bullets patternSpiral
            PatternSpiraleDraw(Constantes._DEBUTPAT5, _tabBulletsSpirale1,Color.Purple);
            PatternSpiraleDraw(Constantes._DEBUTPAT8, _tabBulletsSpirale2,Color.Purple);
            PatternSpiraleDraw(Constantes._PATTERNFINAL, _tabBulletsSpiraleFinal,Color.LawnGreen);

            //Bullets patternCercleDesax

            CercleDesaxDraw(Constantes._DEBUTPAT3, _tabBulletsCercleDesax1);
            CercleDesaxDraw(Constantes._DEBUTPAT6, _tabBulletsCercleDesax2);
            CercleDesaxDraw(Constantes._PATTERNFINAL, _tabBulletsCercleDesaxFinal);

            //Draw pattern focus
            PatternFocusDraw(Constantes._DEBUTFOCUS1 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus1);
            PatternFocusDraw(Constantes._DEBUTFOCUS2 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus2);
            PatternFocusDraw(Constantes._DEBUTFOCUS3 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus3);
            PatternFocusDraw(Constantes._DEBUTFOCUS4 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus4);
            PatternFocusDraw(Constantes._DEBUTFOCUS5 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus5);
            PatternFocusDraw(Constantes._DEBUTFOCUS6 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus6);
            PatternFocusDraw(Constantes._DEBUTFOCUS7 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus7);
            PatternFocusDraw(Constantes._DEBUTFOCUS8 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus8);
            PatternFocusDraw(Constantes._DEBUTFOCUS9 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus9);
            PatternFocusDraw(Constantes._DEBUTFOCUS10 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus10);
            PatternFocusDraw(Constantes._DEBUTFOCUS11 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus11);
            PatternFocusDraw(Constantes._DEBUTFOCUS12 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus12);
            PatternFocusDraw(Constantes._DEBUTFOCUSDOUBLE1 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus13);
            PatternFocusDraw(Constantes._DEBUTFOCUSDOUBLE1 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus14);
            PatternFocusDraw(Constantes._DEBUTFOCUSDOUBLE2 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus15);
            PatternFocusDraw(Constantes._DEBUTFOCUSDOUBLE2 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus16);
            PatternFocusDraw(Constantes._DEBUTFOCUSSPIR1 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus17);
            PatternFocusDraw(Constantes._DEBUTFOCUSSPIR2 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus18);
            PatternFocusDraw(Constantes._DEBUTFOCUSSPIR3 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus19);
            PatternFocusDraw(Constantes._DEBUTFOCUSSPIR4 * _allFocusUsedMult / 2 + _allFocusUsedAdd, _tabBulletFocus20);




            _spriteBatch.Draw(_textureBoss, _myGame.boss1.BossPosition, _couleur);
            _spriteBatch.Draw(_texturePerso, _myGame.hero.PositionPerso, _couleurPerso);
            _spriteBatch.DrawString(_police, "" + Math.Round(_chrono, 2), new Vector2(Constantes._LARGEUR_FENETRE - 100, 0), _couleur);
            _spriteBatch.DrawString(_police, $"Vie Boss : { _myGame.boss1.BossHP}", _positionPvBoss, _couleur);
            _spriteBatch.DrawString(_police, $"Score : {_myGame.hero.Score}", new Vector2(_positionScore.X, _positionScore.Y - 50), _couleur);
            _spriteBatch.Draw(_textureCoin2, new Vector2(5, _positionScore.Y), _couleur);
            _spriteBatch.DrawString(_police, $"{_myGame._money}", new Vector2(60, _positionScore.Y+7), _couleur);

            //HP
            if (Math.Round((_myGame.hero.PvPerso / _pvDepart) * 100) > 80)
                _spriteBatch.Draw(_texture_Full, _positionPv, Color.White);

            else if (Math.Round((_myGame.hero.PvPerso / _pvDepart) * 100) > 60)
                _spriteBatch.Draw(_texture_High, _positionPv, Color.White);

            else if (Math.Round((_myGame.hero.PvPerso / _pvDepart) * 100) > 40)
                _spriteBatch.Draw(_texture_Mid, _positionPv, Color.White);

            else if (Math.Round((_myGame.hero.PvPerso / _pvDepart) * 100) > 20)
                _spriteBatch.Draw(_texture_Low, _positionPv, Color.White);

            else if (Math.Round((_myGame.hero.PvPerso / _pvDepart) * 100) > 0)
                _spriteBatch.Draw(_texture_VeryLow, _positionPv, Color.White);

            else
                _spriteBatch.Draw(_texture_Dead, _positionPv, Color.White);

            _spriteBatch.DrawString(_police, $"{_myGame.hero.PvPerso} / {_pvDepart}", new Vector2(_positionPv.X + _largeurBarreHp/2 - 40, _positionPv.Y + 20), Color.Black);

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
            _spriteBatch.Draw(_textureCoin, _positionCoin, Color.White);
            _spriteBatch.End();

        }
        private void DeplacementPerso(float deltaTime)
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(_myGame._left) && !(_keyboardState.IsKeyDown(_myGame._right)) && _myGame.hero.PositionPerso.X >= 0)
                _sensPersoX = -1;

            else if (_keyboardState.IsKeyDown(_myGame._right) && !(_keyboardState.IsKeyDown(_myGame._left)) && _myGame.hero.PositionPerso.X <= Constantes._LARGEUR_FENETRE - Constantes._LARGEUR_PERSO)
                _sensPersoX = 1;

            if (_keyboardState.IsKeyDown(_myGame._forward) && !(_keyboardState.IsKeyDown(_myGame._behind)) && _myGame.hero.PositionPerso.Y >= 0)
                _sensPersoY = -1;

            else if (_keyboardState.IsKeyDown(_myGame._behind) && !(_keyboardState.IsKeyDown(_myGame._forward)) && _myGame.hero.PositionPerso.Y <= Constantes._HAUTEUR_FENETRE - Constantes._HAUTEUR_PERSO)
                _sensPersoY = 1;

            _myGame.hero.PositionPerso += new Vector2(_sensPersoX * (int)Math.Round(_myGame.hero.DeplacementPerso * _myGame.hero.MultiplicationVitesse, 0) * deltaTime, 0);
            _sensPersoX = 0;

            _myGame.hero.PositionPerso += new Vector2(0, _sensPersoY * (int)Math.Round(_myGame.hero.DeplacementPerso * _myGame.hero.MultiplicationVitesse, 0) * deltaTime);
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
                    Rectangle rect2 = new Rectangle((int)_myGame.hero.PositionPerso.X, (int)_myGame.hero.PositionPerso.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);
                    if (rect1.Intersects(rect2))
                    {
                        tmp = true;
                    }
                }
            }
            return tmp;
        }
        internal bool CollisionCoin(bool ok)
        {
            bool tmp = false;
            if (ok == true)
                return false;

            Rectangle rect1 = new Rectangle((int)_positionCoin.X, (int)_positionCoin.Y, Constantes._LARGEUR_COIN, Constantes._HAUTEUR_COIN);
            Rectangle rect2 = new Rectangle((int)_myGame.hero.PositionPerso.X, (int)_myGame.hero.PositionPerso.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);
            if (rect1.Intersects(rect2))
            {
                tmp = true;
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
                Rectangle rect2 = new Rectangle((int)_myGame.hero.PositionPerso.X, (int)_myGame.hero.PositionPerso.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);
                if (rect1.Intersects(rect2))
                {
                    tmp = true;
                }
            }
            return tmp;
        }
        internal bool CollisionSpirale(bool ok, Bullet[] tabSpiraleCollision)
        {
            bool tmp = false;
            if (ok == true)
                return false;
            for (int i = 0; i < tabSpiraleCollision.Length; i++)
            {
                if (!(tabSpiraleCollision[i].PasseOrigine != true))
                {
                    Rectangle rect1 = new Rectangle((int)tabSpiraleCollision[i].BulletPosition.X, (int)tabSpiraleCollision[i].BulletPosition.Y, Constantes._LARGEUR_BULLETS, Constantes._HAUTEUR_BULLETS);
                    Rectangle rect2 = new Rectangle((int)_myGame.hero.PositionPerso.X, (int)_myGame.hero.PositionPerso.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);
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
                if (!(_tabBulletPerso[i].BulletPosition.Y > _myGame.hero.PositionPerso.Y))
                {
                    Rectangle rect1 = new Rectangle((int)_tabBulletPerso[i].BulletPosition.X, (int)_tabBulletPerso[i].BulletPosition.Y, Constantes._LARGEUR_BULLETS_PERSO, Constantes._HAUTEUR_BULLETS_PERSO);
                    Rectangle rect2 = new Rectangle((int)_myGame.boss1.BossPosition.X, (int)_myGame.boss1.BossPosition.Y, Constantes._LARGEUR_BOSS, Constantes._HAUTEUR_BOSS);

                    if (rect1.Intersects(rect2))
                    {
                        _myGame.boss1.BossHP -= _myGame.hero.DamagePerso;
                        _tabBulletPerso[i].BulletPosition = new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir);

                        if (_redemption == false && _bossAlive && _myGame.hero.PvPerso > 0)
                            _myGame.hero.Score += 10;
                    }
                }
            }
            if (_myGame._upgradeCote)
            {
                for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                {
                    for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                    {

                        if (!(_tabBulletPersoCote[i,j].BulletPosition.Y > _myGame.hero.PositionPerso.Y))
                        {
                            Rectangle rect1 = new Rectangle((int)_tabBulletPersoCote[i,j].BulletPosition.X, (int)_tabBulletPersoCote[i,j].BulletPosition.Y, Constantes._LARGEUR_BULLETS_PERSO, Constantes._HAUTEUR_BULLETS_PERSO);
                            Rectangle rect2 = new Rectangle((int)_myGame.boss1.BossPosition.X, (int)_myGame.boss1.BossPosition.Y, Constantes._LARGEUR_BOSS, Constantes._HAUTEUR_BOSS);

                            if (rect1.Intersects(rect2))
                            {
                                if (j == 1)
                                    _tabBulletPersoCote[i, j].BulletPosition = new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir);
                                else 
                                    _tabBulletPersoCote[i, j].BulletPosition = new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir);
                                _myGame.boss1.BossHP -= _myGame.hero.DamagePerso/5;
                                if (_redemption == false && _bossAlive && _myGame.hero.PvPerso > 0)
                                    _myGame.hero.Score += 1;
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
                    _tabBulletPerso[i].BulletPosition = new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir);
                }
            }
            if (_myGame._upgradeCote == true)
            {
                for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                {
                    for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                    {
                        if (_tabBulletPersoCote[i,j].BulletPosition.Y <= 0)
                        {
                            if (j == 1)
                                _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                            else
                                _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
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
        private void PatternFourchette(int debut, int fin, Bullet[] tabBulletF, float deltaTime)
        {
            if (_chrono > debut)
            {

                for (int i = 0; i < tabBulletF.Length; i++)
                {
                    if (i % 5 == 0)
                        tabBulletF[i].BulletPosition += new Vector2(i * 2 * deltaTime, i * 2 * deltaTime);
                    else if (i % 5 == 1)
                        tabBulletF[i].BulletPosition += new Vector2(-i * 2 * deltaTime, i * 2 * deltaTime);
                    else if (i % 5 == 2)
                        tabBulletF[i].BulletPosition += new Vector2(i * deltaTime, i * 3 * deltaTime);
                    else if (i % 5 == 3)
                        tabBulletF[i].BulletPosition += new Vector2(-i * deltaTime, i * 3 * deltaTime);
                    else
                        tabBulletF[i].BulletPosition += new Vector2(0, i * 3 * deltaTime);
                }
            }
            else if (_chrono > fin)
            {
                for (int i = 0; i < tabBulletF.Length; i++)
                {
                    tabBulletF[i].BulletPosition = new Vector2(-20, -20);
                }
            }
        }

        // 3
        private void PatternCercle(int fin,Bullet[,] tabBulletCercle,int compteur,float angle)
        {
            if (_chrono >= _varCercle && compteur < tabBulletCercle.GetLength(0)-1)
            {
                _varCercle += 3;
                if (compteur == _i2)
                    _i2++;
                if (compteur == _i3)
                    _i3++;
            }
            if (_chrono < fin)
            {
                for (int z = 0; z <= compteur; z++)
                {
                    for (int j = 0; j < tabBulletCercle.GetLength(1); j++)
                    {
                        float bulletDirectionX = MathF.Sin(angle * MathF.PI / 180f);
                        float bulletDirectionY = MathF.Cos(angle * MathF.PI / 180f);
                        Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                        tabBulletCercle[z, j].BulletPosition += bulletDirection;

                        angle += 10f;
                    }
                }
            }
            else
            {
                for (int z = 0; z <= compteur; z++)
                {
                    for (int j = 0; j < _tabBulletsCercle.GetLength(1); j++)
                    {
                        _tabBulletsCercle[z, j].BulletPosition = new Vector2(-20, -20);
                    }
                }

            }
        }

        // 4
        private void PatternSpirale(int debut,Bullet[] tabBulSpirale,float angle)
        {
            if (_chrono > debut)
            {
                for (int i = 0; i < tabBulSpirale.Length; i++)
                {
                    if (!(tabBulSpirale[i].BulletPosition.X <= _myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2 - 1 || tabBulSpirale[i].BulletPosition.X >= _myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2 + 1))
                    {
                        if (!(tabBulSpirale[i].BulletPosition.Y <= _myGame.boss1.BossPosition.Y + Constantes._LARGEUR_BOSS / 2 - 1 || tabBulSpirale[i].BulletPosition.Y >= _myGame.boss1.BossPosition.Y + Constantes._LARGEUR_BOSS / 2 + 1))
                            tabBulSpirale[i].PasseOrigine = true;
                    }
                    else if (_chrono > Constantes._PATTERNFINAL)
                    {
                        if (!(tabBulSpirale[i].BulletPosition.Y <= _myGame.boss1.BossPosition.Y + Constantes._LARGEUR_BOSS / 2 - 1 || tabBulSpirale[i].BulletPosition.Y >= _myGame.boss1.BossPosition.Y + Constantes._LARGEUR_BOSS / 2 + 1))
                            tabBulSpirale[i].PasseOrigine = true;
                    }

                    float bulletDirectionX = MathF.Sin(angle * MathF.PI / 180f);
                    float bulletDirectionY = MathF.Cos(angle * MathF.PI / 180f);
                    Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                        tabBulSpirale[i].BulletPosition += bulletDirection;

                    angle += 10f;
                }
            }
        }

        // 5
        private void PatternCercleDesax(Bullet[] tabBulletDesax,float angle)
        {
            if (_chrono > Constantes._DEBUTPAT3)
            {
                for (int i = 0; i < tabBulletDesax.Length; i++)
                {
                    float bulletDirectionX = MathF.Sin(angle * MathF.PI / 180f);
                    float bulletDirectionY = MathF.Cos(angle * MathF.PI / 180f);
                    Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);
                    if (i % 2 == 0)
                        tabBulletDesax[i].BulletPosition += bulletDirection;
                    else
                        tabBulletDesax[i].BulletPosition += new Vector2(-bulletDirectionX,bulletDirectionY);
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

                    tabBullet[i].BulletDirection = Vector2.Normalize(_myGame.hero.PositionPerso - tabBullet[i].BulletPosition) * tabBullet[i].Vitesse * deltaTime;
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
                    _spriteBatch.Draw(_textureBullet1, tabBullet[i].BulletPosition + new Vector2(Constantes._LARGEUR_BULLETS/2,0), Color.Orange); 
                }
            }
        }

    // redemption de 2 secondes après être touché
    public void Redemption(float deltaTime)
        {
            if ((Collision(_redemption, _tabBulletsFourchette1) && _chrono < Constantes._DEBUTPAT3 - 1) || (Collision(_redemption, _tabBulletsFourchette2) && _chrono < Constantes._FINPAT7) || Collision(_redemption, _tabBulletsCercle) || (Collision(_redemption, _tabBulletsRandom) && _chrono > Constantes._DEBUTPAT4) || (Collision(_redemption, _tabBulletsCercleDesax1) && _chrono > Constantes._DEBUTPAT3) || (Collision(_redemption, _tabBulletsCercleDesax2) && _chrono > Constantes._DEBUTPAT6) || 
                (CollisionSpirale(_redemption, _tabBulletsSpirale1) && _chrono > Constantes._DEBUTPAT5) || (CollisionSpirale(_redemption, _tabBulletsSpirale2) && _chrono > Constantes._DEBUTPAT8) || (CollisionSpirale(_redemption,_tabBulletsSpiraleFinal) || Collision(_redemption,_tabBulletsFourchetteFinal) || Collision(_redemption, _tabBulletsCercleFinal) || Collision(_redemption, _tabBulletsCercleDesaxFinal) && _chrono > Constantes._PATTERNFINAL) && _redemption == false)
            {
                //_alive = true; // pour etre sur
                _myGame.hero.PvPerso -= (int)_myGame.boss1.DamageBoss;
                _redemption = true;
            }
            //pareil pour les pattern focus
            else if (Collision(_redemption, _tabBulletFocus1) || Collision(_redemption, _tabBulletFocus2) || Collision(_redemption, _tabBulletFocus3) || Collision(_redemption, _tabBulletFocus4) || Collision(_redemption, _tabBulletFocus5) || Collision(_redemption, _tabBulletFocus6) || Collision(_redemption, _tabBulletFocus7) || Collision(_redemption, _tabBulletFocus8) || Collision(_redemption, _tabBulletFocus9) ||
                Collision(_redemption, _tabBulletFocus10) || Collision(_redemption, _tabBulletFocus11) || Collision(_redemption, _tabBulletFocus12) || Collision(_redemption, _tabBulletFocus13) || Collision(_redemption, _tabBulletFocus14) || Collision(_redemption, _tabBulletFocus15) || Collision(_redemption, _tabBulletFocus16) || Collision(_redemption, _tabBulletFocus17) || Collision(_redemption, _tabBulletFocus18) || Collision(_redemption, _tabBulletFocus19) || Collision(_redemption, _tabBulletFocus20) && _redemption == false)
            {
                _myGame.hero.PvPerso -= (int)_myGame.boss1.DamageBoss;
                _redemption = true;
            }
            if (_redemption)
            {
                _couleurPerso = Color.Red;
                _myGame.hero.DamagePerso = 0;
                _tmp += deltaTime;
            }
            if (_tmp >= 2)
            {
                _couleurPerso = Color.White;
                _tmp = 0;
                _myGame.hero.DamagePerso = _damagePerso;
                _redemption = false;
                //on remet les bullets au dessus du perso
                for (int i = 0; i < _tabBulletPerso.Length; i++)
                {
                    _tabBulletPerso[i] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                }
                for (int i = 0; i < _tabBulletPersoCote.GetLength(0); i++)
                {
                    for (int j = 0; j < _tabBulletPersoCote.GetLength(1); j++)
                    {
                        if (j == 1)
                            _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_PERSO - Constantes._LARGEUR_BULLETS_PERSO_COTE, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                        else
                            _tabBulletPersoCote[i, j] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_myGame.hero.PositionPerso.X + Constantes._LARGEUR_BULLETS_PERSO_COTE, _myGame.hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * _sequenceTir), "allié");
                    }
                }
            }
            if (_myGame.hero.PvPerso <= 0)
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

        private void InitializeSpirale(Bullet[] tabSpirale, bool moving)
        {
            // Bullets pattern spirale initialize 
            for (int i = 0; i < tabSpirale.Length; i++)
            {
                float bulletDirectionX = MathF.Sin(_angle * MathF.PI / 180f);
                float bulletDirectionY = MathF.Cos(_angle * MathF.PI / 180f);
                Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                if (moving == true)
                    tabSpirale[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(i*5 + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2) - bulletDirection * i * 2, "bulletSpiral", false);
                else
                    tabSpirale[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2) - bulletDirection * i * 2, "bulletSpiral", false);
                _angle += 10f;
            }
        }
        private void InitializeCercleDesax(Bullet [] tabDesax)
        {
            // Bullets pattern cercle desaxé initialize
            for (int i = 0; i < tabDesax.Length; i++)
            {
                if (i%2 == 0)
                    tabDesax[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2 + Constantes._LARGEUR_BOSS / 7 * i, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 7 * i), "bullet");
                else
                    tabDesax[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2 - Constantes._LARGEUR_BOSS / 7 * i, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 7 * i), "bullet");
            }
        }
        private void UpdatePatternsFocus(int debut, Bullet[] tab, float deltaTime)
        {
            if (_chrono > Constantes._VAGUEFOCUS2 && _allFocusUsed == false)
            {
                _allFocusUsed = true;
                _allFocusUsedAdd += Constantes._VAGUEFOCUS2;
                _allFocusUsedMult = 1;
                TabBulletFocusInitialize();
            }
            if (_chrono > debut)
                PatternFocus(tab, deltaTime, _angle);
        }
        private void CercleDesaxDraw(int debut, Bullet[] tabBullet)
        {
            if (_chrono > debut)
            {
                for (int i = 0; i<tabBullet.Length; i++)
                {
                    _spriteBatch.Draw(_textureBullet1, tabBullet[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS, 0), Color.Red);
                }
            }
            //PatternCercleDesax attention //ON TOUCHE PAS SVP
            else if (_chrono > Constantes._DEBUTPAT3 - 1 && _chrono < Constantes._DEBUTPAT3)
                _spriteBatch.Draw(_textureAttentionPattern5, new Vector2(555, 180), Color.White);
            else if (_chrono > Constantes._DEBUTPAT6 - 1 && _chrono < Constantes._DEBUTPAT6)
                _spriteBatch.Draw(_textureAttentionPattern5, new Vector2(555, 180), Color.White);
            else if (_chrono > Constantes._PATTERNFINAL - 1)
                _spriteBatch.Draw(_textureAttentionPattern5, new Vector2(555, 180), Color.White);
        }
        private void InitializePatternFourchette(Bullet[] tabFourchette)
        {
            // Bullets pattern fourchette initialize
            for (int i = 0; i<tabFourchette.Length; i++)
            {
                tabFourchette[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bullet");
            }
        }
        private void InitializePatternCercle(Bullet[,] tabCercle)
        {
        // Bullets pattern cercle initialize
            for (int i = 0; i<tabCercle.GetLength(0); i++)
            {
                for (int j = 0; j<tabCercle.GetLength(1); j++)
                {
                    tabCercle[i, j] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bulletCercle");
                }
            }
        }
        private void PatternFourchetteDraw(int debut, int fin, Bullet[] tabFourchetteDraw)
        {
            if (_chrono > debut && _chrono < fin)
            {
                for (int i = 0; i<tabFourchetteDraw.Length; i++)
                {
                    _spriteBatch.Draw(_textureBullet1, tabFourchetteDraw[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Cyan);
                }
            }
        }
        private void PatternSpiraleDraw(int debut, Bullet[] tabSpiraleDraw, Color couleur)
        //Bullets patternSpiral
        {
            if (_chrono > debut)
            {
                for (int i = 0; i<tabSpiraleDraw.Length; i++)
                {
                    if (tabSpiraleDraw[i].PasseOrigine == true)
                    { 
                        _spriteBatch.Draw(_textureBullet1, tabSpiraleDraw[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), couleur);
                    }
                }
            }
        }
        private void PatternCercleDraw(int debut, int compteur, Bullet[,] tabCercle)
        {
        //Bullets patternCercle
            if (_chrono > debut)
            {
                for (int i = 0; i <= compteur; i++)
                {
                    for (int j = 0; j<tabCercle.GetLength(1); j++)
                    {
                        _spriteBatch.Draw(_textureBullet1, tabCercle[i, j].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Yellow);
                    }
                }
            }
        }

        public void TabBulletFocusInitialize()
        {
            for (int i = 0; i < _tabBulletFocus1.Length; i++)
            {
                _tabBulletFocus1[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus2[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus3[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus4[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus5[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus6[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus7[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus8[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus9[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus10[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus11[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus12[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus13[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(Constantes._LARGEUR_FENETRE / 3, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus14[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(2 * Constantes._LARGEUR_FENETRE / 3, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus15[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(Constantes._LARGEUR_FENETRE / 3, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus16[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(2 * Constantes._LARGEUR_FENETRE / 3, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus17[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus18[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus19[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
                _tabBulletFocus20[i] = new Bullet(Constantes._VITESSE_BULLETS2, new Vector2(_myGame.boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, _myGame.boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), new Vector2(0, 0), "bulletFocus", false);
            }
        }
    }
}

