namespace OpenGLPractice.GameObjects
{
    internal class HeliCup : GameObject
    {
        private readonly Cup r_Cup;
        private readonly Telescope r_Telescope;
        private readonly Propeller r_Propeller;

        public HeliCup(string i_Name) : base(i_Name)
        {
            r_Cup = (Cup)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Cup, "Cup");
            r_Telescope = (Telescope)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Telescope, "Telescope");
            r_Propeller =
                (Propeller)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Propeller, "CupPropeller");

            r_Telescope.Transform.Translate(0, r_Cup.Height + 0.01f, 0);
            r_Telescope.Transform.Rotate(-90, 1, 0, 0);
            r_Telescope.Transform.ChangeScale(0.7f, 0.7f, 0.7f);

            r_Propeller.Transform.Translate(0, r_Cup.Height + 0.7f * r_Telescope.Height, 0);
        }

        protected override void DefineGameObject()
        {
            r_Cup.CallList();
            r_Telescope.Draw();
            r_Propeller.Draw();
        }

        public override void Tick(float i_DeltaTime)
        {
            r_Cup.Tick(i_DeltaTime);
            //r_Telescope.Tick(i_DeltaTime);
            r_Propeller.Tick(i_DeltaTime);
        }
    }
}
