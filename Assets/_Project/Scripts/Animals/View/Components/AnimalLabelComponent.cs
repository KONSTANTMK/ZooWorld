using System;
using R3;
using TMPro;
using UnityEngine;

namespace ZooWorld.Animals
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class AnimalLabelComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private readonly SerialDisposable _hideTimer = new();

        private void Awake()
        {
            if (_text == null)
            {
                throw new InvalidOperationException($"{name}: text is not assigned.");
            }
        }

        private void OnDestroy() => _hideTimer.Dispose();

        public void Show(string text, float seconds)
        {
            _text.text = text;
            gameObject.SetActive(true);
            _hideTimer.Disposable = Observable.Timer(TimeSpan.FromSeconds(seconds))
                .Subscribe(this, (_, label) => label.Hide());
        }

        public void Hide()
        {
            _hideTimer.Disposable = null;
            gameObject.SetActive(false);
        }
    }
}
