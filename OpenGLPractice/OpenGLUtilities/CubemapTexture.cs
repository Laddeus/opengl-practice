using System;

namespace OpenGLPractice.OpenGLUtilities
{
    internal class CubemapTexture
    {
        public enum eTextureFace
        {
            Front,
            Back,
            Left,
            Right,
            Top,
            Bottom
        }

        private const int k_TextureCount = 6;
        private readonly string r_CubemapTexturePath;
        private readonly string r_ImageFormat;

        public string TextureName { get; set; }

        private Texture[] m_CubemapTextures;

        public bool IsLoaded => m_CubemapTextures != null;

        public CubemapTexture(string i_TextureName, string i_CubemapTexturePath, string i_ImageFormat)
        {
            TextureName = i_TextureName;
            r_CubemapTexturePath = i_CubemapTexturePath;
            r_ImageFormat = i_ImageFormat;
        }

        public Texture this[eTextureFace i_TextureFace] => GetTexture(i_TextureFace);

        public Texture GetTexture(eTextureFace i_TextureFace)
        {
            if (!IsLoaded)
            {
                throw new Exception("Textures are not loaded");
            }

            return m_CubemapTextures[(int)i_TextureFace];
        }

        public void LoadTextures()
        {
            m_CubemapTextures = new Texture[k_TextureCount];

            foreach (string textureName in Enum.GetNames(typeof(eTextureFace)))
            {
                string texturePath = $"{r_CubemapTexturePath}\\{textureName}{r_ImageFormat}";
                eTextureFace textureFaceType = (eTextureFace)Enum.Parse(typeof(eTextureFace), textureName);
                m_CubemapTextures[(int)textureFaceType] = new Texture(texturePath);
            }
        }

        public override string ToString()
        {
            return TextureName;
        }
    }
}