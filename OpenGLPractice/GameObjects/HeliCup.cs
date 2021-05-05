using System;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;

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
        }

        private const float k_HoverRange = 0.35f;
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
        private float m_HoverStartHeight;
        ////private float m_Ease = 1.0f;
        private void hoverTick(float i_DeltaTime)
        {
            Vector3 endHoverPosition = m_IsHoveringUp
                ? new Vector3(0, m_HoverStartHeight + k_HoverRange, 0)
                : new Vector3(0, m_HoverStartHeight - k_HoverRange, 0);

            if (Math.Abs(Transform.Position.Y - m_HoverStartHeight) < k_HoverRange - 0.1f)
            {
                Transform.Position = Vector3.LinearlyInterpolate(Transform.Position, endHoverPosition, 0.06f);
            }
            else
            {
                m_IsHoveringUp = !m_IsHoveringUp;
                m_HoverStartHeight = Transform.Position.Y;
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
                m_HoverStartHeight = Transform.Position.Y;
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
