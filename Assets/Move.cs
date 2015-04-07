using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	int score = 0;
	public static string ScoreText = "Score = 0";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);

	}

	void OnCollisionEnter(){
		score++;
		ScoreText = "Score = " + score;
		//print (ScoreText);
	}
}
