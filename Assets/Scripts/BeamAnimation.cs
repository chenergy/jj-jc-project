using UnityEngine;
using System.Collections;

public class BeamAnimation : MonoBehaviour
{
	public float speed = 1f;
	private Renderer renderer;
	private Material material;
	private float timer = 0f;

	// Use this for initialization
	public void Start ()
	{
		renderer = GetComponent<Renderer> ();
		material = renderer.material;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float emission = Mathf.PingPong (timer, 1.0f);
		Color baseColor = new Color (0.25f, 0.5f, 0.25f);
		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
		material.SetColor ("_EmissionColor", finalColor);

		timer += Time.deltaTime * speed;
	}
}

