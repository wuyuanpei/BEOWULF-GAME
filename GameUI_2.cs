using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI_2 : MonoBehaviour {
	public Text txtB,txtD,txtInf;
	public DragonCtrl d1, d2;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		StartCoroutine (this.Timer ());
	}
	IEnumerator Timer(){
		yield return new WaitForSeconds(5.0f);
		d1 = GameObject.FindWithTag("t1").GetComponentInChildren<DragonCtrl> ();
		d2 = GameObject.FindWithTag("t2").GetComponentInChildren<DragonCtrl> ();
		txtInf.text = "";
		while (true) {
			yield return new WaitForSeconds(0.5f);
			int a = d1.hp;
			if (a < 0)
				a = 0;
			int b = d2.hp;
			if (b < 0)
				b = 0;
			txtD.text = a.ToString () + "  " + b.ToString ();
			if (d1.hp <= 0 && d2.hp <= 0)
				Application.LoadLevel ("Transition3");
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
