using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV_PROJ
{
    internal class Perso
    {
        const int _tailleX=0;
        const int _tailleY=0;
        private double _pvPerso;
        private int _damagePerso;
        private bool _godMod;
        private string _skinPerso;
        private double _multiplicationVitesse;
        private int _deplacementPerso;
        private Vector2 _positionDepartPerso;

        public Perso(bool godMod, double pvPerso, int damagePerso, string skinPerso, double multiplicationVitesse, int deplacementPerso, Vector2 positionDepartPerso)
        {
            this.GodMod = godMod;
            this.PvPerso = pvPerso;
            this.DamagePerso = damagePerso;
            this.SkinPerso = skinPerso;
            this.MultiplicationVitesse = multiplicationVitesse;
            this.DeplacementPerso = deplacementPerso;
            this.PositionDepartPerso = positionDepartPerso;
        }


        public bool GodMod
        {
            get
            {
                return this._godMod;
            }

            set
            {
                if (!(value == true || value == false))
                    throw new ArgumentException("value");
                this._godMod = value;
            }
        }
        public double PvPerso
        {
            get
            {
                return this._pvPerso;
            }

            set
            {
                if (GodMod == true)
                    this._pvPerso = 1000000000;
                this._pvPerso = value;
            }
        }

        public string SkinPerso
        {
            get
            {
                return this._skinPerso;
            }

            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                this._skinPerso = value;
            }
        }

        public double MultiplicationVitesse
        {
            get
            {
                return this._multiplicationVitesse;
            }

            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value");
                this._multiplicationVitesse = value;
            }
        }

        public int DeplacementPerso
        {
            get
            {
                return this._deplacementPerso;
            }

            set
            {
                this._deplacementPerso = value;
            }
        }
        public Vector2 PositionDepartPerso
        {
            get
            {
                return this._positionDepartPerso;
            }

            set
            {
                this._positionDepartPerso = value;
            }
        }

        public int DamagePerso
        {
            get
            {
                return this._damagePerso;
            }

            set
            {
                this._damagePerso = value;
            }
        }
    }
}
