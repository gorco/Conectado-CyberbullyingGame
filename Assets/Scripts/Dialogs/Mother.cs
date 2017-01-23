using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour {

	private Sequence late;
	private Sequence soon;

	// Use this for initialization
	void Start()
	{
		soon = new Sequence();
		soon.Root = soon.createChild(new Dialog(new List<Fragment>()
		{
			new Fragment("Mamá", "Buenos días cariño, veo que te has levantado con tiempo"),
			new Fragment("Mamá", "Portate bien y haz muchos amigos"),
		}), 1);

		late = new Sequence();
		late.Root = late.createChild(new Dialog(new List<Fragment>()
		{
			new Fragment("Mamá", "No te pusiste anoche el despertador"),
		}, new List<DialogOption>()
		{
			new DialogOption("Sí, pero me he vuelto a quedar dormido"),
			new DialogOption("Lo puse, pero no ha sonado")
		}), 2);

		Dialog d = new Dialog(new List<Fragment>()
		{
			new Fragment("Mamá", "Es tu primer día y ya llegas tarde"),
			new Fragment("Mamá", "Venga, vete ya")
		});

		late.Root.Childs[0] = late.createChild((d), 1);
		late.Root.Childs[1] = late.createChild((d), 1);
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnMouseDown()
	{
		var ge = new GameEvent();
		ge.Name = "start sequence";
		if(CalendarTime.Hour >= 8 && CalendarTime.Minute > 0)
		{
			ge.setParameter("sequence", late);
		} else
		{
			ge.setParameter("sequence", soon);
		}
			
		Game.main.enqueueEvent(ge);
	}
}
