using UnityEngine;

public class LotusManager : MonoBehaviour
{
    public GameObject lotusPrefab; // hasu_flowerがついたPrefab
    public float spawnInterval = 20f;
    
    [Header("生成範囲設定")]
    public Vector3 baseSpawnPosition = Vector3.zero;
    public float randomRangeX = 3f;
    public float minYPosition = 0f;
    public float maxYPosition = 5f;

    void Start()
    {
        // 20秒間隔で生成
        InvokeRepeating("SpawnNewLotus", spawnInterval, spawnInterval);
    }

    void SpawnNewLotus()
    {
        float offsetX = Random.Range(-randomRangeX, randomRangeX);
        float offsetY = Random.Range(minYPosition, maxYPosition); 
        Vector3 spawnPos = new Vector3(baseSpawnPosition.x + offsetX, offsetY, baseSpawnPosition.z);

        Instantiate(lotusPrefab, spawnPos, Quaternion.identity);
    }

    // ドラッグ＆ドロップスクリプトから呼び出す用
    public void GenerateLotusAt(Vector3 position)
    {
        Instantiate(lotusPrefab, position, Quaternion.identity);
    }
}