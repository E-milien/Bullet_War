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
        public KeyboardState _keyboardState;
        public Keys _forward;
        public Keys _right;
        public Keys _behind;
        public Keys _left;

        public Texture2D _textureFond;
        public Texture2D _textureFond1;
        public Texture2D _textureFond2;
        public Texture2D _textureFond3;
        public Texture2D _textureFond4;
        public Texture2D _textureFond5;
        public Texture2D _textureFond6;
        public Texture2D _textureFond7;
        public Texture2D _textureFond8;


        public SpriteBatch SpriteBatch { get; set; }

        public Game1()
        {
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //_graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            // DEPLACEMENTS PERSO 
            _forward = Keys.Z;
            _right = Keys.D;
            _behind = Keys.S;
            _left = Keys.Q;

            _actif = false;
            _screenDeathOk = false;
            _screenWinOk = false;
            _loaded = false;
            _settingOk = false;
            SetupWindow();
            _widthPlayButton = 1000;
            _heighPlayButton = 150;
            _textureFond = Content.Load<Texture2D>("fond1");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _homeScreen = new HomeScreen(this); 
            _playScreen = new PlayScreen(this);
            _deadScreen = new DeadScreen(this);
            _winScreen = new WinScreen(this);
            _settingScreen = new SettingScreen(this);

            _textureFond1 = Content.Load<Texture2D>("fond1");
            _textureFond2 = Content.Load<Texture2D>("fond2");
            _textureFond3 = Content.Load<Texture2D>("fond3");
            _textureFond4 = Content.Load<Texture2D>("fond4");
            _textureFond5 = Content.Load<Texture2D>("fond5");
            _textureFond6 = Content.Load<Texture2D>("fond6");
            _textureFond7 = Content.Load<Texture2D>("fond7");
            _textureFond8 = Content.Load<Texture2D>("fond8");
        }

        protected override void Update(GameTime gameTime)
        { 
            KeyboardState _keyboardState = Keyboard.GetState();
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

            Rectangle hitboxSettingButtonZ = new Rectangle(0,108,550,50);
            Rectangle hitboxSettingButtonD = new Rectangle(0, 208, 550, 50);
            Rectangle hitboxSettingButtonQ = new Rectangle(0, 308, 550, 50);
            Rectangle hitboxSettingButtonS = new Rectangle(0, 408, 550, 50);

            Rectangle hitboxPic1 = new Rectangle(750, 50, 200, 112);
            Rectangle hitboxPic2 = new Rectangle(1050, 50, 200, 112);
            Rectangle hitboxPic3 = new Rectangle(1350, 50, 200, 112);
            Rectangle hitboxPic4 = new Rectangle(1650, 50, 200, 112);

            Rectangle hitboxPic5 = new Rectangle(750, 250, 200, 112);
            Rectangle hitboxPic6 = new Rectangle(1050, 250, 200, 112);
            Rectangle hitboxPic7 = new Rectangle(1350, 250, 200, 112);
            Rectangle hitboxPic8 = new Rectangle(1650, 250, 200, 112);


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


            // A SUPPRIMER 
            if (_keyboardState.IsKeyDown(Keys.O))
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
            if(_keyboardState.IsKeyDown(Keys.M))
            {
                _screenManager.LoadScreen(_settingScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            // 


            // DEATH SCENE
            if(_screenDeathOk && ms.LeftButton == ButtonState.Pressed && hitboxPlayButton.Contains(ms.X, ms.Y))
            {
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
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
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
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

            // FONDS SUPERIEURS 
            if (_settingOk && ms.LeftButton == ButtonState.Pressed && hitboxPic1.Contains(ms.X, ms.Y))
            {
                _textureFond = _textureFond1;
            }
            if (_settingOk && ms.LeftButton == ButtonState.Pressed && hitboxPic2.Contains(ms.X, ms.Y))
            {
                _textureFond = _textureFond2;
            }
            if (_settingOk && ms.LeftButton == ButtonState.Pressed && hitboxPic3.Contains(ms.X, ms.Y))
            {
                _textureFond = _textureFond3;
            }
            if(_settingOk && ms.LeftButton == ButtonState.Pressed && hitboxPic4.Contains(ms.X, ms.Y))
            {
                _textureFond = _textureFond4;
            }

            // FONDS INFERIEURS 
            if (_settingOk && ms.LeftButton == ButtonState.Pressed && hitboxPic5.Contains(ms.X, ms.Y))
            {
                _textureFond = _textureFond5;
            }
            if (_settingOk && ms.LeftButton == ButtonState.Pressed && hitboxPic6.Contains(ms.X, ms.Y))
            {
                _textureFond = _textureFond6;
            }
            if (_settingOk && ms.LeftButton == ButtonState.Pressed && hitboxPic7.Contains(ms.X, ms.Y))
            {
                _textureFond = _textureFond7;
            }
            if (_settingOk && ms.LeftButton == ButtonState.Pressed && hitboxPic8.Contains(ms.X, ms.Y))
            {
                _textureFond = _textureFond8;
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