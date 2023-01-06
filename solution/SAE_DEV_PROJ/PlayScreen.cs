using System;
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
        internal Bullet[,] _tabBulletsCercle = new Bullet[10,36];
        internal Bullet[] _tabBulletsSpirale = new Bullet[36*10];
        internal Boss boss1;
        internal Perso hero;
        private double _tmp;
        private bool _patternSpiraleGenere;
        private bool _redemption;
        private double _chrono;
        private int _i;
        private int _i2;
        private int _var;
        private double _varCercle;
        private bool _ok1;
        private int _var2;
        private float _angle;
        private double _pvDepart;
        private Vector2 _positionPv = new Vector2(20, 30);
        private Vector2 _positionPvBoss = new Vector2(20, 100);
        private Vector2 _positionScore = new Vector2(20, 200);
        private int _damagePerso;
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
            // initialisation toutes les veriables
            _ok1 = false;
            _bossAlive = true;
            _alive = true;
            _var = 0;
            _i = -1;
            _i2 = 0;
            _chrono = 0;
            _var2 = 2;
            _varCercle = 0;
            _angle = 0f;
            _patternSpiraleGenere = true;

            for (int i = 0; i < _tabBulletsSpirale.Length; i++)
            {

            }

            // initialisation boss & perso
            boss1 = new Boss(1, 20, "boss", new Vector2(Constantes._LARGEUR_FENETRE / 2, Constantes._HAUTEUR_FENETRE / 5) - new Vector2(Constantes._LARGEUR_BOSS / 2, 0));
            hero = new Perso(false, 1, 5, 0, "perso", 1, 500, new Vector2(500, 500) - new Vector2(Constantes._LARGEUR_PERSO / 2, 0));

            _damagePerso = hero.DamagePerso;
            _pvDepart = hero.PvPerso;

            // Bullets initialize
            for (int i = 0; i < _tabBullets.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1); j++)
                {
                    _tabBullets[i,j] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bullet");
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
                _tabBullets2[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bullet");
            }
            // Bullets pattern cercle initialize
            for (int i = 0; i < _tabBulletsCercle.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBulletsCercle.GetLength(1); j++)
                {
                    _tabBulletsCercle[i,j] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2), "bulletSpiral");
                }
            }

            base.Initialize();
        }

        public override void LoadContent()
        {
            _police = Content.Load<SpriteFont>("Font");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texturePerso = Content.Load<Texture2D>("perso");
            _textureBullet = Content.Load<Texture2D>(_tabBullets[0,0].Skin);
            _textureBoss = Content.Load<Texture2D>(boss1.SkinBoss);

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
            _myGame._screenDeathOk = false;
            _myGame._screenWinOk = false;
            _myGame._actif = false;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //pattern1 pour les différentes "vague de bullets"
            _chrono += deltaTime;

            //perd du score a cause du temps
            if(_chrono >= _var2)
            {
                if(hero.Score >= 100)
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
            Redemption(deltaTime);
            // active le 1er partterne (pattern1)
            if (_chrono< 19)
               Pattern1(deltaTime, _i, _chrono);
            //lancer pattern2 au bout de 24 sec
            if(_chrono>=17&&_chrono<=25)
               Pattern2(deltaTime);
            //active le 3eme patterne (paterncercle)
            if (_chrono > 22 && _chrono < 42)
            {
                PatternCercle(_angle);
                if (!_ok1)
                    _varCercle = _chrono;
                    _ok1 = true;
            }
            //génère le pattern spirale a 10s
            if (_chrono >= 10 && _patternSpiraleGenere == true)
                InitializeSpirale();
            //active le pattern spirale après 10s
            if (_chrono > 10 && _chrono < 20)
                PatternSpirale(_angle);

            DeplacementPerso(deltaTime);
            CollisionBoss();
            
            CheckBossDead(boss1);
            BulletAllieReset();

        }

        public override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.BlueViolet);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_police, $"Vie Boss : { boss1.BossHP}", _positionPvBoss, Color.White);
            _spriteBatch.DrawString(_police, $"Score : {hero.Score}", new Vector2(_positionScore.X, _positionScore.Y - 50), Color.White);
            
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

            //Bullets pattern 1
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
            
            //Bullets patternCercle
            for (int i = 0; i <= _i2; i++)
            {
                for (int j = 0; j < _tabBulletsCercle.GetLength(1); j++)
                {
                    _spriteBatch.Draw(_textureBullet, _tabBulletsCercle[i,j].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
                }
            }

            //Bullets patternSpiral
            if (_chrono > 10)
            {
                for (int i = 0; i < _tabBulletsSpirale.Length; i++)
                {
                    if (_tabBulletsSpirale[i].PasseOrigine == true)
                        _spriteBatch.Draw(_textureBullet, _tabBulletsSpirale[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
                }
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
                Rectangle rect2 = new Rectangle((int)boss1.BossPosition.X, (int)boss1.BossPosition.Y, Constantes._LARGEUR_BOSS, Constantes._HAUTEUR_BOSS);

                if (rect1.Intersects(rect2))
                {
                    boss1.BossHP -= hero.DamagePerso;
                    _tabBulletPerso[i].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * 2);

                    if (_redemption == false)
                        hero.Score += 10;
                }
            }
        }

        // Une fois arrivée en bas , les bullets sont remises en-dessous de la fenêtre
        public void BulletAllieReset()
        {
            for (int i = 0; i<_tabBulletPerso.Length; i++)
            {
                if (_tabBulletPerso[i].BulletPosition.Y <= 0)
                {
                    _tabBulletPerso[i].BulletPosition = new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * 2);
                }
            }
        }

        // 1
        public void Pattern1(float deltaTime, int i, double chrono)
        {
            if (chrono >= _var && _i < _tabBullets.GetLength(0))
            {
                _var += 2;
                _i++;
            }

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

        // 2
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

        // 3
        public void PatternCercle(float angle)
        {
            if (_chrono >= _varCercle&& _i2 < _tabBulletsCercle.GetLength(1))
            {
                _varCercle += 3;
                _i2++;
            }
            if (_chrono < 42)
            {
                for (int z = 0; z < _i2; z++)
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
                for (int z = 0; z < _i2; z++)
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

        // redemption de 2 secondes après être touché
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
                //on remet les bullets au dessus du perso
                for (int i = 0; i < _tabBulletPerso.Length; i++)
                {
                    _tabBulletPerso[i] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(hero.PositionPerso.X + Constantes._LARGEUR_PERSO / 2, hero.PositionPerso.Y + i * Constantes._HAUTEUR_BULLETS * 2), "allié");
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
            if (boss.BossHP<=0)
            {
                _bossAlive = false;
                boss.BossHP = 0;
            }
        }

        public void InitializeSpirale()
        {
            // Bullets pattern spirale initialize (gerer les spawns avec i (?))
            for (int i = 0; i<_tabBulletsSpirale.Length; i++)
            {
                float bulletDirectionX = MathF.Sin(_angle * MathF.PI / 180f);
                float bulletDirectionY = MathF.Cos(_angle * MathF.PI / 180f);
                Vector2 bulletDirection = new Vector2(bulletDirectionX, bulletDirectionY);

                _tabBulletsSpirale[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(boss1.BossPosition.X + Constantes._LARGEUR_BOSS / 2, boss1.BossPosition.Y + Constantes._HAUTEUR_BOSS / 2) - bulletDirection* i*2, "bulletSpiral",false);
                _angle += 10f;
            }
            _patternSpiraleGenere = false;
        }
    }
}

