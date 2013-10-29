using UnityEngine;
using System.Collections;

public class ReflectionTrigger : MonoBehaviour
{
	void OnTriggerEnter( Collider other ){
		if (other.tag == "LightBeam"){
			Vector3 oldDirection = other.GetComponent<LightBeam>().direction;
			Vector3 newDirection = this.GetNewDirection(oldDirection);
			Camera.main.GetComponent<CameraMovement>().target = this.CreateBeam(other.transform.position, newDirection);
			//GameObject.Destroy(other.gameObject);
			other.GetComponent<LightBeam>().enabled = false;
		}
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(this.transform.position, Vector3.one);
	}
	
	private Vector3 GetNewDirection(Vector3 incidence){
		return Vector3.down;
	}
	
	private GameObject CreateBeam(Vector3 position, Vector3 direction){
		GameObject prefab = Resources.Load("LightBeam", typeof(GameObject)) as GameObject;
		GameObject newLightBeam = GameObject.Instantiate(prefab, position, Quaternion.identity) as GameObject;
		newLightBeam.GetComponent<LightBeam>().direction = direction;
		return newLightBeam;
	}
}

