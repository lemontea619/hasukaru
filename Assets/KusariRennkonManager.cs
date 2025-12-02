using UnityEngine;

// ファイル名: KusariRennkonManager.cs
public class KusariRennkonManager : MonoBehaviour
{
    // Inspectorで「kusarirennkon」Prefabを接続
    public GameObject rennkonPrefab;

    // 生成間隔 (20秒)
    public float spawnInterval = 20f;
    
    // 生成される基準の位置
    public Vector3 baseSpawnPosition = new Vector3(0, 0, 0); 

    void Start()
    {
        // 20秒後から20秒間隔でSpawnNewRennkonを繰り返し呼び出す
        InvokeRepeating("SpawnNewRennkon", spawnInterval, spawnInterval);
    }

    void SpawnNewRennkon()
    {
        Debug.Log("20秒経過。新しい腐ったレンコンが生成されました。");

        // ランダムな位置のオフセットを計算
        Vector3 randomOffset = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        Vector3 actualSpawnPosition = baseSpawnPosition + randomOffset;

        // 腐ったレンコンを生成
        Instantiate(rennkonPrefab, actualSpawnPosition, Quaternion.identity);
    }
}