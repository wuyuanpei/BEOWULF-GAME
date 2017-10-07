using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehavior : MonoBehaviour {
	public Transform[] points;
	public GameObject monsterPrefab;
	private float createTime = 2.0f;
	private int maxMonster = 30;
	public bool isGameOver = false;
	void Start(){
		points = GameObject.Find ("SpawnPoint").GetComponentsInChildren<Transform> ();
		if (points.Length > 0) {
			StartCoroutine (this.CreateMonster ());
		}
	}
	IEnumerator CreateMonster(){
		while (!isGameOver) {
			int monsterCount = (int)GameObject.FindGameObjectsWithTag ("MONSTER").Length;
			//Debug.Log (monsterCount.ToString ());
			if (monsterCount < maxMonster) {
				yield return new WaitForSeconds (createTime);
				int idx = Random.Range (1, points.Length);
				Instantiate (monsterPrefab, points [idx].position, points [idx].rotation);
			} else {
				yield return null;
			}
		}
	}
}
