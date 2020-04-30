using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hunterBehaviour : MonoBehaviour {

	public float speed = 2.5f;
	Rigidbody2D myBody;
	Transform myTrans;
	float myWidth, myHeight;
	bool bai;
	public GameObject target;
	public GameObject Slider;
	public GameObject Fill;
	public Animator myAnim;
	//player pSc;
	bool captured;

	void Start ()
	{
		myTrans = this.transform;
		myBody = this.GetComponent<Rigidbody2D>();
		SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
		myAnim = this.GetComponent<Animator> ();
		myWidth = mySprite.bounds.extents.x;
		myHeight = mySprite.bounds.extents.y;
		target = GameObject.FindGameObjectWithTag ("Player");
		captured = player.captured;
	}

	void FixedUpdate ()
	{
		Debug.Log (captured);
		//Use this position to cast the isGrounded/isBlocked lines from
		//Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() + Vector2.down;
		//Check to see if there's ground in front of us before moving forward
		//Debug.DrawLine(lineCastPos, lineCastPos + new Vector2(0.5f, 1f));
		//bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + new Vector2(0.5f, 1f), enemyMask);

		//Always move forward
		//float d = Vector2.Distance(myTrans.position, player.transform.position);

		if (!captured) {
			if (myTrans.position.x < target.transform.position.x -0.5f) {
				myAnim.SetBool ("OnSheeto", false);
				Vector2 myVel = myBody.velocity;
				myVel.x = myTrans.right.x * speed;
				myBody.velocity = myVel;
				transform.localScale = new Vector3 (0.31f, 0.31f, 0.31f);
			} else if (myTrans.position.x > target.transform.position.x +0.5f) {
				myAnim.SetBool ("OnSheeto", false);
				Vector2 myVel = myBody.velocity;
				myVel.x = myTrans.right.x * speed * -1;
				myBody.velocity = myVel;
				transform.localScale = new Vector3 (-0.31f, 0.31f, 0.31f);
			}
			captured = player.captured;
		} else { 
			transform.localScale = new Vector3 (-0.31f, 0.31f, 0.31f);
			myAnim.SetBool ("OnSheeto", true);
			captured = player.captured;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player" && bai == false) {
			bai = true;
			gameObject.SetActive (false);
			Invoke ("Bai", 3.4f);
		}
	}

	void Bai(){
		transform.localScale = new Vector3(-0.31f,0.31f,0.31f);
		gameObject.SetActive (true);
	}

}
