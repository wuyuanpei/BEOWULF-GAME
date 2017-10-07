using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class Anim{
	public AnimationClip idle;
	public AnimationClip run;
	public AnimationClip attack;
	public AnimationClip down;
	public AnimationClip damage;
}
	
public class ZombieControl : MonoBehaviour {
	public Anim anim;
	public Animation _animation;
	private Transform monsterTr;
	private Transform playerTr;
	private NavMeshAgent nvAgent;

	public enum MonsterState
	{
		idle,trace,attack,die
	}
	public MonsterState monsterState = MonsterState.idle;

	public float traceDist = 10.0f;
	public float attackDist = 2.0f;
	private bool isDie = false;
	public GameObject bloodEffect, bloodDecal;
	private CharacterControl c;
	// Use this for initialization
	void Start () {
		c = GameObject.Find ("Character Warrior").GetComponent<CharacterControl> ();
		gameUI = GameObject.Find ("GameUI").GetComponent<GameUI> ();
		_animation = GetComponentInChildren<Animation> ();
		_animation.clip = anim.idle;
		_animation.Play ();

		monsterTr = GetComponent<Transform> ();
		playerTr = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		nvAgent = GetComponent<NavMeshAgent> ();
		//nvAgent.destination = playerTr.position;
		StartCoroutine(this.CheckMonsterState());
		StartCoroutine (this.MonsterAction ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator CheckMonsterState(){
		while (!isDie) {
			yield return new WaitForSeconds (2.0f);
			float dist = Vector3.Distance (playerTr.position, monsterTr.position);
			if (dist <= attackDist) {
				monsterState = MonsterState.attack;
			} else if (dist <= traceDist) {
				monsterState = MonsterState.trace;
			} else {
				monsterState = MonsterState.idle;
			}
		}
	}
	IEnumerator MonsterAction(){
		while (!isDie) {
			switch (monsterState) {
			case MonsterState.idle:
				nvAgent.Stop ();
				_animation.CrossFade (anim.idle.name, 0.2f);
				break;
			case MonsterState.trace:
				nvAgent.destination = playerTr.position;
				nvAgent.Resume();
				_animation.CrossFade (anim.run.name, 0.2f);
				break;
			case MonsterState.attack:
				_animation.CrossFade (anim.attack.name, 0.2f);
				break;
			}
			yield return null;
		}
	}
	private GameUI gameUI;
	public int hp = 100;

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "SWORD") {
			hp -= 50;
			//Debug.Log ("怪兽血量" + hp.ToString ());
			CreateBloodEffect(coll.transform.position);
			c.PlayVideo ();
			if (hp <= 0) {
				gameUI.Display (1);
				_animation.CrossFade (anim.down.name, 0.2f);
				StopAllCoroutines ();
				isDie = true;
				monsterState = MonsterState.die;
				nvAgent.Stop ();
				gameObject.GetComponentInChildren<CapsuleCollider> ().enabled = false;
				foreach(Collider colls in gameObject.GetComponentsInChildren<SphereCollider>()){
					colls.enabled = false;
				}
			}
		}
	}
	void CreateBloodEffect(Vector3 pos){
		GameObject blood1 = (GameObject)Instantiate (bloodEffect, pos, Quaternion.identity);
		Destroy (blood1, 2.0f);
	}
}
