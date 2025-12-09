using UnityEngine;
using UnityEngine.UI;

public class EnvironmentController : MonoBehaviour
{
    // ... (既存の変数)
    public Image fillImage; 
    public float increaseAmount = 0.05f; 
    private float currentEnvironmentValue = 0.0f; 

    // === 新しく追加する変数 ===
    [Header("地面の切り替え設定")]
    public SpriteRenderer groundSpriteRenderer; // 地面のスプライトレンダラー
    public Sprite roughGroundSprite;           // 荒れた地面のSprite
    public Sprite cleanGroundSprite;           // 綺麗な地面のSprite
    public float cleanGroundThreshold = 0.9f;  // 綺麗になるしきい値 (90% = 0.9f)
    private bool isGroundClean = false;        // 地面が綺麗になったかどうかのフラグ
    // ===========================

// === 沼の色設定 ===
[Header("沼の色設定")]
public SpriteRenderer swampRenderer; 
// 荒れた沼の色（例: 70%の透明度で定義）
// Color(R, G, B, A)
public Color roughSwampColor = new Color(0.48f, 0.58f, 0.47f, 0.5f); // A=0.7f (不透明度70%)
// 綺麗な沼の色（例: 完全に不透明で定義）
public Color cleanSwampColor = new Color(0.62f, 0.86f, 0.96f, 0.5f); // A=1.0f (不透明度100%)
// 90%で変化が完了するようにしきい値を設定
public float colorTransitionThreshold = 0.9f;

    void Start()
    {
        fillImage.fillAmount = currentEnvironmentValue;
        
        // ゲーム開始時は荒れた地面にする
        if (groundSpriteRenderer != null && roughGroundSprite != null)
        {
            groundSpriteRenderer.sprite = roughGroundSprite;
        }
    }
    
    public void AddEnvironmentValue()
    {
        currentEnvironmentValue += increaseAmount;
        currentEnvironmentValue = Mathf.Clamp(currentEnvironmentValue, 0.0f, 1.0f);

        fillImage.fillAmount = currentEnvironmentValue;
        Debug.Log("現在の環境値: " + (currentEnvironmentValue * 100).ToString("F0") + " %");

        if (currentEnvironmentValue >= 1.0f)
        {
            Debug.Log("環境値が最大 (100%) に達しました！");
        }

        // === 新しく追加するロジック ===
        // 環境値がしきい値を超えて、まだ地面が綺麗になっていない場合
        if (currentEnvironmentValue >= cleanGroundThreshold && !isGroundClean)
        {
            if (groundSpriteRenderer != null && cleanGroundSprite != null)
            {
                groundSpriteRenderer.sprite = cleanGroundSprite; // 綺麗な地面に切り替える
                isGroundClean = true;                             // フラグを立てて一度だけ切り替わるようにする
                Debug.Log("地面が綺麗になりました！");
            }
        }
        // ===========================
        
        // --- ★ 沼のグラデーションロジック ★ ---
    if (swampRenderer != null)
    {
        // 1. 環境値を0から0.9fの間に正規化する (0.9fが最大値になるように調整)
        // LerpRateは 0% (0) から 90% (1) の範囲で変化
        float lerpRate = currentEnvironmentValue / colorTransitionThreshold; 
        
        // 値が 0.9fを超えないようにクランプする (色変化の完了)
        lerpRate = Mathf.Clamp01(lerpRate); 

        // 2. Color.Lerp() を使って色を補間する
        // lerpRateが0のとき roughSwampColor、1のとき cleanSwampColor になる
        swampRenderer.color = Color.Lerp(roughSwampColor, cleanSwampColor, lerpRate);
    }
    // ----------------------------------------
    }
}