using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCDialogue
{
    public int dialogueXP;
    public ShowButtons[] show;
    public HideButtons[] hide;
    public Dialogue[] dialogue;
}

[System.Serializable]
public class Dialogue
{
    public string thought;
    [TextArea(3,8)]
    public string sentence;
}

[System.Serializable]
public class ShowButtons
{
    public string buttonName;
}

[System.Serializable]
public class HideButtons
{
    public string buttonName;
}


