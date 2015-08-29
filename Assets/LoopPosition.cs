using UnityEngine;
using System.Collections;

public class LoopPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var height = Camera.main.orthographicSize;
		var width = height * Camera.main.aspect;

		if (transform.position.x > width) {
			transform.position = new Vector3(-width, transform.position.y, transform.position.z);
		}
		if (transform.position.x < -width) {
			transform.position = new Vector3(width, transform.position.y, transform.position.z);
		}
		if (transform.position.y > height) {
			transform.position = new Vector3(transform.position.x, -height, transform.position.z);
		}
		if (transform.position.y < -height) {
			transform.position = new Vector3(transform.position.x, height, transform.position.z);
		}
	}
}
