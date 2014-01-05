using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour
{
	void OnTriggerEnter( Collider other ){
		if (other.tag == "LightBeam" || other.tag == "Player"){
			int active = GameObject.FindGameObjectsWithTag ("LightBeam").Length + GameObject.FindGameObjectsWithTag ("Player").Length;
			Debug.Log (active);
			if (active == 1) {
				GameObject prefab = Resources.Load ("Character", typeof(GameObject)) as GameObject;
				GameObject newCharacter = GameObject.Instantiate (prefab, GameObject.FindGameObjectWithTag("Respawn").transform.position, Quaternion.identity) as GameObject;
			}
			GameObject.Destroy(other.gameObject);
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, this.GetComponent<BoxCollider>().size);
	}
}

