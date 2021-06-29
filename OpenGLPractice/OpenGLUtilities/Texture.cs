using System.Drawing;
using System.Drawing.Imaging;
using OpenGL;

namespace OpenGLPractice.OpenGLUtilities
{
    internal class Texture
    {
        private const int k_TextureGenerationCount = 1;
        private uint[] m_TextureId;

        public Texture(string i_TextureImagePath)
        {
            Bitmap loadedImages = new Bitmap(i_TextureImagePath);
            m_TextureId = generateTextures(loadedImages);
        }

        public Texture(Bitmap i_TextureImage)
        {
            m_TextureId = generateTextures(i_TextureImage);
        }

        public void BindTexture()
        {
            GLErrorCatcher.TryGLCall(() => GL.glBindTexture(GL.GL_TEXTURE_2D, m_TextureId[0]));
        }

        public void UnbindTexture()
        {
            GLErrorCatcher.TryGLCall(() => GL.glBindTexture(GL.GL_TEXTURE_2D, 0));
        }

        private uint[] generateTextures(Bitmap i_TextureImage)
        {
            uint[] textureId = new uint[k_TextureGenerationCount];
            GLErrorCatcher.TryGLCall(() => GL.glGenTextures(k_TextureGenerationCount, textureId));

            i_TextureImage.RotateFlip(RotateFlipType.RotateNoneFlipY); // Y axis in Windows is directed downwards, while in OpenGL-upwards

            Rectangle rect = new Rectangle(0, 0, i_TextureImage.Width, i_TextureImage.Height);
            BitmapData bitmapImageData = i_TextureImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            GLErrorCatcher.TryGLCall(() => GL.glBindTexture(GL.GL_TEXTURE_2D, textureId[0]));
            GLErrorCatcher.TryGLCall(() => GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, i_TextureImage.Width, i_TextureImage.Height,
                0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapImageData.Scan0));
            GLErrorCatcher.TryGLCall(() => GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR));
            GLErrorCatcher.TryGLCall(() => GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR));
            GLErrorCatcher.TryGLCall(() => GL.glBindTexture(GL.GL_TEXTURE_2D, 0));

            i_TextureImage.UnlockBits(bitmapImageData);
            i_TextureImage.Dispose();

            return textureId;
        }
    }
}
