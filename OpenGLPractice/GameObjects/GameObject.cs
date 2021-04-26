using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OpenGL;
using OpenGLPractice.GLMath;
using OpenGLPractice.Utilities;

namespace OpenGLPractice.GameObjects
{
    internal abstract class GameObject
    {
        public const bool k_UseDisplayList = true;
        private readonly uint r_GLListID;
        private readonly uint r_LocalDirectionCoordinates;
        protected readonly GLUquadric r_gluQuadric;

        public Transform Transform { get; }

        public GameObject Parent { get; set; }

        public Vector4 Color { get; set; }

        public Material Material { get; set; }

        protected string m_Name;

        public bool LocalCoordinatesActive { get; set; }

        public bool UseMaterial { get; set; }

        public bool IsTransparent { get; set; }

        public bool UseDisplayList { get; set; } = true;

        public string Name
        {
            get => m_Name;
            set
            {
                m_Name = value;
            }
        }

        public List<GameObject> Children { get; }

        protected GameObject(string i_Name)
        {
            Children = new List<GameObject>();
            Transform = new Transform();
            Color = new Vector4(1.0f);
            Material = new Material();
            r_gluQuadric = GLU.gluNewQuadric();

            r_GLListID = GL.glGenLists(2);
            r_LocalDirectionCoordinates = r_GLListID + 1;

            defineLocalCoordinateAxes();

            m_Name = i_Name;
        }

        ~GameObject()
        {
            GLU.gluDeleteQuadric(r_gluQuadric);
        }

        protected virtual void DefineGameObject()
        {
        }

        public virtual void Tick(float i_DeltaTime)
        {
            foreach (GameObject gameObject in Children)
            {
                gameObject.Tick(i_DeltaTime);
            }
        }

        public void Draw(bool i_UseDisplayList)
        {
            GLErrorCatcher.TryGLCall(() => GL.glPushMatrix());

            applyAccumulatedTransformations();
            drawGameObject(i_UseDisplayList);

            foreach (GameObject gameObject in Children)
            {
                gameObject.Draw(UseDisplayList);
            }

            GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
        }

        private void applyAccumulatedTransformations()
        {
            GLErrorCatcher.TryGLCall(() => GL.glMultMatrixf(Transform.TransformationMatrix));
        }

        private void drawGameObject(bool i_UseDisplayList)
        {
            if (LocalCoordinatesActive)
            {
                Vector3 currentScale = Transform.Scale;
                GL.glScalef(1.0f / currentScale.X, 1.0f / currentScale.Y, 1.0f / currentScale.Z);
                GLErrorCatcher.TryGLCall(() => GL.glDepthRange(0.0, 0.01));
                GLErrorCatcher.TryGLCall(() => GL.glCallList(r_LocalDirectionCoordinates));
                GLErrorCatcher.TryGLCall(() => GL.glScalef(currentScale.X, currentScale.Y, currentScale.Z));
            }

            Action drawMethod = i_UseDisplayList
                ? new Action(() => GLErrorCatcher.TryGLCall(() => GL.glCallList(r_GLListID)))
                : new Action(DefineGameObject);

            GLErrorCatcher.TryGLCall(() => GL.glDepthRange(0.01, 1.0));

            GLErrorCatcher.TryGLCall(() => GL.glColor4fv(Color.ToArray));
            //Material.ApplyMaterial();

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

            GLU.gluCylinder(r_gluQuadric, 0.1, 0, 0.3, 20, 20);

            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(-forwardVector.X, -forwardVector.Y, -forwardVector.Z));

            // up vector arrow
            GLErrorCatcher.TryGLCall(() => GL.glColor3f(0.0f, 1.0f, 0.0f));

            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(upVector.X, upVector.Y, upVector.Z));
            GLErrorCatcher.TryGLCall(() => GL.glRotatef(-90, 1, 0, 0));

            GLU.gluCylinder(r_gluQuadric, 0.1, 0, 0.3, 20, 20);

            GLErrorCatcher.TryGLCall(() => GL.glRotatef(90, 1, 0, 0));
            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(-upVector.X, -upVector.Y, -upVector.Z));

            // right vector arrow
            GLErrorCatcher.TryGLCall(() => GL.glColor3f(1.0f, 0.0f, 0.0f));

            GLErrorCatcher.TryGLCall(() => GL.glTranslatef(rightVector.X, rightVector.Y, rightVector.Z));
            GLErrorCatcher.TryGLCall(() => GL.glRotatef(-90, 0, 1, 0));

            GLU.gluCylinder(r_gluQuadric, 0.1, 0, 0.3, 20, 20);

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

        //public void CallList()
        //{
        //    GLErrorCatcher.TryGLCall(() => GL.glPushMatrix());

        //    applyAccumulatedTransformations();
        //    GLErrorCatcher.TryGLCall(() => GL.glCallList(r_GLListID));

        //    GLErrorCatcher.TryGLCall(() => GL.glPopMatrix());
        //}
    }
}