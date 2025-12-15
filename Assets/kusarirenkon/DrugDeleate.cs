using UnityEngine;

public class kusarirenkonBehavior : MonoBehaviour
{
    // ItemAのスクリプトから呼び出される公開関数
    public void RemoveItem()
    {
        Debug.Log("kusarirenkonを消去します: " + gameObject.name);
        
        // アイテムBのGameObjectをシーンから破棄する
        Destroy(gameObject);
    }
}