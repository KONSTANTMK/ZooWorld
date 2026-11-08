using UnityEditor;

namespace ZooWorld.Animals
{
    [CustomEditor(typeof(AnimalDefinition))]
    internal sealed class AnimalDefinitionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            foreach (string error in AnimalDefinitionValidator.FindErrors((AnimalDefinition)target))
            {
                EditorGUILayout.HelpBox(error, MessageType.Error);
            }
        }
    }
}
