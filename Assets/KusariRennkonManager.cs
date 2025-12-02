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
    
    [Header("ランダム生成範囲")] 
    public float randomRangeX = 3f;  // X軸の±の範囲
    public float minYPosition = -5f; // 生成されるY軸の最小値
    public float maxYPosition = -2.5f;  // 生成されるY軸の最大値

    void Start()
    {
        // 20秒後から20秒間隔でSpawnNewRennkonを繰り返し呼び出す
        InvokeRepeating("SpawnNewRennkon", spawnInterval, spawnInterval);
    }

void SpawnNewRennkon()
{
    Debug.Log("20秒経過。新しい腐ったレンコンが生成されました。");

    // X軸は±randomRangeXの範囲でランダムにずらす
    float offsetX = Random.Range(-randomRangeX, randomRangeX);
    
    // ★ Y軸は Inspectorで設定した minYPosition と maxYPosition の間でランダムに決定する
    float offsetY = Random.Range(minYPosition, maxYPosition); 

    // Y座標として baseSpawnPosition.y ではなく、絶対的な offsetY を使用
    Vector3 actualSpawnPosition = new Vector3(baseSpawnPosition.x + offsetX, offsetY, baseSpawnPosition.z);

    Instantiate(rennkonPrefab, actualSpawnPosition, Quaternion.identity);
}}