using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public GUIText countText;
	public GUIText winText;
	public GUIText schmooText;
	private int count;
	private float rNum;
	private float resetTimer;

	void Start ()
	{
		count = 0;
		resetTimer = 0.0f;	// The "f" is required here because resetTimer is a float-type
		SetCountText ();
		winText.text = "";
		schmooText.text = "";
	}

	void FixedUpdate ()
	{
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rigidbody.AddForce (movement * speed * Time.deltaTime);

		// SchmooText wiping code

		// I'm guessing that incrementing the resetTimer using Time.deltaTime (time per frame) is a bad idea, because the
		// timing on the wiping is really erratic
		resetTimer = resetTimer + Time.deltaTime;

		if (resetTimer > 1.5) 
		{
			schmooText.text = "";
			resetTimer = 0.0f;
		}
	}

	void OnTriggerEnter (Collider other) 
	{
		if (other.gameObject.tag == "Pickup") 
		{
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	// So if you turn the other object into a trigger, it falls through the map, and since I want to interact with it (make it roll),
	// I had to use a different function entirely (since OnTriggerEnter basically gets called when you go inside an object).

	void OnCollisionEnter (Collision collision)
	{
		// OnCollisionEnter requires a Collision class object, which contains all the information involved with the collision,
		// including what you just collided with - thus, what the comparison you get below
		if (collision.collider.gameObject.tag == "Schmoo")
		{
			// Random.value generates a random number between 0.0 and 1.0
			rNum = Random.value;
			if (rNum < 0.2)
			{
				schmooText.text = "I HAVE YOU NOW";
			}
			else if (rNum < 0.4)
			{
				schmooText.text = "I KNEW IT WAS YOU";
			}
			else if (rNum < 0.6)
			{
				schmooText.text = "I SHOULD'VE KNOWN";
			}
			else if (rNum < 0.8)
			{
				schmooText.text = "GIVE ME EXCALIBUR";
			}
			else
			{
				schmooText.text = "VICTORY IS MINE";
			}
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString();
		if (count >= 12) 
		{
			winText.text = "WIN?!";
		}
				
	}
}
