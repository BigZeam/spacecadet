using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("GameObjects")]
    public GameObject playerObj;
    PlayerController pc;
    Gun startingGun;    
    bool showHealth = true, showRad = false, pastFrame1 = false;
    [SerializeField] Transform computerTransform;

    public Slider timerSlider;
    public int globalTimer;
    public int playerLevel {get; private set;}
    bool canSetTimer, canLowerTimer;
    [SerializeField] Animator effectAnim;
    [SerializeField] GameObject losePanelObj;

    [SerializeField] List<Item> allPossibleItems = new List<Item>();
    [SerializeField] List<Gun> allPossibleGuns = new List<Gun>();
    [SerializeField] AudioClip song1, song2, song3, bossMusic;
    [SerializeField] bool canPlayNewSong;
    [SerializeField] GameObject invObject, mapObject, winObj, effectObj;
    float waitTime;
    public bool isGameOver;
    bool isMapOn, isInvOn, hasFaded;

    void Start()
    {
        
        isMapOn = true;
        canPlayNewSong = true;
        timerSlider.maxValue = globalTimer;
        startingGun = playerObj.GetComponent<PlayerController>().GetGunSlot1();
        startingGun.RestoreDefaults();
        pc = playerObj.GetComponent<PlayerController>();
        ResetItemCounts();
        Invoke(nameof(GameHasStarted), 5f);
        NewSong();
        ResetGunStats();
        //playerObj.transform.position = new Vector3(0, computerTransform.position.y + 10, 0);
        Invoke(nameof(InitializePlayer), .125f);
    }   

    // Update is called once per frame
    void Update()
    {
        if(canSetTimer)
            SetGlobalTimer();
        else
        {
            LowerRadiationLevels();
        }
        CheckForAnim();
        CheckForInput();
        timerSlider.value = globalTimer;
        if(playerObj == null)
        {
            effectAnim = GameObject.FindGameObjectWithTag("Effect").GetComponent<Animator>();
            playerObj = GameObject.FindGameObjectWithTag("Player");
            pc = playerObj.GetComponent<PlayerController>();
        }
    }
    void InitializePlayer()
    {
        playerObj.transform.position = new Vector3(0, computerTransform.position.y + 15, 0);
    }
    void NewSong()
    {
        if(canPlayNewSong)
        {
            int myNum = Random.Range(1, 20);
            if(myNum < 5)
            {
                AudioManager.Instance.PlayMusic(song1);
                waitTime = song1.length;
            }
            else if(myNum < 10)
            {
                AudioManager.Instance.PlayMusic(song2);
                waitTime = song2.length;
            }
            else if(myNum <= 15)
            {
                AudioManager.Instance.PlayMusic(song3);
                waitTime = song3.length;
            }
            else 
            {
                AudioManager.Instance.PlayMusic(bossMusic);
                waitTime = bossMusic.length;
            }
            canPlayNewSong = false;
            
            Invoke("SongTimer", waitTime);
            StartCoroutine(Fade(true));
        }
    }
    void SongTimer()
    {
        canPlayNewSong = true;
        NewSong();
    }
    void SetGlobalTimer()
    {
        globalTimer--;

        timerSlider.value = globalTimer;

        if(globalTimer <= 0)
        {
            LoseGame();
        }
    }
    void FadeMusic(bool fadeIn)
    {
        if(fadeIn)
        {
            AudioManager.Instance.MusicSource.DOFade(1f, 3f);
        }
        else 
        {
            AudioManager.Instance.MusicSource.DOFade(0f, 1f);
        }
    }
    void CheckForAnim()
    {
        if(pc.GetHealth() == 1 && showHealth)
        {
            effectAnim.SetTrigger("LowHealth");
            showHealth = false;
            Invoke(nameof(HealthTimer), 15f);
        }
        if(timerSlider.value < 10000 && showRad && pastFrame1)
        {
            effectAnim.SetTrigger("LowRadiation");
            showRad = false;
            Invoke(nameof(RadTimer), 5f);
        }
    }
    void ResetItemCounts()
    {
        foreach(Item item in allPossibleItems)
        {
            item.ResetCount();
        }
    }
    void ResetGunStats()
    {
        foreach(Gun gun in allPossibleGuns)
        {
            gun.RestoreDefaults();
        }
    }

    public void SetTimer(bool b)
    {
        canSetTimer = b;
        if(!canSetTimer && pastFrame1)
        {
            globalTimer = (int)timerSlider.maxValue;
            timerSlider.DOValue(timerSlider.maxValue, 1f, true);
            showRad = true;
        }
    }
    void RadTimer()
    {
        showRad = true;
    }
    void HealthTimer()
    {
        showHealth = true;
    }
    void GameHasStarted()
    {
        pastFrame1 = true;
    }
    public void LoseGame()
    {
        isGameOver = true;
        losePanelObj.SetActive(true);
    }
    void LowerRadiationLevels()
    {
        globalTimer+=100;
        if(globalTimer > timerSlider.maxValue)
            globalTimer = (int)timerSlider.maxValue;
    }
    IEnumerator Fade(bool fadeIn) 
    {
        FadeMusic(fadeIn);

        yield return new WaitForSeconds(waitTime - 1f);

        FadeMusic(!fadeIn);

    }

    public void FadeOut()
    {
        EndingFade();
        hasFaded = true;
        Invoke("IMBAD", .8f);
        Invoke("newGameEnd", 3f);

        //StartCoroutine(GameEnd());
    }

    private void EndingFade()
    {
        if(hasFaded == false)
            effectAnim.SetTrigger("FadeOut");
    }
    IEnumerator GameEnd()
    {
        Debug.Log("Ending Sequence");
        
        yield return new WaitForSeconds(1.5f);
        EndingFade();

        hasFaded = true;
        yield return new WaitForSeconds(.8f);
        //SceneManager.LoadScene(0);
        winObj.SetActive(true);

    }
    void newGameEnd()
    {
        winObj.SetActive(true);
    }
    void IMBAD()
    {
        //effectObj.SetActive(false);
        Destroy(effectAnim);
        pc.moveSpeed = 0;
    }
    private void CheckForInput()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            isInvOn = !isInvOn;
            invObject.SetActive(isInvOn);
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            isMapOn = !isMapOn;
            mapObject.SetActive(isMapOn);
        }
    }
}
