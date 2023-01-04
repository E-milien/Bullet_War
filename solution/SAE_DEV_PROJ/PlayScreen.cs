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
        internal Bullet[] _tabBulletPerso = new Bullet[20];
        internal Boss boss1;
        internal Perso hero;

        // TEXTURES 
        private Texture2D _textureBoss;
        private Texture2D _textureBullet;

        // BOSS
        Vector2 _bossPos = new Vector2(Constantes._LARGEUR_FENETRE / 2, Constantes._HAUTEUR_FENETRE / 5);
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
            // TODO: Add your initialization logic here

            _persoPos = new Vector2(500, 500);

            boss1 = new Boss(5000, 1, "boss", _bossPos);
            hero = new Perso(true, 100, "perso", 1, 500, _persoPos);

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
                _tabBulletPerso[i] = new Bullet(Constantes._VITESSE_BULLETS_PERSO, new Vector2(_persoPos.X, _persoPos.Y + i * Constantes._HAUTEUR_BULLETS * 2), "allié");
            }
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

            //10 bullets aléatoires qui descendent
            for (int i = 0; i < _tabBullets.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1); j++)
                {
                    _tabBullets[i,j].BulletPosition += new Vector2(0, _tabBullets[i,j].Vitesse * deltaTime);
                }
            }
            //tirs alliés
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                if (_tabBulletPerso[i].BulletPosition.Y > _persoPos.Y)
                {
                    if (_tabBulletPerso[i].BulletPosition.X != _persoPos.X)
                        _tabBulletPerso[i].BulletPosition = new Vector2(_persoPos.X, _tabBulletPerso[i].BulletPosition.Y);
                }
                _tabBulletPerso[i].BulletPosition -= new Vector2(0, _tabBulletPerso[i].Vitesse * deltaTime);
            }
            
            Patern(deltaTime);
            DeplacementPerso(deltaTime);
            BulletAllieReset();
            Collision();
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texturePerso, _persoPos, Color.White);
            _spriteBatch.Draw(_textureBoss, _bossPos - new Vector2(Constantes._LARGEUR_BOSS / 2, 0), Color.White);
            //Bullets adverses
            for (int i = 0; i < _tabBullets.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1); j++)
                {
                    _spriteBatch.Draw(_textureBullet, _tabBullets[i,j].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
                }
            }
            //Bullets alliées
            for (int i = 0; i < _tabBulletPerso.Length; i++)
            {
                if (!(_tabBulletPerso[i].BulletPosition.Y > _persoPos.Y))
                    _spriteBatch.Draw(_textureBullet, _tabBulletPerso[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.White);
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
        public void Collision()
        {
            bool tmp = false;
            for (int i = 0; i < _tabBullets.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1); j++)
                {
                    Rectangle rect1 = new Rectangle((int)_tabBullets[i, j].BulletPosition.X, (int)_tabBullets[i, j].BulletPosition.Y, Constantes._LARGEUR_BULLETS, Constantes._HAUTEUR_BULLETS);
                    Rectangle rect2 = new Rectangle((int)_persoPos.X, (int)_persoPos.Y, Constantes._LARGEUR_PERSO, Constantes._HAUTEUR_PERSO);

                    if (rect1.Intersects(rect2))
                    {
                        tmp = true;
                    }
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
                    _tabBulletPerso[i].BulletPosition = new Vector2(_persoPos.X, _persoPos.Y + i * Constantes._HAUTEUR_BULLETS * 2);
                }
            }
        }
        public void Patern(float deltaTime)
        {
            Random rdn = new Random();
            float tmp = 0;

            for (int i = 1; i < _tabBullets.GetLength(0); i++)
            {
                for (int j = 0; j < _tabBullets.GetLength(1); j++)
                {
                    tmp += deltaTime;

                    if (tmp > 1)
                    {
                        _tabBullets[i, j].BulletPosition += new Vector2(rdn.Next(-50, 50), _tabBullets[i, j].Vitesse * deltaTime);
                        throw new ArgumentException();
                        tmp = 0;
                    }

                    
                    _tabBullets[0, j].BulletPosition += new Vector2(rdn.Next(-50, 50), _tabBullets[i, j].Vitesse * deltaTime);


                }
            }
        }

    }
}

