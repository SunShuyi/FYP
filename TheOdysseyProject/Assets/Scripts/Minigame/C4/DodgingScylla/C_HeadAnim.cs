using UnityEngine;
using System.Collections;

public class C_HeadAnim : MonoBehaviour {

	public GameObject theShadow;
	public GameObject theSplash;
	Animator shadowAnim;
	Animator splashAnim;

	CircleCollider2D atkPoint;

	// Use this for initialization
	void Start () {
		atkPoint = gameObject.GetComponentInParent <CircleCollider2D> ();
		shadowAnim = theShadow.GetComponent<Animator> ();
		splashAnim = theSplash.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{}

	void Attack ()
	{ atkPoint.enabled = true; }

	void Splash ()
	{ splashAnim.SetBool ("isSplashing", true); }

	void ShadowRising()	{
		shadowAnim.SetBool("isRisingShadow", true);
		shadowAnim.SetBool("isAttackingShadow", false);
		shadowAnim.SetBool("isRetractingShadow", false);
	}

	void ShadowAttking() {
		shadowAnim.SetBool("isRisingShadow", false);
		shadowAnim.SetBool("isAttackingShadow", true);
		shadowAnim.SetBool("isRetractingShadow", false);
	}

	void ShadowRetract() {
		shadowAnim.SetBool("isRisingShadow", false);
		shadowAnim.SetBool("isAttackingShadow", false);
		shadowAnim.SetBool("isRetractingShadow", true);
	}
}
