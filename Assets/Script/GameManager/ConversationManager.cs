using UnityEngine;
using System.Collections.Generic;

public class ConversationManager
{
    public Sprite PCPicture;
    public Sprite KittyPicture;
    protected GameObject dialogGO;
    protected List<Dialog> conversation;
    protected int converIndex = 0;
    
    public ConversationManager(GameObject hud)
    {
        dialogGO = hud.transform.FindChild("Dialog").gameObject;
    }

    public void newConversation(List<Dialog> d)
    {
        Canvas dialogCanvas = dialogGO.GetComponent<Canvas>();
        dialogCanvas.enabled = true;
        conversation = d;
        converIndex = 0;
        setDialog();
    }

    public void conversationEnd()
    {
        GameManager.Instance.inConversation = false;
        Canvas dialogCanvas = dialogGO.GetComponent<Canvas>();
        dialogCanvas.enabled = false;
        Time.timeScale = 1;
    }

    protected void setDialog()
    {
        Dialog d = conversation[converIndex];
        Transform dialogTransform = dialogGO.GetComponent<Canvas>().transform;
        UnityEngine.UI.Image image = dialogTransform.FindChild("Picture").GetComponent<UnityEngine.UI.Image>();
        switch (d.person)
        {
            case "PC":
                image.sprite = PCPicture;
                break;
            case "Kitty":
                image.sprite = KittyPicture;
                break;
        }

        dialogTransform.FindChild("Content").GetComponent<UnityEngine.UI.Text>().text = d.content;
    }

    public void nextDialog()
    {
        converIndex += 1;
        if (converIndex < conversation.Count)
        {
            setDialog();
        }
        else
        {
            conversationEnd();
        }
    }
}
