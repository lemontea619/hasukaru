using UnityEngine;

public class hasu_flower : MonoBehaviour
{
    [Header("枯死設定")]
    public Sprite deadHasuSprite; // 枯れた時の画像
    public float wiltingTime = 30f; // 枯れるまでの時間
    
    [HideInInspector]
    public bool isWithered = false; // 枯れているかどうかのフラグ
    
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 1. 環境コントローラーに自分を登録
        EnvironmentController ec = FindObjectOfType<EnvironmentController>();
        if (ec != null)
        {
            ec.RegisterLotus(this);
        }

        // 2. 指定秒数後に枯れる処理を予約
        Invoke("Wilting", wiltingTime); 
    }

    void Wilting()
    {
        if (spriteRenderer != null && deadHasuSprite != null)
        {
            isWithered = true; // 枯れたフラグを立てる（これでEnvironmentController側で5pt消滅と判定される）
            spriteRenderer.sprite = deadHasuSprite;
            Debug.Log("ハスが枯れました。ポイントが減少します。");
        }
    }

    // 鎌などで直接消された時の処理
    void OnDestroy()
    {
        // 予約していたWiltingを解除
        CancelInvoke("Wilting");
    }
}