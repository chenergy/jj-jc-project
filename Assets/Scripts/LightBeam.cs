using UnityEngine;
using System.Collections;

public class LightBeam : MonoBehaviour
{
	[HideInInspector]
	public Vector3 	direction;
	private float	speed = 10.0f;
    public float phase;
    public float period;

	// Use this for initialization
	void Start ()
	{
        phase = 0;
        period = 2f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		this.transform.position += this.direction * this.speed * Time.fixedDeltaTime;
        this.phase = (this.phase + Time.fixedDeltaTime) % (period);
        this.GetComponent<TrailRenderer>().startColor = new Color(Mathf.Sin(2 * Mathf.PI * phase / period), 0, 0);
        this.GetComponent<TrailRenderer>().endColor = new Color(Mathf.Sin(2 * Mathf.PI * phase / period), 0, 0);
        if(this.transform.position.sqrMagnitude >= 10000)
        {
            Destroy(this.gameObject);
        }
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, this.GetComponent<BoxCollider>().size);
	}

	private void ResetLayer(){
		this.gameObject.layer = 0;
	}
}

