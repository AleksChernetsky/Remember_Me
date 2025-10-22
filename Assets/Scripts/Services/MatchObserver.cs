using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MatchObserver
{
    private readonly GameManager _gameManager;
    private readonly PairsCounterView _pairsCounterView;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadScreenService _loadScreenService;

    private readonly List<CardController> _flippedCards = new();

    public MatchObserver(GameManager gameManager, PairsCounterView pairsCounterView, SceneLoader sceneLoader, LoadScreenService loadScreenService)
    {
        _gameManager = gameManager;
        _pairsCounterView = pairsCounterView;
        _sceneLoader = sceneLoader;
        _loadScreenService = loadScreenService;
    }

    public async void OnCardFlipped(CardController card)
    {
        if (_flippedCards.Contains(card))
            return;

        _flippedCards.Add(card);

        if (_flippedCards.Count < 2)
            return;

        var first = _flippedCards[0];
        var second = _flippedCards[1];

        if (first.Id == second.Id)
        {
            await Task.Delay(1000);

            _pairsCounterView.IncreaseScore();
            _gameManager.DecreaseTotalPairs();

            Object.Destroy(first.gameObject);
            Object.Destroy(second.gameObject);

            if (_gameManager.GetTotalPairs() == 0)
            {
                await Task.Delay(250);
                await _loadScreenService.Show();
                await _sceneLoader.ReloadCurrentScene();
            }
        }
        else
        {
            await Task.Delay(750);
            first.Flip();
            second.Flip();

            first.ResetFlags();
            second.ResetFlags();
        }

        _flippedCards.Clear();
    }
}
