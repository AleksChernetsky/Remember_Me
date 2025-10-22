using TMPro;
using UnityEngine;

public class PairsCounterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;

    private int _total;

    private void Start()
    {
        _total = 0;
        UpdateText();
    }

    public void IncreaseScore()
    {
        _total++;
        UpdateText();
    }

    private void UpdateText()
    {
        _counterText.text = $"{_total}";
    }
}
