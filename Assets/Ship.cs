using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public float Acceleration = 10;
	public float RotationSpeed = 5;
	public float FireRate = 0.2f;

	public ParticleSystem Explosion;
	public ParticleSystem Thrust;

	public GameObject BulletPrefab;

	Vector3 speed;
	float rotation;

	float lastFire;

	bool dead = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Application.LoadLevel(Application.loadedLevel);
			}
			return;
		}

		Thrust.enableEmission = Input.GetKey(KeyCode.UpArrow);

		var orientation = Quaternion.AngleAxis(rotation, Vector3.forward);
		var direction = orientation * Vector3.right;
		if (Input.GetKey(KeyCode.UpArrow)) {
			speed += Acceleration * Time.deltaTime * direction;
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			rotation -= RotationSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			rotation += RotationSpeed * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			Fire();
		}

		transform.rotation = orientation;
		transform.position += speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<Asteroid>() != null) {
			Die();
		}
	}

	void Die() {
		if (dead) return;
		dead = true;
		GetComponentInChildren<Renderer>().enabled = false;
		Explosion.Play();
		Thrust.enableEmission = false;
	}

	void Fire() {
		if (Time.time - lastFire < FireRate) return;
		GetComponent<AudioSource>().Play();
		lastFire = Time.time;
		var bulletGo = Instantiate(BulletPrefab);
		var bullet = bulletGo.GetComponent<Bullet>();
		bullet.Fire(transform.position, rotation);
	}
}
