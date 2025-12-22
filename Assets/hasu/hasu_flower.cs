using UnityEngine;

public class hasu_flower : MonoBehaviour
{
    [Header("設定")]
    public Sprite deadHasuSprite;
    public GameObject deadRenkonPrefab; // 枯れたレンコンのPrefab
    public float wiltingTime = 30f;     // 枯れるまでの時間
    
    [HideInInspector] public bool isWithered = false;
    private SpriteRenderer spriteRenderer;
    private EnvironmentController ec;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ec = FindObjectOfType<EnvironmentController>();
        if (ec != null) ec.RegisterLotus(this);

        Invoke("Wilting", wiltingTime); 
    }

    void Wilting()
    {
        if (isWithered) return;
        
        isWithered = true; // ここでポイントが -5pt に切り替わる
        if (spriteRenderer != null && deadHasuSprite != null)
            spriteRenderer.sprite = deadHasuSprite;

        Debug.Log("ハスが枯れました(-5pt)。5秒後にレンコンになります。");
        
        // 5秒後にレンコンを生成する関数を呼び出す
        Invoke("SpawnDeadRenkon", 5f);
    }

  void SpawnDeadRenkon()
{
    if (deadRenkonPrefab != null)
    {
        // --- ★ 変更ポイント ★ ---
        // X軸はハスの位置をそのまま使い、Y軸だけ指定の範囲内でランダムに決める
        float randomY = Random.Range(-5f, -2.5f); 
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);
        // ------------------------

        GameObject renkon = Instantiate(deadRenkonPrefab, spawnPos, Quaternion.identity);
        
        // マネージャーに登録（EnvironmentController側にポイント計算を任せる）
        if (ec != null)
        {
            ec.RegisterDeadRenkon(renkon);
        }
        Debug.Log("枯れたレンコンを画面下の範囲に生成しました: " + spawnPos);
    }

    Destroy(gameObject); // 枯れたハスを消す
}
}