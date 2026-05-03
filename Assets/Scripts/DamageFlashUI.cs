using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlashUI : MonoBehaviour
{
    [Header("UI Reference")]
    public Image damageOverlay;

    [Header("Flash Settings")]
    public float fadeInTime = 0.1f;
    public float holdTime = 0.2f;
    public float fadeOutTime = 0.7f;

    [Range(0f, 1f)]
    public float maxAlpha = 0.2f;

    private Coroutine flashRoutine;
    private Color baseColor;

    void Start()
    {
        if (damageOverlay != null)
        {
            baseColor = damageOverlay.color;
            SetAlpha(0f);
        }
    }

    public void TriggerDamageFlash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        yield return StartCoroutine(Fade(0f, maxAlpha, fadeInTime));
        yield return new WaitForSeconds(holdTime);
        yield return StartCoroutine(Fade(maxAlpha, 0f, fadeOutTime));
    }

    #region
    private IEnumerator Fade(float start, float end, float duration)
    {
        float elapsed = 0f;
        SetAlpha(start);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, elapsed / duration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(end);
    }

    private void SetAlpha(float alpha)
    {
        if (damageOverlay == null) return;

        Color c = baseColor;
        c.a = alpha;
        damageOverlay.color = c;
    }
    #endregion
}