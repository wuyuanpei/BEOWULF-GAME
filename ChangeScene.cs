using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		StartCoroutine (this.WaitScene ());
	}
	public string level;
	IEnumerator WaitScene(){
		yield return new WaitForSeconds (20.0f);
		GameObject.Find ("Text").GetComponent<Text> ().text="Loading... New Mission";
		Application.LoadLevel (level);
	}
}
