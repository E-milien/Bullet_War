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
        private int _vitesse, _hauteur, _largeur;
        private String _skin;

        public Bullet(int vitesse, int hauteur, int largeur, string skin)
        {
            this.Vitesse = vitesse;
            this.Hauteur = hauteur;
            this.Largeur = largeur;
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

        public int Hauteur
        {
            get
            {
                return this._hauteur;
            }

            set
            {
                this._hauteur = value;
            }
        }

        public int Largeur
        {
            get
            {
                return this._largeur;
            }

            set
            {
                this._largeur = value;
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
    }
}
