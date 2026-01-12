using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnvironmentController : MonoBehaviour
{
    // === シーンを跨いでデータを保持するための変数 (static) ===
    public static float finalEnvScore;
    public static float finalSwanScore;

    [Header("UI設定")]
    public Image fillImage;         // 環境メーター
    public Image swanFillImage;     // 白鳥満足度メーター
    private float currentEnvironmentValue = 0.0f; 
    private float currentSwanValue = 0.0f;

    [Header("ポイント設定")]
    public int maxPointsFor100Percent = 100;
    public int totalPoints = 0;

    [Header("地面の切り替え設定")]
    public SpriteRenderer groundSpriteRenderer;
    public Sprite roughGroundSprite;
    public Sprite cleanGroundSprite;
    public float cleanGroundThreshold = 0.9f;

    [Header("沼の色設定")]
    public SpriteRenderer swampRenderer; 
    public Color roughSwampColor = new Color(0.48f, 0.58f, 0.47f, 0.5f);
    public Color cleanSwampColor = new Color(0.62f, 0.86f, 0.96f, 0.5f);
    public float colorTransitionThreshold = 0.9f;
    
    [Header("白鳥演出設定")]
    public GameObject swanPrefab;      
    public Transform spawnPoint;      
    public Transform[] swanAnchors;   
    public float swanScale = 0.5f; // インスペクターで0.5に設定すると2分の1になります

    private BirdMovement[] activeSwans = new BirdMovement[2]; 

    // 各種オブジェクトのリスト
    private List<hasu_flower> allLotuses = new List<hasu_flower>();
    private List<GameObject> allDeadRenkon = new List<GameObject>(); 
    private List<SwanFish> allFishes = new List<SwanFish>();
    private List<BlackBass> allBlackBasses = new List<BlackBass>(); 
    private List<mo> allMo = new List<mo>();

    // 登録用メソッド群
    public void RegisterLotus(hasu_flower lotus) => allLotuses.Add(lotus);
    public void RegisterDeadRenkon(GameObject renkon) => allDeadRenkon.Add(renkon);
    public void RegisterFish(SwanFish fish) => allFishes.Add(fish);
    public void RegisterBlackBass(BlackBass bass) => allBlackBasses.Add(bass);
    public void RegisterMo(mo moScript) => allMo.Add(moScript);
    
    public int GetBlackBassCount()
    {
        allBlackBasses.RemoveAll(b => b == null); 
        return allBlackBasses.Count;
    }

    public int GetLivingLotusCount()
    {
        allLotuses.RemoveAll(l => l == null); 
        int count = 0;
        foreach (var lotus in allLotuses)
        {
            if (lotus != null && !lotus.isWithered) count++;
        }
        return count;
    }

    public void RemoveOneMo()
    {
        allMo.RemoveAll(m => m == null);
        mo targetMoScript = allMo.Find(m => m.gameObject.name.Contains("(Clone)"));
        if (targetMoScript != null)
        {
            GameObject objToDestroy = targetMoScript.gameObject;
            allMo.Remove(targetMoScript);
            Destroy(objToDestroy);
        }
    }

    void Start()
    {
        if (groundSpriteRenderer != null && roughGroundSprite != null)
            groundSpriteRenderer.sprite = roughGroundSprite;
    }

    void Update()
    {
        CalculateTotalPoints();

        // --- 環境メーターの更新 ---
        currentEnvironmentValue = (float)totalPoints / maxPointsFor100Percent;
        currentEnvironmentValue = Mathf.Clamp01(currentEnvironmentValue);
        if (fillImage != null) fillImage.fillAmount = currentEnvironmentValue;

        // --- 白鳥メーター（満足度）の計算 ---
        float swanScore = 0f;
        swanScore += allFishes.Count * 15f; 
        swanScore += allMo.Count * 8f;     
        swanScore += GetLivingLotusCount() * 5f; 
        swanScore -= allBlackBasses.Count * 20f; 

        currentSwanValue = swanScore / 40f; // 40点を100%として計算
        currentSwanValue = Mathf.Clamp01(currentSwanValue);

        if (swanFillImage != null) swanFillImage.fillAmount = currentSwanValue;

        UpdateEnvironmentVisuals();
        UpdateSwanPresence();
    }

    void UpdateEnvironmentVisuals()
    {
        if (groundSpriteRenderer != null)
        {
            groundSpriteRenderer.sprite = (currentEnvironmentValue >= cleanGroundThreshold) ? cleanGroundSprite : roughGroundSprite;
        }
        if (swampRenderer != null)
        {
            float lerpRate = Mathf.Clamp01(currentEnvironmentValue / colorTransitionThreshold);
            swampRenderer.color = Color.Lerp(roughSwampColor, cleanSwampColor, lerpRate);
        }
    }

    void UpdateSwanPresence()
    {
        if (swanPrefab == null || spawnPoint == null || swanAnchors == null || swanAnchors.Length < 2) return;

        int targetCount = 0;
        if (currentSwanValue >= 0.8f) targetCount = 2;
        else if (currentSwanValue > 0.0f) targetCount = 1;
        else targetCount = 0;

        for (int i = 0; i < 2; i++)
        {
            if (i < targetCount && activeSwans[i] == null)
            {
                GameObject newSwan = Instantiate(swanPrefab, spawnPoint.position, Quaternion.identity);
               
SEManager se = FindObjectOfType<SEManager>();
if (se != null) se.PlayHakutyo();
                newSwan.transform.localScale = new Vector3(swanScale, swanScale, 1f);

                activeSwans[i] = newSwan.GetComponent<BirdMovement>();
                if (activeSwans[i] != null) 
                {
                    activeSwans[i].targetPosition = swanAnchors[i].position;
                }
            }
            else if (i >= targetCount && activeSwans[i] != null)
            {
                activeSwans[i].FlyAway(spawnPoint.position);
                activeSwans[i] = null;
            }
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
            if (lotus != null && lotus.isWithered) witheredLotusCount++;
            else if (lotus != null) livingLotusCount++;
        }

        totalPoints = (livingLotusCount * 5) + (witheredLotusCount * -5) + (allDeadRenkon.Count * -5) + (allFishes.Count * 5) + (allBlackBasses.Count * -2) + (allMo.Count * 5);

        if (allMo.Count >= 4) totalPoints -= ((allMo.Count - 3) * 8);
    }

    // ★タイマー終了時にスコアを「共有の箱」へ保存する
    public void PrepareScores()
    {
        // 環境ポイントを0-100で保存
        finalEnvScore = currentEnvironmentValue * 100f;
        // 白鳥満足度を0-100で保存
        finalSwanScore = currentSwanValue * 100f;
    }
}