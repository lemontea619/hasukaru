using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public Sprite duckFinishSprite; 

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

        // タイマー設定に応じてカモの出現時間と画像を変更
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

            if (!duckCalled && currentTime <= duckAppearanceTime)
            {
                duckCalled = true;
                duckWalker?.StartWalking();
            }

            if (currentTime <= 0f && !isEnding)
            {
                currentTime = 0f;
                isEnding = true;
                StartCoroutine(FinishSequence());
            }
            UpdateTimerDisplay();
        }
    }

    // ★終了演出：Zennの記事のスクリプトを呼び出す
    IEnumerator FinishSequence()
    {
        isRunning = false;

        // 1. カモの終了画像を表示
        if (duckWalker != null)
        {
            duckWalker.AppearAtEnd(duckFinishSprite);
        }

        // 2秒間、カモの姿を見せる
        yield return new WaitForSeconds(2.0f);

        // 2. フェードスクリプト（FadeSceneLoader）を探して実行
        FadeSceneLoader loader = FindObjectOfType<FadeSceneLoader>();
        
        if (loader != null)
        {
            // スコアを保存する準備
            if (envController != null) envController.PrepareScores();
            
            // Zennの記事の方式でフェードとシーン遷移を開始
            loader.CallCoroutine(); 
        }
        else
        {
            // 万が一スクリプトが見つからない場合は直接遷移
            Debug.LogWarning("FadeSceneLoaderが見つかりません。直接遷移します。");
            if (envController != null) envController.PrepareScores();
            SceneManager.LoadScene(resultSceneName);
        }
    }

    void UpdateTimerDisplay()
    {
        float displayTime = Mathf.Max(0.0f, currentTime);
        int minutes = Mathf.FloorToInt(displayTime / 60f);
        int seconds = Mathf.FloorToInt(displayTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}