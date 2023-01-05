using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace SAE_DEV_PROJ
{
    public class PlayScreen : GameScreen
    {
        private Game1 _myGame;
        private SpriteBatch _spriteBatch;
        private Texture2D _texturePerso;
        internal Bullet[,] _tabBullets = new Bullet[10,10];
        internal Bullet[] _tabBulletPerso = new Bullet[200];
        internal Bullet[] _tabBullets2 = new Bullet[40];
        internal Bullet[] _tabBulletsCercle = new Bullet[36];
        internal Bullet[] _tabBulletsSpirale = new Bullet[36];
        internal Boss boss1;
        internal Perso hero;
        private System.Timers.Timer _timer;
        private double _tmp;
        private bool _redemption;
        private double _chrono;
        private double _chrono2;
        private int _i;
        private int var;
        private int var2;
        private int var3;
        private float _angle = 0f;
        private double _pvDepart;
        private Vector2 _positionPv = new Vector2(20, 30);
        private Vector2 _positionPvBoss = new Vector2(20, 80);
        private Vector2 _positionScore = new Vector2(20, 200);
        private int _damagePerso;
        bool test = false;
        public bool _alive=true;
        public bool _bossAlive=true;

        // TEXTURES 
        private Texture2D _textureBoss;
        private Texture2D _textureBullet;
        private SpriteFont _police;

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

        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public PlayScreen(Game1 game) : base(game)
        {
            _myGame = game;

        }

        public override void Initialize()
        {
            _bossAlive = true;
            _alive = true;
            var = 0;
            _i = -1;
            _chrono = 0;
            _chrono2 = 0;
            var2 = 2;
            var3 = 2;

            boss1 = new Boss(5000, 20, "boss", new Vector2(Constantes._LARGEUR_FENETRE / 2, Constantes._HAUTEUR_FENETRE / 5) - new Vector2(Constantes._LARGEUR_BOSS / 2, 0));
            hero = new Perso(false, 100, 5, 0, "perso", 1, 500, new Vector2(500, 500) - new Vector2(Constantes._LARGEUR_PERSO / 2, 0));

            _damagePerso = hero.DamagePerso;
            _pvDepart = hero.PvPerso;
            // Bullets initialize

            // Bullets initialize
            for (int i = 0; i < _tabBullets.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1); j++)
                {
                    _tabBullets[i,j] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS), "bullet");
                }
            }
            // BulletsAlliées initialize
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                _tabBulletPerso[i] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * 2), "allié");
            }
            // Bullets pattern 2 initialize
            for (int i = 0; i < _tabBullets2.Length; i++)
            {
                _tabBullets2[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS), "bullet");
            }
            // Bullets pattern cercle initialize
            for (int i = 0; i < _tabBulletsCercle.Length; i++)
            {
                _tabBulletsCercle[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS), "bulletSpiral");
            }
            // Bullets pattern spirale initialize (gerer les spawns avec i (?))
            for (int i = 0; i < _tabBulletsSpirale.Length; i++)
            {
                _tabBulletsCercle[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bulletCercle");
                float bulletDirectionX = MathF.Sin(_angle * MathF.PI / 180f);
                float bulletDirectionY = MathF.Cos(_angle * MathF.PI / 180f);
                Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                _tabBulletsSpirale[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2) - bulletDirection*i*2, "bulletSpiral");
                _angle += 10f;
            }

            _angle = 0f;
            // Initialize timer spirale
            _timer = new System.Timers.Timer(100);
            _timer.AutoReset = true;
            _timer.Enabled = true;


            _police = Content.Load<SpriteFont>("Font");

            base.Initialize();
        }

        public override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texturePerso = Content.Load<Texture2D>("perso");
            _textureBullet = Content.Load<Texture2D>(_tabBullets[0,0].Skin);
            _textureBoss = Content.Load<Texture2D>(boss1.SkinBoss);

            _texture_Full = Content.Load<Texture2D>("Full");
            _texture_High = Content.Load<Texture2D>("High");
            _texture_Mid = Content.Load<Texture2D>("Mid");
            _texture_Low = Content.Load<Texture2D>("Low");
            _texture_VeryLow = Content.Load<Texture2D>("VeryLow");
            _texture_Dead = Content.Load<Texture2D>("Dead");
            // TODO: use this.Content to load your game content here
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _myGame._screenDeathOk = false;
            _myGame._screenWinOk = false;
            _myGame._actif = false;
            Console.WriteLine((hero.PvPerso / _pvDepart) * 100);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //pattern1 pour les différentes "vague de bullets"
            _chrono += deltaTime;
            if (_chrono >= var && _i<_tabBullets.GetLength(0)-1)
            {
                var += 2;
                _i++;
              
            }
            if (_chrono<25)
                Pattern1(deltaTime, _i, _chrono);
            if(_chrono >= var2)
            {
                if(hero.Score >= 100)
                    hero.Score -= 100;
                var2 += 3;
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
            Redemption(deltaTime);

            //lancer pattern2 au bout de 24 sec
            if(_chrono>=22)
                Pattern2(deltaTime);
            
            DeplacementPerso(deltaTime);
            CollisionBoss();
            PatternCercle(_angle);
            PatternSpirale(_angle);
            CheckBossDead(boss1);
            BulletAllieReset();
        }

        public override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.BlueViolet);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_police, $"Vie Hero : {hero.PvPerso}", _positionPv, Color.White);
            _spriteBatch.DrawString(_police, $"Vie Boss : { boss1.BossHP}", _positionScore, Color.White);


            //Bullets adverses
            for (int z = 0; z <= _i; z++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1); j++)
                {
                    _spriteBatch.Draw(_textureBullet, _tabBullets[z, j].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
                }
            }

            //Bullets alliées
            if (_redemption == false)
            {
                for (int i = 0; i < _tabBulletPerso.Length; i++)
                {
                    if (!(_tabBulletPerso[i].BulletPosition.Y > hero.PositionPerso.Y))
                        _spriteBatch.Draw(_textureBullet, _tabBulletPerso[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.White);
                }
            }

            //Bullets pattern2
            for (int i = 0; i < _tabBullets2.Length; i++)
            {
                _spriteBatch.Draw(_textureBullet, _tabBullets2[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
            }

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

            _spriteBatch.DrawString(_police, $"{hero.PvPerso} / {_pvDepart}", new Vector2(_positionPv.X * 10, _positionPv.Y + 10), Color.Black);
            
            //Bullets patternCercle
            for (int i = 0; i < _tabBulletsCercle.Length; i++)
            {
                _spriteBatch.Draw(_textureBullet, _tabBulletsCercle[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
            }

            //Bullets patternSpiral
            for (int i = 0; i < _tabBulletsSpirale.Length; i++)
            {
                _spriteBatch.Draw(_textureBullet, _tabBulletsSpirale[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
            }

            _spriteBatch.Draw(_textureBoss, boss1.BossPosition, Color.White);
            _spriteBatch.Draw(_texturePerso, hero.PositionPerso, Color.White);

            _spriteBatch.End();
            
        }
        private void DeplacementPerso(float deltaTime)
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Q) && !(_keyboardState.IsKeyDown(Keys.D)) && hero.PositionPerso.X >= 0)
                _sensPersoX = -1;

            else if (_keyboardState.IsKeyDown(Keys.D) && !(_keyboardState.IsKeyDown(Keys.Q)) && hero.PositionPerso.X <= Constantes._LARGEUR_FENETRE - Constantes._LARGEUR_PERSO)
                _sensPersoX = 1;

            if (_keyboardState.IsKeyDown(Keys.Z) && !(_keyboardState.IsKeyDown(Keys.S)) && hero.PositionPerso.Y >= 0)
                _sensPersoY = -1;

            else if (_keyboardState.IsKeyDown(Keys.S) && !(_keyboardState.IsKeyDown(Keys.Z)) && hero.PositionPerso.Y <= Constantes._HAUTEUR_FENETRE - Constantes._HAUTEUR_PERSO)
                _sensPersoY = 1;

            hero.PositionPerso += new Vector2(_sensPersoX * (int)Math.Round(hero.DeplacementPerso * hero.MultiplicationVitesse,0) * deltaTime,0);
            _sensPersoX = 0;

            hero.PositionPerso += new Vector2(0,_sensPersoY * (int)Math.Round(hero.DeplacementPerso * hero.MultiplicationVitesse, 0) * deltaTime);
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

            for (int i = 0; i < _tableau.GetLength(0); i++)
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
        public void CollisionBoss()
        {
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                Rectangle rect1 = new Rectangle((int)_tabBulletPerso[i].BulletPosition.X, (int)_tabBulletPerso[i].BulletPosition.Y, Constantes._LARGEUR_BULLETS, Constantes._HAUTEUR_BULLETS);
                Rectangle rect2 = new Rectangle((int)boss1.BossPosition.X, (int)boss1.BossPosition.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);

                if (rect1.Intersects(rect2))
                {
                    boss1.BossHP -= hero.DamagePerso;
                    _tabBulletPerso[i].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * 2);

                    if (_redemption == false)
                        hero.Score += 10;
                }
            }
        }

        // Collision pour le score
        public bool Collision()
        {
            bool tmp = false;
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                
                Rectangle rect1 = new Rectangle((int)_tabBulletPerso[i].BulletPosition.X, (int)_tabBulletPerso[i].BulletPosition.Y, Constantes._LARGEUR_BULLETS, Constantes._HAUTEUR_BULLETS);
                Rectangle rect2 = new Rectangle((int)boss1.BossPosition.X, (int)boss1.BossPosition.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);
                if (rect1.Intersects(rect2))
                {
                    tmp = true;
                }
            }
            return tmp;
        }

        public void BulletAllieReset()
        {
        // Une fois arrivée en bas , les bullets sont remises en-dessous de la fenêtre
            for (int i = 0; i<_tabBulletPerso.Length; i++)
            {
                if (_tabBulletPerso[i].BulletPosition.Y <= 0)
                {
                    _tabBulletPerso[i].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * 2);
                }
            }
        }
        public void Pattern1(float deltaTime, int i, double chrono)
        {
            Random rdn = new Random();
            if (chrono < 24)
            {
                for (int z = 0; z <= i; z++)
                {
                    for (int j = 0; j < _tabBullets.GetLength(1) - 2; j++)
                    {

                        _tabBullets[z, j].BulletPosition += new Vector2(rdn.Next(-50, 50), _tabBullets[z, j].Vitesse * deltaTime);
                    }
                }
            }
            else
            {
                for (int z = 0; z <= i; z++)
                {
                    for (int j = 0; j < _tabBullets.GetLength(1) - 2; j++)
                    {

                        _tabBullets[z, j].BulletPosition = new Vector2(-20,-20);
                    }
                }
            }
        }

        public void Pattern2(float deltaTime)
        {
            for (int i = 0; i < _tabBullets2.Length; i++)
            {
                if (i % 5 == 0)
                    _tabBullets2[i].BulletPosition += new Vector2(i * 2 * deltaTime,i * 2 * deltaTime);
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

        //Ca c'est la galère ptdr aled
        public void PatternCercle(float angle)
        {
            for (int i = 0; i < _tabBulletsCercle.Length; i++)
            {
                float bulletDirectionX = MathF.Sin(angle * MathF.PI / 180f);
                float bulletDirectionY = MathF.Cos(angle * MathF.PI / 180f);
                Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                _tabBulletsCercle[i].BulletPosition += bulletDirection;

                angle += 10f;
            }
        }

        //galère suite AAAAAAAAAAAAAAAAAAAAAAA(pattern spirale)
        public void PatternSpirale(float angle)
        {
            for (int i = 0; i < _tabBulletsSpirale.Length; i++)
            {
                float bulletDirectionX = MathF.Sin(angle * MathF.PI / 180f);
                float bulletDirectionY = MathF.Cos(angle * MathF.PI / 180f);
                Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                _tabBulletsSpirale[i].BulletPosition += bulletDirection;
            
                angle += 10f;
            }
            
        }


        public void Redemption(float deltaTime)
        {
            if (Collision(_redemption, _tabBullets2)||Collision(_redemption, _tabBullets) && _redemption == false)
            {
                //_alive = true; // pour etre sur
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
            }
            if (hero.PvPerso <= 0)
            {
                _alive = false;
            }
        }
        internal void CheckBossDead(Boss boss)
        {
            if (boss.BossHP<=0)
            {
                _bossAlive = false;
                boss.BossHP = 0;
            }
        }
    }
}

