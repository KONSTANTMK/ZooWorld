using System.Collections.Generic;

namespace ZooWorld.Animals
{
    internal static class AnimalDefinitionValidator
    {
        public static IEnumerable<string> FindErrors(AnimalDefinition definition)
        {
            if (string.IsNullOrEmpty(definition.Id))
            {
                yield return "Id is empty.";
            }

            if (definition.SpawnWeight <= 0f)
            {
                yield return "Spawn Weight must be positive.";
            }

            if (definition.ViewPrefab == null)
            {
                yield return "View prefab is not set.";
            }

            foreach (string error in FindMovementErrors(definition.Movement))
            {
                yield return error;
            }
        }

        private static IEnumerable<string> FindMovementErrors(MovementConfig movement)
        {
            if (movement == null)
            {
                return new[] { "Movement is not set." };
            }

            return movement.FindErrors();
        }
    }
}
