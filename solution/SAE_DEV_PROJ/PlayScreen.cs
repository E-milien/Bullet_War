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
        internal Boss boss1;
        internal Perso hero;
        private double _tmp;
        private bool _redemption;
        private double _chrono;
        private int _i;
        private int var;
        private Vector2 _positionPv = new Vector2(20, 20);
        private Vector2 _positionScore = new Vector2(20, 60);

        // TEXTURES 
        private Texture2D _textureBoss;
        private Texture2D _textureBullet;
        private SpriteFont _police; 


        // BOSS
        Vector2 _bossPos;
        Vector2 _persoPos;

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
            var = 0;
            _i = -1;
            // TODO: Add your initialization logic here
            _chrono = 0;
            _persoPos = new Vector2(500, 500) - new Vector2(Constantes._LARGEUR_PERSO / 2,0);
            _bossPos = new Vector2(Constantes._LARGEUR_FENETRE / 2, Constantes._HAUTEUR_FENETRE / 5) - new Vector2(Constantes._LARGEUR_BOSS / 2, 0);

            boss1 = new Boss(5000, 20, "boss", _bossPos);
            hero = new Perso(true, 100, 5, "perso", 1, 500, _persoPos);

            // Bullets initialize

            for (int i = 0; i < _tabBullets.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1); j++)
                {
                    _tabBullets[i,j] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(_bossPos.X, _bossPos.Y + Constantes._HAUTEUR_BOSS), "bullet");
                }
            }
            // BulletsAlliées initialize
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                _tabBulletPerso[i] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_persoPos.X + Constantes._LARGEUR_PERSO / 2, _persoPos.Y + i * Constantes._HAUTEUR_BULLETS * 2), "allié");
            }
            // Bullets pattern 2 initialize
            for (int i = 0; i < _tabBullets2.Length; i++)
            {
                _tabBullets2[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2(_bossPos.X + Constantes._LARGEUR_BOSS / 2, _bossPos.Y + Constantes._HAUTEUR_BOSS), "bullet");
            }

            _police = Content.Load<SpriteFont>("Font");

            base.Initialize();
        }

        public override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texturePerso = Content.Load<Texture2D>("perso");
            _textureBullet = Content.Load<Texture2D>(_tabBullets[0,0].Skin);
            _textureBoss = Content.Load<Texture2D>(boss1.SkinBoss);


            // TODO: use this.Content to load your game content here
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here
            _chrono += deltaTime;
            if (_chrono >= var && _i<_tabBullets.GetLength(0))
            {
                var += 2;
                _i++;
            }


            Patern(deltaTime, _i);
            //tirs alliés
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                if (_tabBulletPerso[i].BulletPosition.Y > _persoPos.Y)
                {
                    if (_tabBulletPerso[i].BulletPosition.X != _persoPos.X)
                        _tabBulletPerso[i].BulletPosition = new Vector2(_persoPos.X + Constantes._LARGEUR_PERSO / 2, _tabBulletPerso[i].BulletPosition.Y);
                }
                _tabBulletPerso[i].BulletPosition -= new Vector2(0, _tabBulletPerso[i].Vitesse * deltaTime);
            }

            Redemption(deltaTime);
            Pattern2(deltaTime);
            DeplacementPerso(deltaTime);
            BulletAllieReset();
            

            CollisionBoss();

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texturePerso, _persoPos, Color.White);
            _spriteBatch.Draw(_textureBoss, _bossPos, Color.White);
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
                    if (!(_tabBulletPerso[i].BulletPosition.Y > _persoPos.Y))
                        _spriteBatch.Draw(_textureBullet, _tabBulletPerso[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.White);
                }
            }

            //Bullets pattern2
            for (int i = 0; i < _tabBullets2.Length; i++)
            {
                _spriteBatch.Draw(_textureBullet, _tabBullets2[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
            }
            _spriteBatch.End();
        }

        private void DeplacementPerso(float deltaTime)
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Q) && !(_keyboardState.IsKeyDown(Keys.D)) && _persoPos.X >= 0)
                _sensPersoX = -1;

            else if (_keyboardState.IsKeyDown(Keys.D) && !(_keyboardState.IsKeyDown(Keys.Q)) && _persoPos.X <= Constantes._LARGEUR_FENETRE - Constantes._LARGEUR_PERSO)
                _sensPersoX = 1;

            if (_keyboardState.IsKeyDown(Keys.Z) && !(_keyboardState.IsKeyDown(Keys.S)) && _persoPos.Y >= 0)
                _sensPersoY = -1;

            else if (_keyboardState.IsKeyDown(Keys.S) && !(_keyboardState.IsKeyDown(Keys.Z)) && _persoPos.Y <= Constantes._HAUTEUR_FENETRE - Constantes._HAUTEUR_PERSO)
                _sensPersoY = 1;

            _persoPos.X += _sensPersoX * (int)Math.Round(hero.DeplacementPerso * hero.MultiplicationVitesse,0) * deltaTime;
            _sensPersoX = 0;

            _persoPos.Y += _sensPersoY * (int)Math.Round(hero.DeplacementPerso * hero.MultiplicationVitesse, 0) * deltaTime;
            _sensPersoY = 0;
        }
        public bool Collision(bool ok)
        {
            bool tmp = false;

            if (ok == true)
                return false;

            for (int i = 0; i < _tabBullets.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1); j++)
                {
                    Rectangle rect1 = new Rectangle((int)_tabBullets[i, j].BulletPosition.X, (int)_tabBullets[i, j].BulletPosition.Y, Constantes._LARGEUR_BULLETS, Constantes._HAUTEUR_BULLETS);
                    Rectangle rect2 = new Rectangle((int)_persoPos.X, (int)_persoPos.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);
                    if (rect1.Intersects(rect2))
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
                Rectangle rect2 = new Rectangle((int)_bossPos.X, (int)_bossPos.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);

                if (rect1.Intersects(rect2))
                {
                    boss1.BossHP -= hero.DamagePerso;
                    _tabBulletPerso[i].BulletPosition = new Vector2(_persoPos.X + Constantes._LARGEUR_PERSO / 2, _persoPos.Y + i * Constantes._HAUTEUR_BULLETS * 2);
                }
            }
        }
        public void BulletAllieReset()
        {
        // Une fois arrivée en bas , les bullets sont remises en-dessous de la fenêtre
            for (int i = 0; i<_tabBulletPerso.Length; i++)
            {
                if (_tabBulletPerso[i].BulletPosition.Y <= 0)
                {
                    _tabBulletPerso[i].BulletPosition = new Vector2(_persoPos.X + Constantes._LARGEUR_PERSO / 2, _persoPos.Y + i * Constantes._HAUTEUR_BULLETS * 2);
                }
            }
        }
        public void Patern(float deltaTime, int i)
        {
            Random rdn = new Random();

            for (int z = 0; z <= i; z++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1) - 2; j++)
                {

                    _tabBullets[z, j].BulletPosition += new Vector2(rdn.Next(-50, 50), _tabBullets[z, j].Vitesse * deltaTime);
                }
            }
        }

        public void Pattern2(float deltaTime)
        {
            for (int i = 0; i < _tabBullets2.Length; i++)
            {
                if (i % 4 == 0)
                    _tabBullets2[i].BulletPosition += new Vector2(i * 2 * deltaTime,i * 2 * deltaTime);
                else if (i % 4 == 1)
                    _tabBullets2[i].BulletPosition += new Vector2(-i * 2 * deltaTime, i * 2 * deltaTime);
                else if (i % 4 == 2)
                    _tabBullets2[i].BulletPosition += new Vector2(i * deltaTime, i * 3 * deltaTime);
                else
                    _tabBullets2[i].BulletPosition += new Vector2(-i * deltaTime, i * 3 * deltaTime);
            }
        }
        public void Redemption(float deltaTime)
        {
            if (Collision(_redemption) && _redemption == false)
            {
                _persoPos = new Vector2(500, 500);
                hero.PvPerso -= (int)boss1.DamageBoss;
                _redemption = true;
            }
            if (_redemption)
            {
                _tmp += deltaTime;
            }
            if (_tmp >= 2)
            {
                _tmp = 0;
                _redemption = false;
            }
            if (hero.PvPerso <= 0)
            {
                _persoPos = new Vector2(0, 0);
            }
        }
    }
}

