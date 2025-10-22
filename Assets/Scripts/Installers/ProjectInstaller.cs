using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject _loadScreenPrefab;

    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().AsSingle();
        Container.Bind<JsonImageLoader>().AsSingle();

        Container.Bind<CanvasGroup>()
            .FromComponentInNewPrefab(_loadScreenPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<LoadScreenService>().AsSingle();
    }
}