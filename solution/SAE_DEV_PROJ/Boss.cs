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
    internal class Boss
    {
        private double _bossHP;
        private double _damageBoss;
        private string _skinBoss;
        private Vector2 _bossPosition;

        public Boss(double bossHP, double damageBoss, string skinBoss, Vector2 bossPosition)
        {
            this.BossHP = bossHP;
            this.DamageBoss = damageBoss;
            this.SkinBoss = skinBoss;
            this.BossPosition = bossPosition;
        }

        public double BossHP
        {
            get
            {
                return this._bossHP;
            }

            set
            {
                this._bossHP = value;
            }
        }

        public double DamageBoss
        {
            get
            {
                return this._damageBoss;
            }

            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Damage <= 0");
                this._damageBoss = value;
            }
        }

        public string SkinBoss
        {
            get
            {
                return this._skinBoss;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Valeur null ou vide");
                this._skinBoss = value;
            }
        }

        public Vector2 BossPosition
        {
            get
            {
                return this._bossPosition;
            }

            set
            {
                // Boss en dehors de la fenetre
                //if (value.X < 0 || value.X > Constantes._LARGEUR_FENETRE|| value.Y < 0 || value.Y > Constantes._HAUTEUR_FENETRE)
                    //throw new ArgumentOutOfRangeException("Position en dehors de la fenetre");

                this._bossPosition = value;
            }
        }
    }
}
