using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public float MaxSpeed = 20;
	public float ExplodeSpeedScale = 10;
	public float ExplodeTime = 0.3f;

	Vector3 speed;
	Vector3 rotation;

	int generation = 0;

	// Use this for initialization
	void Start () {
		speed = Random.onUnitSphere * MaxSpeed;
		speed.z = 0;
		rotation = new Vector3(Random.Range(10, 90), Random.Range(10, 90), Random.Range(10, 90));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += speed * Time.deltaTime;
		transform.rotation *= 
			Quaternion.AngleAxis(rotation.x * Time.deltaTime, Vector3.forward)
				* Quaternion.AngleAxis(rotation.y * Time.deltaTime, Vector3.right)
				* Quaternion.AngleAxis(rotation.z * Time.deltaTime, Vector3.up);
	}

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<Bullet>()) {
			Explode();
			Destroy(other.gameObject);
		}
	}

	void Explode() {
		if (generation < 2) {
			for (int i=0;i<3;++i) {
				var newAsteroid = Instantiate(gameObject).GetComponent<Asteroid>();
				newAsteroid.transform.localScale /= 2;
				newAsteroid.generation = generation + 1;
				newAsteroid.StartCoroutine(newAsteroid.Deaccelerate());
			}
		}
		FindObjectOfType<Score>().AddScore(5);
		Destroy(gameObject);
	}

	IEnumerator Deaccelerate() {
		yield return new WaitForEndOfFrame();
		var startSpeed = speed;
		var startTime = Time.time;
		float scale = ExplodeSpeedScale;
		float delta = ExplodeTime;
		while (Time.time - startTime < delta) {
			float a = (Time.time - startTime) / delta;
			var s = Tween.CubicEaseOut(a, scale, 1, 1);
			speed = startSpeed * s;
			yield return new WaitForEndOfFrame();
		}
		speed = startSpeed;
	}
}
