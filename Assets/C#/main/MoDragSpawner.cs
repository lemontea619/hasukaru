using UnityEngine;

public class MoDragSpawner : MonoBehaviour
{
    private Vector3 dragOffset;
    private float zCoordinate;
    private Vector3 originalPosition; 
    
    [Header("生成するプレハブ")]
    public GameObject moPrefabToSpawn; 

    // マウスボタンが押されたとき (ドラッグ開始)
    void OnMouseDown()
    {
        originalPosition = transform.position;
        
        zCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        dragOffset = gameObject.transform.position - GetMouseWorldPosition();

        // ★アイテムを掴んだ時の音を鳴らす
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
        // ドロップ位置に新しいmoを生成する
        if (moPrefabToSpawn != null)
        {
            Instantiate(moPrefabToSpawn, transform.position, Quaternion.identity);
            Debug.Log("新しいmoが生成されました。");
            // ※ここで音を鳴らさなくても、生成されたmoのStart()内で音が鳴ります
        }

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