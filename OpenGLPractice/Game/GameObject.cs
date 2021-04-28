using System;
using OpenGL;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.Game
{
    internal abstract class GameObject
    {
        public const bool k_UseDisplayList = true;
        private static readonly Vector4 r_ShadowColor = new Vector4(0.35f, 0.35f, 0.35f, 1.0f);
        protected static readonly GLUquadric sr_GluQuadric;
        private readonly uint r_GLListID;
        private readonly uint r_LocalDirectionCoordinates;

        public Transform Transform { get; }

        public GameObject Parent { get; set; }

        public Vector4 Color { get; set; }

        public Material Material { get; set; }

        protected string m_Name;

        public bool LocalCoordinatesActive { get; set; }

        public bool UseMaterial { get; set; }

        public bool DisplayShadow { get; set; }

        public bool IsTransparent { get; set; }

        public bool UseDisplayList { get; set; }

        public string Name
        {
            get => m_Name;
            set
            {
                m_Name = value;
            }
        }

        static GameObject()
        {
            sr_GluQuadric = GLU.gluNewQuadric();

            // clean up statically created memory when on process exit...
            AppDomain.CurrentDomain.ProcessExit += (i_Sender, i_Args) => GLU.gluDeleteQuadric(sr_GluQuadric);
        }

        public GameObjectCollection Children { get; }

        protected GameObject(string i_Name)
        {
            Children = new GameObjectCollection(this);
            Transform = new Transform();
            Color = new Vector4(1.0f);
            Material = new Material();

            r_GLListID = GL.glGenLists(2);
            r_LocalDirectionCoordinates = r_GLListID + 1;

            defineLocalCoordinateAxes();

            m_Name = i_Name;
        }

        protected abstract void DefineGameObject();

        public virtual void Tick(float i_DeltaTime)
        {
            foreach (GameObject gameObject in Children)
            {
                gameObject.Tick(i_DeltaTime);
            }
        }

        public void Draw(bool i_IsShadow = false)
        {
            GLErrorCatcher.TryGLCall(() => GL.glPushMatrix());

            if (i_IsShadow)
            {
                Vector4 oldColor = Color;
                Color = r_ShadowColor;
                applyAccumulatedTransformations();
                drawGameObject();
                Color = oldColor;
            }
            else
            {
                applyAccumulatedTransformations();
                drawGameObject();
            }

            foreach (GameObject gameObject in Children)
            {
                gameObject.Draw(i_IsShadow);
            }

            GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
        }

        private void applyAccumulatedTransformations()
        {
            GLErrorCatcher.TryGLCall(() => GL.glMultMatrixf(Transform.TransformationMatrix));
        }

        private void drawGameObject()
        {
            Action drawMethod = UseDisplayList
                ? new Action(() => GLErrorCatcher.TryGLCall(() => GL.glCallList(r_GLListID)))
                : new Action(DefineGameObject);

            GLErrorCatcher.TryGLCall(() => GL.glColor4fv(Color.ToArray));

            if (UseMaterial)
            {
                defineGameObjectWithMaterial(drawMethod);
            }
            else if (IsTransparent)
            {
                defineGameObjectWithTransparency(drawMethod);
            }
            else
            {
                drawMethod.Invoke();
            }

            if (LocalCoordinatesActive)
            {
                Vector3 currentScale = Transform.Scale;
                GL.glScalef(1.0f / currentScale.X, 1.0f / currentScale.Y, 1.0f / currentScale.Z);
                //GLErrorCatcher.TryGLCall(() => GL.glDepthRange(0.0, 0.01));
                GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_DEPTH_TEST));
                GLErrorCatcher.TryGLCall(() => GL.glCallList(r_LocalDirectionCoordinates));
                GLErrorCatcher.TryGLCall(() => GL.glScalef(currentScale.X, currentScale.Y, currentScale.Z));
                GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_DEPTH_TEST));
            }
        }

        private void defineGameObjectWithTransparency(Action i_DrawMethod)
        {
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_BLEND));
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_CULL_FACE));

            GLErrorCatcher.TryGLCall(() => GL.glCullFace(GL.GL_FRONT));
            i_DrawMethod.Invoke();

            GLErrorCatcher.TryGLCall(() => GL.glCullFace(GL.GL_BACK));
            i_DrawMethod.Invoke();

            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_BLEND));
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_CULL_FACE));
        }

        private void defineGameObjectWithMaterial(Action i_DrawMethod)
        {
            GLErrorCatcher.TryGLCall(() => GL.glDisable(GL.GL_COLOR_MATERIAL));
            GLErrorCatcher.TryGLCall(() => GL.glPushAttrib(GL.GL_LIGHTING_BIT));

            Material.ApplyMaterial();
            i_DrawMethod.Invoke();

            GLErrorCatcher.TryGLCall(() => GL.glPopAttrib());
            GLErrorCatcher.TryGLCall(() => GL.glEnable(GL.GL_COLOR_MATERIAL));
        }

        private void defineLocalCoordinateAxes()
        {
            GLErrorCatcher.TryGLCall(() => GL.glNewList(r_LocalDirectionCoordinates, GL.GL_COMPILE));

            GLErrorCatcher.TryGLCall(() => GL.glBegin(GL.GL_LINES));
            Vector3 forwardVector = 1.2f * Vector3.Forward;
            Vector3 rightVector = 1.2f * Vector3.Left;
            Vector3 upVector = 1.2f * Vector3.Up;

            GL.glColor3f(0, 0, 1);
            GL.glVertex3fv(Vector3.Zero.ToArray);
            GL.glVertex3fv(forwardVector.ToArray);

            GL.glColor3f(1, 0, 0);
            GL.glVertex3fv(Vector3.Zero.ToArray);
            GL.glVertex3fv(rightVector.ToArray);

            GL.glColor3f(0, 1, 0);
            GL.glVertex3fv(Vector3.Zero.ToArray);
            GL.glVertex3fv(upVector.ToArray);

            GL.glEnd();

            // forward vector arrow
            GLErrorCatcher.TryGLCall(() => GL.glColor3f(0.0f, 0.0f, 1.0f));

            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(forwardVector.X, forwardVector.Y, forwardVector.Z));

            GLU.gluCylinder(sr_GluQuadric, 0.1, 0, 0.3, 20, 20);

            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(-forwardVector.X, -forwardVector.Y, -forwardVector.Z));

            // up vector arrow
            GLErrorCatcher.TryGLCall(() => GL.glColor3f(0.0f, 1.0f, 0.0f));

            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(upVector.X, upVector.Y, upVector.Z));
            GLErrorCatcher.TryGLCall(() => GL.glRotatef(-90, 1, 0, 0));

            GLU.gluCylinder(sr_GluQuadric, 0.1, 0, 0.3, 20, 20);

            GLErrorCatcher.TryGLCall(() => GL.glRotatef(90, 1, 0, 0));
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(-upVector.X, -upVector.Y, -upVector.Z));

            // right vector arrow
            GLErrorCatcher.TryGLCall(() => GL.glColor3f(1.0f, 0.0f, 0.0f));

            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(rightVector.X, rightVector.Y, rightVector.Z));
            GLErrorCatcher.TryGLCall(() => GL.glRotatef(-90, 0, 1, 0));

            GLU.gluCylinder(sr_GluQuadric, 0.1, 0, 0.3, 20, 20);

            GLErrorCatcher.TryGLCall(() => GL.glRotatef(90, 0, 1, 0));
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(-rightVector.X, -rightVector.Y, -rightVector.Z));

            GLErrorCatcher.TryGLCall(() => GL.glEndList());
        }

        public void InitializeList()
        {
            GLErrorCatcher.TryGLCall(() => GL.glPushMatrix());
            GLErrorCatcher.TryGLCall(() => GL.glNewList(r_GLListID, GL.GL_COMPILE));

            DefineGameObject();

            GLErrorCatcher.TryGLCall(() => GL.glEndList());
            GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
        }

        protected static void UpdateShadowsDescent(GameObject i_RootGameObject)
        {
            foreach (GameObject gameObject in i_RootGameObject.Children)
            {
                gameObject.DisplayShadow = i_RootGameObject.DisplayShadow;

                UpdateShadowsDescent(gameObject);
            }
        }

        //public void CallList()
        //{
        //    GLErrorCatcher.TryGLCall(() => GL.glPushMatrix());

        //    applyAccumulatedTransformations();
        //    GLErrorCatcher.TryGLCall(() => GL.glCallList(r_GLListID));

        //    GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
        //}
    }
}