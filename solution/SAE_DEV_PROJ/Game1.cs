using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Linq;

namespace SAE_DEV_PROJ
{
    public class Game1 : Game
    {
        private double _tmp;
        public bool _pause;
        private readonly ScreenManager _screenManager;
        private GraphicsDeviceManager _graphics;
        HomeScreen _homeScreen;
        PlayScreen _playScreen;
        DeadScreen _deadScreen;
        WinScreen _winScreen;
        SettingScreen _settingScreen;
        ShopScreen _shopScren;

        public Texture2D _boutonPlay;
        public Texture2D _boutonShop;
        public Texture2D _boutonSettings;
        public Texture2D _boutonQuit;

        public Texture2D _textureButtonPressed;
        public Texture2D _textureButton;

        private bool _loaded;
        public bool _screenDeathOk;
        public bool _screenWinOk;
        public bool _actif;
        public bool _settingOk;
        public bool _shopScreenOk;
        private bool _tmpZ;
        private bool _tmpD;
        private bool _tmpQ;
        private bool _tmpS;
        public bool _touche;

        public int _widthPlayButton;
        public int _heighPlayButton;
        public int _money;
        public int _hpPerso;

        private MouseState _ms;
        private KeyboardState _keyboardState;
        public Texture2D _fondHome;
        public Texture2D _fondSettings;

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

        public int coordXcontourFond;
        public int coordYcontourFond;

        public string _toucheAssignee;

        public Rectangle hitboxPlayButton;
        public Rectangle hitboxShopButton;
        public Rectangle hitboxOptionButton;
        public Rectangle hitboxLeaveButton;

        public Rectangle hitboxSettingButtonZ;
        public Rectangle hitboxSettingButtonD;
        public Rectangle hitboxSettingButtonQ;
        public Rectangle hitboxSettingButtonS;

        Rectangle hitboxPic1;
        Rectangle hitboxPic2;
        Rectangle hitboxPic3;
        Rectangle hitboxPic4;

        Rectangle hitboxPic5;
        Rectangle hitboxPic6;
        Rectangle hitboxPic7;
        Rectangle hitboxPic8;

        public Rectangle _hitboxBoutonMReplay;
        public Rectangle _hitboxMenuButton;
        public Rectangle _hitboxExitButton;

        public Rectangle _hitboxReplayWinScreen;
        public Rectangle _hitboxMainMenuWinScreen;


        // SON
        public SoundEffect _soundButton;
        public SoundEffect _soundButton2;
        public SoundEffect _soundButton3;
        public Song _musiqueHome;

        public bool _homeScreenOpen;

        public SpriteBatch SpriteBatch { get; set; }

        public Game1()
        {
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //-_graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {

            _boutonPlay = _textureButton;
            _boutonShop = _textureButton;
            _boutonSettings = _textureButton;
            _boutonQuit = _textureButton;

            _tmp = 0;
            _pause = false;
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
            _textureFond = Content.Load<Texture2D>("fond1");

            hitboxPic1 = new Rectangle(750, 50, 200, 112);
            hitboxPic2 = new Rectangle(1050, 50, 200, 112);
            hitboxPic3 = new Rectangle(1350, 50, 200, 112);
            hitboxPic4 = new Rectangle(1650, 50, 200, 112);

            hitboxPic5 = new Rectangle(750, 240, 200, 112);
            hitboxPic6 = new Rectangle(1050, 240, 200, 112);
            hitboxPic7 = new Rectangle(1350, 240, 200, 112);
            hitboxPic8 = new Rectangle(1650, 240, 200, 112);

            hitboxSettingButtonZ = new Rectangle(0, 108, 550, 50);
            hitboxSettingButtonD = new Rectangle(0, 208, 550, 50);
            hitboxSettingButtonQ = new Rectangle(0, 308, 550, 50);
            hitboxSettingButtonS = new Rectangle(0, 408, 550, 50);

            hitboxPlayButton = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 4 / 8 - 50, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            hitboxShopButton = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 5 / 8 - 50, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            hitboxOptionButton = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 6 / 8 - 50, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            hitboxLeaveButton = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE * 7 / 8 - 50, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);

            _hitboxBoutonMReplay = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 300, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            _hitboxMenuButton = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 500, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            _hitboxExitButton = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, 700, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);

