using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Translator
{
    public string translatorName;
    public bool hasTranslator;
}

[System.Serializable]
public class ItemBool
{
    public string itemName;
    public bool hasItem;
}

[System.Serializable]
public class CompletedConversation
{
    public string conversationName;
    public bool didConversation;
}

[System.Serializable]
public class CompletedMilestone
{
    public string mileStoneName;
    public bool didMileStone;
}

[System.Serializable]
public class SkillCheck
{
    public string skillName;
    public int skillValue;
}