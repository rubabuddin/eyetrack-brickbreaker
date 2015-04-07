using UnityEngine;
using System.Collections;

public class Balls : MonoBehaviour {
	float sx;
	float sy;

	public static int ballCount = 0;

	//int ballExists = 0;

	// Use this for initialization
	void Start () {
		sx = Random.Range (0, 2) == 0 ? -1 : 1;
		sy = Random.Range (0, 2) == 0 ? -1 : 1;

		rigidbody.velocity = new Vector3 (Random.Range (5, 10) * sx, Random.Range (5, 10) * sy, 0);
	}
	
	// Update is called once per frame

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "Catcher")
		{	
			ballCount++;
			rigidbody.transform.position = new Vector3 (Random.Range (-1, 5), 5, 0);
		}	
	}
}