using System;
using System.Linq;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice.GameObjects
{
    internal class Table : GameObject
    {
        private readonly TableLeg[] r_TableLegs;
        private readonly TableTop r_TableTop;

        public float TableHeight => TableLeg.LegHeight + r_TableTop.TopHeight;

        public float TableTopRadius => r_TableTop.TableRadius;

        public Table(string i_Name)
            : base(i_Name)
        {
            Color = new Vector4(0.5882f, 0.3098f, 0.2353f, 1.0f);

            Texture legWoodTexture = new Texture(@"Textures\Materials\LegWood.png");
            Texture topWoodTexture = new Texture(@"Textures\Materials\TopWood.png");

            Material woodMaterial = new Material()
                                        {
                                            Ambient = new Vector4(0.25f, 0.148f, 0.06475f, 1.0f) * 2,
                                            Diffuse = new Vector4(0.8f, 0.4368f, 0.2036f, 1.0f),
                                            Specular = new Vector4(
                                                0.774597f,
                                                0.458561f,
                                                0.200621f,
                                                1.0f) * 0.5f,
                                            Shininess = 76.8f
                                        };

            //r_TableLegs = new TableLeg[]
            //                  {
            //                      new TableLeg("TableLeg1", topWoodTexture),
            //                      new TableLeg("TableLeg2", topWoodTexture),
            //                      new TableLeg("TableLeg3", topWoodTexture),
            //                  };

            r_TableTop = new TableTop("TableTop", topWoodTexture);


            double angleRadians = Math.PI / 4;
            r_TableLegs = Enumerable.Range(1, 3).Select(
                i_Index =>
                    {
                        TableLeg tableLeg = new TableLeg($"TableLeg{i_Index}", legWoodTexture);

                        tableLeg.Transform.Position = new Vector3((float)Math.Cos(angleRadians), 0, (float)Math.Sin(angleRadians))
                                                      * (r_TableTop.TableRadius - TableLeg.LegRadius * 4);
                        tableLeg.Material = woodMaterial;
                        tableLeg.UseMaterial = v_UseMaterial;

                        angleRadians *= (4 - i_Index);
                        return tableLeg;
                    }).ToArray();

            r_TableTop.Transform.Translate(0,TableLeg.LegHeight,0);
            r_TableTop.Material = woodMaterial;
            r_TableTop.UseMaterial = v_UseMaterial;

            UseDisplayList = v_UseDisplayList;

            Children.AddRange(r_TableLegs);
            Children.Add(r_TableTop);
        }

        protected override void DefineGameObject()
        {
        }
    }
}
