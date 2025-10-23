using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MatchObserver
{
    private readonly GameManager _gameManager;
    private readonly PairsCounterView _pairsCounterView;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadScreenService _loadScreenService;

    private readonly List<CardController> _flippedCards = new();

    public bool IsChecking { get; private set; } = false;

    public MatchObserver(GameManager gameManager, PairsCounterView pairsCounterView, SceneLoader sceneLoader, LoadScreenService loadScreenService)
    {
        _gameManager = gameManager;
        _pairsCounterView = pairsCounterView;
        _sceneLoader = sceneLoader;
        _loadScreenService = loadScreenService;
    }

    public void OnCardFlipped(CardController card)
    {
        if (IsChecking)
            return;

        _flippedCards.Add(card);
        if (_flippedCards.Count < 2)
            return;

        IsChecking = true;
        _ = CheckForMatchAsync();
    }

    private async Task CheckForMatchAsync()
    {
        try
        {
            var first = _flippedCards[0];
            var second = _flippedCards[1];

            if (first.Id == second.Id)
            {
                await Task.Delay(1000);
                _pairsCounterView.IncreaseScore();
                _gameManager.DecreaseTotalPairs();
                first.DestroyIfMatched();
                second.DestroyIfMatched();

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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            IsChecking = false;
        }
    }
}
