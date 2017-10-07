using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
	private Animator animator;
	private Transform tr;
	public float speed = 4.0f;
	private Collider swordC;
	public AudioSource audio,audio2,step;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		tr = GetComponent<Transform> ();
		swordC = GameObject.FindWithTag ("SWORD").GetComponent<Collider> ();
		swordC.enabled = false;

	}
	private int attackOn = 0;
	// Update is called once per frame
	void Update () {
		if (attackOn == 0) {
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			float j = Input.GetAxis ("Jump");

			tr.Rotate (Vector3.up * Time.deltaTime * 100 * Input.GetAxis ("Mouse X"));
			if (h != 0 || v != 0) {
				animator.SetBool ("IsWalk", true);
				Vector3 move = (Vector3.forward * v) + (Vector3.right * h);
				tr.Translate (move * Time.deltaTime * speed, Space.Self);
				if(!step.isPlaying)
					step.Play ();
			} else {
				animator.SetBool ("IsWalk", false);
				step.Pause ();
			}
			if (Input.GetAxis ("Run") != 0) {
				animator.SetBool ("IsRun", true);
				speed = 7;
			} else {
				animator.SetBool ("IsRun", false);
				speed = 4;
			}
		}

		//animator.SetBool ("IsHit", false);
		if (attackOn == 0) {
			if (Input.GetAxis ("Attack1") != 0) {
				animator.SetTrigger ("Attack1");
				attackOn = 1;
				swordC.enabled = true;
				audio.Play ();
			}
			if (Input.GetAxis ("Attack2") != 0) {
				animator.SetTrigger ("Attack2");
				attackOn = 1;
				swordC.enabled = true;
				audio.Play ();
			} 
			if (Input.GetAxis ("Attack3") != 0) {
				animator.SetTrigger ("Attack3");
				attackOn = 1;
				swordC.enabled = true;
				audio.Play ();
			}
		}
		if (attackOn >= 1)
			attackOn++;
		if (attackOn == 30) {
			attackOn = 0;
			swordC.enabled = false;
		}
	}
	public void PlayVideo(){
		audio2.Play ();
	}
	public int hp = 100;
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "PUNCH") {
			hp -= 10;
			animator.SetTrigger ("IsHit");

			if (hp <= 0) {
				PlayerDie ();
			}
		}
	}
	void PlayerDie(){
		//Debug.Log ("Player Die!");
	}
}
