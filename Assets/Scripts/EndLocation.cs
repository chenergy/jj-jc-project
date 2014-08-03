using UnityEngine;
using System.Collections;

public class EndLocation : MonoBehaviour
{
	public string nextLevel;
	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter(Collider other){
		if(other.GetComponent<PlayerCharacter>() != null) {
			Application.LoadLevel(nextLevel);
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(this.transform.position, Vector3.one);
	}
}

