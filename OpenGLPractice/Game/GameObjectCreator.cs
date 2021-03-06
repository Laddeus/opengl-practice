using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenGLPractice.GameObjects;
using Texture = OpenGLPractice.OpenGLUtilities.Texture;

namespace OpenGLPractice.Game
{
    internal enum eGameObjectTypes
    {
        Axes,
        Cup,
        TelescopicPropeller,
        HeliCup,
        Rod,
        Cube,
        Plane,
        Sphere,
        Arrow,
        PropellerWing,
        Propeller,
        Particle,
        Surface,
        TableLeg,
        TableTop,
        Table
    }

    internal static class GameObjectCreator
    {
        private static readonly Dictionary<string, Type> sr_NameToGameObjectTypeDictionary;

        static GameObjectCreator()
        {
            sr_NameToGameObjectTypeDictionary = new Dictionary<string, Type>();
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            foreach (Type type in currentAssembly.GetTypes())
            {
                if (type.Namespace == "OpenGLPractice.GameObjects" && type.IsSubclassOf(typeof(GameObject)))
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
            GameObject gameObjectToCreate;
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
                gameObjectToCreate.InitializeDisplayList();
            }
            else
            {
                throw new Exception($"There is no GameObject with class name {i_ClassName}");
            }

            return gameObjectToCreate;
        }

        public static Rod CreateRod(string i_Name, float i_InnerRodRadius = 0.1f, float i_OuterRingWidth = 0.05f,
            float i_Height = 1, Texture i_RodTexture = null)
        {
            Rod rodCreated = new Rod(i_Name, i_InnerRodRadius, i_OuterRingWidth, i_Height, i_RodTexture);

            rodCreated.InitializeDisplayList();

            return rodCreated;
        }

        public static Surface CreateSurface(string i_Name, float i_XZCoverage, float i_QuadPieceSize, Func<float, float, float> i_SurfaceFunctionXZ)
        {
            Surface surfaceToBeCreated = new Surface(i_Name, i_XZCoverage, i_QuadPieceSize, i_SurfaceFunctionXZ);
            surfaceToBeCreated.InitializeDisplayList();

            return surfaceToBeCreated;
        }

        public static Sphere CreateSphere(string i_Name, float i_SphereRadius, Texture i_SphereTexture)
        {
            Sphere sphereToBeCreated= new Sphere(i_Name, i_SphereRadius, i_SphereTexture);
            sphereToBeCreated.InitializeDisplayList();

            return sphereToBeCreated;
        }

        public static string[] GetAllGameObjectTypeNames()
        {
            return sr_NameToGameObjectTypeDictionary.Keys.ToArray();
        }
    }
}
