namespace OpenGLPractice.GameObjects
{
    internal class HeliCup : GameObject
    {
        public enum eHeliCupFlyingStates
        {
            Grounded,
            FlyingUp,
            FlyingDown,
            Hovering
        };

        private readonly Cup r_Cup;
        private readonly TelescopicPropeller r_TelescopicPropeller;

        public eHeliCupFlyingStates State { get; set; }

        public HeliCup(string i_Name) : base(i_Name)
        {
            r_Cup = (Cup)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Cup, "Cup");
            r_TelescopicPropeller = (TelescopicPropeller)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.TelescopicPropeller, "TelescopicPropeller");

            r_TelescopicPropeller.Transform.Translate(0, r_Cup.Height + 0.01f, 0);
            r_TelescopicPropeller.Transform.ChangeScale(0.7f, 0.7f, 0.7f);

            Children.AddRange(new GameObject[] { r_Cup, r_TelescopicPropeller });
        }

        protected override void DefineGameObject()
        {
        }

        public override void Tick(float i_DeltaTime)
        {
            base.Tick(i_DeltaTime);
        }

        public void FlyUp()
        {

        }
    }
}
