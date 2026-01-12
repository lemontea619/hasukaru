using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [Header("画像設定")]
    public Sprite flyingSprite1; // 羽が上の画像
    public Sprite flyingSprite2; // 羽が下の画像
    public Sprite sittingSprite;  // 沼で浮いている時の画像
    public float flapSpeed = 0.2f; // 羽ばたきの速さ（秒）

    [Header("移動設定")]
    [HideInInspector] public Vector3 targetPosition;
    public float speed = 1.5f;
    public float wanderRange = 2.0f;
    
    private bool isFlyingAway = false;
    private bool hasArrived = false;
    private Vector3 baseAnchorPos;
    private float moveDirection = 1f;
    private SpriteRenderer spriteRenderer;
    
    // 羽ばたき制御用
    private float flapTimer;
    private int flapState = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }

    void Update()
    {
        if (isFlyingAway || !hasArrived)
        {
            // --- 飛んでいる時：羽ばたきアニメーション ---
            PlayFlapAnimation();

            // 移動処理
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            FlipSprite(targetPosition.x - transform.position.x);

            if (!isFlyingAway && Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                hasArrived = true;
                baseAnchorPos = transform.position;
            }
            else if (isFlyingAway && Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // --- 沼に到着後：静止画で左右往復 ---
            if (spriteRenderer.sprite != sittingSprite) spriteRenderer.sprite = sittingSprite;

            transform.position += Vector3.right * moveDirection * (speed * 0.5f) * Time.deltaTime;

            float distanceFromAnchor = transform.position.x - baseAnchorPos.x;
            if (Mathf.Abs(distanceFromAnchor) > wanderRange)
            {
                moveDirection *= -1f;
            }
            FlipSprite(moveDirection);
        }
    }

    // 2枚の画像を交互に切り替える処理
    void PlayFlapAnimation()
    {
        flapTimer += Time.deltaTime;
        if (flapTimer >= flapSpeed)
        {
            flapTimer = 0;
            flapState = (flapState + 1) % 2;
            spriteRenderer.sprite = (flapState == 0) ? flyingSprite1 : flyingSprite2;
        }
    }

    void FlipSprite(float direction)
    {
        if (spriteRenderer != null && direction != 0)
        {
            spriteRenderer.flipX = (direction < 0);
        }
    }

    public void FlyAway(Vector3 exitPos)
    {
        targetPosition = exitPos;
        isFlyingAway = true;
        hasArrived = false;
    }
}