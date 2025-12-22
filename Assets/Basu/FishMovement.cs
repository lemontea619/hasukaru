using UnityEngine;

public class FishMovement : MonoBehaviour
{
    // === Inspectorで設定する変数 ===
    public float speed = 2.0f;          // 魚の移動速度
    public float boundaryX = 9.0f;      // 画面端のX座標 (Unityワールド座標の目安)
    
    // === 内部変数 ===
    private bool movingRight = true;    // 現在右に移動しているか
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // どちらの方向から生成されても動けるように、初期方向を設定
        // 初期位置が画面左側なら右へ、右側なら左へ動かすなど、必要に応じて調整可能
        // 今回はシンプルに、初期設定のまま右向きからスタートとします
    }

    void Update()
    {
        // 1. 移動の処理
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * speed * Time.deltaTime);

        // 2. 端のチェックと反転処理
        // 現在のX座標を取得
        float currentX = transform.position.x;

        if (movingRight)
        {
            // 右端に到達したかチェック
            if (currentX >= boundaryX)
            {
                TurnAround();
            }
        }
        else // movingLeft
        {
            // 左端に到達したかチェック
            if (currentX <= -boundaryX)
            {
                TurnAround();
            }
        }
    }

    /// <summary>
    /// 方向とスプライトを反転させる
    /// </summary>
    void TurnAround()
    {
        // 進行方向を反転
        movingRight = !movingRight;

        // 画像を反転（スプライトレンダラーのFlip Xを使用）
        // 右向き (movingRight = true) の場合は反転させない (false)
        // 左向き (movingRight = false) の場合は反転させる (true)
        spriteRenderer.flipX = !movingRight;
        
        // 魚が境界線付近で震えるのを防ぐため、位置を少しだけ境界線の内側に戻す
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundaryX + 0.1f, boundaryX - 0.1f), 
                                         transform.position.y, 
                                         transform.position.z);
    }
    
    /// <summary>
    /// 魚がクリックされたときに呼び出される
    /// </summary>
    void OnMouseDown()
    {
        Debug.Log("魚がクリックされました。");
        
        // ★ 環境メーターへの連携（任意）★
        // 環境メーターを増やす、または減らす処理を入れる場合はここに追加します。
        // 例: FindObjectOfType<EnvironmentController>()?.AddEnvironmentValue();

        // この魚のゲームオブジェクトをシーンから削除する
        Destroy(gameObject);
    }
}
