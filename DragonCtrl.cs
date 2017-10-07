using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class Anim2{
	public AnimationClip idle;
	public AnimationClip attack1;
	public AnimationClip attack2;
	public AnimationClip breathfire;
	public AnimationClip die;
}
public class DragonCtrl : MonoBehaviour {
	public Anim2 anim;
	private Animation _animation;
	private Transform monsterTr;
	private Transform playerTr;
	private NavMeshAgent nvAgent;
	public enum MonsterState
	{
		idle,trace,attack,die
	}
	public MonsterState monsterState = MonsterState.idle;
	private CharacterControl2 c;
	// Use this for initialization
	void Start () {
		hp = 100;
		_animation = GetComponentInChildren<Animation> ();
		_animation.clip = anim.idle;
		_animation.Play ();
		c = GameObject.Find ("Character Warrior").GetComponent<CharacterControl2> ();
		monsterTr = GetComponent<Transform> ();
		playerTr = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		nvAgent = GetComponent<NavMeshAgent> ();
		StartCoroutine(this.CheckMonsterState());
		StartCoroutine (this.MonsterAction ());
	}
	public float traceDist = 10.0f;
	public float attackDist = 2.0f;
	private bool isDie = false;
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
	public int hp;

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "SWORD") {
			hp -= 8;
			//Debug.Log ("怪兽血量" + hp.ToString ());
			CreateBloodEffect(coll.transform.position);
			c.PlayVideo ();
			if (hp <= 0) {
				_animation.CrossFade (anim.die.name, 0.2f);
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
	IEnumerator died(){
		yield return new WaitForSeconds (1.0f);
		Application.LoadLevel ("Transition3");
	}
	public GameObject bloodEffect, bloodDecal;
	void CreateBloodEffect(Vector3 pos){
		GameObject blood1 = (GameObject)Instantiate (bloodEffect, pos, Quaternion.identity);
		Destroy (blood1, 2.0f);
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
				_animation.CrossFade (anim.breathfire.name, 0.2f);
				break;
			case MonsterState.attack:
					_animation.CrossFade (anim.attack1.name, 0.2f);
				break;
			}
			yield return null;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
