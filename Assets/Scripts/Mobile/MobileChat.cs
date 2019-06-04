using Isometra;
using Isometra.Sequences;
using RAGE.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MobileChat : MonoBehaviour
{

    public GameObject exitButton;

    GameObject lastBubble;
    GameObject lastChat;

    public GameObject locker;

    public GameObject chatList;
    public GameObject friendsApp;
    public GameObject settingsApp;
    public GameObject appsMenu;

    public GameObject otherBubblePrefab;
    public GameObject myBubblePrefab;
    public GameObject chatPrefab;
    public ScrollRect chatScroll;

    public Text chatTitle;
    public Text textTemplate;
    public float firstBubbleY;
    public float firstChatY;
    public RectTransform bubbleContent;
    public RectTransform chatListContent;
    public float initContentSize = 200;
    public float offset;

    public Text hourText;
    public Text dayText;

    private string currentChat;

    private bool outPocket;

    Dictionary<string, List<GameObject>> chats;
    Dictionary<string, Sequence> sequences;

    private List<GameObject> tempBubbles;

    private bool animate;
    private float delta;
    private float animationSeconds;
    private Vector2 goal;
    private Vector2 start;
    private Vector3 hidePosition;
    public Vector3 showPosition;
    public AnimationCurve curve;

    private GameEvent gameEvent;

    private float exitButtonTime = 0;


    //private TextAsset daysName;

    //private string[] days;

    // Use this for initialization
    void Start()
    {

        //daysName = (TextAsset)Resources.Load("Localization/" +
        //LanguageSelector.instance.GetCurrentLanguage() + "/mobileProperties.txt", typeof(TextAsset));

        //days = daysName.text.Split(',');

        exitButton.SetActive(false);

        locker.SetActive(false);
        outPocket = false;
        hidePosition = this.GetComponent<Transform>().localPosition;
        chats = new Dictionary<string, List<GameObject>>();
        tempBubbles = new List<GameObject>();
        bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, initContentSize);
    }

    private void Awake()
    {
        sequences = new Dictionary<string, Sequence>();

        this.gameEvent = new GameEvent();
        this.gameEvent.Name = "start sequence";
    }

    public void AddMessage(string text, string from, string chat = "")
    {
        GameObject bubble;
        if (from == null || from == "")
        {
            bubble = Instantiate(myBubblePrefab, bubbleContent);
        }
        else
        {
            bubble = (GameObject) Instantiate(otherBubblePrefab, bubbleContent);
        }
        TextBubble script = bubble.GetComponent<TextBubble>();
        script.textPrefab = textTemplate;
        script.CreateBubble(text, from);
        AddBubble(bubble, chat);
        tempBubbles.Add(bubble);
    }

    public Sequence GetChatSequence(string chat)
    {
        if (sequences.ContainsKey(chat))
        {
            return sequences[chat];
        }
        else if (sequences.ContainsKey("default"))
        {
            return sequences["default"];
        }
        else
        {
            return null;
        }
    }

    public void AddChatSequence(string chat, Sequence seq)
    {
        if (!sequences.ContainsKey(chat))
        {
            sequences.Add(chat, seq);
            return;
        }
        sequences[chat] = seq;
    }

    public void ClearChatSequence(string chat)
    {
        sequences.Remove(chat);
    }

    private void AddBubble(GameObject bubble, string chat)
    {
        if (chat == "")
        {
            if (currentChat == null || currentChat == "")
            {
                return;
            }
            chat = currentChat;
        }

        RectTransform rectTransform = bubble.GetComponent<RectTransform>();
        RectTransform lastTransform = null;

        if (chats.ContainsKey(chat))
        {
            List<GameObject> bubbles = chats[chat];
            if (bubbles.Count > 0)
            {
                lastBubble = bubbles[bubbles.Count - 1];
            }
            else
            {
                lastBubble = null;
            }
            if (lastBubble != null)
            {
                lastTransform = lastBubble.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, lastTransform.localPosition.y - lastTransform.sizeDelta.y - offset);
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(0, firstBubbleY);
            }
        }
        else
        {
            CreateChat(chat);
            rectTransform.anchoredPosition = new Vector2(0, firstBubbleY);
        }
        bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, bubbleContent.sizeDelta.y + (lastTransform != null ? lastTransform.sizeDelta.y : 0) + offset);

        chats[chat].Add(bubble);
        ScrollChatToBottom();

        if (currentChat != chat)
        {
            LoadChat(chat, false);
            foreach (ChatGroup script in chatListContent.GetComponentsInChildren<ChatGroup>())
            {
                if (script.nameChat.text == chat)
                {
                    script.AddAdvertisement();
                }
            }
            HideChat(chat);
        }
    }

    public void RemoveCurrentChat()
    {
        RemoveChat("");
    }

    public void RemoveChat(string chatKey)
    {
        if (chatKey == "")
        {
            chatKey = currentChat;
        }
        foreach (GameObject g in chats[chatKey])
        {
            Destroy(g);
        }
        chats.Remove(chatKey);
    }

    public void HideChat(string chat)
    {
        chatList.SetActive(true);
        if (chat == "")
        {
            if (currentChat != null && currentChat != "")
            {
                chat = currentChat;
            }
            else
            {
                return;
            }
        }
        if (!chats.ContainsKey(chat))
        {
            chats.Add(chat, new List<GameObject>());
        }
        List<GameObject> list = chats[chat];
        foreach (GameObject g in list)
        {
            g.SetActive(false);
        }
        currentChat = "";
    }

    public void CreateChat(string name)
    {
        GameObject chat;
        if (name != null || name != "")
        {
            chat = Instantiate(chatPrefab, chatListContent);
            chat.transform.localScale = new Vector2(1, 1);
            ChatGroup script = chat.GetComponent<ChatGroup>();
            script.InitChat(name, null, this);
            AddChat(chat);
        }
        chats.Add(name, new List<GameObject>());
    }

    public void AddChat(GameObject chat)
    {
        RectTransform rectTransform = chat.GetComponent<RectTransform>();
        RectTransform lastTransform = null;
        if (lastChat == null)
        {
            rectTransform.anchoredPosition = new Vector2(0, firstChatY);
        }
        else
        {
            lastTransform = lastChat.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, lastTransform.localPosition.y - lastTransform.sizeDelta.y);
        }
        //bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, bubbleContent.sizeDelta.y + (lastTransform != null ? lastTransform.sizeDelta.y : 0) + offset);
        rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
        rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
        //chats[currentChat].Add(bubble);
        lastChat = chat;
        //ScrollChatToBottom();
    }

    private void FixedUpdate()
    {
        if (currentChat != null && chats.ContainsKey(currentChat))
        {
            lastBubble = null;
            RectTransform lastTransform = null;
            List<GameObject> list = chats[currentChat];
            foreach (GameObject g in list)
            {
                RectTransform rectTransform = g.GetComponent<RectTransform>();
                if (lastBubble != null)
                {
                    rectTransform.anchoredPosition = new Vector2(0, lastTransform.anchoredPosition.y - lastTransform.sizeDelta.y - offset);
                }
                else
                {
                    rectTransform.anchoredPosition = new Vector2(0, firstBubbleY);
                }
                lastTransform = rectTransform;
                lastBubble = g;
            }
        }
    }

    public void LoadChat(string chat, bool show = true)
    {
        if (chat != currentChat)
        {
            HideChat("");
        }
        chatTitle.text = chat;
        bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, initContentSize);
        RectTransform lastTransform = null;
        if (chats.ContainsKey(chat))
        {
            List<GameObject> list = chats[chat];
            foreach (GameObject g in list)
            {
                RectTransform rectTransform = g.GetComponent<RectTransform>();
                if (lastBubble != null)
                {
                    lastTransform = lastBubble.GetComponent<RectTransform>();
                }
                rectTransform.parent = bubbleContent;
                g.SetActive(true);
                bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, bubbleContent.sizeDelta.y + (lastTransform != null ? lastTransform.sizeDelta.y : 0) + offset);
                lastBubble = g;
            }
        }
        ScrollChatToBottom();
        if (show)
        {
            chatList.SetActive(false);
            currentChat = chat;
        }
    }

    public void SaveMobileState()
    {
        tempBubbles.Clear();
    }

    public void LoadMobileState()
    {
        foreach (ChatGroup chat in chatListContent.GetComponentsInChildren<ChatGroup>(true))
        {
            List<GameObject> tmp = new List<GameObject>();
            foreach (GameObject bubble in chats[chat.nameChat.text])
            {
                if (tempBubbles.Contains(bubble))
                {
                    tmp.Add(bubble);
                }
            }
            foreach (GameObject bubble in tmp)
            {
                ClearChatSequence(chat.nameChat.text);
                tempBubbles.Remove(bubble);
                chats[chat.nameChat.text].Remove(bubble);
                Destroy(bubble);
            }
        }
    }

    public void ScrollChatToTop()
    {
        chatScroll.normalizedPosition = new Vector2(0, 1);
    }

    public void ScrollChatToBottom()
    {
        chatScroll.normalizedPosition = new Vector2(0, 0);
    }

    void Update()
    {
        if (animate)
        {
            this.GetComponent<Transform>().localPosition = Vector3.Lerp(start, goal, curve.Evaluate((delta) / animationSeconds));
            delta += Time.deltaTime;
            if (delta >= animationSeconds)
            {
                animate = false;
                if (!outPocket)
                {
                    OpenMobileHome();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitButton.SetActive(!exitButton.activeInHierarchy);
            exitButtonTime = 0;
        }

        if (exitButton.activeInHierarchy)
        {
            exitButtonTime += Time.deltaTime;
            if (exitButtonTime > 5)
            {
                exitButton.SetActive(false);
            }
        }

    }

    public void SwitchMobilePosition(float seconds)
    {
        if (!outPocket && !animate)
        {
            Interacted("takeMobile");
            takeMobile(seconds);
        }
        else if (outPocket && !animate)
        {
            Interacted("hideMobile");
            hideMobile(seconds);
        }
    }

    public int GetAdvertisementNumber()
    {
        int news = 0;
        foreach (ChatGroup chat in chatListContent.GetComponentsInChildren<ChatGroup>())
        {
            news += chat.GetAdvertisementNumber();
        }
        return news;
    }

    public void OpenMobileHome()
    {
        Debug.LogWarning("SHOW MENU");
        appsMenu.SetActive(true);
        chatList.SetActive(false);
        friendsApp.SetActive(false);
        settingsApp.SetActive(false);
    }

    public void OpenChatApp()
    {
        Debug.LogWarning("SHOW CHATLIST");
        Interacted("openMobileChat");
        appsMenu.SetActive(false);
        chatList.SetActive(true);
        friendsApp.SetActive(false);
        settingsApp.SetActive(false);
    }

    public void OpenFriendshipApp()
    {
        Debug.LogWarning("SHOW FRIENDS");
        Interacted("openFriendsApp");
        appsMenu.SetActive(false);
        chatList.SetActive(false);
        friendsApp.SetActive(true);
        settingsApp.SetActive(false);
        friendsApp.GetComponent<NetApp>().UpdateBars();
    }

    public void OpenSettingsApp()
    {
        Debug.LogWarning("SHOW SETINGS");
        Interacted("openMobileSettings");
        appsMenu.SetActive(false);
        chatList.SetActive(false);
        friendsApp.SetActive(false);
        settingsApp.SetActive(true);
    }

    private void UpdateHour()
    {
        hourText.text = "";
        if (GlobalState.Hour < 10)
        {
            hourText.text = "0";
        }
        hourText.text += GlobalState.Hour + ":";
        if (GlobalState.Minute < 10)
        {
            hourText.text += "0";
        }
        hourText.text += GlobalState.Minute;
    }

    //private string[] daysName = { "LUNES", "MARTES", "MIERCOLES", "JUEVES", "VIERNES" };

    private void UpdateDay()
    {
        dayText.text = LanguageSelector.instance.GetName(GlobalState.Day.ToString()) +
            ", " + LanguageSelector.instance.GetName("day") + " " + (GlobalState.Day + 1).ToString();
    }

    public void takeMobile(float seconds)
    {
        locker.SetActive(true);
        UpdateHour();
        UpdateDay();
        goal = showPosition;
        start = hidePosition;
        animationSeconds = seconds;
        delta = 0;
        animate = true;
        outPocket = true;
    }

    public void hideMobile(float seconds)
    {

        locker.SetActive(false);
        goal = hidePosition;
        start = showPosition;
        animationSeconds = seconds;
        delta = 0;
        outPocket = false;
        currentChat = "";
        if (seconds > 0)
        {
            animate = true;
        }
        else
        {
            this.GetComponent<Transform>().localPosition = goal;
            OpenMobileHome();
        }

    }

    public void ThrowResponseMessage()
    {
        if (sequences.ContainsKey(currentChat))
        {
            this.gameEvent.setParameter("sequence", sequences[currentChat]);
        }
        else if (sequences.ContainsKey("default"))
        {
            this.gameEvent.setParameter("sequence", sequences["default"]);
        }
        Game.main.enqueueEvent(this.gameEvent);
    }

    public void Interacted(string id)
    {
        try
        {
            Tracker.T.GameObject.Interacted(id, GameObjectTracker.TrackedGameObject.Item);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
