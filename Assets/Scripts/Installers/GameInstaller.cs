using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CardController _cardPrefab;
    [SerializeField] private Transform _gridParent;
    [SerializeField] private GameMenuController _menuController;
    [SerializeField] private PairsCounterView _pairsCounterView;

    public override void InstallBindings()
    {
        Container.Bind<JsonImageLoader>().AsSingle();
        Container.Bind<MatchObserver>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();

        Container.Bind<GameMenuController>().FromInstance(_menuController).AsSingle();
        Container.Bind<PairsCounterView>().FromInstance(_pairsCounterView).AsSingle();

        Container.BindFactory<int, Sprite, Sprite, CardController, CardFactory>()
            .FromComponentInNewPrefab(_cardPrefab)
            .UnderTransform(_gridParent);
    }
}
