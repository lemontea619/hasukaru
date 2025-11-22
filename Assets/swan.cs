using UnityEngine;

// 白鳥にアタッチ
public class SwanMovement : MonoBehaviour
{
    public float speed = 0.5f; // ゆっくりとした速度
    private float moveDirection = 1f; // 1は右、-1は左

    void Update()
    {
        // 常に水平に移動
        transform.position += Vector3.right * moveDirection * speed * Time.deltaTime;

        // 例: 特定のX座標に達したら方向転換する
        if (transform.position.x > 5f || transform.position.x < -5f)
        {
            moveDirection *= -1f; // 方向を反転
            // スプライトを反転させて白鳥の向きを変える
            GetComponent<SpriteRenderer>().flipX = (moveDirection < 0);
        }
    }
}