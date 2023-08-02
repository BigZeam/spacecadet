using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AltarOffering : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Item currency;
    [SerializeField] Gun opGun;
    public TMP_Text dialogueBox;
    int kronSpent;
    bool canCheckInput;
    PlayerController pc;
    [SerializeField] GameObject altarObj;
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update() {
        CheckInput();
    }

    public void SpendKron()
    {
        if(currency.count >= 5)
        {
            kronSpent+=5;
            currency.count = currency.count - 5;
            InventoryManager.Instance.ListItems();
        }

        switch(kronSpent)
        {
            case 0:
            dialogueBox.text = "Bring me some kron.";
            break;
            case 5:
            dialogueBox.text = "Thanks. You got any more?";
            break;
            case 10:
            dialogueBox.text = "Are you expecting something?";
            break;
            case 15:
            dialogueBox.text = "...";
            break;
            default:
            dialogueBox.text = "ok fine here you go";
            pc.gunSlot1 = opGun;
            pc.SetNewGunImg();
            if(altarObj)
                Destroy(altarObj.gameObject);
            break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        canCheckInput = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        canCheckInput = false;
    }
    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.F) && canCheckInput)
        {
           SpendKron(); 
        }
    }
}
