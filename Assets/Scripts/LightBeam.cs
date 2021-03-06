using UnityEngine;
using System.Collections;

public class LightBeam : MonoBehaviour
{
	[HideInInspector]
	public Vector3 	direction;

    public float phase;
//    public float period;

	public static float SPEED = 10.0f;
	public static float PERIOD = 2f;

	public Color CurColor
	{
		get { return GetComponent<TrailRenderer> ().startColor; }
	}

	// Use this for initialization
	void Start ()
	{
        phase = 0;
//        period = 2f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		this.GetComponent<TrailRenderer>().startColor = new Color(.5f + Mathf.Sin(2 * Mathf.PI * phase / PERIOD)/2f , .5f + Mathf.Cos(2 * Mathf.PI * phase / PERIOD)/2f, 0);
		this.GetComponent<TrailRenderer>().endColor = new Color(.5f + Mathf.Sin(2 * Mathf.PI * phase / PERIOD)/2f, .5f +  Mathf.Cos(2 * Mathf.PI * phase / PERIOD)/2f , 0);
        this.transform.position += this.direction * SPEED * Time.fixedDeltaTime;
        this.phase = (this.phase + Time.fixedDeltaTime) % (PERIOD);

        if (this.transform.position.sqrMagnitude >= 10000)
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

