using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenGL;
using OpenGLPractice.GameObjects;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.Game
{
    internal class GameEnvironment
    {
        public static Random RandomNumberGenerator { get; } = new Random();

        private const float k_GreyscaleValue = 0.2f;
        private const float k_MirrorSize = 3.0f;

        public Camera Camera { get; }

        public GameObjectCollection GameObjects { get; }

        public Light Light { get; private set; }

        public List<ShadowSurface> ShadowSurfaces { get; }

        public List<GameObject> ReflectionSurfaces { get; }

        private readonly WorldCube r_WorldCube;

        public WorldCube WorldCube => r_WorldCube;

        public HeliCup[] HeliCups { get; set; }

        public Sphere HiddenBall { get; set; }

        public Table Table { get; set; }

        private CupSwapper m_CupSwapper;

        public bool UseLight { get; set; }

        public bool DrawShadows { get; set; } // requires ShadowSurfaces 

        public bool DrawReflections { get; set; } // requires ReflectionSurfaces

        private int m_HiddenBallLocation;

        private Light r_Spotlight;

        public int GameScore { get; set; }

        public event Action GameAnimationStarted;
        public event Action GameAnimationEnded;

        public GameEnvironment()
        {
            GameObjects = new GameObjectCollection();
            Light = Light.CreateLight(Light.eLightTypes.Point);
            Camera = new Camera();
            Light.Position = new Vector3(0, 5f, 0f);
            ShadowSurfaces = new List<ShadowSurface>();
            ReflectionSurfaces = new List<GameObject>();
            Camera.CameraUpdated += Light.ApplyPositionsAndDirection;
            
            Axes axes = (Axes)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Axes, "Axes");
            GameObjects.Add(axes);

            r_WorldCube = new WorldCube("WorldCube");
            r_WorldCube.Size = 100.0f;
        }

        private CubemapTexture generateSpaceCubemapTexture()
        {
            CubemapTexture spaceCubemapTexture = new CubemapTexture("Space1", @"Textures\Cubemaps\Space1", ".png");
            spaceCubemapTexture.LoadTextures();

            return spaceCubemapTexture;
        }

        public void Update(float i_DeltaTime)
        {
            if(m_CupSwapper != null)
            {
                m_CupSwapper.PerformSwapAnimation(i_DeltaTime);
            }
        }

        public void DrawScene()
        {
            // this makes spotlight works for SOME reason..
            GLErrorCatcher.TryGLCall(() => GL.glPushAttrib(GL.GL_LIGHTING_BIT));

            drawWorldCube();

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

            foreach (GameObject reflectionSurface in ReflectionSurfaces)
            {
                GL.glLoadMatrixf(matrixBeforeAnyTransformations);

                applyClipping(reflectionSurface);

                GL.glScalef(-1, 1, 1);
                GL.glTranslatef(-2 * reflectionSurface.Transform.Position.X, 0, 0);

                foreach (GameObject gameObject in GameObjects)
                {
                    if(gameObject.DisplayReflection)
                    {
                        Vector3 oldScale = gameObject.Transform.Scale;

                        gameObject.Transform.ChangeScale(-1, 1, 1);
                        gameObject.Draw();
                        gameObject.Transform.Scale = oldScale;
                    }
                }

                //if (DrawShadows)
                //{
                //    //GL.glPushAttrib(GL.GL_STENCIL_BUFFER_BIT);
                //    GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_BLEND));
                //    drawShadows(reflectionSurface);
                //    GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_BLEND));
                //    //GL.glPopAttrib();
                //}
            }

            GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_BLEND));
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_STENCIL_TEST));

            // draw reflection surface after everything to get transparent effect
            foreach (GameObject reflectionSurface in ReflectionSurfaces)
            {
                //GLErrorCatcher.TryGLCall(() => GL.glDepthMask((byte)GL.GL_FALSE));
                reflectionSurface.Draw();
                //GLErrorCatcher.TryGLCall(() => GL.glDepthMask((byte)GL.GL_TRUE));
            }
        }

        private void drawShadows(GameObject i_ReflectiveSurface = null)
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

                // (matrixBeforeAnyTransformations * (shadowMatrix * (specificObjectsAccumulatedMatrix * every_pixel_of_object_vector)))

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
            GLErrorCatcher.TryGLCall(() => GL.glClear(GL.GL_STENCIL_BUFFER_BIT));
            GLErrorCatcher.TryGLCall(() => GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE));
            GLErrorCatcher.TryGLCall(() => GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFF));
            GLErrorCatcher.TryGLCall(() =>
                GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE));
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_DEPTH_TEST));

            i_ClippingObject?.Draw();

            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_DEPTH_TEST));
            GLErrorCatcher.TryGLCall(
                () => GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE));
            GLErrorCatcher.TryGLCall(() => GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_REPLACE));

            uint functionMode = i_ClippingObject != null ? GL.GL_EQUAL : GL.GL_NOTEQUAL;
            GLErrorCatcher.TryGLCall(() => GL.glStencilFunc(functionMode, 1, 0xFF));
        }

        private void initializeGameSpecificObjects()
        {
            // main game objects
            Table = new Table("Table");
            HeliCups = generateHeliCups();
            HiddenBall = generateHiddenBall();

            // environment stuff
            ShadowSurface tableShadow = generateShadowSurfaceOnTable();
            CubemapTexture spaceCubemapTexture = generateSpaceCubemapTexture();
            WorldCube.UseTexture(spaceCubemapTexture);

            r_Spotlight = Light.CreateLight(Light.eLightTypes.Point);
            r_Spotlight.Position = new Vector3(5.571759f, 14.668005f, 0.03756857f);
            r_Spotlight.SpotlightCutoff = 20;
            r_Spotlight.SpotlightExponent = 100;
            r_Spotlight.TurnOff();
            Camera.CameraUpdated += r_Spotlight.ApplyPositionsAndDirection;

            // mirror
            Mirror mirror = new Mirror("Mirror", 5);
            mirror.Transform.Position = new Vector3(10, 11, 0);

            resetGameObjectsState();
            ReflectionSurfaces.Add(mirror);
            ShadowSurfaces.Add(tableShadow);
            GameObjects.AddRange(HeliCups);
            GameObjects.AddRange(new GameObject[]{Table, HiddenBall /*mirrorFrame*/ });
        }

        private void resetGameObjectsState()
        {
            foreach(int index in Enumerable.Range(0, HeliCups.Length) )
            {
                float xLocation = 0;
                float yLocation = Table.TableHeight;
                float zLocation = (index - 1) * (Table.TableTopRadius - HeliCups[index].CupBottomRadius * 4);

                HeliCups[index].Transform.Position = new Vector3(xLocation, yLocation, zLocation);
            }

            int randomCupIndex = RandomNumberGenerator.Next(HeliCups.Length);
            m_HiddenBallLocation = randomCupIndex;

            Vector3 randomBallPosition = HeliCups[randomCupIndex].Transform.Position;
            randomBallPosition.Y += HiddenBall.Radius;
            HiddenBall.Transform.Position = randomBallPosition;

            Light.Position = new Vector3(-9.430399f, 11.1951046f, -0.11611516f);
            Light.TurnOn();
            Camera.EyePosition = new Vector3(-10.1737862f, 13f, 0);
            Camera.YawAngle = 0;
            Camera.PitchAngle = -30;
        }

        private Sphere generateHiddenBall()
        {
            Sphere hiddenBall = (Sphere)GameObjectCreator.CreateSphere("HiddenBall", 0.3f, null);

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

                heliCup.DisplayShadow = true;

                return heliCup;
            }).ToArray();
        }

        private ShadowSurface generateShadowSurfaceOnTable()
        {
            ShadowSurface tableShadowSurface = new ShadowSurface()
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

        public async Task StartGame()
        {
            initializeGameSpecificObjects();

            OnGameAnimationStarted();
            await revealBall();
            swapHeliCups();
        }

        public async Task ResetGame()
        {
            if(m_CupSwapper != null)
            {
                m_CupSwapper.StopAnimation();
            }

            resetGameObjectsState();
            OnGameAnimationStarted();
            await revealBall();
            swapHeliCups();
        }

        private void swapHeliCups()
        {
            if(m_CupSwapper == null)
            {
                m_CupSwapper = new CupSwapper(HeliCups, HiddenBall, ref m_HiddenBallLocation);
                m_CupSwapper.SwapAnimationEnded += OnGameAnimationEnded;
            }

            m_CupSwapper.StartAnimation();
        }

        private async Task revealBall()
        {
            foreach (HeliCup heliCup in HeliCups)
            {
                heliCup.Ascend(3);
            }

            await Task.Delay(10000);

            foreach (HeliCup heliCup in HeliCups)
            {
                heliCup.Descend(3);
            }

            await Task.Delay(7000);
        }

        protected virtual void OnGameAnimationEnded()
        {
            GameAnimationEnded?.Invoke();
        }

        protected virtual void OnGameAnimationStarted()
        {
            GameAnimationStarted?.Invoke();
        }

        public async Task RevealBall(int i_CupSelectionIndexLocation)
        {
            toggleShadowsForCups(i_CupSelectionIndexLocation);

            Vector3 heliCupPosition = HeliCups[i_CupSelectionIndexLocation].Transform.Position;
            setSpotlightPositionAndDirection(heliCupPosition);

            // save the main light because we are going to swap it with spotlight to be the main light now.
            Light mainLight = Light;
            // change main light to be the spotlight.
            Light = r_Spotlight;
            mainLight.TurnOff();
            r_Spotlight.TurnOn();

            bool isCorrectSelection = m_CupSwapper.HiddenBallLocationIndex == i_CupSelectionIndexLocation;
            if (isCorrectSelection)
            {
                GameScore++;
            }

            HiddenBall.DisplayShadow = isCorrectSelection;

            await revealBall();

            toggleShadowsForCups(i_CupSelectionIndexLocation);

            Light = mainLight;
            r_Spotlight.TurnOff();
            Light.TurnOn();

            HiddenBall.DisplayShadow = true;

            await Task.Delay(3000);
            swapHeliCups();
        }

        private void setSpotlightPositionAndDirection(Vector3 i_CupPosition)
        {
            Vector3 spotLightPosition = i_CupPosition;
            spotLightPosition.Y += 10;
            spotLightPosition.X += 5;
            r_Spotlight.Position = spotLightPosition;

            Vector3 spotLightDirection = (i_CupPosition - r_Spotlight.Position).Normalized;
            r_Spotlight.SpotlightDirection = spotLightDirection;
        }

        private void toggleShadowsForCups(int i_CupSelectionIndexLocation)
        {
            foreach(HeliCup heliCup in HeliCups)
            {
                heliCup.DisplayShadow = !heliCup.DisplayShadow;
            }
        }
    }
}