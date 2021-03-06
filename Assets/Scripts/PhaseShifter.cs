﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseShifter : MonoBehaviour {

    public float shiftAmount;

	// Use this for initialization
	void Start () {
		shiftAmount = .5f;
	}

    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LightBeam")
        {
            other.GetComponent<LightBeam>().phase+= shiftAmount;
        }
        if(other.tag  == "RotatorBeam")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (!player.GetComponent<PlayerMovement>().hasShifter)
            {
                player.GetComponent<PlayerMovement>().hasShifter = true;
				player.GetComponent<PlayerMovement> ().phaseShifterIcon.SetActive (true);
                GameObject.Destroy(this.gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
