using OpenGLPractice.Game;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.GameObjects
{
    internal class MirrorFrame : GameObject
    {
        private const float k_FrameWidth = 0.2f;
        private const float k_FrameHeight = 0.5f;

        public float FrameWidth => k_FrameHeight;

        public float FrameHeight=> k_FrameHeight;

        public MirrorFrame(string i_Name, float i_MirrorFrameRadius = 1)
            : base(i_Name)
        {
            Texture woodTexture = new Texture(@"Textures\Materials\Metal_Plate_044_BaseColor.jpg");

            Rod mirrorFrame = GameObjectCreator.CreateRod("Mirror Frame", i_MirrorFrameRadius, k_FrameWidth, k_FrameHeight, woodTexture);
            mirrorFrame.Transform.Rotate(90, 0, 0, 1);
            mirrorFrame.Color = mirrorFrame.Color * 0.5f;
            //mirrorFrame.UseMaterial = v_UseMaterial;

            mirrorFrame.Material.Ambient = new Vector4(0.05375f, 0.05f, 0.06625f, 0.82f);
            mirrorFrame.Material.Diffuse = new Vector4(0.18275f, 0.17f, 0.22525f, 0.82f);
            mirrorFrame.Material.Specular = new Vector4(0.332741f, 0.328634f, 0.346435f, 0.82f);
            mirrorFrame.Material.Shininess = 38.4f;
            Children.Add(mirrorFrame);
        }

        protected override void DefineGameObject()
        {
        }
    }
}
