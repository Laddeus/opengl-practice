using OpenGLPractice.Game;

namespace OpenGLPractice.GameObjects
{
    internal class Plane : GameObject
    {
        private readonly Cube r_CubePlane;

        public Plane(string i_Name) : base(i_Name)
        {
            r_CubePlane = (Cube)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Cube, "Ground");
            r_CubePlane.Transform.ChangeScale(5.0f, 0.01f, 5.0f);
            DisplayShadow = false;

            Children.Add(r_CubePlane);
        }

        protected override void DefineGameObject()
        {
        }
    }
}
