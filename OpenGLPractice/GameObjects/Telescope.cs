using OpenGL;

namespace OpenGLPractice.GameObjects
{
    internal class Telescope : GameObject
    {
        private const float k_InitialRodRadius = 0.2f;
        private const float k_InitialRodHeight = 3.0f;
        private readonly Rod r_BottomRod;
        private readonly Rod r_MiddleRod;
        private readonly Rod r_UpperRod;

        public Telescope(string i_Name) : base(i_Name)
        {
            float middleRodRadius = k_InitialRodRadius / 2.0f;
            float upperRodRadius = middleRodRadius / 2.0f;

            r_BottomRod = new Rod("bottomRod", k_InitialRodRadius, middleRodRadius, k_InitialRodHeight);
            r_MiddleRod = new Rod("middleRod", middleRodRadius, upperRodRadius, k_InitialRodHeight - 1);
            r_UpperRod = new Rod("upperRod", upperRodRadius, upperRodRadius / 2.0f, k_InitialRodHeight - 2);

            r_MiddleRod.Transform.Translate(0, 0, k_InitialRodHeight);
            r_UpperRod.Transform.Translate(0, 0, (2 * k_InitialRodHeight) - 1);
        }

        protected override void DefineGameObject()
        {
            r_BottomRod.CallList();
            r_MiddleRod.CallList();
            r_UpperRod.CallList();
        }
    }
}
