using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.GameObjects
{
    internal class TableLeg : GameObject
    {
        private const float k_LegRadius = 0.3f;
        private const float k_LegHeight = 6.0f;
        private readonly Texture r_LegWoodTexture;

        public float LegHeight => k_LegHeight;
        public float LegRadius => k_LegRadius;

        public TableLeg(string i_Name, Texture i_LegWoodTexture = null) : base(i_Name)
        {
            r_LegWoodTexture = i_LegWoodTexture;
            Transform.Rotate(-90, 1, 0, 0);
            Material = new Material();
        }

        
        protected override void DefineGameObject()
        {
            if (r_LegWoodTexture != null)
            {
                GL.glEnable(GL.GL_TEXTURE_2D);
                GLU.gluQuadricTexture(sr_GluQuadric, 2);
                r_LegWoodTexture.BindTexture();
            }

            GLU.gluCylinder(sr_GluQuadric, k_LegRadius, k_LegRadius, k_LegHeight, 20, 20);

            GL.glDisable(GL.GL_TEXTURE_2D);
        }
    }
}
