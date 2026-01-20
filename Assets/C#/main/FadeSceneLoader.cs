using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeSceneLoader : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1.0f;
    public string nextSceneName = "result"; // 遷移先のシーン名

    // Timer.csからこれを呼び出します
    public void CallCoroutine()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        // パネルを有効化（最初はInspectorでチェックを外しておいてもOK）
        fadePanel.gameObject.SetActive(true);
        
        float elapsedTime = 0.0f;
        Color startColor = fadePanel.color;
        // アルファ値を0にしてからスタート
        startColor.a = 0f;
        fadePanel.color = startColor;

        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        fadePanel.color = endColor;

        // シーン遷移（Timer.cs側で指定したシーン名があればそちらを優先）
        SceneManager.LoadScene(nextSceneName);
    }
}