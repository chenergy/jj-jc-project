using UnityEngine;
using System.Collections;

public class GenerateLightTrigger : MonoBehaviour
{
	public Vector3[] directions;
	public GameObject lightBeam;
    public bool isEnabled;
    public float timer;
    public float cycleTime;

	void Start(){
		for (int i = 0; i < this.directions.Length; i++){
			this.directions[i] = this.directions[i].normalized;
		}
        isEnabled = true;
        timer = 0;
	}

    private void FixedUpdate()
    {
        timer = (timer + Time.fixedDeltaTime);
        if (this.isEnabled && Mathf.Abs(this.timer) > cycleTime)
        {
            timer -= cycleTime;
           	foreach (Vector3 direction in this.directions)
            {
        		GameObject newLightBeam = GameObject.Instantiate(lightBeam, this.transform.position, Quaternion.identity) as GameObject;
        		newLightBeam.GetComponent<LightBeam>().direction = direction;
        	}

        }
    }

    void OnTriggerEnter( Collider other ){
		//if (other.tag == "Player"){
		//	foreach (Vector3 direction in this.directions){
		//		GameObject newLightBeam = GameObject.Instantiate(lightBeam, this.transform.position, Quaternion.identity) as GameObject;
		//		newLightBeam.GetComponent<LightBeam>().direction = direction;
		//	}
        //GameObject.Destroy(other.gameObject);

//        }

    }
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, this.GetComponent<BoxCollider>().size);
//		Gizmos.DrawRay(this.transform.position, this.directions);
	}
}

