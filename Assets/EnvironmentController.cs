using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnvironmentController : MonoBehaviour
{
    [Header("UI設定")]
    public Image fillImage; 
    private float currentEnvironmentValue = 0.0f; 

    [Header("ポイント設定")]
    public int maxPointsFor100Percent = 100;
    public int totalPoints = 0;

    [Header("地面の切り替え設定")]
    public SpriteRenderer groundSpriteRenderer;
    public Sprite roughGroundSprite;
    public Sprite cleanGroundSprite;
    public float cleanGroundThreshold = 0.9f; // 90%で地面が綺麗になる
    private bool isGroundClean = false;

    [Header("沼の色設定")]
    public SpriteRenderer swampRenderer; 
    public Color roughSwampColor = new Color(0.48f, 0.58f, 0.47f, 0.5f);
    public Color cleanSwampColor = new Color(0.62f, 0.86f, 0.96f, 0.5f);
    public float colorTransitionThreshold = 0.9f;

    // --- 管理リスト ---
    private List<hasu_flower> allLotuses = new List<hasu_flower>();
    private List<GameObject> allDeadRenkon = new List<GameObject>(); 
    private List<SwanFish> allFishes = new List<SwanFish>();
    private List<BlackBass> allBlackBasses = new List<BlackBass>(); 
    private List<mo> allMo = new List<mo>();

    public void RegisterLotus(hasu_flower lotus) => allLotuses.Add(lotus);
    public void RegisterDeadRenkon(GameObject renkon) => allDeadRenkon.Add(renkon);
    public void RegisterFish(SwanFish fish) => allFishes.Add(fish);
    public void RegisterBlackBass(BlackBass bass) => allBlackBasses.Add(bass);
    public void RegisterMo(mo moScript) => allMo.Add(moScript);
    
    // EnvironmentController.cs のクラス内（RegisterMo関数の下あたり）に追加してください
public int GetBlackBassCount()
{
    allBlackBasses.RemoveAll(b => b == null); // 念のためリストを整理
    return allBlackBasses.Count;
}

// EnvironmentController.cs のクラス内に追加

// 1. 生きているハスの数を計算して返す関数
public int GetLivingLotusCount()
{
    int count = 0;
    allLotuses.RemoveAll(l => l == null); // リストを掃除
    foreach (var lotus in allLotuses)
    {
        if (lotus != null && !lotus.isWithered) count++;
    }
    return count;
}

public void RemoveOneMo()
{
    allMo.RemoveAll(m => m == null);
    
    // アイテム（原本）ではなく、クローンされた藻だけを探す
    mo target = allMo.Find(m => m.gameObject.name.Contains("(Clone)") || m.CompareTag("PlantedMo"));

    if (target != null)
    {
        allMo.Remove(target);
        Destroy(target.gameObject);
        Debug.Log("ブラックバスの影響で藻が1つ消失しました。");
    }
}


    void Start()
    {
        // 初期状態は荒れた地面にする
        if (groundSpriteRenderer != null && roughGroundSprite != null)
        {
            groundSpriteRenderer.sprite = roughGroundSprite;
        }
    }

    void Update()
    {
        CalculateTotalPoints();
        
        // 環境値(0.0 ~ 1.0)を計算
        currentEnvironmentValue = (float)totalPoints / maxPointsFor100Percent;
        currentEnvironmentValue = Mathf.Clamp01(currentEnvironmentValue);
        
        if (fillImage != null) fillImage.fillAmount = currentEnvironmentValue;

        // ★ここで見た目を更新する関数を呼ぶ★
        UpdateEnvironmentVisuals();
    }

    void UpdateEnvironmentVisuals()
    {
        // 1. 地面の切り替え
        if (currentEnvironmentValue >= cleanGroundThreshold && !isGroundClean)
        {
            if (groundSpriteRenderer != null && cleanGroundSprite != null)
            {
                groundSpriteRenderer.sprite = cleanGroundSprite;
                isGroundClean = true;
                Debug.Log("地面が綺麗になりました！");
            }
        }

        // 2. 沼の色変化（グラデーション）
        if (swampRenderer != null)
        {
            float lerpRate = currentEnvironmentValue / colorTransitionThreshold; 
            lerpRate = Mathf.Clamp01(lerpRate); 
            swampRenderer.color = Color.Lerp(roughSwampColor, cleanSwampColor, lerpRate);
        }
    }

    void CalculateTotalPoints()
    {
        allLotuses.RemoveAll(l => l == null);
        allDeadRenkon.RemoveAll(r => r == null);
        allFishes.RemoveAll(f => f == null);
        allBlackBasses.RemoveAll(b => b == null);
        allMo.RemoveAll(m => m == null);

        int livingLotusCount = 0;
        int witheredLotusCount = 0;
        foreach (var lotus in allLotuses)
        {
            if (lotus.isWithered) witheredLotusCount++;
            else livingLotusCount++;
        }

        int deadRenkonCount = allDeadRenkon.Count;
        int fishCount = allFishes.Count;
        int blackBassCount = allBlackBasses.Count;
        int moCount = allMo.Count;

        totalPoints = (livingLotusCount * 5) 
                    + (witheredLotusCount * -5) 
                    + (deadRenkonCount * -5) 
                    + (fishCount * 5) 
                    + (blackBassCount * -2)
                    + (moCount * 5);
    }
}