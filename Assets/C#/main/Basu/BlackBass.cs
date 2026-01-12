using UnityEngine;

public class BlackBass : MonoBehaviour
{
    void Start()
    {
        // 生成された瞬間に EnvironmentController を探して自分を登録
        EnvironmentController ec = FindObjectOfType<EnvironmentController>();
        if (ec != null)
        {
            ec.RegisterBlackBass(this);
        }
    }

    // 釣り上げられたり、画面外で消えたりした時に自動的にポイントが回復します
    void OnDestroy()
    {
        // リストから消えることで、CalculateTotalPointsでの -2pt がなくなります
    }
}