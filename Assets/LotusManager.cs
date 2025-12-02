using UnityEngine;

public class LotusManager : MonoBehaviour
{
    // Inspectorで「hasu_flower」Prefabを接続するための変数
    public GameObject lotusPrefab;

    // ハスが生成される間隔 (20秒)
    public float spawnInterval = 20f;
    
    // ハスが生成される基準の位置
    public Vector3 baseSpawnPosition = new Vector3(0, 0, 0); 

    void Start()
    {
        // ゲーム開始後、20秒後から20秒間隔でSpawnNewLotusを繰り返し呼び出す
        InvokeRepeating("SpawnNewLotus", spawnInterval, spawnInterval);
    }

   void SpawnNewLotus()
{
    Debug.Log("20秒経過。新しいハスが生成されました。");

    // 生成位置をベース位置から少しランダムにずらす（ランダム範囲：-1f から 1f）
    Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    Vector3 actualSpawnPosition = baseSpawnPosition + randomOffset;

    // ハスを生成
    Instantiate(lotusPrefab, actualSpawnPosition, Quaternion.identity);
}
}