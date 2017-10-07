using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
	public Text txtScore,txtTime,txtInf;
	private int totScore = 0;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Display (0);
		StartCoroutine (this.Timer ());
	}
	IEnumerator Timer(){
		for (int i = 0; i < 150; i++) {
			yield return new WaitForSeconds (1.0f);
			txtTime.text = (150 - i).ToString();
			if (i == 5)
				txtInf.text = "";
		}
		Application.LoadLevel ("Transition2");
	}
	// Update is called once per frame
	public void Display (int a) {
		totScore += a;
		txtScore.text = totScore.ToString()+"/30";
		if (totScore == 30) {
			Application.LoadLevel ("Transition");
		}
	}
}
