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
    }
}