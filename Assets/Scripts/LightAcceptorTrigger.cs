using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAcceptorTrigger : MonoBehaviour {

    public int acceptorNum;
    private int currBeams;
    public GenerateLightTrigger LightGenerator;
    private float timer;
    private bool isTiming;
    public float TIME_LIMIT;
    public float setPhase;
    public float phaseTol = 0.1f;
        
    void Start()
    {
        ResetAcceptor();
    }

    private void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
            if(timer > TIME_LIMIT)
            {
                ResetAcceptor();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision detected");
        if (other.tag == "LightBeam")
        {
            Debug.Log(other.GetComponent<LightBeam>().phase);
            if (Mathf.Abs(other.GetComponent<LightBeam>().phase - this.setPhase) < phaseTol ||
                (Mathf.Abs(other.GetComponent<LightBeam>().phase-other. GetComponent<LightBeam>().period - this.setPhase) < phaseTol))
            {
                this.currBeams++;
                GameObject.Destroy(other.gameObject);
            }
            if (currBeams == acceptorNum)
            {
                LightGenerator.isEnabled = false;
                ResetAcceptor();
            } else
            {
                isTiming = true;
            }
        }
    }

    void ResetAcceptor()
    {
        isTiming = false;
        timer = 0;
        currBeams = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, this.GetComponent<BoxCollider>().size);
        //		Gizmos.DrawRay(this.transform.position, this.directions);
    }
}
