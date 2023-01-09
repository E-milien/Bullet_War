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
    internal class Constantes
    {


        // TAILLE FENETRE
        public const int _LARGEUR_MENU = 600;
        public const int _HAUTEUR_MENU = 800;
        public const int _LARGEUR_FENETRE = 1920;
        public const int _HAUTEUR_FENETRE = 1080;
        public const int _VITESSE_BULLETS1 = 100;
        public const int _VITESSE_BULLETS_PERSO = 1000;
        public const int _LARGEUR_BULLETS = 10;
        public const int _HAUTEUR_BULLETS = 10;
        public const int _LARGEUR_BULLETS_PERSO = 6;
        public const int _HAUTEUR_BULLETS_PERSO = 10;
        public const int _LARGEUR_BOSS = 50;
        public const int _HAUTEUR_BOSS = 50;
        public const int _LARGEUR_PERSO = 30;
        public const int _HAUTEUR_PERSO = 30;
        public const int _DEBUTPAT1 = 3;
        public const int _DEBUTPAT2 = 15;
        public const int _DEBUTPAT3 = 24;
        public const int _DEBUTPAT4 = 50;
        public const int _DEBUTPAT5 = 61;
        private bool _estMort;

        public bool EstMort { get => _estMort; set => _estMort = value; }
    }
}
