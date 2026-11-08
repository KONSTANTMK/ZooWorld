using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using ZooWorld.Animals;
using Object = UnityEngine.Object;

namespace ZooWorld.Tests
{
    internal static class TestContent
    {
        private const string ContentFolder = "Assets/_Project/Content";

        public static AnimalDefinition LoadAnimal(string id)
        {
            return LoadAll<AnimalDefinition>().Single(animal => animal.Id == id);
        }

        public static T LoadSingle<T>()
            where T : Object
        {
            return LoadAll<T>().Single();
        }

        public static IEnumerable<T> LoadAll<T>()
            where T : Object
        {
            return AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { ContentFolder })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>);
        }
    }
}
