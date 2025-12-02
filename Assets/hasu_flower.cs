using UnityEngine;

public class hasu_flower : MonoBehaviour
{
    // === 新しく追加する変数 ===
    public Sprite deadHasuSprite;
    public float wiltingTime = 30f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // SpriteRendererコンポーネントを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 30秒後に "Wilting" 関数を呼び出す
        // ハスが生成された瞬間（Startが呼ばれた瞬間）からカウントダウン開始
        Invoke("Wilting", wiltingTime); 
    }

    // OnMouseDown()関数はそのまま（クリックで消える機能）
    void OnMouseDown()
    {
        Debug.Log("ハスがクリックされました。");
        
        // 30秒後のInvokeを取り消し、枯れる前に消えるようにする
        CancelInvoke("Wilting"); 
        
        EnvironmentController ec = FindObjectOfType<EnvironmentController>();
        if (ec != null)
        {
            ec.AddEnvironmentValue();
        }

        Destroy(gameObject); 
    }

    /// <summary>
    /// 30秒後に呼び出され、ハスのスプライトを枯れたものに切り替える
    /// </summary>
    void Wilting()
    {
        if (spriteRenderer != null)
        {
            // スプライトを枯れたハスの画像に置き換える
            spriteRenderer.sprite = deadHasuSprite;
            Debug.Log("ハスが枯れました。");
            
            // ★ 枯れた後の処理を追加可能 ★
            // 例: コライダーを削除してクリックできなくする、環境値を減らすなど。
            // GetComponent<Collider2D>().enabled = false;
        }
    }
}