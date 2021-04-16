using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenGLPractice.GameObjects
{
    internal enum eGameObjectTypes
    {
        Cup,
        Telescope,
        HeliCup,
        Rod,
        Cube,
        Sphere,
        Arrow,
        PropellerWing,
        Propeller
    }

    internal static class GameObjectCreator
    {
        private static readonly Dictionary<string, Type> sr_NameToGameObjectTypeDictionary;

        static GameObjectCreator()
        {
            string currentNamespace = typeof(GameObjectCreator).Namespace;
            sr_NameToGameObjectTypeDictionary = new Dictionary<string, Type>();
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            foreach (Type type in currentAssembly.GetTypes())
            {
                if (type.Namespace == currentNamespace && type.IsSubclassOf(typeof(GameObject)))
                {
                    sr_NameToGameObjectTypeDictionary.Add(type.Name, type);
                }
            }
        }

        public static GameObject CreateGameObjectDefault(eGameObjectTypes i_GameObjectType, string i_GameObjectName, params object[] i_Arguments)
        {
            return CreateGameObjectDefault(i_GameObjectType.ToString(), i_GameObjectName, i_Arguments);
        }

        public static GameObject CreateGameObjectDefault(Type i_ClassType, string i_GameObjectName, params object[] i_Arguments)
        {
            return CreateGameObjectDefault(i_ClassType.Name, i_GameObjectName, i_Arguments);
        }

        public static GameObject CreateGameObjectDefault(string i_ClassName, string i_GameObjectName, params object[] i_Arguments)
        {
            GameObject gameObjectToCreate = null;
            Type gameObjectType;

            bool isClassNameAType = sr_NameToGameObjectTypeDictionary.TryGetValue(i_ClassName, out gameObjectType);

            if (isClassNameAType)
            {
                ConstructorInfo gameObjectConstructor = gameObjectType.GetConstructors()[0];
                int parameterCount = gameObjectConstructor.GetParameters().Length;
                object[] parameters = new object[parameterCount];

                parameters[0] = i_GameObjectName;

                for (int i = 1; i < parameterCount; i++)
                {
                    parameters[i] = Type.Missing;
                }

                gameObjectToCreate = (GameObject)gameObjectConstructor.Invoke(parameters);
                gameObjectToCreate.InitializeList();
            }
            else
            {
                throw new Exception($"There is no GameObject with class name {i_ClassName}");
            }

            return gameObjectToCreate;
        }

        public static Rod CreateRod(string i_Name, float i_InnerRodRadius = 0.1f, float i_OuterRingWidth = 0.05f,
            float i_Height = 1)
        {
            Rod rodCreated = new Rod(i_Name, i_InnerRodRadius, i_OuterRingWidth, i_Height);
            rodCreated.InitializeList();

            return rodCreated;
        }

        public static string[] GetAllGameObjectTypeNames()
        {
            return sr_NameToGameObjectTypeDictionary.Keys.ToArray();
        }
    }
}
