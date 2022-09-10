using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    [Header("Active Dialogue")]
    [SerializeField] private Dialogue activeDialogue;
    
    [Header("Setting")]
    [SerializeField]private float textSpeed;
    
    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text speaker;
    //[SerializeField] private TMP_Text mood;
    [SerializeField] private TMP_Text message;

    private int line;
    

    private void Start()
    {
        StartLine();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (message.text == activeDialogue.dialogue[line].message)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                message.text = activeDialogue.dialogue[line].message;
            }
        }
    }

    void StartLine()
    {
        line = 0;
        StartCoroutine(TypeName());
        StartCoroutine(TypeLine());
    }
    
    IEnumerator TypeLine()
    {
        message.text = String.Empty;
        
        foreach (char c in activeDialogue.dialogue[line].message)
        {
            message.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    
    IEnumerator TypeName()
    {
        speaker.text = String.Empty;

        foreach (char c in activeDialogue.dialogue[line].character.ToString())
        {
            speaker.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (line < activeDialogue.dialogue.Length)
        {
            line++;

            if (speaker.text != activeDialogue.dialogue[line].character.ToString()) StartCoroutine(TypeName());
                
            StartCoroutine(TypeLine());
        }
        else
        {
            Debug.Log("End of all Line");
        }
    }
}
