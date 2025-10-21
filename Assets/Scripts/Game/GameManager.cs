using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class GameManager : IInitializable
{
    [Inject] private CardFactory _cardFactory;

    private string _jsonUrl = "https://drive.google.com/uc?export=download&id=1_Yv7b863IPvwxYZo1tx3Pu9rViTNKyvJ";
    private CardData _cardData;

    public async void Initialize()
    {
        await LoadCardsFromJson();

        var backCard = _cardData.cards.Find(c => c.id == 0);
        foreach (var card in _cardData.cards)
        {
            if (card.id == 0) continue;
            _cardFactory.Create(card, backCard.imageUrl);
        }
    }

    private async Task LoadCardsFromJson()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_jsonUrl))
        {
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                return;
            }

            string jsonText = request.downloadHandler.text;

            _cardData = JsonUtility.FromJson<CardData>(jsonText);
        }
    }
}
