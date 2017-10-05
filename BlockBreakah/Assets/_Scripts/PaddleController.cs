using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {
	public float paddleSpeed;

	private Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();
	}

	void Update () {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		Vector3 targetPos = transform.position.WithX (Mathf.Clamp (mousePos.x, -521.2f, 521.2f));

		if ((targetPos - transform.position).magnitude > paddleSpeed * Time.deltaTime) {
			targetPos = 
				transform.position 
				+ (targetPos - transform.position).ScaleTo (paddleSpeed * Time.deltaTime);
		}

		transform.position = targetPos;
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			anim.SetTrigger ("OnHit");
		}
	}
}
