using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider))]
public class BasicReflectionTrigger : MonoBehaviour
{
	public GameObject lightBeam;
	public bool isRotatable;
	static float dTheta = 1.0f;
    private Vector3 reflectionPlane;
    public float angle;

	protected virtual void Start(){
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.reflectionPlane = Quaternion.Euler(0, 0, angle) * Vector3.down;
	}

	protected virtual void Update(){
	}

	void OnTriggerEnter( Collider other ){
		if (other.tag == "LightBeam"){
 			Vector3 inDirection = other.GetComponent<LightBeam>().direction;
			//Vector3 newDirection = this.GetNewDirection(oldDirection, this.reflectionPlane);
            Vector3 newDirection = Vector3.Reflect(inDirection, this.reflectionPlane);
			this.CreateBeam(other.transform.position, newDirection);
			GameObject.Destroy (other.gameObject);
		} 
        if (other.tag == "RotatorBeam")
        {
            if (this.isRotatable)
            {
                this.transform.Rotate(Vector3.forward, dTheta);
                this.angle += dTheta;
                this.reflectionPlane = Quaternion.Euler(0, 0, angle) * Vector3.down;
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "RotatorBeam")
        {
            if (this.isRotatable)
            {
                this.transform.Rotate(Vector3.forward, dTheta);
                this.angle += dTheta;
                this.reflectionPlane = Quaternion.Euler(0, 0, angle) * Vector3.down;
            }
        }
    }


    void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, this.GetComponent<BoxCollider>().size);
//		Gizmos.DrawRay (this.transform.position, this.transform.TransformDirection (Vector3.down));
        Gizmos.DrawRay(this.transform.position, this.reflectionPlane);
	}
	
	private Vector3 GetNewDirection(Vector3 incidence, Vector3 reflectionPlane){
		Vector3 localIncidence = this.transform.InverseTransformDirection (incidence);
		float angle = Mathf.Acos( Vector3.Dot( incidence, reflectionPlane ) / (incidence.magnitude * reflectionPlane.magnitude) );
		float v_perp = Mathf.Cos (angle) * incidence.magnitude;
		return this.transform.TransformDirection( new Vector3(localIncidence.x, -1.0f * Mathf.Sign(reflectionPlane.y) * v_perp, 0.0f) );
	}
	
	private void CreateBeam(Vector3 position, Vector3 direction){
		//GameObject prefab = Resources.Load("LightBeam", typeof(GameObject)) as GameObject;
		GameObject newLightBeam = GameObject.Instantiate(lightBeam, position, Quaternion.identity) as GameObject;
		newLightBeam.GetComponent<LightBeam>().direction = direction;
		//return newLightBeam;
	}
}

