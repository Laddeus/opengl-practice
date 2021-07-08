using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class MirrorSurface : GameObject
    {
        private readonly float r_MirrorSurfaceRadius;
        private readonly Vector4 r_MirrorColor = new Vector4(0.23125f, 0.23125f, 0.23125f, 0.3f);

        public MirrorSurface(string i_Name, float i_MirrorSurfaceRadius = 1)
            : base(i_Name)
        {
            r_MirrorSurfaceRadius = i_MirrorSurfaceRadius;
            //UseMaterial = v_UseMaterial;
            IsTransparent = v_IsTransparent;
            Color = r_MirrorColor;
            Transform.Rotate(90, 0, 1, 0);
            //Material.Ambient = new Vector4(0.23125f, 0.23125f, 0.23125f, 1.0f);
            //Material.Diffuse = new Vector4(0.2775f, 0.2775f, 0.2775f, 1.0f);
            //Material.Specular = new Vector4(0.773911f, 0.773911f, 0.773911f, 1.0f);
            //Material.Shininess = 38.4f;
        }

        protected override void DefineGameObject()
        {
            GLU.gluDisk(sr_GluQuadric, 0, r_MirrorSurfaceRadius, 40, 40);
        }
    }
}
