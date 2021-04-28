using OpenGLPractice.Game;

namespace OpenGLPractice.GameObjects
{
    internal class HeliCup : GameObject
    {
        public enum eHeliCupFlyingStates
        {
            Grounded,
            Ascending,
            Descending,
            Hovering
        };

        private const float k_HoverRange = 0.5f;
        private const float k_FlyingSpeed = 5.0f;
        private const float k_HoverSpeed = 1.5f;
        private readonly Cup r_Cup;
        private readonly TelescopicPropeller r_TelescopicPropeller;
        private float m_FlyingDistance;

        public eHeliCupFlyingStates State { get; set; }

        public HeliCup(string i_Name) : base(i_Name)
        {
            r_Cup = (Cup)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Cup, "Cup");
            r_TelescopicPropeller = (TelescopicPropeller)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.TelescopicPropeller, "TelescopicPropeller");

            r_TelescopicPropeller.Transform.Translate(0, r_Cup.Height + 0.01f, 0);
            r_TelescopicPropeller.Transform.ChangeScale(0.7f, 0.7f, 0.7f);

            DisplayShadow = true;

            Children.AddRange(new GameObject[] { r_Cup, r_TelescopicPropeller });
            UpdateShadowsDescent(this);

            Ascend(10.0f);
        }

        protected override void DefineGameObject()
        {
        }

        public override void Tick(float i_DeltaTime)
        {
            base.Tick(i_DeltaTime);

            switch (State)
            {
                case eHeliCupFlyingStates.Ascending:
                    ascendTick(i_DeltaTime);
                    break;
                case eHeliCupFlyingStates.Hovering:
                    hoverTick(i_DeltaTime);
                    break;
                case eHeliCupFlyingStates.Descending:
                    descendTick(i_DeltaTime);
                    break;
                default:
                    break;
            }
        }


        // TODO: fix hovering
        private bool m_IsHoveringUp = true;
        private float m_Ease = 1.0f;
        private void hoverTick(float i_DeltaTime)
        {
            if (m_IsHoveringUp)
            {
                if (Transform.Position.Y < m_FlyingDistance + k_HoverRange)
                {
                    Transform.Translate(k_HoverSpeed * m_Ease * i_DeltaTime * Transform.UpVector);
                    m_Ease -= 0.01f;
                }
                else
                {
                    m_Ease = 1.0f;
                    m_IsHoveringUp = false;
                }
            }
            else
            {
                if (Transform.Position.Y > m_FlyingDistance - k_HoverRange)
                {
                    Transform.Translate(k_HoverSpeed * m_Ease * i_DeltaTime * -Transform.UpVector);
                    m_Ease -= 0.01f;
                }
                else
                {
                    m_Ease = 1.0f;
                    m_IsHoveringUp = true;
                }
            }
        }

        private void descendTick(float i_DeltaTime)
        {
        }

        private void ascendTick(float i_DeltaTime)
        {
            if (Transform.Position.Y < m_FlyingDistance)
            {
                Transform.Translate(k_FlyingSpeed * i_DeltaTime * Transform.UpVector);
            }
            else
            {
                State = eHeliCupFlyingStates.Hovering;
            }
        }

        public void Ascend(float i_DistanceToAscend)
        {
            m_FlyingDistance = i_DistanceToAscend;
            r_TelescopicPropeller.OpenTelescope();

            r_TelescopicPropeller.Opened += TelescopicPropeller_Opened;
        }

        private void TelescopicPropeller_Opened()
        {
            State = eHeliCupFlyingStates.Ascending;
            r_TelescopicPropeller.Spin();
            r_TelescopicPropeller.Opened -= TelescopicPropeller_Opened;
        }

        public void Descend(float i_DistanceToDescend)
        {
            m_FlyingDistance = i_DistanceToDescend;
            State = eHeliCupFlyingStates.Descending;
        }
    }
}
