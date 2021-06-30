using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGLPractice.GameObjects;
using OpenGLPractice.GLMath;
using OpenGLPractice.Milkshake;

namespace OpenGLPractice.Game
{
    internal class CupSwapper
    {
        public int FirstCupIndex { get; set; }

        public int SecondCupIndex { get; set; }

        public bool IsClockwise { get; set; }

        public float SwapSpeedDegrees { get; set; }

        public int NumberOfSwaps { get; set; }

        private readonly HeliCup[] r_HeliCups;
        private readonly Sphere r_HiddenBall;

        private Vector3 m_PointToRotateAround;

        private int m_CurrentSwapNumber = 0;

        private float m_AccumulatedRotation = 0;

        private int m_HiddenBallLocationIndex;

        private bool m_AnimationStarted = false;

        public event Action SwapAnimationEnded;


        public CupSwapper(HeliCup[] i_HeliCups, Sphere i_HiddenBall, ref int i_HiddenBallLocationIndex)
        {
            NumberOfSwaps = GameEnvironment.RandomNumberGenerator.Next(8, 16);
            r_HiddenBall = i_HiddenBall;
            m_HiddenBallLocationIndex = i_HiddenBallLocationIndex;
            r_HeliCups = i_HeliCups;

            //randomlySetSwapParameters();
        }

        public void PerformSwapAnimation(float i_DeltaTime)
        {
            if(m_AnimationStarted)
            {
                if(m_CurrentSwapNumber < NumberOfSwaps)
                {
                    rotateCups(i_DeltaTime);
                    fixLocationsWhenRotationFinished();
                }
                else
                {
                    Vector3 ballPosition = r_HeliCups[m_HiddenBallLocationIndex].Transform.Position;
                    ballPosition.Y += r_HiddenBall.Radius;
                    r_HiddenBall.Transform.Position = ballPosition;
                    r_HiddenBall.Render = true;
                    m_AnimationStarted = false;
                    OnSwapAnimationEnded();
                }
            }
        }

        private void rotateCups(float i_DeltaTime)
        {
            float speedDegreesWithDirection = IsClockwise ? SwapSpeedDegrees : (-1) * SwapSpeedDegrees;

            r_HeliCups[FirstCupIndex].Transform.RotateAround(
                i_DeltaTime * speedDegreesWithDirection,
                m_PointToRotateAround,
                Vector3.Up);

            r_HeliCups[SecondCupIndex].Transform.RotateAround(
                i_DeltaTime * speedDegreesWithDirection,
                m_PointToRotateAround,
                Vector3.Up);

            m_AccumulatedRotation += Math.Abs(i_DeltaTime * speedDegreesWithDirection);
        }

        private void fixLocationsWhenRotationFinished()
        {
            if(m_AccumulatedRotation >= 180)
            {
                float fixPositionDegrees =
                    IsClockwise ? (-1) * (m_AccumulatedRotation - 180) : (m_AccumulatedRotation - 180);

                r_HeliCups[FirstCupIndex].Transform.RotateAround(fixPositionDegrees, m_PointToRotateAround, Vector3.Up);

                r_HeliCups[SecondCupIndex].Transform.RotateAround(
                    fixPositionDegrees,
                    m_PointToRotateAround,
                    Vector3.Up);

                if (m_HiddenBallLocationIndex == FirstCupIndex || m_HiddenBallLocationIndex == SecondCupIndex)
                {
                    m_HiddenBallLocationIndex =
                        m_HiddenBallLocationIndex == FirstCupIndex ? SecondCupIndex : FirstCupIndex;
                }

                swapCupsInArray();
                randomlySetSwapParameters();

                m_AccumulatedRotation = 0;
                m_CurrentSwapNumber++;
            }
        }

        private void swapCupsInArray()
        {
            HeliCup cupPlaceholder = r_HeliCups[FirstCupIndex];
            r_HeliCups[FirstCupIndex] = r_HeliCups[SecondCupIndex];
            r_HeliCups[SecondCupIndex] = cupPlaceholder;
        }

        public void Animate()
        {
            randomlySetSwapParameters();
            m_CurrentSwapNumber = 0;
            m_AccumulatedRotation = 0;
            r_HiddenBall.Render = false;
            m_AnimationStarted = true;
        }

        private void randomlySetSwapParameters()
        {
            FirstCupIndex = GameEnvironment.RandomNumberGenerator.Next(0, 3);
            SecondCupIndex = GameEnvironment.RandomNumberGenerator.Next(0, 3);

            while (SecondCupIndex == FirstCupIndex)
            {
                SecondCupIndex = GameEnvironment.RandomNumberGenerator.Next(0, 3);
            }

            IsClockwise = GameEnvironment.RandomNumberGenerator.Next(0, 2) != 0;
            SwapSpeedDegrees = GameEnvironment.RandomNumberGenerator.Next(360, 720);

            m_PointToRotateAround =
                (r_HeliCups[FirstCupIndex].Transform.Position + r_HeliCups[SecondCupIndex].Transform.Position) / 2;
        }

        protected virtual void OnSwapAnimationEnded()
        {
            SwapAnimationEnded?.Invoke();
        }
    }
}
