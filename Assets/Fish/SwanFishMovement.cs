using UnityEngine;

// クラス名はファイル名と一致させる必要があります
public class SwanFishMovement : MonoBehaviour
{
    // === Inspectorで設定する変数 ===
    public float speed = 2.0f;          // 魚の移動速度
    public float boundaryX = 9.0f;      // 画面端のX座標
    
    // === 内部変数 ===
    // 最初から左へ動かすために false に設定
    private bool movingRight = false;    
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 【向きの修正】
        // 元の画像が左向きなので、movingRightがfalse（左移動）の時はflipXをfalseにします
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = movingRight; 
        }
    }

    void Update()
    {
        // 1. 移動の処理
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * speed * Time.deltaTime);

        // 2. 端のチェックと反転処理
        float currentX = transform.position.x;

        if (movingRight)
        {
            if (currentX >= boundaryX)
            {
                TurnAround();
            }
        }
        else // 左へ移動中
        {
            if (currentX <= -boundaryX)
            {
                TurnAround();
            }
        }
    }

    void TurnAround()
    {
        movingRight = !movingRight;

        // スプライトを反転
        // 右に動く(true)なら反転(true)、左に動く(false)なら反転なし(false)
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = movingRight;
        }
        
        // 境界線でのガタつき防止
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -boundaryX + 0.1f, boundaryX - 0.1f), 
            transform.position.y, 
            transform.position.z
        );
    }
    
    // この関数はクラス内に1つだけにします
    void OnMouseDown()
    {
        Debug.Log("魚がクリックされました。");
        Destroy(gameObject);
    }
}