﻿using UnityEngine;
using System.Collections;

public class PhaseShifterIcon : MonoBehaviour
{
	private GameObject player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = player.transform.position + Vector3.up;
	}
}

