using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAE_DEV_PROJ
{
    internal class Variables
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texturePerso;
        private Bullet[] tabBullets = new Bullet[10];

        // TEXTURES 
        private string _skinBoss1 = "boss";
        private Texture2D _textureBoss;
        private Texture2D _textureBullet;

        // BOSS
        Vector2 bossPos = new Vector2(_LARGEUR_FENETRE / 2, _HAUTEUR_FENETRE / 2);

        // PERSO
        private int _sensPersoX;
        private int _sensPersoY;
        private int _vitessePerso;
        private Vector2 _positionPerso;
        private KeyboardState _keyboardState;

        // TAILLE FENETRE
        public const int _LARGEUR_FENETRE = 1920;
        public const int _HAUTEUR_FENETRE = 1000;
        public const int _VITESSE_BULLETS1 = 100;
        public const int _LARGEUR_BULLETS = 10;
        public const int _LARGEUR_BOSS = 50;

        public GraphicsDeviceManager Graphics
        {
            get
            {
                return this._graphics;
            }

            set
            {
                this._graphics = value;
            }
        }

        public SpriteBatch SpriteBatch
        {
            get
            {
                return this._spriteBatch;
            }

            set
            {
                this._spriteBatch = value;
            }
        }

        public Texture2D TexturePerso
        {
            get
            {
                return this._texturePerso;
            }

            set
            {
                this._texturePerso = value;
            }
        }

        internal Bullet[] TabBullets
        {
            get
            {
                return this.tabBullets;
            }

            set
            {
                this.tabBullets = value;
            }
        }

        public string SkinBoss1
        {
            get
            {
                return this._skinBoss1;
            }

            set
            {
                this._skinBoss1 = value;
            }
        }

        public Texture2D TextureBoss
        {
            get
            {
                return this._textureBoss;
            }

            set
            {
                this._textureBoss = value;
            }
        }

        public Texture2D TextureBullet
        {
            get
            {
                return this._textureBullet;
            }

            set
            {
                this._textureBullet = value;
            }
        }

        public Vector2 BossPos
        {
            get
            {
                return this.bossPos;
            }

            set
            {
                this.bossPos = value;
            }
        }

        public int SensPersoX
        {
            get
            {
                return this._sensPersoX;
            }

            set
            {
                this._sensPersoX = value;
            }
        }

        public int SensPersoY
        {
            get
            {
                return this._sensPersoY;
            }

            set
            {
                this._sensPersoY = value;
            }
        }

        public int VitessePerso
        {
            get
            {
                return this._vitessePerso;
            }

            set
            {
                this._vitessePerso = value;
            }
        }

        public Vector2 PositionPerso
        {
            get
            {
                return this._positionPerso;
            }

            set
            {
                this._positionPerso = value;
            }
        }

        public KeyboardState KeyboardState
        {
            get
            {
                return this._keyboardState;
            }

            set
            {
                this._keyboardState = value;
            }
        }
    }
}
