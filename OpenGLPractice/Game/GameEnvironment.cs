using System.Collections.Generic;
using OpenGL;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.Game
{
    internal class GameEnvironment
    {
        public Camera Camera { get; private set; }

        public List<GameObject> GameObjects { get; private set; }

        public Light Light { get; set; }

        public List<ShadowSurface> ShadowSurfaces { get; private set; }

        public bool UseLight { get; set; }

        public bool DrawShadows { get; set; } // requires ShadowPlanes 

        public GameEnvironment()
        {
            GameObjects = new List<GameObject>();
            Light = Light.CreateLight(Light.eLightTypes.Directional);
            Camera = new Camera();
            ShadowSurfaces = new List<ShadowSurface>();
        }

        public void DrawScene()
        {
            Camera.ApplyChanges();

            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw();
            }

            if (DrawShadows)
            {
                drawShadows();
            }
        }

        private void drawShadows()
        {
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_STENCIL_TEST));
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_LIGHTING));
            GLErrorCatcher.TryGLCall(() => GL.glPushMatrix());

            float[] matrixBeforeAnyTransformations = new float[Transform.TransformationMatrixSize];
            GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, matrixBeforeAnyTransformations);

            foreach (ShadowSurface shadowSurface in ShadowSurfaces)
            {
                GLErrorCatcher.TryGLCall(() => GL.glLoadMatrixf(matrixBeforeAnyTransformations));
                float[] shadowMatrix = Transform.CalculateShadowMatrix(shadowSurface.SurfacePoints, Light.Position4);
                GLErrorCatcher.TryGLCall(() => GL.glMultMatrixf(shadowMatrix));

                applyClipping(shadowSurface.ClippingSurface);

                foreach (GameObject gameObject in GameObjects)
                {
                    if (gameObject.DisplayShadow)
                    {
                        gameObject.Draw(true);
                    }
                }
            }

            GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_LIGHTING));
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_STENCIL_TEST));
        }

        private void applyClipping(GameObject i_ClippingObject)
        {
            GLErrorCatcher.TryGLCall(() => GL.glClear(GL.GL_STENCIL_BUFFER_BIT));
            GLErrorCatcher.TryGLCall(() => GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_REPLACE));
            GLErrorCatcher.TryGLCall(() => GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFF));
            GLErrorCatcher.TryGLCall(() =>
                GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE));
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_DEPTH_TEST));

            if (i_ClippingObject != null)
            {
                i_ClippingObject.Draw();
            }

            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_DEPTH_TEST));
            GLErrorCatcher.TryGLCall(() =>
            GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE));

            uint functionMode = i_ClippingObject != null ? GL.GL_EQUAL : GL.GL_NOTEQUAL;
            GLErrorCatcher.TryGLCall(() => GL.glStencilFunc(functionMode, 1, 0xFF));
        }
    }
}