namespace OpenGLPractice.GameObjects
{
    internal class Propeller : GameObject
    {

        public enum ePropellerState
        {
            Folded,
            Opened,
            Folding,
            Opening
        };

        private PropellerWing r_FirstPropellerWing;
        private PropellerWing r_SecondPropellerWing;
        private const float k_RotationsPerSecond = 5.0f;
        private const float k_WingsFoldAngle = 80.0f;
        private const float k_WingsOpenedAngle = 0.0f;
        private const float k_FoldOpenSpeed = 10.0f;
        private float m_CurrentWingsAngle;
        private bool m_NeedFolding = false;
        private bool m_NeedOpening = false;

        public ePropellerState State { get; private set; } = ePropellerState.Folded;

        public Propeller(string i_Name) : base(i_Name)
        {
            r_FirstPropellerWing = new PropellerWing("Wing1");
            r_SecondPropellerWing = new PropellerWing("Wing1");

            r_SecondPropellerWing.Transform.Rotate(180, 0, 1, 0);

            r_FirstPropellerWing.Transform.Rotate(k_WingsFoldAngle, 1, 0, 0);
            r_SecondPropellerWing.Transform.Rotate(-k_WingsFoldAngle, 1, 0, 0);
            m_CurrentWingsAngle = 80.0f;

            OpenWings();
        }

        protected override void DefineGameObject()
        {
            r_FirstPropellerWing.Draw();
            r_SecondPropellerWing.Draw();
        }

        public override void Tick(float i_DeltaTime)
        {
            if (m_NeedFolding)
            {
                foldWings(i_DeltaTime);
            }
            else if (m_NeedOpening)
            {
                openWings(i_DeltaTime);
            }
        }

        public void OpenWings()
        {
            m_NeedOpening = true;
        }

        public void FoldWings()
        {
            m_NeedFolding = true;
        }

        private void foldWings(float i_DeltaTime)
        {
            if (State == ePropellerState.Folding || State == ePropellerState.Opened)
            {
                State = ePropellerState.Folding;

                r_FirstPropellerWing.Transform.Rotate(k_FoldOpenSpeed * i_DeltaTime, 1, 0, 0);
                r_SecondPropellerWing.Transform.Rotate(-k_FoldOpenSpeed * i_DeltaTime, 1, 0, 0);
                m_CurrentWingsAngle += k_FoldOpenSpeed * i_DeltaTime;

                if (m_CurrentWingsAngle >= k_WingsFoldAngle)
                {
                    State = ePropellerState.Folded;
                    m_NeedFolding = false;
                }
            }
        }

        private void openWings(float i_DeltaTime)
        {
            if (State == ePropellerState.Opening || State == ePropellerState.Folded)
            {
                State = ePropellerState.Opening;

                r_FirstPropellerWing.Transform.Rotate(-k_FoldOpenSpeed * i_DeltaTime, 1, 0, 0);
                r_SecondPropellerWing.Transform.Rotate(k_FoldOpenSpeed * i_DeltaTime, 1, 0, 0);

                m_CurrentWingsAngle -= k_FoldOpenSpeed * i_DeltaTime;

                if (m_CurrentWingsAngle <= k_WingsOpenedAngle)
                {
                    State = ePropellerState.Opened;
                    m_NeedOpening = false;
                }
            }
        }
    }
}
