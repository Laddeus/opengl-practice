using OpenGL;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class Propeller : GameObject
    {
        public enum ePropellerState
        {
            Folded,
            Opened,
            Folding,
            Opening,
            Spinning
        };

        private PropellerWing r_FirstPropellerWing;
        private PropellerWing r_SecondPropellerWing;
        private Sphere r_PropellerNose;
        private const float k_RotationsPerSecond = 5.0f;
        private const float k_WingsFoldAngle = 80.0f;
        private const float k_WingsOpenedAngle = 0.0f;
        private const float k_FoldOpenSpeed = 10.0f;
        private float m_CurrentWingsAngle;

        public ePropellerState State { get; private set; } = ePropellerState.Folded;

        public Propeller(string i_Name, float i_NoseRadius) : base(i_Name)
        {
            r_FirstPropellerWing = (PropellerWing)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.PropellerWing, "Wing1");
            r_SecondPropellerWing = (PropellerWing)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.PropellerWing, "Wing2");
            r_PropellerNose = (Sphere)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Sphere, "PropellerNose");

            Children.AddRange(new GameObject[] { r_FirstPropellerWing, r_SecondPropellerWing, r_PropellerNose });

            r_SecondPropellerWing.Transform.Rotate(180, 0, 1, 0);
            r_FirstPropellerWing.Transform.Rotate(k_WingsFoldAngle, 1, 0, 0);
            r_SecondPropellerWing.Transform.Rotate(-k_WingsFoldAngle, 1, 0, 0);
            r_PropellerNose.Transform.ChangeScale(i_NoseRadius, i_NoseRadius, i_NoseRadius);
            r_PropellerNose.Transform.ChangeScale(1, 1.5f, 1);
            r_PropellerNose.Color = new Vector4(0, 0, 0, 1);

            m_CurrentWingsAngle = 80.0f;
        }

        protected override void DefineGameObject()
        {
        }

        public override void Tick(float i_DeltaTime)
        {
            base.Tick(i_DeltaTime);

            switch (State)
            {
                case ePropellerState.Folding:
                    foldWings(i_DeltaTime);
                    break;
                case ePropellerState.Opening:
                    openWings(i_DeltaTime);
                    break;
                case ePropellerState.Spinning:
                    spin(i_DeltaTime);
                    break;
                default:
                    break;
            }
        }

        private void spin(float i_DeltaTime)
        {
            Transform.Rotate(1800 * i_DeltaTime, 0, 1, 0);
        }

        public void Spin()
        {
            State = ePropellerState.Spinning;
        }

        public void StopSpin()
        {
            State = ePropellerState.Opened;
        }

        public void OpenWings()
        {
            State = ePropellerState.Opening;
        }

        public void FoldWings()
        {
            State = ePropellerState.Folding;
        }

        private void foldWings(float i_DeltaTime)
        {
            r_FirstPropellerWing.Transform.Rotate(k_FoldOpenSpeed * i_DeltaTime, 1, 0, 0);
            r_SecondPropellerWing.Transform.Rotate(-k_FoldOpenSpeed * i_DeltaTime, 1, 0, 0);
            m_CurrentWingsAngle += k_FoldOpenSpeed * i_DeltaTime;

            if (m_CurrentWingsAngle >= k_WingsFoldAngle)
            {
                State = ePropellerState.Folded;
            }
        }

        private void openWings(float i_DeltaTime)
        {
            r_FirstPropellerWing.Transform.Rotate(-k_FoldOpenSpeed * i_DeltaTime, 1, 0, 0);
            r_SecondPropellerWing.Transform.Rotate(k_FoldOpenSpeed * i_DeltaTime, 1, 0, 0);

            m_CurrentWingsAngle -= k_FoldOpenSpeed * i_DeltaTime;

            if (m_CurrentWingsAngle <= k_WingsOpenedAngle)
            {
                State = ePropellerState.Opened;
            }
        }
    }
}
