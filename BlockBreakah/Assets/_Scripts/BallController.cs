using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class BallController : MonoBehaviour {
	public float ballSpeed;
	public float forceMultipler;
    public TrailRenderer trailrenderer;
	public Transform spritePivot;

	private Rigidbody2D rgBody;
	private Animator anim;

	void Awake(){
		rgBody = GetComponent<Rigidbody2D> ();	
		anim = GetComponent<Animator> ();
        trailrenderer = GetComponent<TrailRenderer>();
	}

	void Start () {
		rgBody.velocity = new Vector2 (1, 1).ScaleTo (ballSpeed);
	}

	void Update(){
		rgBody.velocity = rgBody.velocity.ScaleTo (ballSpeed);
		spritePivot.localRotation = Quaternion.Euler(
			0,
			0,
			Vector2.SignedAngle(Vector2.up, rgBody.velocity)
		);
	}

	void OnCollisionEnter2D(Collision2D coll){
		anim.SetTrigger ("OnHit");
        trailrenderer.material.color = Color.blue;

        Camera.main.GetComponent<CameraShaker>().shakeMagnitude = 20;
		if (coll.gameObject.tag == "Paddle") {
			float offsetX = transform.position.x - coll.transform.position.x;
			rgBody.AddForce (Vector2.right * offsetX * forceMultipler);
            GameplayManager.Instance.OnBallHitPaddle();
		}else if(coll.gameObject.tag == "Wall")
        {
            GameplayManager.Instance.WallFlash();
        }else if(coll.gameObject.tag == "BLock")
        {
            GameplayManager.Instance.OnBallHitBlock();
        }
	}

	void OnCollisionExit2D(Collision2D coll){
		if (Mathf.Abs(rgBody.velocity.y) < 200f) {
			rgBody.velocity = rgBody.velocity.WithY (Mathf.Sign (rgBody.velocity.y) * 200f).ScaleTo (ballSpeed);
		}
	}
}
