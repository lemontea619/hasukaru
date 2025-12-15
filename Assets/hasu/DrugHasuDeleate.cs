using UnityEngine;

public class DrugHasuDeleate : MonoBehaviour
{
    private Vector3 dragOffset;
    private float zCoordinate;

    // ★★★ 1. 元の位置を記憶する変数と衝突成功フラグを宣言 (追加) ★★★
    private Vector3 originalPosition; 
    private bool hasuDestroyed = false; 
    // ★★★ ----------------------------------------------------- ★★★

    // マウスボタンが押されたとき (ドラッグ開始)
    void OnMouseDown()
    {
        // ★★★ 2. 元の位置を保存 (追加) ★★★
        originalPosition = transform.position;
        hasuDestroyed = false; // フラグをリセット
        // ★★★ -------------------------- ★★★

        zCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        dragOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    // マウスがドラッグされている間
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + dragOffset;
    }
    
    // マウスボタンが離されたとき (ドラッグ終了/ドロップ時)
    void OnMouseUp()
    {
        // originalPosition を使用して元の位置に戻す
        transform.position = originalPosition;
        
        Debug.Log("ドロップ操作が終了しました。アイテムは必ず元の位置に戻りました。");

        if (hasuDestroyed)
        {
            Debug.Log("【成功】ドロップ操作中にhasuは消去されました。");
            // 成功時の追加処理（例：スコア加算など）があればここに入れる
        }
    }

    // マウスのスクリーン座標をワールド座標に変換する
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    // 魚（hasu）との接触が始まったとき
    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突相手が "hasu" タグを持っているかチェック
        if (other.CompareTag("hasu"))
        {
            Debug.Log("hasuと衝突検知: " + other.gameObject.name);
            
            // 消去スクリプトの関数を呼び出す
            hasuBehavior hasuScript = other.gameObject.GetComponent<hasuBehavior>();
            
            if (hasuScript != null)
            {
                hasuScript.RemoveItem();
                
                // ★★★ 3. 衝突が成功したことをフラグで記録 (追加) ★★★
                hasuDestroyed = true; 
                // ★★★ ------------------------------------------ ★★★
            }
        }
    }
}