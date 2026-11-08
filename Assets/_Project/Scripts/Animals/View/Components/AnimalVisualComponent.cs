using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace ZooWorld.Animals
{
    public sealed class AnimalVisualComponent : MonoBehaviour
    {
        private const float MinDirectionSquared = 0.0001f;

        [SerializeField]
        private Transform _model;

        private Vector3 _basePosition;
        private Vector3 _baseScale;

        private void Awake()
        {
            if (_model == null)
            {
                throw new InvalidOperationException($"{name}: model is not assigned.");
            }

            _basePosition = _model.localPosition;
            _baseScale = _model.localScale;
        }

        public void Face(Vector2 direction)
        {
            if (direction.LengthSquared() < MinDirectionSquared)
            {
                return;
            }

            _model.localRotation = Quaternion.LookRotation(AnimalBodyComponent.ToWorld(direction));
        }

        public void SetHeight(float height) => _model.localPosition = _basePosition + Vector3.up * height;

        public void SetSquash(float squash)
        {
            _model.localScale = Vector3.Scale(_baseScale, new Vector3(1f + squash, 1f - squash, 1f + squash));
        }
    }
}
