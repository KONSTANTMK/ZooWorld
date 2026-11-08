using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using ZooWorld.Animals;

namespace ZooWorld.UI
{
    public sealed class DeathCounterView : MonoBehaviour, IDeathCounterView
    {
        [SerializeField]
        private List<Entry> _entries = new();

        public IReadOnlyList<AnimalRole> GetRoles() => _entries.Select(entry => entry.Role).ToArray();

        public void SetCount(AnimalRole role, int count)
        {
            foreach (Entry entry in _entries)
            {
                if (entry.Role == role)
                {
                    entry.Label.text = $"{entry.Title}: {count}";
                    return;
                }
            }
        }

        [Serializable]
        private sealed class Entry
        {
            [SerializeField]
            private AnimalRole _role;
            public AnimalRole Role => _role;

            [SerializeField]
            private string _title;
            public string Title => _title;

            [SerializeField]
            private TMP_Text _label;
            public TMP_Text Label => _label;
        }
    }
}
