using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<UnityEngine.UI.Text>().text = score.ToString();
	}

	public void AddScore(int s) {
		score += s;
	}
}
