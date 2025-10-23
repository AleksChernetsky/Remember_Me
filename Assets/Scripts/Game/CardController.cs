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
    private MatchObserver _matchObserver;

    public int Id { get; private set; }

    [Inject]
    public void Construct(int id, Sprite front, Sprite back, MatchObserver matchObserver)
    {
        Id = id;
        _front = front;
        _back = back;
        _image.sprite = _back;
        _flipped = false;
        _matchObserver = matchObserver;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_matchObserver.IsChecking && !_flipped)
        {
            Flip();
        }
    }

    public void Flip()
    {
        _flipSequence?.Kill();
        _flipSequence = DOTween.Sequence();

        float halfFlipTime = 0.15f;
        float scaleUp = 1.2f;
        Sprite targetSprite = _flipped ? _back : _front;
        float endScaleX = _flipped ? 1f : -1f;

        _flipped = true;

        _flipSequence.Append(transform.DOScale(new Vector3(0, scaleUp, 1), halfFlipTime));
        _flipSequence.AppendCallback(() => _image.sprite = targetSprite);
        _flipSequence.Append(transform.DOScale(new Vector3(endScaleX, 1f, 1f), halfFlipTime));

        _matchObserver.OnCardFlipped(this);
    }
    public void ResetFlags()
    {
        _flipped = false;
    }
    public void DestroyIfMatched()
    {
        Destroy(_image);
        _flipSequence?.Kill();
    }
}