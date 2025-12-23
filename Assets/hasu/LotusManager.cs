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

    [Header("藻の抑制設定")]
    public float checkInterval = 5f; // 5秒ごとにチェック

    private EnvironmentController ec;

    void Start()
    {
        ec = FindObjectOfType<EnvironmentController>();

        // 20秒間隔で生成
        InvokeRepeating("SpawnNewLotus", spawnInterval, spawnInterval);

        // ★追加：定期的にハスの数をチェックして藻を減らす処理を開始
        InvokeRepeating("CheckAlgaeReduction", checkInterval, checkInterval);
    }

    void SpawnNewLotus()
    {
        float offsetX = Random.Range(-randomRangeX, randomRangeX);
        float offsetY = Random.Range(minYPosition, maxYPosition); 
        Vector3 spawnPos = new Vector3(baseSpawnPosition.x + offsetX, offsetY, baseSpawnPosition.z);

        Instantiate(lotusPrefab, spawnPos, Quaternion.identity);
    }

    public void GenerateLotusAt(Vector3 position)
    {
        Instantiate(lotusPrefab, position, Quaternion.identity);
    }

    // ★追加：ハスが6個以上なら藻を1つ消す
    void CheckAlgaeReduction()
    {
        if (ec == null) return;

        // EnvironmentControllerから「生きているハス」の数を聞く
        int livingLotusCount = ec.GetLivingLotusCount();

        // ハスが6個以上なら、藻を削除する命令を出す
        if (livingLotusCount >= 6)
        {
            ec.RemoveOneMo();
        }
    }
}