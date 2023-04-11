using UnityEngine;
using UnityEngine.UI;

public class GetStringName : MonoBehaviour
{

    private void Awake()
    {
        FillName();
    }

    void FillName()
    {
        if(gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = LanguageSelector.instance.GetName(gameObject.name);
        }
        else
        {
			Text textobject = gameObject.GetComponentInChildren<Text>();
			if (textobject != null)
			{
				textobject.text = LanguageSelector.instance.GetName(gameObject.name);
			} else
			{
				TextMesh textmesh = gameObject.GetComponentInChildren<TextMesh>();
				if (textmesh != null)
				{
					textmesh.text = LanguageSelector.instance.GetName(gameObject.name);
				}
			}
			
        }
	}
}
