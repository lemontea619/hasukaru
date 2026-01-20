using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class OPSplitManager : MonoBehaviour
{
    [Header("ボタンを押した瞬間に消すもの（ロゴやボタン）")]
    public List<GameObject> objectsToHide; 
    public GameObject originalBackground; 

    [Header("扉演出（背景の半分ずつを入れた板）")]
    public GameObject doorCanvas;
    public RectTransform leftHalf;
    public RectTransform rightHalf;

    [Header("設定")]
    public float openDuration = 2.0f;

    // ボタンから呼び出す関数
    public void StartOpeningSequence(string targetSceneName)
    {
        StartCoroutine(ExecuteSequence(targetSceneName));
    }

    IEnumerator ExecuteSequence(string targetSceneName)
    {
        // --- 1. 安全チェック（Nullエラー対策） ---
        if (doorCanvas == null || leftHalf == null || rightHalf == null)
        {
            Debug.LogError("インスペクターの設定が足りません！");
            yield break;
        }

        // --- 2. シーンを「重ねて」読み込む ---
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone) { yield return null; }

        // --- 3. 時間を止めて演出開始 ---
        Time.timeScale = 0f;
        doorCanvas.SetActive(true);

        // ボタンやロゴを消す
        if (objectsToHide != null)
        {
            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null) obj.SetActive(false);
            }
        }
        if (originalBackground != null) originalBackground.SetActive(false);

        // タイトルのカメラをオフにする（奥のゲーム画面を出す）
        if (Camera.main != null) Camera.main.gameObject.SetActive(false);

        // --- 4. 扉を割る（Time.unscaledDeltaTimeを使用） ---
        float elapsed = 0f;
        float screenWidth = doorCanvas.GetComponent<RectTransform>().rect.width;
        Vector2 leftEnd = new Vector2(-screenWidth, 0);
        Vector2 rightEnd = new Vector2(screenWidth, 0);

        while (elapsed < openDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / openDuration);

            leftHalf.anchoredPosition = Vector2.Lerp(Vector2.zero, leftEnd, t);
            rightHalf.anchoredPosition = Vector2.Lerp(Vector2.zero, rightEnd, t);
            yield return null;
        }

        // --- 5. 仕上げ ---
        Time.timeScale = 1f; // 時間を動かす
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
} // ← この最後のカッコが重要です！