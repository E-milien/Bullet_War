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
    internal class Bullet
    {
        private int _vitesse;
        private Vector2 _bulletPosition;
        private String _skin;
        private bool _passeOrigine;

        public Bullet(int vitesse, Vector2 bulletPosition, string skin, bool passeOrigine)
        {
            this.Vitesse = vitesse;
            this.BulletPosition = bulletPosition;
            this.Skin = skin;
            this.PasseOrigine = passeOrigine;
        }
        public Bullet(int vitesse, Vector2 bulletPosition, string skin)
        {
            this.Vitesse = vitesse;
            this.BulletPosition = bulletPosition;
            this.Skin = skin;
        }

        public int Vitesse
        {
            get
            {
                return this._vitesse;
            }

            set
            {
                this._vitesse = value;
            }
        }

        public string Skin
        {
            get
            {
                return this._skin;
            }

            set
            {
                this._skin = value;
            }
        }

        public Vector2 BulletPosition
        {
            get
            {
                return this._bulletPosition;
            }

            set
            {
                this._bulletPosition = value;
            }
        }

        public bool PasseOrigine
        {
            get
            {
                return this._passeOrigine;
            }

            set
            {
                this._passeOrigine = value;
            }
        }
    }
}
