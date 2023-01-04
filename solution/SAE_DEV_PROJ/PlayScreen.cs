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
    public class PlayScreen : GameScreen
    {
        private Game1 _myGame;
        private SpriteBatch _spriteBatch;
        private Texture2D _texturePerso;
        private Bullet[] _tabBullets = new Bullet[10];

        // TEXTURES 
        private string _skinBoss1 = "boss";
        private Texture2D _textureBoss;
        private Texture2D _textureBullet;

        // BOSS
        Vector2 _bossPos = new Vector2(Constantes._LARGEUR_FENETRE / 2, Constantes._HAUTEUR_FENETRE / 2);
        Vector2 _persoPos;

        // PERSO
        private int _sensPersoX;
        private int _sensPersoY;
        private int _vitessePerso;
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
            _vitessePerso = 500;

            // BOSS INITIALIZE
            Boss boss1 = new Boss(5000, 1, _skinBoss1, _bossPos);
            Perso hero = new Perso(true, 10, "perso", 1, new Vector2(1, 1), _persoPos);
            
            // Bullets initialize
            for (int i = 0; i < _tabBullets.Length; i++)
            {
                _tabBullets[i] = new Bullet(Constantes._VITESSE_BULLETS1, new Vector2((new Random()).Next(0, Constantes._LARGEUR_FENETRE), 0), "bullet");
            }

            base.Initialize();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texturePerso = Content.Load<Texture2D>("perso");
            _textureBullet = Content.Load<Texture2D>("bullet");
            _textureBoss = Content.Load<Texture2D>(_skinBoss1);


            // TODO: use this.Content to load your game content here
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here

            for (int i = 0; i < _tabBullets.Length; i++)
                _tabBullets[i].BulletPosition += new Vector2(0, _tabBullets[i].Vitesse * deltaTime);

            DeplacementPerso();

            _persoPos.X += _sensPersoX * _vitessePerso * deltaTime;
            _sensPersoX = 0;

            _persoPos.Y += _sensPersoY * _vitessePerso * deltaTime;
            _sensPersoY = 0;
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texturePerso, _persoPos, Color.White);
            _spriteBatch.Draw(_textureBoss, _bossPos - new Vector2(Constantes._LARGEUR_BOSS / 2, 0), Color.White);
            for (int i = 0; i < _tabBullets.Length; i++)
            {
                _spriteBatch.Draw(_textureBullet, _tabBullets[i].BulletPosition - new Vector2(Constantes._LARGEUR_BULLETS / 2, 0), Color.Black);
            }
            _spriteBatch.End();
        }

        private void DeplacementPerso()
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Q) && !(_keyboardState.IsKeyDown(Keys.D)))
                _sensPersoX = -1;

            else if (_keyboardState.IsKeyDown(Keys.D) && !(_keyboardState.IsKeyDown(Keys.Q)))
                _sensPersoX = 1;

            if (_keyboardState.IsKeyDown(Keys.Z) && !(_keyboardState.IsKeyDown(Keys.S)))
                _sensPersoY = -1;

            else if (_keyboardState.IsKeyDown(Keys.S) && !(_keyboardState.IsKeyDown(Keys.Z)))
                _sensPersoY = 1; 

        }
        public bool Colision(Perso hero)
        {
            bool tmp = false;
            for (int i = 0; i < _tabBullets.Length; i++)
            {
                Rectangle rect1 = new Rectangle((int)_tabBullets[i].BulletPosition.X, (int))_tabBullets[i].BulletPosition.Y, _LARGEUR_BULLETS, _HAUTEUR_BULLETS);
                Rectangle rect2 = new Rectangle((int)hero._persoPos.X, (int)hero._persoPos.Y, _LARGEUR_PERSO, _HAUTEUR_PERSO);
                if (rect1.Intersects(rect2))
                {
                    tmp = true;
                }
            }
            return tmp;
        }

    }
}

