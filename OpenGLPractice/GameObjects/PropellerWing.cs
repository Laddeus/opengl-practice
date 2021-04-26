namespace OpenGLPractice.GameObjects
{
    internal class PropellerWing : GameObject
    {
        private Sphere r_FirstWing;

        public PropellerWing(string i_Name) : base(i_Name)
        {
            r_FirstWing = new Sphere("Wing1");

            r_FirstWing.Transform.Translate(0, 0, 0.25f);
            r_FirstWing.Transform.ChangeScale(0.15f, 0.01f, 0.5f);
        }

        protected override void DefineGameObject()
        {
            r_FirstWing.Draw(!k_UseDisplayList);
        }

        public override void Tick(float i_DeltaTime)
        {
        }
    }
}
