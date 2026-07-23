using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonCustomization : MonoBehaviour
{
    private Button b;
    private RectTransform rectTransform;

    [SerializeField] private float popScale = 1.1f; // scale up to 110%
    [SerializeField] private float popDuration = 0.1f; // quick pop

    private void OnEnable()
    {
        b = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        b.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        b.onClick.RemoveAllListeners();
    }

    private void OnButtonClick()
    {
        PlayTap();
        StartCoroutine(PopEffect());
    }

    private void PlayTap()
    {
        AudioPlayer.instance.Play("TAP");
    }

    private IEnumerator PopEffect()
    {
        Vector3 originalScale = rectTransform.localScale;
        Vector3 targetScale = originalScale * popScale;

        // Scale up
        float t = 0f;
        while (t < popDuration)
        {
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, t / popDuration);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        rectTransform.localScale = targetScale;

        // Scale back down
        t = 0f;
        while (t < popDuration)
        {
            rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, t / popDuration);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        rectTransform.localScale = originalScale;
    }
}