using System;
using System.Collections.Generic;
using System.Linq;
using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.OpenGLUtilities
{
    internal class Light
    {
        public enum eLightTypes
        {
            Point,
            Directional,
            Spotlight,
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
        private Vector3 m_SpotlightDirection;
        private float m_SpotlightCutoff;
        private float m_SpotlightExponent;
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

                GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_POSITION, m_Position.ToArray));
            }
        }

        public Vector4 Position4 => m_Position;

        public Vector4 Ambient
        {
            get => m_Ambient;
            set
            {
                m_Ambient = value;
                GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_AMBIENT, m_Ambient.ToArray));
            }
        }

        public Vector4 Diffuse
        {
            get => m_Diffuse;
            set
            {
                m_Diffuse = value;
                GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_DIFFUSE, m_Diffuse.ToArray));
            }
        }

        public Vector4 Specular
        {
            get => m_Specular;
            set
            {
                m_Specular = value;
                GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_SPECULAR, m_Specular.ToArray));
            }
        }

        public Vector3 SpotlightDirection
        {
            get => m_SpotlightDirection;
            set
            {
                m_SpotlightDirection = value;
                GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_SPOT_DIRECTION, m_SpotlightDirection.ToArray));
            }
        }

        public float SpotlightCutoff
        {
            get => m_SpotlightCutoff;
            set
            {
                m_SpotlightCutoff = value;
                GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_SPOT_CUTOFF, m_SpotlightCutoff));
            }
        }

        public float SpotlightExponent
        {
            get => m_SpotlightExponent;
            set
            {
                m_SpotlightExponent = value;
                GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_SPOT_EXPONENT, m_SpotlightExponent));
            }
        }

        public float ConstantAttenuation
        {
            get => m_ConstantAttenuation;
            set
            {
                m_ConstantAttenuation = value;
                GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_CONSTANT_ATTENUATION, m_ConstantAttenuation));
            }
        }

        public float LinearAttenuation
        {
            get => m_LinearAttenuation;
            set
            {
                m_LinearAttenuation = value;
                GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_LINEAR_ATTENUATION, m_LinearAttenuation));
            }
        }

        public float QuadraticAttenuation
        {
            get => m_QuadraticAttenuation;
            set
            {
                m_QuadraticAttenuation = value;
                GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_QUADRATIC_ATTENUATION, m_QuadraticAttenuation));
            }
        }

        private Light(uint i_LightSourceId, eLightTypes i_LightType)
        {
            sr_AvailableLights[i_LightSourceId] = false;
            r_LightSourceId = i_LightSourceId;
            m_LightType = i_LightType;

            initializeDefaultLightParameters();

            if (m_LightType == eLightTypes.Spotlight)
            {
                initializeSpotlightDefaultParameters();
            }
            else
            {
                SpotlightCutoff = 180.0f; // 180 means spotlight is off 
            }

            TurnOn();
        }

        ~Light()
        {
            ////TurnOff();
            sr_AvailableLights[r_LightSourceId] = true;
        }

        public void ApplyPositionsAndDirection()
        {
            GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_POSITION, m_Position.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_SPOT_DIRECTION, m_SpotlightDirection.ToArray));
        }

        public void ApplyLight()
        {
            GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_POSITION, m_Position.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_AMBIENT, m_Ambient.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_DIFFUSE, m_Diffuse.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_SPECULAR, m_Specular.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glLightfv(r_LightSourceId, GL.GL_SPOT_DIRECTION, m_SpotlightDirection.ToArray));
            GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_SPOT_CUTOFF, m_SpotlightCutoff));
            GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_SPOT_EXPONENT, m_SpotlightExponent));
            GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_CONSTANT_ATTENUATION, m_ConstantAttenuation));
            GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_LINEAR_ATTENUATION, m_LinearAttenuation));
            GLErrorCatcher.TryGLCall(() => GL.glLightf(r_LightSourceId, GL.GL_QUADRATIC_ATTENUATION, m_QuadraticAttenuation));
        }

        private void initializeSpotlightDefaultParameters()
        {
            SpotlightDirection = new Vector3(0, 0, -1f);
            SpotlightCutoff = 10.0f;
            SpotlightExponent = 30.0f;
        }

        private void initializeDefaultLightParameters()
        {
            Position = new Vector3(0, 0, 1);
            Ambient = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            Diffuse = new Vector4(1.0f);
            Specular = new Vector4(1.0f);
            ConstantAttenuation = 1.0f;
            LinearAttenuation = 0.14f;
            QuadraticAttenuation = 0.07f;
        }

        public void TurnOn()
        {
            GLErrorCatcher.TryGLCall(() => GL.glEnable(r_LightSourceId));
        }

        public void TurnOff()
        {
            GLErrorCatcher.TryGLCall(() => GL.glDisable(r_LightSourceId));
        }
    }
}
