using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	public int hitToBreak = 1;
	public List<Color> colors;
    private ParticleSystem ps;
	public SpriteRenderer sprite;
    public int test = 0;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void Start(){
		GameplayManager.Instance.OnNewBlockCreated (this);

		if (hitToBreak > colors.Count) {
			hitToBreak = colors.Count;
		}

		UpdateColor ();
	}

	void OnDestroy(){
		GameplayManager.Instance.OnBlockDestroyed (this);
	}
    public void Break()
    {
        sprite.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        DestroyObject(gameObject, 1.5f);
        GameplayManager.Instance.OnBlockDestroyed(this);
       
    }
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			hitToBreak--;
            ParticleSystem.MainModule main = ps.main;
            main.startColor = sprite.color;
            ps.Play();
			if (hitToBreak <= 0) {
                Break();
                Database.Instance.achievementdata.IncreaseBlockDestroyed();
                Debug.Log("BlockDestroyed: " + Database.Instance.achievementdata.BlockDestroyed);
                //test++;
                //Debug.Log(test);
            } else {
				UpdateColor ();
			}
		}
	}

	public void UpdateColor(){
		sprite.color = colors [hitToBreak - 1];
	}
}
