using UnityEngine;

public class kusarirenkon : MonoBehaviour
{
    private Vector3 dragOffset;
    private float zCoordinate;

private Vector3 originalPosition; // 元の位置を記憶
    private bool kusarirenkonDestroyed = false; // renkonが消去されたかどうかのフラグ

    // マウスボタンが押されたとき (ドラッグ開始)
    void OnMouseDown()
    {
        originalPosition = transform.position;
        // フラグをリセット
        kusarirenkonDestroyed = false;
        // オブジェクトのZ座標を保存 (2Dでも奥行きが必要な場合があるため)
        zCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        // マウス位置とオブジェクト位置の差分を計算
        dragOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    // マウスがドラッグされている間
    void OnMouseDrag()
    {
        // マウス位置にオフセットを加えてオブジェクトを移動
        transform.position = GetMouseWorldPosition() + dragOffset;
    }

// マウスボタンが離されたとき (ドラッグ終了/ドロップ時)
    void OnMouseUp()
    {
        // ★ 判定条件を削除し、必ず元の位置に戻す処理を実行する ★
        transform.position = originalPosition;
        
        Debug.Log("ドロップ操作が終了しました。アイテムは必ず元の位置に戻りました。");

        // (デバッグ用) kusarirenkonが消えたかどうかは、ログや他のロジックで確認できます
        if (kusarirenkonDestroyed)
        {
            Debug.Log("【成功】ドロップ操作中にkusarirenkonは消去されました。");
        }
    }

    // マウスのスクリーン座標をワールド座標に変換する
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate; // Z座標は固定
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    // アイテムBとの接触が始まったとき (ItemBが Is Trigger = ON のため)
    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突相手が "ItemB" タグを持っているかチェック
        if (other.CompareTag("kusarirenkon"))
        {
            Debug.Log("kusarirenkonと衝突検知: " + other.gameObject.name);
            
            // kusarirenkonにアタッチされている消去スクリプトの関数を呼び出す
            kusarirenkonBehavior kusarirenkonScript = other.gameObject.GetComponent<kusarirenkonBehavior>();
            
            if (kusarirenkonScript != null)
            {
                kusarirenkonScript.RemoveItem();
                kusarirenkonDestroyed = true;
            }
        }
    }
}