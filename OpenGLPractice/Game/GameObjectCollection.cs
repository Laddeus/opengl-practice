using System.Collections.ObjectModel;

namespace OpenGLPractice.Game
{
    internal class GameObjectCollection : Collection<GameObject>
    {
        private readonly GameObject r_CollectionParent;

        public GameObjectCollection(GameObject i_CollectionParent)
        {
            r_CollectionParent = i_CollectionParent;
        }

        public new void Add(GameObject i_GameObjectChild)
        {
            i_GameObjectChild.Parent = r_CollectionParent;
            Items.Add(i_GameObjectChild);
        }

        public new bool Remove(GameObject i_GameObjectChild)
        {
            bool isRemoved = Items.Remove(i_GameObjectChild);

            if (isRemoved)
            {
                i_GameObjectChild.Parent = null;
            }

            return isRemoved;
        }

        public void AddRange(GameObject[] i_GameObjects)
        {
            foreach (GameObject gameObject in i_GameObjects)
            {
                Add(gameObject);
            }
        }
    }
}
