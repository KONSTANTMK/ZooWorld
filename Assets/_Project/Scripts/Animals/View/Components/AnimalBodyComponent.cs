using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace ZooWorld.Animals
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class AnimalBodyComponent : MonoBehaviour, IAnimalBodyComponent
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        public Vector2 Position
        {
            get
            {
                Vector3 position = _rigidbody.position;
                return new Vector2(position.x, position.z);
            }
        }

        public event Action<GameObject> Collided;

        private void Awake()
        {
            if (_rigidbody == null)
            {
                throw new InvalidOperationException($"{name}: rigidbody is not assigned.");
            }
        }

        private void OnEnable() => EnableCollisions();

        private void OnCollisionEnter(Collision collision)
        {
            Rigidbody other = collision.rigidbody;
            if (other != null)
            {
                Collided?.Invoke(other.gameObject);
            }
        }

        internal void Place(Vector2 position)
        {
            var worldPosition = new Vector3(position.X, transform.position.y, position.Y);
            transform.position = worldPosition;
            _rigidbody.position = worldPosition;
            _rigidbody.linearVelocity = Vector3.zero;
        }

        public void SetVelocity(Vector2 velocity) => _rigidbody.linearVelocity = ToWorld(velocity);

        public void AddImpulse(Vector2 impulse) => _rigidbody.AddForce(ToWorld(impulse), ForceMode.VelocityChange);

        public void DisableCollisions() => _rigidbody.detectCollisions = false;

        private void EnableCollisions() => _rigidbody.detectCollisions = true;

        internal static Vector3 ToWorld(Vector2 value) => new(value.X, 0f, value.Y);
    }
}
