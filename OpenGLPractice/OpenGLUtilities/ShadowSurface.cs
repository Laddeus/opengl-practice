using OpenGLPractice.Game;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.OpenGLUtilities
{
    internal class ShadowSurface
    {
        public Matrix3 SurfacePoints { get; set; }

        public GameObject ClippingSurface { get; set; }

        public ShadowSurface()
        {
        }
    }
}
