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
        private GraphicsDeviceManager _graphics;
        HomeScreen _homeScreen;
        PlayScreen _playScreen;
        DeadScreen _deadScreen;
        private bool _loaded;
        
        

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
            _loaded = false;
            SetupWindow();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _homeScreen = new HomeScreen(this); // en leur donnant une référence au Game
            _playScreen = new PlayScreen(this);
            _deadScreen = new DeadScreen(this);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (!_loaded)
            {
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
                _loaded = true;
            }
            
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void SetupWindow()
        {
            _graphics.PreferredBackBufferWidth = Constantes._LARGEUR_FENETRE;
            _graphics.PreferredBackBufferHeight = Constantes._HAUTEUR_FENETRE;
            _graphics.ApplyChanges();
        }

    }
}