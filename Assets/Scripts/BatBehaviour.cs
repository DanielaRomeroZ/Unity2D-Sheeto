/*/
* Script by Devin Curry
* Modified by Jhazier Pineda
/*/
using UnityEngine;
using System.Collections;

public class BatBehaviour : MonoBehaviour
{
	public LayerMask enemyMask;
	public float speed = 2;
	public float distance = 4;
	float myWidth, myHeight;
	float initPos;
	float newPos;
	Rigidbody2D myBody;
	Transform myTrans;

	void Start ()
	{
		myTrans = this.transform;
		initPos = myTrans.position.x;
		myBody = this.GetComponent<Rigidbody2D>();
		SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
		myWidth = mySprite.bounds.extents.x;
		myHeight = mySprite.bounds.extents.y;
	}

	void FixedUpdate ()
	{
		newPos = myTrans.position.x;
		Vector2 lineCastPos = myTrans.position.toVector2 () - myTrans.right.toVector2 () * (myWidth/2) + Vector2.down * (myHeight-.2f);
		Debug.DrawLine (lineCastPos, lineCastPos - myTrans.right.toVector2 () * .05f);
		bool isBlocked = Physics2D.Linecast (lineCastPos, lineCastPos - myTrans.right.toVector2 () * .05f, enemyMask);

		if( isBlocked || newPos > (initPos+distance) || newPos < (initPos-distance) )
		{
			Vector3 currRot = myTrans.eulerAngles;
				currRot.y += 180;
			myTrans.eulerAngles = currRot;
		}

		Vector2 myVel = myBody.velocity;
		myVel.x = -myTrans.right.x * speed;
		myBody.velocity = myVel;
	}

	public void Damage(){
		Destroy (gameObject);
	}
}