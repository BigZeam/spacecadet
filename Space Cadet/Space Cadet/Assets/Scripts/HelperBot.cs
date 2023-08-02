using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class HelperBot : MonoBehaviour
{
    // Start is called before the first frame update
    int choice;
    [SerializeField] Item currency;
    [SerializeField] Item tronCurrency;

    [SerializeField] TMP_Text helperBotText;
    [SerializeField] List<string> botMessages = new List<string>();
    [SerializeField] List<string> questMessages = new List<string>();
    [SerializeField] Sprite onSprite;
    [SerializeField] AudioClip helperBotSound;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Transform beemoSpriteTransform;

    [SerializeField] Vector3 punchVal;
    [SerializeField] float punchDuration, elasticityVal;
    [SerializeField] int vibratoVal;

    [SerializeField] AstroDialogue _astroDialogue;

    [Header("CameraShake")]

    [SerializeField] Vector3 camStrength;
    [SerializeField] int camVibrato;
    [SerializeField] float camRandomness;
    [SerializeField] bool camFadeout;
    [SerializeField] float camDuration;
    [SerializeField] Camera mc;

    [SerializeField] GameObject shopsContainerObj;
    [SerializeField] GameObject botBossContainerObj;

    public bool QuestTime;
    
    bool canContinue;
    void Start()
    {
        //CameraShake();
    }

    // Update is called once per frame
    void Update()
    {
        if(!QuestTime)
            helperBotText.text = botMessages[choice];
        else 
            helperBotText.text = questMessages[choice];
        if(Input.GetKeyDown(KeyCode.F) && canContinue)
        {
            NextText();
            AudioManager.Instance.Play(helperBotSound);
        }
    }

    public void NextText()
    {
        if(!QuestTime)
        {
            if(choice == 4)
            {
                if(currency.count >= 5)
                {
                    incValue();
                    sr.sprite = onSprite;
                    Sequence makeUpright = DOTween.Sequence();
                    makeUpright.Append(beemoSpriteTransform.DOLocalRotate(new Vector3(0, 0, 0), punchDuration));
                    makeUpright.Append(beemoSpriteTransform.DOPunchRotation(punchVal, punchDuration, vibratoVal, elasticityVal));
                    
                }   
            }
            else if(choice == 7)
            {
                incValue();
                //CancelInvoke();
                //Invoke(nameof(incValue),2f);
            }
            else if(choice > 15)
            {
                CancelInvoke();
                Invoke(nameof(incValue),5f);
            }
            else 
            {
                incValue();
            }
        }
        else 
        {
            if(choice == 7)
            {
                if(currency.count >= 10)
                {
                    currency.count -= 10;
                    incValue();
                    _astroDialogue.NextStep();
                }
            }
            else if(choice == 8)
            {
                if(tronCurrency.count >= 15)
                {
                    tronCurrency.count -= 15;
                    incValue();
                    _astroDialogue.NextStep();
                }
            }
            else if(choice == 9)
            {
                if(currency.count >= 20)
                {
                    currency.count -= 20;
                    incValue();
                    _astroDialogue.NextStep();
                }
            }
            else if(choice == 10)
            {
                _astroDialogue.NextStep();

                botBossContainerObj.SetActive(true);
                shopsContainerObj.SetActive(false);

                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().DisableShop();
                
                CameraShake();
                Destroy(this.gameObject);
            }
            else
            {
                incValue();
                CameraShake();
            }
        }


    }

    void incValue()
    {
        choice++;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            canContinue = true;
            helperBotText.gameObject.transform.parent.gameObject.SetActive(true);
            helperBotText.gameObject.transform.parent.gameObject.GetComponent<ScalePunchAwake>().PunchScaleMe();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
            canContinue = false;
            helperBotText.gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void ResetCount()
    {
        choice = 0;
    }

    private void CameraShake()
    {
        mc.DOShakePosition(camDuration, camStrength, camVibrato, camRandomness, camFadeout);
    }
}
