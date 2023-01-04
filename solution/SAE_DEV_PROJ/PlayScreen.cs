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
        private Bullet[] tabBullets = new Bullet[10];

        // TEXTURES 
        private string _skinBoss1 = "boss";
        private Texture2D _textureBoss;
        private Texture2D _textureBullet;

        // TAILLE FENETRE
        public const int _LARGEUR_FENETRE = 1920;
        public const int _HAUTEUR_FENETRE = 1000;
        public const int _VITESSE_BULLETS1 = 100;
        public const int _LARGEUR_BULLETS = 10;
        public const int _LARGEUR_BOSS = 50;

        // BOSS
        Vector2 bossPos = new Vector2(_LARGEUR_FENETRE / 2, _HAUTEUR_FENETRE / 2);

        // PERSO
        private int _sensPersoX;
        private int _sensPersoY;
        private int _vitessePerso;
        private Vector2 _positionPerso;
        private KeyboardState _keyboardState;

        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public PlayScreen(Game1 game) : base(game)
        {
            _myGame = game;
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

            for (int i = 0; i < tabBullets.Length; i++)
                tabBullets[i].BulletPosition += new Vector2(0, tabBullets[i].Vitesse * deltaTime);

            DeplacementPerso();

            _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;
            _sensPersoX = 0;

            _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;
            _sensPersoY = 0;
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texturePerso, new Vector2(500, 500), Color.White);
            _spriteBatch.Draw(_textureBoss, bossPos - new Vector2(_LARGEUR_BOSS / 2, 0), Color.White);
            for (int i = 0; i < tabBullets.Length; i++)
            {
                _spriteBatch.Draw(_textureBullet, tabBullets[i].BulletPosition - new Vector2(_LARGEUR_BULLETS / 2, 0), Color.Black);
            }
            _spriteBatch.End();
        }
        private void SetupWindow()
        {
            _graphics.PreferredBackBufferWidth = _LARGEUR_FENETRE;
            _graphics.PreferredBackBufferHeight = _HAUTEUR_FENETRE;
            _graphics.ApplyChanges();
        }

        private void InitializePerso()
        {
            Perso hero = new Perso(true, 10, "perso", 1, new Vector2(1, 1), new Vector2(500, 500));
        }
        private void DeplacementPerso()
        {
            if (_keyboardState.IsKeyDown(Keys.Q))
                _sensPersoX = -1;

            else if (_keyboardState.IsKeyDown(Keys.D))
                _sensPersoX = 1;

            if (_keyboardState.IsKeyDown(Keys.Z))
                _sensPersoY = -1;

            else if (_keyboardState.IsKeyDown(Keys.S))
                _sensPersoY = 1; 

        }
    }
}

