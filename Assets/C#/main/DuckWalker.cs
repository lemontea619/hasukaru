using UnityEngine;

public class DuckWalker : MonoBehaviour
{
    [Header("アニメーション・移動設定")]
    public Sprite sprite1;
    public Sprite sprite2;
    public float animInterval = 0.2f;
    public float walkSpeed = 2.0f;
    public float startX = 10f;
    public float endX = -10f;

    [Header("音の設定")]
    public AudioClip walkSound; 
    public AudioClip finishSound; // 終了時の音
    private AudioSource myAudioSource;

    private SpriteRenderer spriteRenderer;
    private float animTimer;
    private bool isWalking = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
        if (myAudioSource == null) myAudioSource = gameObject.AddComponent<AudioSource>();
        
        myAudioSource.playOnAwake = false;
        gameObject.SetActive(false);
    }

    // 横歩き用のスプライト設定（既存）
    public void SetSprites(Sprite s1, Sprite s2)
    {
        sprite1 = s1;
        sprite2 = s2;
        if (spriteRenderer != null) spriteRenderer.sprite = sprite1;
    }

    public void StartWalking()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        isWalking = true;
        myAudioSource.clip = walkSound;
        myAudioSource.loop = false; 
        myAudioSource.Play();
    }

    // ★修正：終了時に新しい画像（finishSprite）を表示して下から出す
    public void AppearAtEnd(Sprite finishSprite)
    {
        isWalking = false; 
        myAudioSource.Stop();

        // 1. 画像を新しいものに切り替える
        if (spriteRenderer != null && finishSprite != null)
        {
            spriteRenderer.sprite = finishSprite;
        }

        // 2. 位置を画面下中央にセット
        transform.position = new Vector3(0, -6f, 0); 
        gameObject.SetActive(true);

        // 3. 上に表示させる（数値はカメラ設定に合わせて調整してください）
        transform.position = new Vector3(0, -3.5f, 0); 

        // 4. 音を鳴らす
        if (finishSound != null)
        {
            myAudioSource.PlayOneShot(finishSound);
        }
    }

    void Update()
    {
        if (!isWalking) return;

        transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);

        animTimer += Time.deltaTime;
        if (animTimer >= animInterval)
        {
            animTimer = 0f;
            spriteRenderer.sprite = (spriteRenderer.sprite == sprite1) ? sprite2 : sprite1;
        }

        if (transform.position.x < endX)
        {
            myAudioSource.Stop();
            gameObject.SetActive(false);
            isWalking = false;
        }
    }
}