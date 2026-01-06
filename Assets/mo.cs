using UnityEngine;

public class mo : MonoBehaviour
{
    void Start()
    {
        // ★修正ポイント：名前に "(Clone)" が含まれている場合のみ登録する
        // これにより、アイテムBOXにあるオリジナルは無視されます
        if (gameObject.name.Contains("(Clone)"))
        {
            EnvironmentController ec = FindObjectOfType<EnvironmentController>();
            
            if (ec != null)
            {
                ec.RegisterMo(this);
                Debug.Log("クローンのmoが登録されました。");
            }
        }
        else
        {
            Debug.Log("これはアイテムBOXのオリジナルなので登録しません。");
        }
    }

    void OnDestroy()
    {
        // 削除時は自動的にリストから外れる仕組み（EnvironmentController側）
    }
}