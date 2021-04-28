using System;

namespace OpenGLPractice.Game
{
    internal interface IFoldable
    {
        event Action Folded;

        event Action Opened;

        void OnFolded();

        void OnOpened();
    }
}
