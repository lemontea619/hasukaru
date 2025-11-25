using UnityEngine;
using TMPro; // TextMeshProを使用

public class Timer : MonoBehaviour
{
    // === インスペクターで設定する変数 ===
    public TMP_Text timerText; // 時間を表示するTextMeshProテキスト

    // タイマーの合計時間（3分 = 180秒）
    public float totalTime = 180f;

    // === 内部変数 ===
    private float currentTime;
    private bool isRunning = false;

    void Start()
    {
        // 1. 初期時間を設定
        currentTime = totalTime;
        
        // 2. タイマーをすぐにスタートさせる
        isRunning = true;
        
        // 3. 初期表示を更新
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isRunning)
        {
            // 時間を減らす
            currentTime -= Time.deltaTime;

            // 0秒以下になったらタイマー停止
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                Debug.Log("Time's Up! 3分経過しました。");
                // 終了時の処理をここに追加
            }

            // 表示を更新
            UpdateTimerDisplay();
        }
    }

    // タイマー表示を更新する関数
    void UpdateTimerDisplay()
    {
        // currentTimeが負にならないようにMathf.Maxで0.0fを下限に設定
        float displayTime = Mathf.Max(0.0f, currentTime);
        
        // 分と秒を計算
        int minutes = Mathf.FloorToInt(displayTime / 60f);
        int seconds = Mathf.FloorToInt(displayTime % 60f);

        // "00:00"形式で表示を整形して更新
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}