using UnityEngine;
using System.Collections;

public class PhaseAcceptor : MonoBehaviour
{
	public int thresholdBeams = 1;

	[HideInInspector]
	public int currentBeams = 0;

	private bool isEnabled = true;

	void Start(){
	}

	void Update(){
		if (this.currentBeams == this.thresholdBeams && this.isEnabled) {
			GameObject prefab = Resources.Load("Character", typeof(GameObject)) as GameObject;
			GameObject newCharacter = GameObject.Instantiate(prefab, this.transform.position + new Vector3(0,2,0), Quaternion.identity) as GameObject;
			Camera.main.GetComponent<CameraMovement>().target = newCharacter;
			foreach (GameObject lightbeam in GameObject.FindGameObjectsWithTag("LightBeam"))
				//lightbeam.GetComponent<LightBeam>().enabled = false;
				GameObject.Destroy (lightbeam);
			this.currentBeams = 0;
			//this.isEnabled = false;
			//Invoke ("Reset", 0.5f);
		}
	}

	void OnTriggerEnter( Collider other ){
		if (other.tag == "LightBeam") {
			this.currentBeams++;
		}
	}

	void OnTriggerExit( Collider other ){
		if (other.tag == "LightBeam") {
			this.currentBeams--;
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, this.GetComponent<BoxCollider>().size);
	}
}

