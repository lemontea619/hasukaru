using UnityEngine;

public class FishManager : MonoBehaviour
{
    // Inspectorで「Fish」Prefabを接続
    public GameObject fishPrefab;
    
    // 魚が生成される間隔 (15秒)
    public float spawnInterval = 15f; 
    
    // 魚の初期Y座標の範囲
    public float minY = -4.0f;
    public float maxY = 4.0f;

    void Start()
    {
        // 15秒後から15秒間隔でSpawnNewFishを繰り返し呼び出す
        InvokeRepeating("SpawnNewFish", spawnInterval, spawnInterval);
        FindObjectOfType<SEManager>()?.PlayFish();
    }

    void SpawnNewFish()
    {
        Debug.Log("15秒経過。新しい魚が生成されました。");

        // ランダムなY座標を決定
        float randomY = Random.Range(minY, maxY);
        
        // 生成位置を画面の左端または右端にランダムに設定（例: 右端の少し外側から生成）
        float spawnX = 10f; 

        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);

        // 魚を生成
        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
    }
}