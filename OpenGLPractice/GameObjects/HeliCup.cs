using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLPractice.GameObjects
{
    internal class HeliCup : GameObject
    {
        private readonly Cup r_Cup;
        private readonly Telescope r_Telescope;

        public HeliCup(string i_Name) : base(i_Name)
        {
            r_Cup = new Cup("Cup");
            r_Telescope = new Telescope("Telescope");
        }

        protected override void DefineGameObject()
        {
            r_Cup.CallList();
            r_Telescope.CallList();
        }
    }
}
