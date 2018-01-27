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
		//Invoke("ResetLayer", 0.5f);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		this.transform.position += this.direction * this.speed * Time.fixedDeltaTime;
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, this.GetComponent<BoxCollider>().size);
	}

	private void ResetLayer(){
		this.gameObject.layer = 0;
	}
}

