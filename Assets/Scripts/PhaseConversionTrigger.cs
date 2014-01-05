using UnityEngine;
using System.Collections;

public class PhaseConversionTrigger : MonoBehaviour
{
	public Vector3 direction;
	
	void Start(){
		this.direction = this.direction.normalized;
	}
	
	void OnTriggerEnter( Collider other ){
		if (other.tag == "Player"){
			GameObject prefab = Resources.Load("LightBeam", typeof(GameObject)) as GameObject;
			GameObject newLightBeam = GameObject.Instantiate(prefab, this.transform.position, Quaternion.identity) as GameObject;
			newLightBeam.GetComponent<LightBeam>().direction = this.direction;
			Camera.main.GetComponent<CameraMovement>().target = newLightBeam;
			GameObject.Destroy(other.gameObject);
		}
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, this.GetComponent<BoxCollider>().size);
		Gizmos.DrawRay(this.transform.position, this.direction);
	}
}

