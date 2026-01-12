using UnityEngine;

public class KohasuDragSpawner : MonoBehaviour
{
    private Vector3 dragOffset;
    private float zCoordinate;

    private Vector3 originalPosition; 
    
    // ★★★ 生成したいプレハブの変数 (Inspectorから設定) ★★★
    public GameObject hasuPrefabToSpawn; 

    // マウスボタンが押されたとき (ドラッグ開始)
    void OnMouseDown()
    {
        originalPosition = transform.position;
        
        zCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        dragOffset = gameObject.transform.position - GetMouseWorldPosition();
        
        FindObjectOfType<SEManager>()?.PlayItem();
    }

    // マウスがドラッグされている間
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + dragOffset;
    }
    
    // マウスボタンが離されたとき (ドロップ時)
    void OnMouseUp()
    {
        // ★★★ ドロップ位置に新しいHasuを生成する処理 ★★★
        if (hasuPrefabToSpawn != null)
        {
            // 現在のkohasuの位置に新しいHasuを生成
            Instantiate(hasuPrefabToSpawn, transform.position, Quaternion.identity);
            Debug.Log("新しいHasuがkohasuによって生成されました。");
        }
        // ★★★ --------------------------------------- ★★★

        // 必ず元の位置に戻す
        transform.position = originalPosition;
        
        Debug.Log("kohasuドロップ終了。元の位置に戻りました。");
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}