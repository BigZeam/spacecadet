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

    [SerializeField] TMP_Text helperBotText;
    [SerializeField] List<string> botMessages = new List<string>();
    [SerializeField] Sprite onSprite;
    [SerializeField] AudioClip helperBotSound;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Transform beemoSpriteTransform;

    [SerializeField] Vector3 punchVal;
    [SerializeField] float punchDuration, elasticityVal;
    [SerializeField] int vibratoVal;
    
    bool canContinue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        helperBotText.text = botMessages[choice];
        if(Input.GetKeyDown(KeyCode.F) && canContinue)
        {
            NextText();
            AudioManager.Instance.Play(helperBotSound);
        }
    }

    public void NextText()
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
            CancelInvoke();
            Invoke(nameof(incValue),10f);
        }
        else if(choice > 8)
        {
            CancelInvoke();
            Invoke(nameof(incValue),10f);
        }
        else 
        {
            incValue();
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
}
