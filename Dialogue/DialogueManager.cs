using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public string NPCName;
    public string requiredTranslatorName;
    [TextArea(3,8)]
    public string noTranslatorSentence;
    
    public PlayerTranslator playerTranslator;

    [Tooltip("Game object with saved images, answers and texts")]
    public GameObject canvas;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI thoughtText;

    private Queue<Dialogue> dialogueTree;

    void Start()
    {
        dialogueTree = new Queue<Dialogue>();
    }
    
    public void StartDialogue(NPCDialogue convo, string pressedButton)
    {
        if(pressedButton == "END")
        {
            CloseDialogue();
            return;
        }

        nameText.text = NPCName;
        dialogueTree.Clear();

        FindObjectOfType<ButtonController>().HideAllExceptContinue();
        FindObjectOfType<ButtonController>().ConditionSet(pressedButton);

        AddXP(convo.dialogueXP);

        SetButtons(convo);
        SetCanvas();

        foreach(Dialogue sentence in convo.dialogue)
        {
            dialogueTree.Enqueue(sentence);
        }

        foreach(Translator tran in playerTranslator.translator)
        {
            if(tran.translatorName == requiredTranslatorName)
            {
                if (tran.hasTranslator)
                {
                    DisplayNextSentence();
                }
                else
                {
                    FindObjectOfType<ButtonController>().HideAllExceptEnd();
                    StopAllCoroutines();    
                    StartCoroutine(TypeChar(noTranslatorSentence));
                }
            }
        }
    }

    public void DisplayNextSentence()
    {
        if (dialogueTree.Count == 0)
        {
            EndDialogue();
            return;
        }
        Dialogue sentence = dialogueTree.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeChar(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    IEnumerator TypeSentence(Dialogue sentence)
    {
        dialogText.text = "";
        thoughtText.text = "";
        foreach (char l in sentence.thought.ToCharArray())
        {
            thoughtText.text += l;
            yield return null;
        }
        foreach (char letter in sentence.sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        FindObjectOfType<ButtonController>().ShowButtons();
    }

    void CloseDialogue()
    {
        canvas.GetComponent<RectTransform>().transform.localScale = new Vector3(0, 0, 0);
    }

    public void SetCanvas()
    {
        canvas.transform.localScale = new Vector3(1, 1, 1);
    }

    void SetButtons(NPCDialogue buttons)
    {
        if (buttons.show != null)
        {
            foreach (ShowButtons x in buttons.show)
            {
                FindObjectOfType<ButtonController>().SetStatusPos(x);
            }
        }
        if (buttons.hide != null)
        {
            foreach (HideButtons x in buttons.hide)
            {
                FindObjectOfType<ButtonController>().SetStatusNeg(x);
            }
        }
    }

    void AddXP(int xp)
    {
        FindObjectOfType<PlayerLevels>().addXP(xp);
    }
}