            _hitboxReplayWinScreen = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE / 2 - 200, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);
            _hitboxMainMenuWinScreen = new Rectangle(Constantes._LARGEUR_FENETRE / 2 - Constantes._LARGEUR_BOUTON / 2, Constantes._HAUTEUR_FENETRE / 2, Constantes._LARGEUR_BOUTON, Constantes._HAUTEUR_BOUTON);

            base.Initialize();
        }

        protected override void LoadContent()
        {

            _textureButton = Content.Load<Texture2D>("boutonM");
            _textureButtonPressed = Content.Load<Texture2D>("boutonM_pressed");

            _fondHome = Content.Load<Texture2D>("homeScreen");
            _fondSettings = Content.Load<Texture2D>("settingsFond");
            _soundButton = Content.Load<SoundEffect>("sondBouton");
            _soundButton2 = Content.Load<SoundEffect>("soundBouton2");
            _soundButton3 = Content.Load<SoundEffect>("sondBouton3");
            _musiqueHome = Content.Load<Song>("musiqueHome");

            _homeScreen = new HomeScreen(this);
            _playScreen = new PlayScreen(this);
            _deadScreen = new DeadScreen(this);
            _winScreen = new WinScreen(this);
            _settingScreen = new SettingScreen(this);
            _shopScren = new ShopScreen(this);

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
            if (!_loaded)
            {
                MediaPlayer.Play(_musiqueHome);
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

            _ms = Mouse.GetState();
            _keyboardState = Keyboard.GetState();
            var keys = _keyboardState.GetPressedKeys();

            // MENU PRINCIPAL 
            if (_actif && _ms.LeftButton == ButtonState.Pressed && hitboxPlayButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                MediaPlayer.Stop();
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (_actif && _ms.LeftButton == ButtonState.Pressed && hitboxOptionButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                _screenManager.LoadScreen(_settingScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if(_actif && _ms.LeftButton == ButtonState.Pressed && hitboxShopButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                _screenManager.LoadScreen(_shopScren, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (_actif && _ms.LeftButton == ButtonState.Pressed && hitboxLeaveButton.Contains(_ms.X, _ms.Y))
            {
                MediaPlayer.Stop();
                _soundButton.Play();
                Exit();
            }


            // SHOP

            // PAUSE
            if(_pause && _ms.LeftButton == ButtonState.Pressed && _hitboxMenuButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if(_pause && _ms.LeftButton == ButtonState.Pressed && _hitboxExitButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                Exit();
            }
            if (_hitboxMenuButton.Contains(_ms.X, _ms.Y))
            {
                _playScreen._boutonMenuHome = _playScreen._textureButtonMenuPressed;
            }
            else
            {
                _playScreen._boutonMenuHome = _playScreen._textureButtonMenu;
            }
            if (_hitboxExitButton.Contains(_ms.X, _ms.Y))
            {
                _playScreen._boutonMenuExit = _playScreen._textureButtonMenuPressed;
            }
            else
            {
                _playScreen._boutonMenuExit = _playScreen._textureButtonMenu;
            }
            // home
            if (hitboxPlayButton.Contains(_ms.X, _ms.Y))
            {
                _boutonPlay = _textureButtonPressed;
            }
            else
            {
                _boutonPlay = _textureButton;
            }
            if (hitboxShopButton.Contains(_ms.X, _ms.Y))
            {
                _boutonShop = _textureButtonPressed;
            }
            else
            {
                _boutonShop = _textureButton;
            }
            if (hitboxOptionButton.Contains(_ms.X, _ms.Y))
            {
                _boutonSettings = _textureButtonPressed;
            }
            else
            {
                _boutonSettings = _textureButton;
            }
            if (hitboxLeaveButton.Contains(_ms.X, _ms.Y))
            {
                _boutonQuit = _textureButtonPressed;
            }
            else
            {
                _boutonQuit = _textureButton;
            }


            // A SUPPRIMER 
            if (_keyboardState.IsKeyDown(Keys.O))
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
            if (_keyboardState.IsKeyDown(Keys.M))
            {
                _screenManager.LoadScreen(_settingScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }

            // DEATH SCENE
            if (_screenDeathOk && _ms.LeftButton == ButtonState.Pressed && hitboxPlayButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if (_screenDeathOk && _ms.LeftButton == ButtonState.Pressed && _hitboxMenuButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if (_screenDeathOk && _ms.LeftButton == ButtonState.Pressed && hitboxLeaveButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                Exit();
            }

            // WIN SCENE
            if (_screenWinOk && _ms.LeftButton == ButtonState.Pressed && hitboxPlayButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if (_screenWinOk && _ms.LeftButton == ButtonState.Pressed && hitboxOptionButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }

            // DEATH SCENE

            if (_screenDeathOk && _ms.LeftButton == ButtonState.Pressed && _hitboxBoutonMReplay.Contains(_ms.X, _ms.Y))
            {
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (_screenDeathOk && _ms.LeftButton == ButtonState.Pressed && _hitboxMenuButton.Contains(_ms.X, _ms.Y))
            {
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (_screenDeathOk && _ms.LeftButton == ButtonState.Pressed && _hitboxExitButton.Contains(_ms.X, _ms.Y))
            {
                Exit();
            }


            // WIN SCENE
            if (_screenWinOk && _ms.LeftButton == ButtonState.Pressed && _hitboxReplayWinScreen.Contains(_ms.X, _ms.Y))
            {
                _screenManager.LoadScreen(_playScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if (_screenWinOk && _ms.LeftButton == ButtonState.Pressed && _hitboxMainMenuWinScreen.Contains(_ms.X, _ms.Y))
            {
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }

            // SETTINGS SCREEN
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxLeaveButton.Contains(_ms.X, _ms.Y))
            {
                _soundButton.Play();
                _screenManager.LoadScreen(_homeScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }

            // FONDS SUPERIEURS 
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxPic1.Contains(_ms.X, _ms.Y))
            {
                _soundButton2.Play();
                _textureFond = _textureFond1;
            }
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxPic2.Contains(_ms.X, _ms.Y))
            {
                _soundButton2.Play();
                _textureFond = _textureFond2;
            }
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxPic3.Contains(_ms.X, _ms.Y))
            {
                _soundButton2.Play();
                _textureFond = _textureFond3;
            }
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxPic4.Contains(_ms.X, _ms.Y))
            {
                _soundButton2.Play();
                _textureFond = _textureFond4;
            }

            // FONDS INFERIEURS 
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxPic5.Contains(_ms.X, _ms.Y))
            {
                _soundButton2.Play();
                _textureFond = _textureFond5;
            }
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxPic6.Contains(_ms.X, _ms.Y))
            {
                _soundButton2.Play();
                _textureFond = _textureFond6;
            }
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxPic7.Contains(_ms.X, _ms.Y))
            {
                _soundButton2.Play();
                _textureFond = _textureFond7;
            }
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxPic8.Contains(_ms.X, _ms.Y))
            {
                _soundButton2.Play();
                _textureFond = _textureFond8;
            }

            if (_textureFond == _textureFond1 || _textureFond == _textureFond5)
                coordXcontourFond = 750;

            if (_textureFond == _textureFond2 || _textureFond == _textureFond6)
                coordXcontourFond = 1050;

            if (_textureFond == _textureFond3 || _textureFond == _textureFond7)
                coordXcontourFond = 1350;

            if (_textureFond == _textureFond4 || _textureFond == _textureFond8)
                coordXcontourFond = 1650;

            if (_textureFond == _textureFond1 || _textureFond == _textureFond2 || _textureFond == _textureFond3 || _textureFond == _textureFond4)
                coordYcontourFond = 50;
            else
                coordYcontourFond = 240;


            // PAUSE
            if (_keyboardState.IsKeyDown(Keys.Escape) && _pause == false && _playScreen._chrono >=_tmp+1)
            {
                _pause = true;
            }
            if (_keyboardState.IsKeyDown(Keys.Escape) && _pause == true && _playScreen._chronoPause >= 1)
            {
                _pause = false;
                _tmp=_playScreen._chrono;
            }

            // CHANGEMENTS DE TOUCHE Z 
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxSettingButtonZ.Contains(_ms.X, _ms.Y))
            {
                _tmpZ = true;
                _soundButton3.Play();
            }
                

            if(_tmpZ == true)
            {

                if (keys.Length > 0)
                {

                    if(keys[0] == _forward || keys[0] == _left || keys[0] == _right || keys[0] == _behind)
                    {
                        _touche = true;
                        _toucheAssignee = keys[0].ToString();
                    }
                    else
                    {
                        _forward = keys[0];
                        _tmpZ = false;
                        _touche = false;
                    }
                }
            }

            // TOUCHE D 
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxSettingButtonD.Contains(_ms.X, _ms.Y))
            {
                _tmpD = true;
                _soundButton3.Play();
            }

            if (_tmpD == true)
            {

                if (keys.Length > 0)
                {

                    if (keys[0] == _forward || keys[0] == _left || keys[0] == _right || keys[0] == _behind)
                    {
                        _touche = true;
                        _toucheAssignee = keys[0].ToString();
                    }
                    else
                    {
                        _right = keys[0];
                        _tmpD = false;
                        _touche = false;
                    }
                }
            }

            // TOUCHE Q 
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxSettingButtonQ.Contains(_ms.X, _ms.Y))
            {
                _tmpQ = true;
                _soundButton3.Play();
            }

            if (_tmpQ == true)
            {

                if (keys.Length > 0)
                {

                    if (keys[0] == _forward || keys[0] == _left || keys[0] == _right || keys[0] == _behind)
                    {
                        _touche = true;
                        _toucheAssignee = keys[0].ToString();
                    }
                    else
                    {
                        _left = keys[0];
                        _tmpQ = false;
                        _touche = false;
                    }
                }
            }

            // TOUCHE S
            if (_settingOk && _ms.LeftButton == ButtonState.Pressed && hitboxSettingButtonS.Contains(_ms.X, _ms.Y))
            {
                _tmpS = true;
                _soundButton3.Play();
            }

            if (_tmpS == true)
            {

                if (keys.Length > 0)
                {

                    if (keys[0] == _forward || keys[0] == _left || keys[0] == _right || keys[0] == _behind)
                    {
                        _touche = true;
                        _toucheAssignee = keys[0].ToString();
                    }
                    else
                    {
                        _behind = keys[0];
                        _tmpS = false;
                        _touche = false;
                    }
                }
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