namespace OpenGLPractice.GameObjects
{
    internal class HeliCup : GameObject
    {
        private readonly Cup r_Cup;
        private readonly Telescope r_Telescope;

        public HeliCup(string i_Name) : base(i_Name)
        {
            r_Cup = (Cup)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Cup, "Cup");
            r_Telescope = (Telescope)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Telescope, "Telescope");

            r_Telescope.Transform.Translate(0, r_Cup.Height, 0);
            r_Telescope.Transform.Rotate(-90, 1, 0, 0);
            r_Telescope.Transform.ChangeScale(0.7f, 0.7f, 0.7f);
        }

        protected override void DefineGameObject()
        {
            r_Cup.CallList();
            r_Telescope.CallList();
        }
    }
}
