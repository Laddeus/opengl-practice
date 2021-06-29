using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OpenGL;
using OpenGLPractice.GameObjects;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.Game
{
    internal class GameEnvironment
    {
        private static readonly Random sr_RandomNumberGenerator = new Random();

        private const float k_GreyscaleValue = 0.2f;

        public Camera Camera { get; }

        public GameObjectCollection GameObjects { get; }

        public Light Light { get; }

        public List<ShadowSurface> ShadowSurfaces { get; }

        public List<Plane> ReflectionSurfaces { get; }

        private readonly WorldCube r_WorldCube;

        public WorldCube WorldCube => r_WorldCube;

        public HeliCup[] HeliCups { get; set; }

        public Sphere HiddenBall { get; set; }

        public Table Table { get; set; }

        public bool UseLight { get; set; }

        public bool DrawShadows { get; set; } // requires ShadowSurfaces 

        public bool DrawReflections { get; set; } // requires ReflectionSurfaces

        private int m_HiddenBallLocation;

        public GameEnvironment()
        {
            GameObjects = new GameObjectCollection();
            Light = Light.CreateLight(Light.eLightTypes.Point);
            Light.TurnOff();
            Camera = new Camera();
            Light.Position = new Vector3(0, 5f, 0f);
            ShadowSurfaces = new List<ShadowSurface>();
            ReflectionSurfaces = new List<Plane>();
            Camera.CameraUpdated += Light.ApplyPositionsAndDirection;
            
            Axes axes = (Axes)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Axes, "Axes");
            GameObjects.Add(axes);

            r_WorldCube = new WorldCube("WorldCube");
            r_WorldCube.Size = 100.0f;

            // ShadowSurface groundSurface = new ShadowSurface()
            // {
            //     SurfacePoints = new Matrix3(new Vector3[]
            //     {
            //         new Vector3(0, -0, 0),
            //         new Vector3(-1, -0, 1),
            //         new Vector3(1, -0, 1)
            //     }),

            //     ClippingObject = ground
            // };

            // ShadowSurfaces.Add(groundSurface);
            Random random = new Random();
            Surface surface =
                GameObjectCreator.CreateSurface("Surface", 5, 0.05f,
                    (i_X, i_Z) => i_X * i_X / 6 + i_Z * i_Z / 6);

            surface.UseDisplayList = GameObject.v_UseDisplayList;
            surface.Color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
            //GameObjects.Add(surface);
        }

        private void initializeGameSpecificObjects()
        {
            Table = new Table("Table");
            HeliCups = generateHeliCups();
            HiddenBall = generateHiddenBall();

            ShadowSurface tableShadow = generateShadowSurfaceOnTable();
            Light.Position = new Vector3(1, Table.TableHeight * 2, 1);
            Light.TurnOn();
            ShadowSurfaces.Add(tableShadow);
            GameObjects.Add(Table);
            GameObjects.AddRange(HeliCups);
            GameObjects.Add(HiddenBall);
        }

        private ShadowSurface generateShadowSurfaceOnTable()
        {
            ShadowSurface tableShadowSurface =  new ShadowSurface()
                {
                    SurfacePoints = new Matrix3(new Vector3[]
                                                    {
                                                        new Vector3(0, Table.TableHeight + 0.01f, 0),
                                                        new Vector3(-1, Table.TableHeight + 0.01f, 1),
                                                        new Vector3(1, Table.TableHeight + 0.01f, 1)
                                                    }),

                    ClippingObject = Table
                };

            return tableShadowSurface;
        }

        private Sphere generateHiddenBall()
        {
            Sphere hiddenBall = (Sphere)GameObjectCreator.CreateSphere("HiddenBall", 0.3f, null);
            int randomCupIndex = sr_RandomNumberGenerator.Next(HeliCups.Length);
            m_HiddenBallLocation = randomCupIndex;

            Vector3 randomBallPosition = HeliCups[randomCupIndex].Transform.Position;
            randomBallPosition.Y += hiddenBall.Radius;
            hiddenBall.Transform.Position = randomBallPosition;

            hiddenBall.UseMaterial = true;
            hiddenBall.DisplayShadow = true;

            hiddenBall.Material.Diffuse = new Vector4(0.1745f, 0.01175f, 0.01175f, 0.55f);
            hiddenBall.Material.Ambient = new Vector4(0.61424f, 0.04136f, 0.04136f, 0.55f);
            hiddenBall.Material.Specular = new Vector4(0.727811f, 0.626959f, 0.626959f, 0.55f);
            hiddenBall.Material.Shininess = 76.8f;

            return hiddenBall;
        }

        private HeliCup[] generateHeliCups()
        {
            return Enumerable.Range(1, 3).Select(i_Index =>
                {
                    HeliCup heliCup = (HeliCup)GameObjectCreator.CreateGameObjectDefault(
                        eGameObjectTypes.HeliCup,
                        $"HeliCup{i_Index}"); 

                    heliCup.Transform.Translate(0, Table.TableHeight, (i_Index - 2) * (Table.TableTopRadius - heliCup.CupBottomRadius * 4));
                    heliCup.DisplayShadow = true;

                    return heliCup;
                }).ToArray();
        }

        public void Update(float i_DeltaTime)
        {

        }

        public void DrawScene()
        {
            // this makes spotlight works for SOME reason..
            GLErrorCatcher.TryGLCall(() => GL.glPushAttrib(GL.GL_LIGHTING_BIT));

            drawWorldCube();
            //character.DrawModel();

            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw();
            }

            if (DrawShadows)
            {
                drawShadows();
            }

            if (DrawReflections)
            {
                drawReflections();
            }

            GLErrorCatcher.TryGLCall(() => GL.glPopAttrib());
        }


        private void drawWorldCube()
        {
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_LIGHTING));
            GLErrorCatcher.TryGLCall(() => GL.glDepthMask((byte)GL.GL_FALSE));
            r_WorldCube.Draw();
            GLErrorCatcher.TryGLCall(() => GL.glDepthMask((byte)GL.GL_TRUE));
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_LIGHTING));
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

                GL.glTranslatef(0, 0, 2 * reflectionSurface.Transform.Position.Z);
                GL.glScalef(1, 1, -1);

                applyClipping(reflectionSurface);

                foreach (GameObject gameObject in GameObjects)
                {
                    gameObject.Draw();
                }

                if (DrawShadows)
                {
                    GL.glPushAttrib(GL.GL_STENCIL_BUFFER_BIT);
                    drawShadows();
                    GL.glPopAttrib();
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

                GLErrorCatcher.TryGLCall(() => GL.glClear(GL.GL_STENCIL_BUFFER_BIT));
                applyClipping(shadowSurface.ClippingObject);
                GameObject.ShadowColor = shadowSurface.ClippingObject.Color * k_GreyscaleValue;
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
            GLErrorCatcher.TryGLCall(() => GL.glStencilOp(GL.GL_KEEP, GL.GL_REPLACE, GL.GL_REPLACE));
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

        public void StartGame()
        {
            initializeGameSpecificObjects();
            showBall();
        }

        private void showBall()
        {
            foreach (HeliCup heliCup in HeliCups)
            {
                heliCup.Ascend(3);
            }

            // TODO: change waiting method...
            wait(15000);

            foreach (HeliCup heliCup in HeliCups)
            {
                heliCup.Descend(3);
            }
        }

        public void ResetGame()
        {
            foreach(HeliCup heliCup in HeliCups)
            {
                GameObjects.Remove(heliCup);
            }

            GameObjects.Remove(HiddenBall);
            GameObjects.Remove(Table);

            StartGame();
        }

        public void wait(int milliseconds)
        {
            Timer timer = new Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer.Interval = milliseconds;
            timer.Enabled = true;
            timer.Start();

            timer.Tick += (s, e) =>
                {
                    timer.Enabled = false;
                    timer.Stop();
                    // Console.WriteLine("stop wait timer");
                };

            while (timer.Enabled)
            {
                Application.DoEvents();
            }
        }
    }
}