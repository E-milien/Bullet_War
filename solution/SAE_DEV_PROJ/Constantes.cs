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

        public const int _LARGEUR_BOUTON = 403;
        public const int _HAUTEUR_BOUTON = 103;

        public const int _LARGEUR_FENETRE = 1920;
        public const int _HAUTEUR_FENETRE = 1080;
        public const int _VITESSE_BULLETS1 = 100;
        public const int _VITESSE_BULLETS2 = 150;
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
        public const int _DEBUTFOCUS1 = 3;
        public const int _DEBUTFOCUS2 = 5;
        public const int _DEBUTFOCUS3 = 8;
        public const int _DEBUTFOCUS4 = 9;
        public const int _DEBUTFOCUS5 = 13;
        public const int _DEBUTFOCUS6 = 15;
        public const int _DEBUTFOCUS7 = 17;
        public const int _DEBUTFOCUS8 = 20;
        public const int _DEBUTFOCUS9 = 21;
        public const int _DAMAGEPERSOCOTE = 1;
        private bool _estMort;

        public bool EstMort { get => _estMort; set => _estMort = value; }
    }
}
