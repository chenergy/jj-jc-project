using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	[HideInInspector]
	public GameObject 	target;
	public float		moveSpeed = 5.0f;
	// Use this for initialization
	void Start ()
	{
		this.target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//this.transform.position = this.target.transform.position + new Vector3(0, 1, -10);
		this.transform.position = Vector3.Lerp(this.transform.position, this.target.transform.position + new Vector3(0, 1, -10), Time.deltaTime * this.moveSpeed);
	}
}

