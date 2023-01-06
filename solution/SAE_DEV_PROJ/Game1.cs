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
        WinScreen _winScreen;
        SettingScreen _settingScreen;

        private bool _loaded;
        public bool _screenDeathOk;
        public bool _screenWinOk;
        public bool _actif;
        public bool _settingOk;
        
        public int _widthPlayButton;
        public int _heighPlayButton;

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
            _actif = false;
            _screenDeathOk = false;
            _screenWinOk = false;
            _loaded = false;
            _settingOk = false;
            SetupWindow();
            _widthPlayButton = 1000;
            _heighPlayButton = 150;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _homeScreen = new HomeScreen(this); // en leur donnant une référence au Game
            _playScreen = new PlayScreen(this);
            _deadScreen = new DeadScreen(this);
            _winScreen = new WinScreen(this);
            _settingScreen = new SettingScreen(this);
        }

        protected override void Update(GameTime gameTime)
        { 
            KeyboardState keyboardState = Keyboard.GetState();
            if (!_loaded)
            {
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
                _loaded = true;
            }

            if (!_playScreen._alive && !_screenDeathOk)
            {
                _screenManager.LoadScreen(_deadScreen, new FadeTransition(GraphicsDevice, Color.Black));
                _screenDeathOk = true;
            }
            if (!_playScreen._bossAlive && !_screenWinOk)
            {
                _screenManager.LoadScreen(_winScreen, new FadeTransition(GraphicsDevice, Color.Black));
                _screenWinOk = true;
            }

            MouseState ms = Mouse.GetState();
            Rectangle hitboxPlayButton = new Rectangle(500, 200, _widthPlayButton, _heighPlayButton);
            Rectangle hitboxOptionButton = new Rectangle(500, 400, _widthPlayButton, _heighPlayButton);
            Rectangle hitboxLeaveButton = new Rectangle(500, 600, _widthPlayButton, _heighPlayButton);
            // MENU PRINCIPAL 
            if (_actif && ms.LeftButton == ButtonState.Pressed && hitboxPlayButton.Contains(ms.X, ms.Y))
            {
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if(_actif && ms.LeftButton == ButtonState.Pressed && hitboxOptionButton.Contains(ms.X, ms.Y))
            {
                _screenManager.LoadScreen(_settingScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (_actif && ms.LeftButton == ButtonState.Pressed && hitboxLeaveButton.Contains(ms.X, ms.Y))
            {
                Exit();
            }

            // DEATH SCENE
            if(_screenDeathOk && ms.LeftButton == ButtonState.Pressed && hitboxPlayButton.Contains(ms.X, ms.Y))
            {
                _screenManager.LoadScreen(_deadScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if(_screenDeathOk && ms.LeftButton == ButtonState.Pressed && hitboxOptionButton.Contains(ms.X, ms.Y))
            {
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if(_screenDeathOk && ms.LeftButton == ButtonState.Pressed && hitboxLeaveButton.Contains(ms.X, ms.Y))
            {
                Exit();
            }

            // WIN SCENE
            if (_screenWinOk && ms.LeftButton == ButtonState.Pressed && hitboxPlayButton.Contains(ms.X, ms.Y))
            {
                _screenManager.LoadScreen(_winScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if (_screenWinOk && ms.LeftButton == ButtonState.Pressed && hitboxOptionButton.Contains(ms.X, ms.Y))
            {
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }


            // SETTINGS SCREEN
            if(_settingOk && ms.LeftButton == ButtonState.Pressed && hitboxLeaveButton.Contains(ms.X, ms.Y))
            {
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
                
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