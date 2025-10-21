using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;

    private Sprite _back;
    private Sprite _front;
    private bool _flipped;
    private Sequence _flipSequence;


    [Inject] private JsonImageLoader _imageLoader;

    [Inject]
    public async void Construct(CardInfo data, string backUrl)
    {
        _front = await _imageLoader.LoadImageFromUrlAsync(data.imageUrl);
        _back = await _imageLoader.LoadImageFromUrlAsync(backUrl);
        _image.sprite = _back;
        _flipped = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Flip();
    }

    private void Flip()
    {
        _flipSequence?.Kill();

        _flipSequence = DOTween.Sequence();

        float flipTime = 0.15f;
        float scaleUp = 1.15f;

        Sprite targetSprite = _flipped ? _back : _front;
        Vector3 endRotation = _flipped ? Vector3.zero : new Vector3(0, 180, 0);

        _flipSequence.Append(transform.DOScale(scaleUp, 0.1f));
        _flipSequence.Append(transform.DORotate(new Vector3(0, 90, 0), flipTime)).OnComplete(() => _image.sprite = targetSprite);
        _flipSequence.Append(transform.DORotate(endRotation, flipTime));
        _flipSequence.Append(transform.DOScale(1f, 0.1f));

        _flipped = !_flipped;
    }
}