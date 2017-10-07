using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform targetTr;
	public float dist = 10.0f;
	public float height = 3.0f;
	public float dampTrace = 20.0f;
	private Vector3 up;

	private Transform tr;
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		up = Vector3.up * height;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Mouse Y") != 0)
			up += Vector3.up * Input.GetAxis ("Mouse Y")/20;
		tr.position = Vector3.Lerp (tr.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);
		tr.LookAt (targetTr.position+ up);
	}
}
