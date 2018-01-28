using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
	public GameObject lightBeam;
	public Transform generator;

	private TextMesh[] text;
	private bool triggered = false;
	private float timer;
	private float diff = 5f;

	// Use this for initialization
	public void Start ()
	{
		text = GetComponentsInChildren<TextMesh> ();
	}
	
	// Update is called once per frame
	public void Update ()
	{
		timer += Time.deltaTime * 20;
		for (int i = 0; i < text.Length; i++)
		{
			text [i].transform.localRotation = Quaternion.Euler (0, Mathf.Max (0, 90 - (timer - (text.Length * diff) + (i * diff))), 0);
		}

		if (triggered)
		{
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space))
		{
			triggered = true;
			GameObject newLightBeam = GameObject.Instantiate(lightBeam, generator.transform.position, Quaternion.identity) as GameObject;
			newLightBeam.GetComponent<LightBeam> ().direction = Vector3.right;
		}
	}
}

