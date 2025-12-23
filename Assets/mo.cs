using UnityEngine;

public class mo : MonoBehaviour
{
    void Start()
    {
        // 生まれた瞬間に EnvironmentController を探す
        EnvironmentController ec = FindObjectOfType<EnvironmentController>();
        
        if (ec != null)
        {
            // Controller側に自分（mo）を登録する
            ec.RegisterMo(this);
            Debug.Log("moが登録されました。点数が更新されます。");
        }
    }

    // もし後で藻を削除する機能（鎌で刈るなど）を作った場合、
    // 消えた瞬間に自動でポイントが減るようになります。
    void OnDestroy()
    {
        // 削除時はEnvironmentControllerのリスト掃除機能で自動的にポイント再計算されます
    }
}