/*/
* Script by Devin Curry
* www.Devination.com
* www.youtube.com/user/curryboy001
* Please like and subscribe if you found my tutorials helpful :D
* Google+ Community: https://plus.google.com/communities/108584850180626452949
* Twitter: https://twitter.com/Devination3D
* Facebook Page: https://www.facebook.com/unity3Dtutorialsbydevin
/*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class player : MonoBehaviour
{
	public float speed = 10, jumpVelocity = 10;
	float hInput = 0;
	public int health = 3;
	public float invincibleTimeAfterHurt = 2;
	public float invincibleTime = 10;

	//Booleans
	public static bool captured = false;
	public bool canMoveInAir = true, isGrounded = false, jumping, canDoubleJump, win;

	public int levelNum;
	public static int curScene;
	public static int prevScene;

	//Referencias Locales
	public LayerMask playerMask;
	Transform myTrans, tagGround;
	Rigidbody2D myBody;
	Animator anim;

	public Text countText;
	public Text shadow;

	public AudioClip JumpSound;
	public AudioClip WinSound;
	public AudioClip LoseSound;

	void Start ()
	{
		myBody = this.GetComponent<Rigidbody2D>();
		myTrans = this.transform;
		captured = false;
		tagGround = GameObject.Find (this.name + "/GroundCheck").transform;
		anim = this.GetComponent<Animator> ();
		levelNum = SceneManager.sceneCountInBuildSettings;
		curScene = SceneManager.GetActiveScene ().buildIndex;

	}

	void Update()
	{
		anim.SetBool ("OnGround", isGrounded);
		//anim.SetBool ("Win", win);
		anim.SetFloat ("Speed", Mathf.Abs (myBody.velocity.x));
		anim.SetInteger ("Lifes", health);

		//Dirección de los sprites
		if (Input.GetAxis ("Horizontal") > 0.01f) {
			transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
		}
		if (Input.GetAxis ("Horizontal") < -0.01f) {
			transform.localScale = new Vector3 (-0.25f, 0.25f, 0.25f);
		}

	}

	void FixedUpdate ()
	{
		Debug.Log (Input.GetAxisRaw("Horizontal"));

		isGrounded = Physics2D.Linecast (myTrans.position, tagGround.position, playerMask);

		if (!captured){
			#if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
			Move(Input.GetAxisRaw("Horizontal"));
			if(Input.GetButtonDown("Jump"))
				Jump();
			if(Input.GetButtonDown("Fire1"))
				Attack();
			if(Input.GetButtonDown("Fire2"))
				Charge();
			#else
			Move (hInput);
			#endif
		}
	}

	void Move(float horizonalInput)
	{
		if(!canMoveInAir && !isGrounded)
			return;

		Vector2 moveVel = myBody.velocity;
		moveVel.x = horizonalInput * speed;
		myBody.velocity = moveVel;
	}

	public void Jump()
	{
		AudioSource audio = GetComponent<AudioSource> ();
		audio.clip = JumpSound;
		audio.loop = false;
		if (isGrounded) {
			audio.Play ();
			jumping = true;
			myBody.velocity += jumpVelocity * Vector2.up;
			canDoubleJump = true;
		} else if (canDoubleJump) {
			audio.Play ();
			jumping = true;
			canDoubleJump = false;
			myBody.velocity = new Vector2 (myBody.velocity.x, 0);
			myBody.velocity += jumpVelocity * Vector2.up;
		}
	}

	public void Attack()
	{
		anim.SetTrigger ("Attacking");
	}

	public void Charge ()
	{
		anim.SetTrigger ("Charging");
	}

	public void StartMoving(float horizonalInput)
	{
		hInput = horizonalInput;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (win == false){
			if (col.gameObject.name == "EndOfLevel") {
				win = true;
				AudioSource audio = GetComponent<AudioSource> ();
				Camera.main.GetComponent<AudioSource>().Pause();
				audio.clip = WinSound;
				audio.loop = false;
				audio.Play ();
				Invoke ("NextLvl", 2);
			}
		}
	}

	void NextLvl()
	{
		if (curScene < levelNum) {
			SceneManager.LoadScene (curScene+1);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Enemies") {
			Hurt ();
			Debug.Log ("H: " + health);
		}
		if (col.gameObject.tag == "Hunter" && !captured) {
			anim.SetTrigger ("OnHunter");
			captured = true;
			AudioSource audio = GetComponent<AudioSource> ();
			Camera.main.GetComponent<AudioSource>().Stop();
			audio.clip = LoseSound;
			audio.loop = false;
			audio.Play ();
			Invoke ("Restart", 4.7f);
		}
	}

	void Hurt (){
		health --;
		SetCountText ();
		if (health <= 0) {
			AudioSource audio = GetComponent<AudioSource> ();
			Camera.main.GetComponent<AudioSource>().Stop();
			audio.clip = LoseSound;
			audio.loop = false;
			audio.Play ();
			//anim.SetTrigger ("OnEnemy");
			//Invoke ("Restart", 2.6f);
			Restart ();
		} else {
			StartCoroutine (HurtBlinker ());
		}
	}

	void Restart (){
		SceneManager.LoadScene (1);
	}

	IEnumerator HurtBlinker(){
		//Ignore collisions
		int enemyLayer = LayerMask.NameToLayer ("Enemies");
		int playerLayer = LayerMask.NameToLayer ("Player");
		Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer);

		//Start Blinking
		anim.SetLayerWeight (1, 1);

		//Wait n secs
		yield return new WaitForSeconds (invincibleTimeAfterHurt);

		//Stop Blinking
		anim.SetLayerWeight (1, 0);

		//Don't ignore anymore
		Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer, false);
	}

	void SetCountText(){
		countText.text = health.ToString () + "x";
		shadow.text = health.ToString () + "x";
	}

}