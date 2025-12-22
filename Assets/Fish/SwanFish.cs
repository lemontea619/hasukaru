using UnityEngine;

public class SwanFish : MonoBehaviour
{
    void Start()
    {
        // 生成された瞬間に EnvironmentController を探して自分を登録
        EnvironmentController ec = FindObjectOfType<EnvironmentController>();
        if (ec != null)
        {
            ec.RegisterFish(this);
        }
    }

    // 魚が画面外に出て消えた時や、食べられた時にポイントが減るようにする
    void OnDestroy()
    {
        // Destroyされると、EnvironmentController側のリスト掃除(nullチェック)で
        // 自動的にポイント計算から除外されます
    }
}