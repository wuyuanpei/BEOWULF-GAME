using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {
	public void OnClickStartBtn(string level){
		GameObject.Find ("loading").GetComponent<Text> ().text="Loading...";
		Application.LoadLevel (level);
	}
	void Start(){
		Cursor.visible = true;
	}
}
