using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private bool tmp;

    private bool conConvo;
    private bool conItem;
    private bool conMileStone;
    private bool conSkill;
    private int conLength;

    private int positionX;
    private int positionY;
    private int space;

    public PlayerConversations playerConvo;
    public PlayerItems playerItems;
    public PlayerMileStone playerMileStone;
    public PlayerSkills playerSkills;
    public PlayerTranslator playerTranslator;

    [Tooltip("Always check CONTINUE, END and starting conversations")]
    public ButtonStats[] buttonVisibility;

    private Button b;
    private bool status;

    public void ConditionSet(string buttonName)
    {
        if(buttonVisibility != null)
        {
            foreach(ButtonStats x in buttonVisibility)
            {
                if (buttonName == x.answer.name)
                {
                    if (x.conditionMet != null)
                    {
                        foreach (Conmet y in x.conditionMet)
                        {
                            switch (y.conditionMetType)
                            {
                                case selectionConditionMet.Conversation:
                                    SetConvoCon(y.conditionMetName);
                                    break;
                                case selectionConditionMet.Item:
                                    SetItemCon(y.conditionMetName);
                                    break;
                                case selectionConditionMet.Milestone:
                                    SetMileCon(y.conditionMetName);
                                    break;
                                case selectionConditionMet.Translator:
                                    SetTranslator(y.conditionMetName);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void SetConvoCon(string conversation)
    {
        if (playerConvo != null)
        {
            foreach (CompletedConversation x in playerConvo.conversations)
            {
                if (x.conversationName == conversation && x.didConversation == false)
                {
                    x.didConversation = true;
                }
            }
        }
    }

    public void SetItemCon(string item)
    {
        if(playerItems != null)
        {
            foreach (ItemBool x in playerItems.item)
            {
                if(x.itemName == item && x.hasItem == false)
                {
                    x.hasItem = true;
                }
            }
        }
    }

    public void SetMileCon(string milestone)
    {
        if (playerMileStone != null)
        {
            foreach(CompletedMilestone x in playerMileStone.milestone)
            {
                if(x.mileStoneName == milestone && x.didMileStone == false)
                {
                    x.didMileStone = true;
                }
            }
        }
    }

    public void SetTranslator(string translator)
    {
        if(playerTranslator != null)
        {
            foreach(Translator x in playerTranslator.translator)
            {
                if(x.translatorName == translator && x.hasTranslator == false)
                {
                    x.hasTranslator = true;
                }
            }
        }
    }

    public void SetStatusPos(ShowButtons newStatus)
    {
        if (buttonVisibility != null)
        {
            foreach (ButtonStats x in buttonVisibility)
            {
                if (newStatus.buttonName == x.answer.name)
                {
                    x.visible = true;
                }
            }
        }
    }

    public void SetStatusNeg(HideButtons newStatus)
    {
        if (buttonVisibility != null)
        {
            foreach (ButtonStats x in buttonVisibility)
            {
                if (newStatus.buttonName == x.answer.name)
                {
                    x.visible = false;
                }
            }
        }
    }

    public void HideAllExceptContinue()
    {
        if(buttonVisibility != null)
        {
            foreach (ButtonStats button in buttonVisibility)
            {
                if (button.answer.name != "CONTINUE")
                {
                    b = GameObject.Find(button.answer.name).GetComponent<Button>();
                    b.gameObject.transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    b = GameObject.Find(button.answer.name).GetComponent<Button>();
                    b.gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    public void HideAllExceptEnd()
    {
        if (buttonVisibility != null)
        {
            foreach (ButtonStats button in buttonVisibility)
            {
                if (button.answer.name != "END")
                {
                    b = GameObject.Find(button.answer.name).GetComponent<Button>();
                    b.gameObject.transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    b = GameObject.Find(button.answer.name).GetComponent<Button>();
                    b.gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    public void ShowButtons()
    {
        positionX = 0;
        positionY = 0;
        space = -30;

        conConvo = false;
        conItem = false;
        conMileStone = false;
        conSkill = false;

        if (buttonVisibility != null)
        {
            foreach (ButtonStats button in buttonVisibility)
            {
                if (button.visible)
                {
                    if (button.prerequisite.Length != 0)
                    {
                        conLength = button.prerequisite.Length;
                        foreach(Prereq p in button.prerequisite)
                        {
                            switch (p.prerequisiteType)
                            {
                                case selectionPrerequisite.Conversation:
                                    conConvo = ConversationCheck(p.prerequisiteName);
                                    if(conConvo == true)
                                    {
                                        conLength -= 1;
                                    }
                                    break;
                                case selectionPrerequisite.Item:
                                    conItem = ItemCheck(p.prerequisiteName);
                                    if (conItem == true)
                                    {
                                        conLength -= 1;
                                    }
                                    break;
                                case selectionPrerequisite.Milestone:
                                    conMileStone = MileCheck(p.prerequisiteName);
                                    if(conMileStone == true)
                                    {
                                        conLength -= 1;
                                    }
                                    break;
                                case selectionPrerequisite.Skill:
                                    conSkill = SkillCheck(p.prerequisiteName, p.prerequisiteValue);
                                    if(conSkill == true)
                                    {
                                        conLength -= 1;
                                    }
                                    break;
                            }
                        }
                        if (conLength == 0)
                        {
                            b = GameObject.Find(button.answer.name).GetComponent<Button>();
                            b.gameObject.transform.localScale = new Vector3(1, 1, 1);
                            b.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                            b.GetComponent<RectTransform>().transform.localPosition = new Vector3(positionX, positionY, 0);
                            positionY += space;
                        }
                    }
                    else
                    {
                        b = GameObject.Find(button.answer.name).GetComponent<Button>();
                        b.gameObject.transform.localScale = new Vector3(1, 1, 1);
                        b.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                        b.GetComponent<RectTransform>().transform.localPosition = new Vector3(positionX, positionY, 0);
                        positionY += space;
                    }
                }
                if (button.answer.name == "CONTINUE")
                {
                    b = GameObject.Find(button.answer.name).GetComponent<Button>();
                    b.gameObject.transform.localScale = new Vector3(0, 0, 0);
                }
            }
        }
    }

    public bool ConversationCheck(string convoName)
    {
        tmp = false;
        if (playerConvo != null)
        {
            foreach (CompletedConversation con in playerConvo.conversations)
            {
                if(con.conversationName == convoName)
                {
                    if(con.didConversation == true)
                    {
                        tmp = true;
                        return tmp;
                    }
                }
            }
        }
        return tmp;
    }

    public bool ItemCheck(string itemName)
    {
        tmp = false;
        if (playerItems != null)
        {
            foreach (ItemBool item in playerItems.item)
            {
                if (item.itemName == itemName)
                {
                    if(item.hasItem == true)
                    {
                        tmp = true;
                        return tmp;
                    }
                }
            }
        }
        return tmp;
    }

    public bool MileCheck(string mileName)
    {
        tmp = false;
        if (playerMileStone != null)
        {
            foreach (CompletedMilestone mile in playerMileStone.milestone)
            {
                if(mile.mileStoneName == mileName)
                {
                    if(mile.didMileStone == true)
                    {
                        tmp = true;
                        return tmp;
                    }
                }
            }
        }
        return tmp;
    }

    public bool SkillCheck(string skillCheck, int skillValue)
    {
        tmp = false;
        if (playerSkills != null)
        {
            foreach (SkillCheck skill in playerSkills.skills)
            {
                if (skill.skillName == skillCheck)
                {
                    if (skillValue <= skill.skillValue)
                    {
                        tmp = true;
                        return tmp;
                    }
                }
            }
        }
        return tmp;
    }
}
