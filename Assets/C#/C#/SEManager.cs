using UnityEngine;

public class SEManager : MonoBehaviour
{
    public AudioSource audioSource;
    
    [Range(0f, 1f)] 
    public float volume = 0.8f;

    [Header("SEリスト")]
    public AudioClip clickSE;
    public AudioClip backSE;
    public AudioClip lotusSpawnSE; // 前回のハス用
    public AudioClip hakutyo;      // 白鳥が来る
    public AudioClip item;         // アイテムドラッグ
    public AudioClip tong;         // トングで拾う
    public AudioClip hasukaru;     // 鎌で刈る
    public AudioClip basuturu;     // バスを釣る
    public AudioClip fish;         // 魚・バスが増える
    public AudioClip mo;           // 藻を植える

    void Awake()
    {
        SEManager[] objects = FindObjectsOfType<SEManager>();
        if (objects.Length > 1) { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
    }

    void Start() { if (audioSource != null) audioSource.volume = volume; }

    // --- 再生用関数群 ---
    private void Play(AudioClip clip) { if (clip != null) audioSource.PlayOneShot(clip, volume); }

    public void PlayClick() => Play(clickSE);
    public void PlayBack() => Play(backSE);
    public void PlayLotusSpawn() => Play(lotusSpawnSE);
    public void PlayHakutyo() => Play(hakutyo);
    public void PlayItem() => Play(item);
    public void PlayTong() => Play(tong);
    public void PlayHasukaru() => Play(hasukaru);
    public void PlayBasuturu() => Play(basuturu);
    public void PlayFish() => Play(fish);
    public void PlayMo() => Play(mo);
}