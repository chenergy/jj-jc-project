using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{
	public Color color;

	// Use this for initialization
	void Start ()
	{
		Camera.main.backgroundColor = color;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

