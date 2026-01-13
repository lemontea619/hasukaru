using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections; // コルーチンに必要

public class Timer : MonoBehaviour
{
    [Header("参照設定")]
    public TMP_Text timerText;
    public EnvironmentController envController;
    public DuckWalker duckWalker;

    [Header("カモの画像設定")]
    public Sprite duck1min_A;
    public Sprite duck1min_B;
    public Sprite duck3min_A;
    public Sprite duck3min_B;
    public Sprite duckFinishSprite; // 終了用の新しいカモ画像

    [Header("タイマー設定")]
    public float totalTime = 60f;
    public string resultSceneName = "ResultScene";

    private float currentTime;
    private float duckAppearanceTime;
    private bool isRunning = false;
    private bool duckCalled = false;
    private bool isEnding = false;

    void Start()
    {
        currentTime = totalTime;
        isRunning = true;

        // 合計時間によって出現タイミングと画像を切り替え
        if (totalTime >= 180f) 
        {
            duckAppearanceTime = 60f;
            if (duckWalker != null) duckWalker.SetSprites(duck3min_A, duck3min_B);
        }
        else 
        {
            duckAppearanceTime = 30f;
            if (duckWalker != null) duckWalker.SetSprites(duck1min_A, duck1min_B);
        }
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;

            // 30秒/1分でのカモ出現チェック
            if (!duckCalled && currentTime <= duckAppearanceTime)
            {
                duckCalled = true;
                duckWalker?.StartWalking();
            }

            // タイムアップ判定
            if (currentTime <= 0f && !isEnding)
            {
                currentTime = 0f;
                isEnding = true;
                StartCoroutine(FinishSequence());
            }
            UpdateTimerDisplay();
        }
    }

    // ★終了演出シーケンス
    IEnumerator FinishSequence()
    {
        // 1. カモに終了用画像を渡して、下から出す
        if (duckWalker != null)
        {
            duckWalker.AppearAtEnd(duckFinishSprite);
        }

        // 2. ★ここを 1.0f から 2.0f に変更しました
        yield return new WaitForSeconds(2.0f);

        // 3. スコアを保存してシーン遷移
        if (envController != null) envController.PrepareScores();
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