using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class GameManager : IInitializable
{
    [Inject] private CardFactory _cardFactory;
    [Inject] private LoadScreenService _loadScreenService;
    [Inject] private JsonImageLoader _imageLoader;

    private string _jsonUrl = "https://drive.google.com/uc?export=download&id=1_Yv7b863IPvwxYZo1tx3Pu9rViTNKyvJ";
    private CardData _cardData;

    private int _totalPairs;

    public async void Initialize()
    {
        await LoadCardsFromJson();

        var backCard = _cardData.cards.Find(c => c.id == 0);
        var backSprite = await _imageLoader.LoadImageFromUrlAsync(backCard.imageUrl);

        var availableCards = _cardData.cards.Where(c => c.id != 0).OrderBy(_ => Random.value).ToList();
        var pairs = availableCards.Take(3).ToList();

        var frontSprites = new Dictionary<int, Sprite>();
        foreach (var card in pairs)
        {
            var sprite = await _imageLoader.LoadImageFromUrlAsync(card.imageUrl);
            frontSprites[card.id] = sprite;
        }

        var cardsToSpawn = new List<CardInfo>();
        foreach (var card in pairs)
        {
            cardsToSpawn.Add(card);
            cardsToSpawn.Add(card);
            _totalPairs++;
        }

        var shuffledCards = cardsToSpawn.OrderBy(_ => Random.value).ToList();

#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif

        foreach (var cardInfo in shuffledCards)
        {
            var front = frontSprites[cardInfo.id];
            _cardFactory.Create(cardInfo.id, front, backSprite);
        }

        await _loadScreenService.Hide();
    }

    public int GetTotalPairs()
    {
        return _totalPairs;
    }
    public void DecreaseTotalPairs()
    {
        _totalPairs--;
    }

    private async Task LoadCardsFromJson()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_jsonUrl))
        {
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load JSON: {request.error}");
                return;
            }

            string jsonText = request.downloadHandler.text;
            _cardData = JsonUtility.FromJson<CardData>(jsonText);
        }
    }
}
