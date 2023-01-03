using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAE_DEV_PROJ
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // TEXTURES 
        private string _skinBoss = "boss.png";
        private Texture2D _textureBoss;

        // TAILLE FENETRE
        public const int _LARGEUR_FENETRE = 1920;
        public const int _HAUTEUR_FENETRE = 1080;

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
            InitializeBoss();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureBoss = Content.Load<Texture2D>(_skinBoss);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        private void SetupWindow()
        {
            _graphics.PreferredBackBufferWidth = _LARGEUR_FENETRE;
            _graphics.PreferredBackBufferHeight = _HAUTEUR_FENETRE;
            _graphics.ApplyChanges();
        }









        public void InitializeBoss()
        {
            Vector2 bossPos = new Vector2(_LARGEUR_FENETRE / 2, _HAUTEUR_FENETRE / 2);

            Boss boss = new Boss(5000, 1, _skinBoss, bossPos);
        }
    }
}