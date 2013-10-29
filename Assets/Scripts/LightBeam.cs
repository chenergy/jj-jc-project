using UnityEngine;
using System.Collections;

public class LightBeam : MonoBehaviour
{
	[HideInInspector]
	public Vector3 	direction;
	public float	speed = 10.0f;
	public Vector3	newCharacterOffset;
	// Use this for initialization
	void Start ()
	{
		Invoke("ResetLayer", 0.5f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.position += this.direction * this.speed * Time.deltaTime;
	}
	
	void OnTriggerEnter( Collider other ){
		if (other.tag == "Obstacle"){
			GameObject prefab = Resources.Load("Character", typeof(GameObject)) as GameObject;
			GameObject newCharacter = GameObject.Instantiate(prefab, this.transform.position + this.newCharacterOffset, Quaternion.identity) as GameObject;
			Camera.main.GetComponent<CameraMovement>().target = newCharacter;
			//GameObject.Destroy(this.gameObject);
			this.enabled = false;
		}
	}
	
	private void ResetLayer(){
		this.gameObject.layer = 0;
	}
}

