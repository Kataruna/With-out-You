using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : Singleton<DialogueDisplay>
{
    #region - Variable Decleration -

    [Header("Active Dialogue")]
    [SerializeField] private Dialogue activeDialogue;

    [Header("Setting")]
    [SerializeField] private float textSpeed;

    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text speaker;
    [SerializeField] private TMP_Text message;

    [SerializeField] private CharacterDisplay silvia;
    [SerializeField] private CharacterDisplay jane;
    //[SerializeField] private TMP_Text mood;

    [Header("Animator Controller")]
    [SerializeField] private Animator dialogAnimator;
    [SerializeField] private Animator choicesAnimator;

    [Header("Object Assign")]
    [SerializeField] private Transform choiceHolder;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private Sprite empty;

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
        PlayerController.Instance.SetControlState(false);

        _line = 0;
        StartCoroutine(TypeName());
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        message.text = String.Empty;

        ImageMap();
        
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
                case DialogueProperties.Mode.SwitchMood:
                    ImageMap();
                    NextLine();
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
                case DialogueProperties.Mode.UpdateEvent:
                    if(activeDialogue.dialogue[_line].eventKey != String.Empty)
                        EventHorizon.Instance.UpdateEvent(activeDialogue.dialogue[_line].eventKey, 
                                                            activeDialogue.dialogue[_line].eventStatus);
                    DialogueInteraction();
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
        if (activeDialogue.dialogue[_line].mode == DialogueProperties.Mode.Choice) return;

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
        PlayerController.Instance.SetControlState(true);
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
        
        ClearImage();
        dialogAnimator.SetTrigger("Enter");
        
        Debug.LogWarning("Enter Dialogue");
    }

    public void ExitDialogue()
    {
        ClearImage();
        dialogAnimator.SetTrigger("Exit");
        activeDialogue = null;
    }

    public void ImageMap()
    {
        DialogueProperties rightNow = activeDialogue.dialogue[_line];
        
        switch (rightNow.character)
        {
            case DialogueProperties.Character.January:
                switch (rightNow.mood)
                {
                    case DialogueProperties.Mood.Angry:
                        jane.image.sprite = jane.face.Angry;
                        break;
                    case DialogueProperties.Mood.Curious:
                        jane.image.sprite = jane.face.Curious;
                        break;
                    case DialogueProperties.Mood.Neutral:
                        jane.image.sprite = jane.face.Neutral;
                        break;
                    case DialogueProperties.Mood.Sad:
                        jane.image.sprite = jane.face.Sad;
                        break;
                }
                break;
            case DialogueProperties.Character.Silvia:
                switch (rightNow.mood)
                {
                    case DialogueProperties.Mood.Angry:
                        silvia.image.sprite = silvia.face.Angry;
                        break;
                    case DialogueProperties.Mood.Curious:
                        silvia.image.sprite = silvia.face.Curious;
                        break;
                    case DialogueProperties.Mood.Neutral:
                        silvia.image.sprite = silvia.face.Neutral;
                        break;
                    case DialogueProperties.Mood.Sad:
                        silvia.image.sprite = silvia.face.Sad;
                        break;
                }
                break;
        }
    }

    public void ClearImage()
    {
        jane.image.sprite = empty;
        silvia.image.sprite = empty;
    }

    #endregion
}

[Serializable]
public class CharacterDisplay
{
    public CharacterFace face;
    public Image image;
}