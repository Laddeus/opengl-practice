using System;
using System.Linq;
using OpenGLPractice.GLMath;

namespace OpenGLPractice.GameObjects
{
    internal class Particles : GameObject
    {
        private readonly Cube[] r_CubeParticles;
        private const int k_ParticleCount = 10;
        private const float k_ParticleTravelDistance = 5.0f;
        private const float k_ParticleSize = 0.25f;
        private static readonly Random sr_Random = new Random();

        public Particles(string i_Name) : base(i_Name)
        {
            r_CubeParticles = Enumerable.Range(1, k_ParticleCount).
                Select(i_Inedx =>
                {
                    Cube particle =
                        (Cube)GameObjectCreator.CreateGameObjectDefault(eGameObjectTypes.Cube, $"Particle{i_Inedx}");
                    particle.Transform.ChangeScale(new Vector3(k_ParticleSize));
                    particle.Color = new Vector4(0.2f);

                    return particle;
                }).
                ToArray();
        }

        protected override void DefineGameObject()
        {
            foreach (Cube cubeParticle in r_CubeParticles)
            {
                cubeParticle.Draw(!k_UseDisplayList);
            }
        }

        public override void Tick(float i_DeltaTime)
        {
            foreach (Cube cubeParticle in r_CubeParticles)
            {
                float currentParticleSpeed = (float)(1.5f * sr_Random.NextDouble());
                float randomAngle = (float)(sr_Random.Next(-60, 60) * Math.PI / 180.0f);
                Vector3 currentParticleDirection =
                    new Vector3(0, (float)Math.Sin(randomAngle), (float)Math.Cos(randomAngle));

                cubeParticle.Transform.Translate(currentParticleSpeed * i_DeltaTime * currentParticleDirection);

                if (cubeParticle.Transform.Position.Z >= k_ParticleTravelDistance)
                {
                    // position 0 is the center of the parent gameobject
                    cubeParticle.Transform.Position = Vector3.Zero;
                }
            }
        }
    }
}
