using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimator : MonoBehaviour
{
    public RectTransform mainButton;
    public RectTransform[] icons;

    public float radius = 230f;
    public float openAngle = 120f;
    public float duration = 0.35f;

    bool isOpen = false;

    public void ToggleMenu()
    {
        if (!isOpen) StartCoroutine(OpenMenu());
        else StartCoroutine(CloseMenu());
    }

    IEnumerator OpenMenu()
    {
        isOpen = true;

        for (int i = 0; i < icons.Length; i++)
        {
            float angle = (openAngle / (icons.Length - 1)) * i;
            float rad = Mathf.Deg2Rad * (180 - angle);

            Vector2 targetPos = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

            StartCoroutine(AnimateMove(icons[i], Vector2.zero, targetPos));
            yield return new WaitForSeconds(0.03f);
        }
    }

    IEnumerator CloseMenu()
    {
        isOpen = false;

        for (int i = 0; i < icons.Length; i++)
        {
            StartCoroutine(AnimateMove(icons[i], icons[i].anchoredPosition, Vector2.zero));
            yield return new WaitForSeconds(0.03f);
        }
    }

    IEnumerator AnimateMove(RectTransform rect, Vector2 from, Vector2 to)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            float ease = Mathf.Sin(t * Mathf.PI * 0.5f);
            rect.anchoredPosition = Vector2.Lerp(from, to, ease);
            yield return null;
        }
    }
}

