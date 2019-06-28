using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour {

    private Text text;
    private RectTransform textTransform;
    private Text from;

    public Text textPrefab;
    private Text maxTextSize;

    [SerializeField]
    private Vector2 templateSizeDelta;

    private RectTransform myRect;

    // Use this for initialization
    private void Awake()
    {
        Debug.Log("Creating Bubble");

        foreach (Text child in gameObject.GetComponentsInChildren<Text>())
        {
            if (child.gameObject.name == "Who")
            {
                from = child;
            }
            else
            {
                text = child;
            }
        }
        textTransform = text.GetComponent<RectTransform>();
        myRect = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (maxTextSize != null)
        {
            templateSizeDelta = maxTextSize.rectTransform.sizeDelta;
        }

        if (textTransform.sizeDelta.y < templateSizeDelta.y)
        {
            float difference = templateSizeDelta.y - textTransform.sizeDelta.y;
            textTransform.sizeDelta = templateSizeDelta;
            myRect.sizeDelta = new Vector2(0, myRect.sizeDelta.y + difference);
        }
        myRect.localScale = new Vector2(1, 1);
    }

    public void CreateBubble(string text, string from)
    {
        maxTextSize = Instantiate(textPrefab, this.transform);

        this.text.text = text;
        this.maxTextSize.text = text;
        if (from != null && from != "")
        {
            this.from.text = from;
        }
    }
}
