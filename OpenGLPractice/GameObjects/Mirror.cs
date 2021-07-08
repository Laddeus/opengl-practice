using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGLPractice.Game;

namespace OpenGLPractice.GameObjects
{
    internal class Mirror : GameObject
    {
        private readonly MirrorSurface r_ReflectiveMirrorSurface;

        public MirrorSurface ReflectiveMirrorSurface => r_ReflectiveMirrorSurface;

        public Mirror(string i_Name, float i_MirrorSize = 1.0f) 
            : base(i_Name)
        {
            MirrorFrame mirrorFrame = new MirrorFrame("Mirror Frame", i_MirrorSize);
            TelescopicPropeller mirrorPropeller = new TelescopicPropeller("Mirror Propeller", true);
            mirrorFrame.Transform.Translate(mirrorFrame.FrameHeight / 2, 0, 0);
            mirrorPropeller.Transform.Translate(0, i_MirrorSize + 1.2f, 0);
            mirrorPropeller.DisplayReflection = false;
            r_ReflectiveMirrorSurface = new MirrorSurface("Mirror Surface", i_MirrorSize);
            //UseDisplayList = v_UseDisplayList;
            mirrorPropeller.Spin();
            Children.AddRange(new GameObject[] { mirrorPropeller, mirrorFrame, r_ReflectiveMirrorSurface});
        }

        protected override void DefineGameObject()
        {
        }
    }
}
