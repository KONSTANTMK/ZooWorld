using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace ZooWorld.Animals
{
    [RequireComponent(typeof(AnimalBodyComponent), typeof(AnimalVisualComponent))]
    public sealed class AnimalView : MonoBehaviour, IAnimalView
    {
        [SerializeField]
        private AnimalBodyComponent _body;

        [SerializeField]
        private AnimalVisualComponent _visual;

        [SerializeField]
        private AnimalLabelComponent _label;

        internal AnimalBodyComponent Body => _body;

        public event Action<IAnimalView> Collided;

        private void Awake()
        {
            if (_body == null || _visual == null || _label == null)
            {
                throw new InvalidOperationException($"{name}: body, visual or label component is not assigned.");
            }
        }

        private void OnEnable() => _body.Collided += OnCollided;
        private void OnDisable() => _body.Collided -= OnCollided;

        public void Face(Vector2 direction) => _visual.Face(direction);
        public void SetHeight(float height) => _visual.SetHeight(height);
        public void SetSquash(float squash) => _visual.SetSquash(squash);

        public void ShowLabel(string text, float seconds) => _label.Show(text, seconds);

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate()
        {
            _label.Hide();
            gameObject.SetActive(false);
        }

        private void OnCollided(GameObject other)
        {
            if (other.TryGetComponent(out AnimalView otherView))
            {
                Collided?.Invoke(otherView);
            }
        }
    }
}
