using OpenGLPractice.Game;

namespace OpenGLPractice.GameObjects
{
    internal class PropellerWing : GameObject
    {
        private readonly Sphere r_Wing;

        public PropellerWing(string i_Name) : base(i_Name)
        {
            r_Wing = (Sphere)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Sphere, "Wing");
            r_Wing.Transform.Translate(0, 0, 0.25f);
            r_Wing.Transform.ChangeScale(0.15f, 0.01f, 0.5f);

            Children.Add(r_Wing);
        }

        protected override void DefineGameObject()
        {
        }
    }
}
