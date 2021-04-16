﻿using System.Collections.Generic;
using OpenGL;
using OpenGLPractice.Utilities;

namespace OpenGLPractice.GameObjects
{
    internal abstract class GameObject
    {
        public Transform Transform { get; }

        public GameObject Parent { get; set; }

        private string m_Name;

        private readonly uint r_GLListID;

        protected readonly GLUquadric r_gluQuadric;

        private readonly uint r_LocalDirectionCoordinates;

        public bool LocalCoordinatesActive { get; set; }

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

        protected abstract void DefineGameObject();

        public abstract void Tick(float i_DeltaTime);

        public void Draw()
        {
            GL.glPushMatrix();

            applyAccumulatedTransformations();
            drawGameObject();

            GL.glPopMatrix();
        }

        private void applyAccumulatedTransformations()
        {
            GL.glMultMatrixf(Transform.TransformationMatrix);
        }

        private void drawGameObject()
        {
            if (LocalCoordinatesActive)
            {
                Vector3 currentScale = Transform.Scale;
                GL.glScalef(1.0f / currentScale.X, 1.0f / currentScale.Y, 1.0f / currentScale.Z);
                GL.glDepthRange(0.0, 0.01);
                GL.glCallList(r_LocalDirectionCoordinates);
                GL.glScalef(currentScale.X, currentScale.Y, currentScale.Z);
            }

            GL.glDepthRange(0.01, 1.0);
            DefineGameObject();
        }

        private void defineLocalCoordinateAxes()
        {
            GL.glNewList(r_LocalDirectionCoordinates, GL.GL_COMPILE);
            GL.glBegin(GL.GL_LINES);
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
            GL.glColor3f(0.0f, 0.0f, 1.0f);

            GL.glTranslatef(forwardVector.X, forwardVector.Y, forwardVector.Z);

            GLU.gluCylinder(r_gluQuadric, 0.1, 0, 0.3, 20, 20);

            GL.glTranslatef(-forwardVector.X, -forwardVector.Y, -forwardVector.Z);

            // up vector arrow
            GL.glColor3f(0.0f, 1.0f, 0.0f);

            GL.glTranslatef(upVector.X, upVector.Y, upVector.Z);
            GL.glRotatef(-90, 1, 0, 0);

            GLU.gluCylinder(r_gluQuadric, 0.1, 0, 0.3, 20, 20);

            GL.glRotatef(90, 1, 0, 0);
            GL.glTranslatef(-upVector.X, -upVector.Y, -upVector.Z);

            // right vector arrow
            GL.glColor3f(1.0f, 0.0f, 0.0f);

            GL.glTranslatef(rightVector.X, rightVector.Y, rightVector.Z);
            GL.glRotatef(-90, 0, 1, 0);

            GLU.gluCylinder(r_gluQuadric, 0.1, 0, 0.3, 20, 20);

            GL.glRotatef(90, 0, 1, 0);
            GL.glTranslatef(-rightVector.X, -rightVector.Y, -rightVector.Z);

            GL.glEndList();
        }

        public void InitializeList()
        {
            GL.glPushMatrix();
            GL.glNewList(r_GLListID, GL.GL_COMPILE);

            DefineGameObject();

            GL.glEndList();
            GL.glPopMatrix();
        }

        public void CallList()
        {
            GL.glPushMatrix();

            applyAccumulatedTransformations();
            GLUtilities.CallGLMethod(() => GL.glCallList(r_GLListID));

            GL.glPopMatrix();
        }
    }
}