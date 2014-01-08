using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider))]
public class BasicReflectionTrigger : MonoBehaviour
{

	public bool isRotatable;
	static float dTheta = 1.0;

	protected virtual void Start(){
	}

	protected virtual void Update(){
	}

	void OnTriggerEnter( Collider other ){
		if (other.tag == "LightBeam"){
			Vector3 oldDirection = other.GetComponent<LightBeam>().direction;
			Vector3 reflectionPlane = this.transform.TransformDirection (Vector3.down);
			Vector3 newDirection = this.GetNewDirection(oldDirection, reflectionPlane);
			//Camera.main.GetComponent<CameraMovement>().target = this.CreateBeam(other.transform.position, newDirection);
			this.CreateBeam(other.transform.position, newDirection);
			//other.GetComponent<LightBeam>().enabled = false;
			GameObject.Destroy (other.gameObject);
		}
	}

	void OnTriggerStay( Collider other){
		if (other.tag == "Tongue") {
			this.transform.Rotate (0, 0, dTheta);
			yield return;
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, this.GetComponent<BoxCollider>().size);
		Gizmos.DrawRay (this.transform.position, this.transform.TransformDirection (Vector3.down));
	}
	
	private Vector3 GetNewDirection(Vector3 incidence, Vector3 reflectionPlane){
		Vector3 localIncidence = this.transform.InverseTransformDirection (incidence);
		float angle = Mathf.Acos( Vector3.Dot( incidence, reflectionPlane ) / (incidence.magnitude * reflectionPlane.magnitude) );
		float v_perp = Mathf.Cos (angle) * incidence.magnitude;

		return this.transform.TransformDirection( new Vector3(localIncidence.x, -1.0f * Mathf.Sign(reflectionPlane.y) * v_perp, 0.0f) );
	}
	
	private void CreateBeam(Vector3 position, Vector3 direction){
		GameObject prefab = Resources.Load("LightBeam", typeof(GameObject)) as GameObject;
		GameObject newLightBeam = GameObject.Instantiate(prefab, position, Quaternion.identity) as GameObject;
		newLightBeam.GetComponent<LightBeam>().direction = direction;
		//return newLightBeam;
	}
}

