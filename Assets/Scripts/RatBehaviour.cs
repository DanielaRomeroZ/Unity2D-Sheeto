﻿/*/
* Script by Devin Curry
* www.Devination.com
* www.youtube.com/user/curryboy001
* Please like and subscribe if you found my tutorials helpful :D
* Google+ Community: https://plus.google.com/communities/108584850180626452949
* Twitter: https://twitter.com/Devination3D
* Facebook Page: https://www.facebook.com/unity3Dtutorialsbydevin
/*/
using UnityEngine;
using System.Collections;

public class RatBehaviour : MonoBehaviour
{
	public LayerMask enemyMask;
	public float speed = 1;
	Rigidbody2D myBody;
	Transform myTrans;
	float myWidth, myHeight;

	void Start ()
	{
		myTrans = this.transform;
		myBody = this.GetComponent<Rigidbody2D>();
		SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
		myWidth = mySprite.bounds.extents.x;
		myHeight = mySprite.bounds.extents.y;
	}

	void FixedUpdate ()
	{
		//Use this position to cast the isGrounded/isBlocked lines from
		Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * (myWidth-0.4f) + Vector2.up * (myHeight-0.35f);
		//Check to see if there's ground in front of us before moving forward
		Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down+new Vector2(0f,.1f));
		bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down+new Vector2(0f,.1f), enemyMask);
		//Check to see if there's a wall in front of us before moving forward
		Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .25f);
		bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .25f, enemyMask);

		//If theres no ground, turn around. Or if I hit a wall, turn around
		if(!isGrounded || isBlocked)
		{
			Vector3 currRot = myTrans.eulerAngles;
			currRot.y += 180;
			myTrans.eulerAngles = currRot;
		}

		//Always move forward
		Vector2 myVel = myBody.velocity;
		myVel.x = -myTrans.right.x * speed;
		myBody.velocity = myVel;
	}

	public void Damage(){
		Destroy (gameObject);
	}

}