using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAcceptorTrigger : MonoBehaviour {

    public int acceptorNum;
    private int currBeams;
    public GameObject player;
    private float timer;
    private bool isTiming;
    public float TIME_LIMIT;
        
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
                foreach (GameObject lightbeam in GameObject.FindGameObjectsWithTag("LightBeam"))
                {
                    GameObject.Destroy(lightbeam);
                }
                ResetAcceptor();
                Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision detected");
        if (other.tag == "LightBeam")
        {
            Debug.Log("light detected");
            this.currBeams++;
            if(currBeams == 1 && acceptorNum!= 1)
            {
                isTiming = true;
            }
            GameObject.Destroy(other.gameObject);
            if(this.currBeams == acceptorNum)
            {
                ResetAcceptor();
                GameObject newPlayer = GameObject.Instantiate(player, this.transform.position, Quaternion.identity) as GameObject;
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
