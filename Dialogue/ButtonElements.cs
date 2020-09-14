using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ButtonStats
{
    public Button answer;
    public Prereq[] prerequisite;
    public Conmet[] conditionMet;
    public bool visible;
}

[System.Serializable]
public class Prereq
{
    public selectionPrerequisite prerequisiteType;
    public string prerequisiteName;
    public int prerequisiteValue;
}

[System.Serializable]
public class Conmet
{
    public selectionConditionMet conditionMetType;
    public string conditionMetName;
}

public enum selectionPrerequisite
{
    Conversation = 1, 
    Item = 2, 
    Milestone = 3, 
    Skill = 4
}

public enum selectionConditionMet
{
    Conversation = 1,
    Item = 2,
    Milestone = 3,
    Translator = 4,
}