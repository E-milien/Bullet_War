using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace SAE_DEV_PROJ
{
    public class HomeScreen : GameScreen
    {
        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public HomeScreen(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        { 
        base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        { 

        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Red); // on utilise la reference vers
                                                     // Game1 pour chnager le graphisme
        }
        }
}

