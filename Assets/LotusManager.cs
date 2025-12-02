using UnityEngine;

public class LotusManager : MonoBehaviour
{
    // Inspectorで「hasu_flower」Prefabを接続するための変数
    public GameObject lotusPrefab;

    // ハスが生成される間隔 (20秒)
    public float spawnInterval = 20f;
    
    // ハスが生成される基準の位置
    public Vector3 baseSpawnPosition = new Vector3(0, 0, 0); 
    
    [Header("ランダム生成範囲")]
public float randomRangeX = 3f; // X軸の±の範囲
public float minYPosition = 0f; // 生成されるY軸の最小値
public float maxYPosition = 5f;  // 生成されるY軸の最大値

    void Start()
    {
        // ゲーム開始後、20秒後から20秒間隔でSpawnNewLotusを繰り返し呼び出す
        InvokeRepeating("SpawnNewLotus", spawnInterval, spawnInterval);
    }

  void SpawnNewLotus()
{
    Debug.Log("20秒経過。新しいハスが生成されました。");

    // X軸はベース位置から±randomRangeXの範囲でランダムにずらす
    float offsetX = Random.Range(-randomRangeX, randomRangeX);
    
    // Y軸はベース位置からのずれではなく、絶対的な minYPosition と maxYPosition の間でランダムに決定する
    float offsetY = Random.Range(minYPosition, maxYPosition); 

    // Y軸の生成に baseSpawnPosition.y を使わないように、ここでは Vector3 の値を直接代入
    Vector3 actualSpawnPosition = new Vector3(baseSpawnPosition.x + offsetX, offsetY, baseSpawnPosition.z);

    Instantiate(lotusPrefab, actualSpawnPosition, Quaternion.identity);
}
}