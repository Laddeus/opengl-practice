namespace OpenGLPractice.GameObjects
{
    internal class Telescope : GameObject
    {
        private const float k_InitialRodRadius = 0.1f;
        private const float k_InitialRodOuterRingWidth = 0.1f;
        private const float k_InitialRodHeight = 1.0f;
        private readonly Rod r_BottomRod;
        private readonly Rod r_MiddleRod;
        private readonly Rod r_UpperRod;

        public float Height => 1.75f * k_InitialRodHeight;

        public Telescope(string i_Name) : base(i_Name)
        {
            r_BottomRod = GameObjectCreator.CreateRod("bottomRod", k_InitialRodRadius, k_InitialRodOuterRingWidth,
                k_InitialRodHeight);
            r_MiddleRod = GameObjectCreator.CreateRod("middleRod", k_InitialRodRadius, k_InitialRodOuterRingWidth,
                k_InitialRodHeight);
            r_UpperRod = GameObjectCreator.CreateRod("upperRod", k_InitialRodRadius, k_InitialRodOuterRingWidth,
                k_InitialRodHeight);

            r_MiddleRod.Transform.ChangeScale(0.5f, 0.5f, 0.5f);
            r_UpperRod.Transform.ChangeScale(0.25f, 0.25f, 0.25f);
            r_MiddleRod.Transform.Translate(0, 0, k_InitialRodHeight);
            r_UpperRod.Transform.Translate(0, 0, 1.5f * k_InitialRodHeight);
        }

        protected override void DefineGameObject()
        {
            r_BottomRod.CallList();
            r_MiddleRod.CallList();
            r_UpperRod.CallList();
        }

        public override void Tick(float i_DeltaTime)
        {
            //if (r_UpperRod.Transform.Position.Z > 1.25f * k_InitialRodHeight)
            //{
            //    r_UpperRod.Transform.Translate(0, 0, -0.25f * i_DeltaTime);
            //}
            //else if (r_MiddleRod.Transform.Position.Z > 0.5f * k_InitialRodHeight)
            //{
            //    r_UpperRod.Transform.Translate(0, 0, -0.25f * i_DeltaTime);
            //    r_MiddleRod.Transform.Translate(0, 0, -0.25f * i_DeltaTime);
            //}
            //else if (r_BottomRod.Transform.Position.Z > -k_InitialRodHeight)
            //{
            //    r_UpperRod.Transform.Translate(0, 0, -0.25f * i_DeltaTime);
            //    r_MiddleRod.Transform.Translate(0, 0, -0.25f * i_DeltaTime);
            //    r_BottomRod.Transform.Translate(0, 0, -0.25f * i_DeltaTime);
            //}
        }
    }
}
