using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
	// Rotate the box by the Vector amount every frame
	void Update ()
	{
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}
}
