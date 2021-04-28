using System;
using OpenGLPractice.Game;

namespace OpenGLPractice.GameObjects
{
    internal class TelescopicPropeller : GameObject, IFoldable
    {
        public enum eTelescopeState
        {
            Folded,
            Opened,
            Folding,
            Opening
        };

        public event Action Folded;

        public event Action Opened;

        private const float k_InitialRodRadius = 0.1f;
        private const float k_InitialRodOuterRingWidth = 0.1f;
        private const float k_InitialRodHeight = 1.0f;
        private readonly Rod r_BottomRod;
        private readonly Rod r_MiddleRod;
        private readonly Rod r_UpperRod;
        private readonly Propeller r_Propeller;

        public eTelescopeState State { get; set; } = eTelescopeState.Folded;

        public float Height => (1.75f * k_InitialRodHeight) * Transform.Scale.Y;

        public TelescopicPropeller(string i_Name) : base(i_Name)
        {
            r_BottomRod = GameObjectCreator.CreateRod("bottomRod", k_InitialRodRadius, k_InitialRodOuterRingWidth,
                k_InitialRodHeight);
            r_MiddleRod = GameObjectCreator.CreateRod("middleRod", k_InitialRodRadius, k_InitialRodOuterRingWidth,
                k_InitialRodHeight);
            r_UpperRod = GameObjectCreator.CreateRod("upperRod", k_InitialRodRadius, k_InitialRodOuterRingWidth,
                k_InitialRodHeight);

            r_MiddleRod.Transform.ChangeScale(0.5f, 0.5f, 0.5f);
            r_UpperRod.Transform.ChangeScale(0.25f, 0.25f, 0.25f);
            r_BottomRod.Transform.Translate(0, -k_InitialRodHeight, 0);
            r_MiddleRod.Transform.Translate(0, -0.5f * k_InitialRodHeight, 0);
            r_UpperRod.Transform.Translate(0, -0.25f * k_InitialRodHeight, 0);

            r_Propeller = new Propeller("Propeller", r_MiddleRod.RodRadius);

            Children.AddRange(new GameObject[] { r_BottomRod, r_MiddleRod, r_UpperRod, r_Propeller });
        }

        protected override void DefineGameObject()
        {
        }

        public override void Tick(float i_DeltaTime)
        {
            base.Tick(i_DeltaTime);

            switch (State)
            {
                case eTelescopeState.Folding:
                    foldTelescopeTick(i_DeltaTime);
                    break;
                case eTelescopeState.Opening:
                    openTelescopeTick(i_DeltaTime);
                    break;
                default:
                    break;
            }
        }

        public void OpenTelescope()
        {
            State = eTelescopeState.Opening;
        }

        public void FoldTelescope()
        {
            State = eTelescopeState.Folding;
            r_Propeller.FoldWings();
        }

        private void openTelescopeTick(float i_DeltaTime)
        {
            if (r_Propeller.State == Propeller.ePropellerState.Folded)
            {
                if (r_BottomRod.Transform.Position.Y < 0.0f)
                {
                    r_Propeller.Transform.Translate(0, 0.25f * i_DeltaTime, 0);
                    r_UpperRod.Transform.Translate(0, 0.25f * i_DeltaTime, 0);
                    r_MiddleRod.Transform.Translate(0, 0.25f * i_DeltaTime, 0);
                    r_BottomRod.Transform.Translate(0, 0.25f * i_DeltaTime, 0);
                }
                else if (r_MiddleRod.Transform.Position.Y < k_InitialRodHeight)
                {
                    r_Propeller.Transform.Translate(0, 0.25f * i_DeltaTime, 0);
                    r_UpperRod.Transform.Translate(0, 0.25f * i_DeltaTime, 0);
                    r_MiddleRod.Transform.Translate(0, 0.25f * i_DeltaTime, 0);
                }
                else if (r_UpperRod.Transform.Position.Y < 1.5f * k_InitialRodHeight)
                {
                    r_Propeller.Transform.Translate(0, 0.25f * i_DeltaTime, 0);
                    r_UpperRod.Transform.Translate(0, 0.25f * i_DeltaTime, 0);
                }
                else
                {
                    r_Propeller.OpenWings();
                    r_Propeller.Opened += Propeller_Opened;
                }
            }
        }

        private void Propeller_Opened()
        {
            State = eTelescopeState.Opened;
            OnOpened();
            r_Propeller.Opened -= Propeller_Opened;
        }

        private void foldTelescopeTick(float i_DeltaTime)
        {
            if (r_Propeller.State == Propeller.ePropellerState.Folded)
            {
                if (r_UpperRod.Transform.Position.Y > 1.25f * k_InitialRodHeight)
                {
                    r_Propeller.Transform.Translate(0, -0.25f * i_DeltaTime, 0);
                    r_UpperRod.Transform.Translate(0, -0.25f * i_DeltaTime, 0);
                }
                else if (r_MiddleRod.Transform.Position.Y > 0.5f * k_InitialRodHeight)
                {
                    r_Propeller.Transform.Translate(0, -0.25f * i_DeltaTime, 0);
                    r_UpperRod.Transform.Translate(0, -0.25f * i_DeltaTime, 0);
                    r_MiddleRod.Transform.Translate(0, -0.25f * i_DeltaTime, 0);
                }
                else if (r_BottomRod.Transform.Position.Y > -k_InitialRodHeight)
                {
                    r_Propeller.Transform.Translate(0, -0.25f * i_DeltaTime, 0);
                    r_UpperRod.Transform.Translate(0, -0.25f * i_DeltaTime, 0);
                    r_MiddleRod.Transform.Translate(0, -0.25f * i_DeltaTime, 0);
                    r_BottomRod.Transform.Translate(0, -0.25f * i_DeltaTime, 0);
                }
                else
                {
                    State = eTelescopeState.Folded;
                    OnFolded();
                }
            }
        }

        public void OnOpened()
        {
            if (Opened != null)
            {
                Opened.Invoke();
            }
        }

        public void OnFolded()
        {
            if (Folded != null)
            {
                Folded.Invoke();
            }
        }

        public void Spin()
        {
            r_Propeller.Spin();
        }
    }
}
