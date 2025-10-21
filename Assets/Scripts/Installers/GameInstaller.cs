using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CardController cardPrefab;
    [SerializeField] private Transform gridParent;

    public override void InstallBindings()
    {
        Container.Bind<JsonImageLoader>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();

        Container.BindFactory<CardInfo, string, CardController, CardFactory>()
            .FromComponentInNewPrefab(cardPrefab)
            .UnderTransform(gridParent);
    }
}
