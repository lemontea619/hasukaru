using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    // === Inspectorで設定する変数 ===
    
    // 画面左端から右端までの移動距離（ワールド座標）
    // 例：左端の開始X座標が-12f、右端の終了X座標が12fの場合、距離は24f
    public float totalDistance = 24f; 
    
    // 移動にかける時間 (3分 = 180秒)
    public float totalTimeSeconds = 180f; 
    
    // === 内部変数 ===
    private float movementSpeed;

    void Start()
    {
        // 速度を計算: 距離 / 時間
        movementSpeed = totalDistance / totalTimeSeconds;
        
        Debug.Log($"雲の移動速度は {movementSpeed} ユニット/秒 です。");
    }

    void Update()
    {
        // 毎フレーム、計算された速度で右方向に移動
        // Time.deltaTimeをかけることで、フレームレートに依存しないスムーズな移動を実現
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        // 雲が画面の右端（例: X=12f）を越えたら、自身を削除する（ループさせない場合）
        // if (transform.position.x > 12f)
        // {
        //     Destroy(gameObject);
        // }
    }
}