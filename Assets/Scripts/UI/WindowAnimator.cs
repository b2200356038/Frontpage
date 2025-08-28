using UnityEngine;
using DG.Tweening;

public class WindowAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    public float openDuration = 0.3f;
    public float closeDuration = 0.2f;
    public float minimizeDuration = 0.25f;
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            
        originalScale = rectTransform.localScale;
    }
    
    public void PlayOpenAnimation()
    {
        originalPosition = rectTransform.localPosition;
        rectTransform.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;
        gameObject.SetActive(true);
        Sequence openSequence = DOTween.Sequence();
        openSequence.Append(rectTransform.DOScale(originalScale, openDuration).SetEase(Ease.InQuad));
        openSequence.Join(canvasGroup.DOFade(1f, openDuration));
    }
    
    public void PlayCloseAnimation(System.Action onComplete = null)
    {
        Sequence closeSequence = DOTween.Sequence();
        closeSequence.Append(rectTransform.DOScale(Vector3.zero, closeDuration).SetEase(Ease.InQuad));
        closeSequence.Join(canvasGroup.DOFade(0f, closeDuration));
        closeSequence.OnComplete(() => onComplete?.Invoke());
    }
    
    public void PlayMinimizeAnimation(Vector3 taskbarButtonPosition, System.Action onComplete = null)
    {
        originalPosition = rectTransform.localPosition;
        Sequence minimizeSequence = DOTween.Sequence();
        minimizeSequence.Append(rectTransform.DOLocalMove(taskbarButtonPosition, minimizeDuration).SetEase(Ease.InQuad));
        minimizeSequence.Join(rectTransform.DOScale(Vector3.zero, minimizeDuration).SetEase(Ease.InQuad));
        minimizeSequence.Join(canvasGroup.DOFade(0f, minimizeDuration));
        minimizeSequence.OnComplete(() => onComplete?.Invoke());
    }
    
    public void PlayRestoreAnimation()
    {
        gameObject.SetActive(true);
        Sequence restoreSequence = DOTween.Sequence();
        restoreSequence.Append(rectTransform.DOLocalMove(originalPosition, minimizeDuration).SetEase(Ease.InQuad));
        restoreSequence.Join(rectTransform.DOScale(originalScale, minimizeDuration).SetEase(Ease.InQuad));
        restoreSequence.Join(canvasGroup.DOFade(1f, minimizeDuration));
    }
}