using UnityEngine;
using UnityEngine.UI; // Imageコンポーネントを使うために必要

public class EnvironmentController : MonoBehaviour
{
    // === インスペクターで設定する変数 ===
    // 埋まっていく前景のImageをアタッチ
    public Image fillImage; 
    
    [Header("環境値設定")]
    // 1回のクリックで増える量 (5% = 0.05f)
    public float increaseAmount = 0.05f; 

    // === 内部変数 ===
    // 現在の環境値（0.0fから1.0fの間で管理）
    private float currentEnvironmentValue = 0.0f; 

    void Start()
    {
        // ゲーム開始時の初期表示を0%に設定
        fillImage.fillAmount = currentEnvironmentValue;
    }

    void Update()
    {
        // マウスの左クリック（または画面タップ）を検出
        if (Input.GetMouseButtonDown(0))
        {
            // 環境値を増加させる
            AddEnvironmentValue();
        }
    }

    /// <summary>
    /// 環境値を増加させ、メーターを更新する
    /// </summary>
    public void AddEnvironmentValue()
    {
        // 現在値に増加量を足す
        currentEnvironmentValue += increaseAmount;

        // 値を0.0fから1.0fの間に制限する (Mathf.Clamp)
        // 1.0f は 100% を意味する
        currentEnvironmentValue = Mathf.Clamp(currentEnvironmentValue, 0.0f, 1.0f);

        // UIのFill Amountを更新
        fillImage.fillAmount = currentEnvironmentValue;

        // デバッグログで現在の値を表示
        Debug.Log("現在の環境値: " + (currentEnvironmentValue * 100).ToString("F0") + " %");

        if (currentEnvironmentValue >= 1.0f)
        {
            Debug.Log("環境値が最大 (100%) に達しました！");
            // 例: ゲームクリア処理などをここに追加
        }
    }
}