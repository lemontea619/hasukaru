using UnityEngine;

public class MoDragSpawner : MonoBehaviour // ★ クラス名を変更
{
    private Vector3 dragOffset;
    private float zCoordinate;

    private Vector3 originalPosition; 
    
    // ★★★ 生成したいプレハブの変数 (Inspectorから設定) ★★★
    public GameObject moPrefabToSpawn; // 変数名を mo 用に変更

    // マウスボタンが押されたとき (ドラッグ開始)
    void OnMouseDown()
    {
        originalPosition = transform.position;
        
        zCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        dragOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    // マウスがドラッグされている間
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + dragOffset;
    }
    
    // マウスボタンが離されたとき (ドロップ時)
    void OnMouseUp()
    {
        // ★★★ ドロップ位置に新しいmoを生成する処理 ★★★
        if (moPrefabToSpawn != null)
        {
            // 現在のmoの位置に新しいmoを生成
            Instantiate(moPrefabToSpawn, transform.position, Quaternion.identity);
            Debug.Log("新しいmoが生成されました。");
        }
        // ★★★ --------------------------------------- ★★★

        // 必ず元の位置に戻す
        transform.position = originalPosition;
        
        Debug.Log("moドロップ終了。元の位置に戻りました。");
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}