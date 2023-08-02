using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AstroDialogue : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<string> botMessages = new List<string>();

    [SerializeField] GameObject dialoguePanel;

    [SerializeField] TMP_Text dialogueText;

    int step;


    void Start()
    {
        step = 1;   
    }

    public void NextStep()
    {
        if(dialoguePanel != null && step < (botMessages.Count -1));
        {
            dialoguePanel.SetActive(true);
            CancelInvoke();
            Invoke("Turnoff", 2f);
            if(botMessages[step] != null)
                dialogueText.text = botMessages[step];

            if(step < botMessages.Count)
            step++;
        }
    }

    private void Turnoff()
    {

            
        dialoguePanel.SetActive(false);
    }
}
