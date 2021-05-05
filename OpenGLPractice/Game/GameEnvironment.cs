using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using OpenGL;
using OpenGLPractice.GameObjects;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.Game
{
    internal class GameEnvironment
    {
        public Camera Camera { get; private set; }

        public List<GameObject> GameObjects { get; private set; }

        public Light Light { get; set; }

        public List<ShadowSurface> ShadowSurfaces { get; private set; }

        public List<Plane> ReflectionSurfaces { get; private set; }

        private readonly WorldCube r_WorldCube;

        public WorldCube WorldCube => r_WorldCube;

        //// private readonly Sphere r_SkyDome;

        public bool UseLight { get; set; }

        public bool DrawShadows { get; set; } // requires ShadowSurfaces 

        public bool DrawReflections { get; set; } // requires ReflectionSurfaces

        public GameEnvironment()
        {
            GameObjects = new List<GameObject>();
            Light = Light.CreateLight(Light.eLightTypes.Directional);
            Camera = new Camera();
            ShadowSurfaces = new List<ShadowSurface>();
            ReflectionSurfaces = new List<Plane>();

            Axes axes = (Axes)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Axes, "Axes");
            Plane wall = (Plane)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Plane, "Wall");
            Plane ground = (Plane)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Plane, "Ground");
            wall.Transform.Rotate(90, 1, 0, 0);
            wall.Transform.Translate(0, 5f, -5f);
            wall.Color = new Vector4(0, 0, 0.5f, 0.5f);
            wall.IsTransparent = true;
            ground.Transform.Translate(0, -0.01f, 0);
            ground.Color = new Vector4(1.0f, 1.0f, 0, 1.0f);
            GameObjects.Add(axes);

            r_WorldCube = new WorldCube("WorldCube");
            r_WorldCube.Size = 100.0f;

            ShadowSurface groundSurface = new ShadowSurface()
            {
                SurfacePoints = new Matrix3(new Vector3[]
                {
                    new Vector3(0, -0, 0),
                    new Vector3(-1, -0, 1),
                    new Vector3(1, -0, 1)
                }),

                ClippingObject = ground
            };

            ShadowSurfaces.Add(groundSurface);
            ReflectionSurfaces.Add(wall);
            GameObjects.Add(ground);
        }

        public void DrawScene()
        {
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_LIGHTING));
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_TEXTURE_2D));
            GLErrorCatcher.TryGLCall(() => GL.glDepthMask((byte)GL.GL_FALSE));
            r_WorldCube.Draw();
            GLErrorCatcher.TryGLCall(() => GL.glDepthMask((byte)GL.GL_TRUE));
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_TEXTURE_2D));
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_LIGHTING));

            if (DrawShadows)
            {
                drawShadows();
            }

            if (DrawReflections)
            {
                drawReflections();
            }

            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw();
            }
        }

        private void drawReflections()
        {
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_BLEND));
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_STENCIL_TEST));
            GLErrorCatcher.TryGLCall(() => GL.glPushMatrix());

            float[] matrixBeforeAnyTransformations = new float[Transform.TransformationMatrixSize];
            GL.glGetFloatv(GL.GL_MODELVIEW_MATRIX, matrixBeforeAnyTransformations);

            foreach (Plane reflectionSurface in ReflectionSurfaces)
            {
                GL.glLoadMatrixf(matrixBeforeAnyTransformations);

                applyClipping(reflectionSurface);

                GL.glTranslatef(-2 * reflectionSurface.Transform.Position.X, -2 * reflectionSurface.Transform.Position.Y, -2 * reflectionSurface.Transform.Position.Z);
                GL.glMultMatrixf(reflectionSurface.Transform.RotationMatrix);
                GL.glScalef(1, 1, -1);
                foreach (GameObject gameObject in GameObjects)
                {
                    gameObject.Draw();
                }
            }

            GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_BLEND));
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_STENCIL_TEST));

            foreach (Plane reflectionSurface in ReflectionSurfaces)
            {
                GLErrorCatcher.TryGLCall(() => GL.glDepthMask((byte)GL.GL_FALSE));
                reflectionSurface.Draw();
                GLErrorCatcher.TryGLCall(() => GL.glDepthMask((byte)GL.GL_TRUE));
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

                applyClipping(shadowSurface.ClippingObject);
                GameObject.ShadowColor = shadowSurface.ClippingObject.Color * 0.5f;
                foreach (GameObject gameObject in GameObjects)
                {
                    if (gameObject.DisplayShadow)
                    {
                        gameObject.Draw(GameObject.eDrawMode.Shadow);
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

        private uint[] generateCubemapTextures(IReadOnlyList<Bitmap> i_TextureImages)
        {
            uint[] textureIds = new uint[i_TextureImages.Count];
            GLErrorCatcher.TryGLCall(() => GL.glGenTextures(textureIds.Length, textureIds));

            for (int i = 0; i < textureIds.Length; i++)
            {
                Bitmap currentBitmap = i_TextureImages[i];
                currentBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY); // Y axis in Windows is directed downwards, while in OpenGL-upwards
                Rectangle rect = new Rectangle(0, 0, currentBitmap.Width, currentBitmap.Height);
                BitmapData bitmapImageData = currentBitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                GLErrorCatcher.TryGLCall(() => GL.glBindTexture(GL.GL_TEXTURE_2D, textureIds[i]));
                GLErrorCatcher.TryGLCall(() => GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, currentBitmap.Width, currentBitmap.Height,
                    0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapImageData.Scan0));
                GLErrorCatcher.TryGLCall(() => GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR));
                GLErrorCatcher.TryGLCall(() => GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR));

                currentBitmap.UnlockBits(bitmapImageData);
                currentBitmap.Dispose();
            }

            return textureIds;
        }
    }
}