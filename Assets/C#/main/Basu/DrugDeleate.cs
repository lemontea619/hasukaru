using UnityEngine;

public class ItemBBehavior : MonoBehaviour
{
    // ItemAのスクリプトから呼び出される公開関数
    public void RemoveItem()
    {
        Debug.Log("アイテムBを消去します: " + gameObject.name);
        
        // アイテムBのGameObjectをシーンから破棄する
        Destroy(gameObject);
    }
}