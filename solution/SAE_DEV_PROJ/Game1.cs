using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;

namespace SAE_DEV_PROJ
{
    public class Game1 : Game
    {
        private readonly ScreenManager _screenManager;
        HomeScreen _homeScreen;
        PlayScreen _playScreen;
        private GraphicsDeviceManager _graphics;

        public SpriteBatch SpriteBatch { get; set; }

        public Game1()
        {
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SetupWindow();
            InitializePerso();

            _positionPerso = new Vector2(500, 500);
            _vitessePerso = 500;


            // BOSS INITIALIZE
            Boss boss1 = new Boss(5000, 1, _skinBoss1, bossPos);

            // Bullets initialize
            for (int i = 0; i < tabBullets.Length; i++)
            {
                Variables.tab[i] = new Bullet(Variables._VITESSE_BULLETS1, new Vector2((new Random()).Next(0, Variables._LARGEUR_FENETRE), 0), "bullet");
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _homeScreen = new HomeScreen(this); // en leur donnant une référence au Game
            _playScreen = new PlayScreen(this);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice,
                Color.Black));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        
    }
}