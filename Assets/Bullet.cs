using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float Speed = 50;

	Vector3 speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += speed * Time.deltaTime;
	}

	public void Fire (Vector3 position, float rotation)
	{
		transform.position = new Vector3(position.x, position.y, transform.position.z);
		speed = Quaternion.AngleAxis(rotation, Vector3.forward) * Vector3.right * Speed;
	}
}
