using UnityEngine;

public class SwanFishManager : MonoBehaviour
{
    public GameObject SwanFishPrefab;
    public float spawnInterval = 15f; 
    public float minY = -4.0f;
    public float maxY = 4.0f;

    [Header("生態系設定")]
    public float predationCheckInterval = 5f; // 5秒ごとにチェック

    private EnvironmentController ec;

    void Start()
    {
        ec = FindObjectOfType<EnvironmentController>();

        // 15秒間隔で魚を生成
        InvokeRepeating("SpawnNewFish", spawnInterval, spawnInterval);
        
        // 5秒間隔で「バスに食べられるか」をチェック
        InvokeRepeating("CheckPredation", predationCheckInterval, predationCheckInterval);
    }

    void SpawnNewFish()
    {
        float randomY = Random.Range(minY, maxY);
        float spawnX = 10f; 
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);
        Instantiate(SwanFishPrefab, spawnPosition, Quaternion.identity);
    }

    // ブラックバスの数を確認して SwanFish を消す
 void CheckPredation()
{
    int bassCount = ec.GetBlackBassCount();
    // バス2匹につき1匹消す計算（例：5匹いたら2匹消える）
    int killCount = bassCount / 2; 

    for (int i = 0; i < killCount; i++)
    {
        RemoveOneSwanFish();
        ec.RemoveOneMo();
    }
}


    void RemoveOneSwanFish()
    {
        // シーン内のSwanFishコンポーネントがついたオブジェクトをすべて取得
        SwanFish[] allFishes = FindObjectsOfType<SwanFish>();

        if (allFishes.Length > 0)
        {
            // 最初に見つかった1匹を消去
            Destroy(allFishes[0].gameObject);
            Debug.Log("ブラックバスによってSwanFishが1匹捕食されました。");
        }
    }
}