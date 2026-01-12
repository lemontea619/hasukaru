using UnityEngine;

public class hasuBehavior : MonoBehaviour
{
    // ItemAのスクリプトから呼び出される公開関数
    public void RemoveItem()
    {
        Debug.Log("hasuを消去します: " + gameObject.name);
        
        // アイテムBのGameObjectをシーンから破棄する
        Destroy(gameObject);
    }
}