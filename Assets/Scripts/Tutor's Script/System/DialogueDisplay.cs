using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    #region - Variable Decleration -

    [Header("Active Dialogue")]
    [SerializeField] private Dialogue activeDialogue;

    [Header("Setting")]
    [SerializeField] private float textSpeed;

    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text speaker;
    [SerializeField] private TMP_Text message;
    //[SerializeField] private TMP_Text mood;

    [Header("Animator Controller")]
    [SerializeField] private Animator dialogAnimator;
    [SerializeField] private Animator choicesAnimator;

    [Header("Object Assign")]
    [SerializeField] private Transform choiceHolder;
    [SerializeField] private GameObject choicePrefab;

    private int _line;
    private List<GameObject> _choices = new List<GameObject>();
    private Controller _controller;
    private static readonly int ChoicePhrase = Animator.StringToHash("ChoicePhrase");

    #endregion

    #region - Unity's Method -

    private void Awake()
    {
        _controller = new Controller();
    }

    private void OnEnable()
    {
        _controller.Enable();
    }

    private void OnDisable()
    {
        _controller.Disable();
    }

    private void Start()
    {
        CleanMessage();
        _controller.UI.Interact.performed += _ => DialogueInteraction();
    }

    #endregion

    #region - Custom Method -

    void StartLine()
    {
        _line = 0;
        StartCoroutine(TypeName());
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        message.text = String.Empty;

        foreach (char c in activeDialogue.dialogue[_line].message)
        {
            message.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator TypeName()
    {
        speaker.text = String.Empty;

        foreach (char c in activeDialogue.dialogue[_line].character.ToString())
        {
            speaker.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        _line++;
        
        if (_line < activeDialogue.dialogue.Length)
        {
            Debug.Log($"Line: {_line}");
            Debug.Log($"Dialogue Size: {activeDialogue.dialogue.Length}");

            switch (activeDialogue.dialogue[_line].mode)
            {
                case DialogueProperties.Mode.MainCharacter:
                case DialogueProperties.Mode.SideCharacter:
                    if (speaker.text != activeDialogue.dialogue[_line].character.ToString()) StartCoroutine(TypeName());
                    StartCoroutine(TypeLine());
                    break;
                case DialogueProperties.Mode.Choice:
                    ClearChoice();
                    
                    _choices.Clear();
                
                    dialogAnimator.SetBool(ChoicePhrase, true);
                    choicesAnimator.SetBool(ChoicePhrase, true);

                    foreach (Choices path in activeDialogue.dialogue[_line].choices)
                    {
                        GameObject go = Instantiate(choicePrefab, choiceHolder);

                        go.name = path.choice;

                        go.transform.SetSiblingIndex(go.transform.parent.childCount - 2);

                        go.GetComponent<Button>().onClick.AddListener(() => ChangeDialog(path.blueprint, path.eventKey, path.eventValue));
                        go.transform.GetChild(0).GetComponent<TMP_Text>().text = path.choice;

                        _choices.Add(go);
                    }

                    break;
            }
        }
        else
        {
            Debug.Log("End of all Line");
        }
    }

    void DialogueInteraction()
    {
        if (_line == activeDialogue.dialogue.Length - 1 && message.text == activeDialogue.dialogue[_line].message)
            ConversationEnd();
        else if (message.text == activeDialogue.dialogue[_line].message)
            NextLine();
        else
        {
            StopAllCoroutines();
            message.text = activeDialogue.dialogue[_line].message;
        }
    }

    void ConversationEnd()
    {
        ExitDialogue();
        Debug.Log("End of all Line");
        CleanMessage();
    }

    void CleanMessage()
    {
        message.text = String.Empty;
        speaker.text = String.Empty;
    }

    void ChangeDialog(Dialogue dialogue, string key, bool value)
    {
        dialogAnimator.SetBool(ChoicePhrase, false);
        choicesAnimator.SetBool(ChoicePhrase, false);

        if(key != String.Empty)
            EventHorizon.Instance.UpdateEvent(key, value);
        
        activeDialogue = dialogue;
        StartLine();
    }

    void ClearChoice()
    {
        foreach (GameObject go in _choices)
        {
            Destroy(go);
        }
    }

    public void EnterDialogue(Dialogue dialogue)
    {
        activeDialogue = dialogue;
        
        dialogAnimator.SetTrigger("Enter");
    }

    public void ExitDialogue()
    {
        dialogAnimator.SetTrigger("Exit");
    }

    #endregion
}