using System;
using System.Collections.Generic;
using System.Linq;
using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.Utilities
{
    internal class Light
    {
        public enum eLightTypes
        {
            Point,
            Directional,
            Spot,
            Area
        }

        private const int k_MaximumSourceLights = 8;

        private static readonly Dictionary<uint, bool> sr_AvailableLights = new Dictionary<uint, bool>
        {
            { GL.GL_LIGHT0, true },
            { GL.GL_LIGHT1, true },
            { GL.GL_LIGHT2, true },
            { GL.GL_LIGHT3, true },
            { GL.GL_LIGHT4, true },
            { GL.GL_LIGHT5, true },
            { GL.GL_LIGHT6, true },
            { GL.GL_LIGHT7, true },
        };

        public static Light CreateLight(eLightTypes i_LightType = eLightTypes.Directional)
        {
            KeyValuePair<uint, bool>[] lightSourceIdPair = sr_AvailableLights.Where(i_LightSourcePair => i_LightSourcePair.Value == true).ToArray();

            if (lightSourceIdPair.Length == 0)
            {
                throw new Exception($"Number of light sources cannot be bigger than {k_MaximumSourceLights}");
            }

            uint lightSourceId = lightSourceIdPair[0].Key;

            Light newLightInstance = new Light(lightSourceId, i_LightType);

            return newLightInstance;
        }

        private readonly uint r_LightSourceId;
        private eLightTypes m_LightType;
        private Vector4 m_Position;
        private Vector4 m_Ambient;
        private Vector4 m_Diffuse;
        private Vector4 m_Specular;
        private Vector4 m_Emission;
        private float m_ConstantAttenuation;
        private float m_LinearAttenuation;
        private float m_QuadraticAttenuation;

        public eLightTypes LightType
        {
            get => m_LightType;
            set
            {
                m_LightType = value;
                Position = m_Position.ToVector3;
            }
        }

        public Vector3 Position
        {
            get => m_Position.ToVector3;
            set
            {
                float lightTypeValue = m_LightType == eLightTypes.Directional ? 0.0f : 1.0f;
                m_Position = new Vector4(value.X, value.Y, value.Z, lightTypeValue);

                GL.glLightfv(r_LightSourceId, GL.GL_POSITION, m_Position.ToArray);
            }
        }

        public Vector4 Ambient
        {
            get => m_Ambient;
            set
            {
                m_Ambient = value;
                GL.glLightfv(r_LightSourceId, GL.GL_AMBIENT, m_Ambient.ToArray);
            }
        }

        public Vector4 Diffuse
        {
            get => m_Diffuse;
            set
            {
                m_Diffuse = value;
                GL.glLightfv(r_LightSourceId, GL.GL_DIFFUSE, m_Diffuse.ToArray);
            }
        }

        public Vector4 Specular
        {
            get => m_Specular;
            set
            {
                m_Specular = value;
                GL.glLightfv(r_LightSourceId, GL.GL_SPECULAR, m_Specular.ToArray);
            }
        }

        public Vector4 Emission
        {
            get => m_Emission;
            set
            {
                m_Emission = value;
                GL.glLightfv(r_LightSourceId, GL.GL_EMISSION, m_Emission.ToArray);
            }
        }

        public float ConstantAttenuation
        {
            get => m_ConstantAttenuation;
            set
            {
                m_ConstantAttenuation = value;
                GL.glLightf(r_LightSourceId, GL.GL_CONSTANT_ATTENUATION, m_ConstantAttenuation);
            }
        }

        public float LinearAttenuation
        {
            get => m_LinearAttenuation;
            set
            {
                m_LinearAttenuation = value;
                GL.glLightf(r_LightSourceId, GL.GL_LINEAR_ATTENUATION, m_LinearAttenuation);
            }
        }

        public float QuadraticAttenuation
        {
            get => m_QuadraticAttenuation;
            set
            {
                m_QuadraticAttenuation = value;
                GL.glLightf(r_LightSourceId, GL.GL_QUADRATIC_ATTENUATION, m_QuadraticAttenuation);
            }
        }

        private Light(uint i_LightSourceId, eLightTypes i_LightType)
        {
            sr_AvailableLights[i_LightSourceId] = false;
            r_LightSourceId = i_LightSourceId;
            m_LightType = i_LightType;

            setDefaultLightParameters();

            TurnOn();
        }

        private void setDefaultLightParameters()
        {
            Ambient = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            Diffuse = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
            Specular = new Vector4(1.0f);
            Emission = new Vector4(0.0f);
            ConstantAttenuation = 0.0f;
            LinearAttenuation = 1.0f;
            QuadraticAttenuation = 0.0f;
        }

        ~Light()
        {
            //TurnOff();
            sr_AvailableLights[r_LightSourceId] = true;
        }

        public void TurnOn()
        {
            GL.glEnable(r_LightSourceId);
        }

        public void TurnOff()
        {
            GL.glDisable(r_LightSourceId);
        }
    }
}
