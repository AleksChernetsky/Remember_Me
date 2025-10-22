using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoadScreenService
{
    private readonly CanvasGroup _canvas;
    private readonly TextMeshProUGUI _loadingText;
    private float _fadeDuration;

    public LoadScreenService(CanvasGroup canvas, float fadeDuration = 0.5f)
    {
        _canvas = canvas;
        _loadingText = _canvas.GetComponentInChildren<TextMeshProUGUI>();
        _fadeDuration = fadeDuration;
    }
    public async Task Show()
    {
        _canvas.gameObject.SetActive(true);
        _ = LoadingTextAnim();
        await Fade(0f, 1f);
    }

    public async Task Hide()
    {
        await Fade(1f, 0f);
        _canvas.gameObject.SetActive(false);
    }

    private async Task Fade(float from, float to)
    {
        float elapsed = 0f;
        _canvas.alpha = from;

        while (elapsed < _fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / _fadeDuration);
            _canvas.alpha = Mathf.Lerp(from, to, t);
            await Task.Yield();
        }

        _canvas.alpha = to;
    }

    private async Task LoadingTextAnim()
    {
        string baseText = "Loading";
        int dotCount = 0;
        while (_canvas.gameObject.activeSelf)
        {
            _loadingText.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4;
            await Task.Delay(500);
        }
    }
}