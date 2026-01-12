using UnityEngine;

public class mo : MonoBehaviour
{
    void Start()
    {
        // ★修正ポイント：名前に "(Clone)" が含まれている場合のみ実行
        // これにより、アイテムBOXにあるオリジナルがシーン開始時に音を鳴らすのを防ぎます
        if (gameObject.name.Contains("(Clone)"))
        {
            EnvironmentController ec = FindObjectOfType<EnvironmentController>();
            
            if (ec != null)
            {
                ec.RegisterMo(this);
                Debug.Log("クローンのmoが登録されました。");
            }

            // ★植えられた（生成された）時だけ音を鳴らす
            FindObjectOfType<SEManager>()?.PlayMo();
        }
        else
        {
            Debug.Log("これはアイテムBOXのオリジナルなので登録・発音しません。");
        }
    }

    void OnDestroy()
    {
        // 削除時は自動的にリストから外れる仕組み（EnvironmentController側でRemoveAllしているため空でもOK）
    }
}