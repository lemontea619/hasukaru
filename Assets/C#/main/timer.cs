using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // ★追加：シーン切り替えに必要

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public EnvironmentController envController; // meterをドラッグ
    public float totalTime = 60f;
    public string resultSceneName = "ResultScene"; // ★結果シーンの正確な名前

    private float currentTime;
    private bool isRunning = false;

    void Start()
    {
        currentTime = totalTime;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                FinishGame();
            }
            UpdateTimerDisplay();
        }
    }

    void FinishGame()
    {
        // 1. スコアをstatic変数に保存
        if (envController != null) envController.PrepareScores();

        // 2. 結果シーンへジャンプ！
        SceneManager.LoadScene(resultSceneName);
    }

    void UpdateTimerDisplay()
    {
        float displayTime = Mathf.Max(0.0f, currentTime);
        int minutes = Mathf.FloorToInt(displayTime / 60f);
        int seconds = Mathf.FloorToInt(displayTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}