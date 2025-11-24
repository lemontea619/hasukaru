using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    // 飛行速度を設定
    public float flySpeed = 5f;

    void Update()
    {
        // 現在の位置から、毎フレーム flySpeed の速さで右方向に移動
        // Time.deltaTime を掛けることで、フレームレートに依存しないスムーズな移動になる
        transform.Translate(Vector3.right * flySpeed * Time.deltaTime);
    }
}