using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAE_DEV_PROJ
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
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

        // BOSS
        Vector2 bossPos = new Vector2(_LARGEUR_FENETRE / 2, _HAUTEUR_FENETRE / 2);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SetupWindow();
            InitializePerso();

            // BOSS INITIALIZE
            Boss boss1 = new Boss(5000, 1, _skinBoss1, bossPos);

            // Bullets initialize
            for (int i = 0; i < tabBullets.Length; i++)
            {
                tabBullets[i] = new Bullet(_VITESSE_BULLETS1, new Vector2(_LARGEUR_FENETRE /2, 0), "bullet");
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texturePerso = Content.Load<Texture2D>("perso");
            _textureBullet = Content.Load<Texture2D>("bullet");
            _textureBoss = Content.Load<Texture2D>(_skinBoss1);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here
            for (int i = 0; i < tabBullets.Length; i++)
                tabBullets[i].BulletPosition += new Vector2(0,tabBullets[i].Vitesse * deltaTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texturePerso, new Vector2(500,500), Color.White);
            _spriteBatch.Draw(_textureBoss, bossPos, Color.White);
            for (int i = 0; i < tabBullets.Length; i++)
            {
                _spriteBatch.Draw(_textureBullet, tabBullets[i].BulletPosition, Color.Black);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void SetupWindow()
        {
            _graphics.PreferredBackBufferWidth = _LARGEUR_FENETRE;
            _graphics.PreferredBackBufferHeight = _HAUTEUR_FENETRE;
            _graphics.ApplyChanges();
        }
        
        private void InitializePerso()
        {
            Perso hero = new Perso(true, 10, "perso", 1, new Vector2(1, 1), new Vector2(500,500));
        }


        public void InitializeBoss()
        {
            Vector2 bossPos = new Vector2(_LARGEUR_FENETRE / 2 - GraphicsDevice.Viewport.Width/2, _HAUTEUR_FENETRE / 2 - GraphicsDevice.Viewport.Height / 2);

            Boss boss = new Boss(5000, 1, _skinBoss1, bossPos);
        }

    }
}