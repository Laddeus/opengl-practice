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

        public Material()
        {
            Ambient = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            Diffuse = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
            Specular = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            Emission = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            Shininess = 0.0f;
        }

        public void ApplyMaterial()
        {
            GLErrorCatcher.TryGLCall(() => GL.glMaterialfv(GL.GL_FRONT_AND_BACK, GL.GL_AMBIENT, Ambient.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glMaterialfv(GL.GL_FRONT_AND_BACK, GL.GL_DIFFUSE, Diffuse.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glMaterialfv(GL.GL_FRONT_AND_BACK, GL.GL_SPECULAR, Specular.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glMaterialfv(GL.GL_FRONT_AND_BACK, GL.GL_EMISSION, Emission.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glMaterialf(GL.GL_FRONT_AND_BACK, GL.GL_SHININESS, Shininess));
        }
    }
}
