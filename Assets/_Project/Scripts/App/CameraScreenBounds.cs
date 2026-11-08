using UnityEngine;
using Zenject;
using ZooWorld.Shared;
using Vector2 = System.Numerics.Vector2;

namespace ZooWorld.App
{
    internal sealed class CameraScreenBounds : IScreenBounds, ITickable
    {
        private readonly Camera _camera;
        private readonly ScreenSettings _settings;

        private float _aspect;

        public Vector2 Min { get; private set; }
        public Vector2 Max { get; private set; }

        public CameraScreenBounds(Camera camera, ScreenSettings settings)
        {
            _camera = camera;
            _settings = settings;
            Refresh();
        }

        public void Tick()
        {
            if (!Mathf.Approximately(_aspect, _camera.aspect))
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            _aspect = _camera.aspect;
            Vector3 min = _camera.ViewportToWorldPoint(Vector3.zero);
            Vector3 max = _camera.ViewportToWorldPoint(Vector3.one);
            float margin = _settings.Margin;
            Min = new Vector2(min.x + margin, min.z + margin);
            Max = new Vector2(max.x - margin, max.z - margin);
        }
    }
}
