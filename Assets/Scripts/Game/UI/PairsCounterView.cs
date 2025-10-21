using TMPro;
using UnityEngine;

public class PairsCounterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;

    private int _found;
    private int _total;

    public void SetTotalPairs(int total)
    {
        _total = total;
        UpdateText();
    }

    public void IncrementFound()
    {
        _found++;
        UpdateText();
    }

    private void UpdateText()
    {
        _counterText.text = $"{_found}/{_total}";
    }
}
