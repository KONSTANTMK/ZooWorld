using System;
using UnityEngine;
using Zenject;
using ZooWorld.Animals;
using ZooWorld.Shared;
using ZooWorld.UI;

namespace ZooWorld.App
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private ScreenSettings _screenSettings;

        [SerializeField]
        private SpawnTable _spawnTable;

        [SerializeField]
        private SpawnSettings _spawnSettings;

        [SerializeField]
        private InteractionTable _interactionTable;

        [SerializeField]
        private Transform _animalsRoot;

        [SerializeField]
        private AnimalLabel _fedLabel;

        [SerializeField]
        private DeathCounterView _deathCounterView;

        public override void InstallBindings()
        {
            EnsureAssigned();
            BindShared();
            InstallFeatures();
            Container.BindInterfacesTo<GameController>().AsSingle();
        }

        private void EnsureAssigned()
        {
            Require(_camera, nameof(_camera));
            Require(_screenSettings, nameof(_screenSettings));
            Require(_spawnTable, nameof(_spawnTable));
            Require(_spawnSettings, nameof(_spawnSettings));
            Require(_interactionTable, nameof(_interactionTable));
            Require(_animalsRoot, nameof(_animalsRoot));
            Require(_fedLabel, nameof(_fedLabel));
            Require(_deathCounterView, nameof(_deathCounterView));
        }

        private void BindShared()
        {
            Container.BindInterfacesTo<UnityRandom>().AsSingle();
            Container.BindInterfacesTo<CameraScreenBounds>().AsSingle().WithArguments(_camera, _screenSettings);
        }

        private void InstallFeatures()
        {
            AnimalsInstaller.Install(
                Container,
                _spawnTable,
                _spawnSettings,
                _interactionTable,
                _animalsRoot,
                _fedLabel);
            UIInstaller.Install(Container, _deathCounterView);
        }

        private void Require(UnityEngine.Object value, string fieldName)
        {
            if (value == null)
            {
                throw new InvalidOperationException($"{name}: field {fieldName} is not assigned.");
            }
        }
    }
}
