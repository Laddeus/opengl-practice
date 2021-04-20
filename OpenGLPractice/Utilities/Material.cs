using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.Utilities
{
    internal class Material
    {
        public Vector4 Ambient { get; set; }

        public Vector4 Diffuse { get; set; }

        public Vector4 Specular { get; set; }

        public Vector4 Emission { get; set; }

        public float Shininess { get; set; }

        public void ApplyMaterial()
        {
            GL.glColor3fv(Diffuse.ToArray);
            GL.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT, Ambient.ToArray);
            GL.glMaterialfv(GL.GL_FRONT, GL.GL_DIFFUSE, Diffuse.ToArray);
            GL.glMaterialfv(GL.GL_FRONT, GL.GL_SPECULAR, Specular.ToArray);
            GL.glMaterialfv(GL.GL_FRONT, GL.GL_EMISSION, Emission.ToArray);
            GL.glMaterialf(GL.GL_FRONT, GL.GL_SHININESS, Shininess);
        }
    }
}
