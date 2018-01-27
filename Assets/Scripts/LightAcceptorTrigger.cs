using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAcceptorTrigger : MonoBehaviour {

    public int acceptorNum;
    public int currBeams;
    public GameObject player;

    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LightBeam")
        {
            this.currBeams++;
            GameObject.Destroy(other.gameObject);
            if(this.currBeams == acceptorNum)
            {
                currBeams = 0;
                GameObject newPlayer = GameObject.Instantiate(player, this.transform.position, Quaternion.identity) as GameObject;

            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, this.GetComponent<BoxCollider>().size);
        //		Gizmos.DrawRay(this.transform.position, this.directions);
    }
}
