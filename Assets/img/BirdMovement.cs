using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float flySpeed = 5f;
    public Vector3 targetPosition; // 目的地（池の中、または画面外）
    private bool isLeaving = false;

    void Start()
    {
        // ゲーム開始時に現在の位置を目的地に設定（初期設定用）
        if (targetPosition == Vector3.zero) targetPosition = transform.position;
    }

    void Update()
    {
        // 目的地に向かって移動
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, flySpeed * Time.deltaTime);

        // 進行方向を向く（目的地が右なら右を向く）
        if (targetPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 画像の向きに合わせて調整
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // 「立ち去りモード」かつ「画面外の目的地に到着」したら消える
        if (isLeaving && Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    // 画面外へ飛んでいかせるための関数
    public void FlyAway(Vector3 exitPos)
    {
        targetPosition = exitPos;
        isLeaving = true;
    }
}