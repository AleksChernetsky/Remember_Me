using Zenject;

public class MenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().AsSingle();
        Container.Bind<JsonImageLoader>().AsSingle();

        Container.Bind<MenuController>().FromComponentInHierarchy().AsSingle();
    }
}