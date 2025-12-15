using UnityEngine;

// ファイル名: kusarirennkon.cs
public class kusarirennkon : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("腐ったレンコンがクリックされました。");
        
        // 環境メーターを減らす処理を追加する場合はここに追加
        // EnvironmentController ec = FindObjectOfType<EnvironmentController>();
        // if (ec != null)
        // {
        //     // ec.DecreaseEnvironmentValue(); // 環境値を減らす関数（別途作成が必要）
        // }

        // この腐ったレンコンオブジェクトを削除する
        Destroy(gameObject); 
    }
}