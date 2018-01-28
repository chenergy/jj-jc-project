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
	public string nextLevel;
	public Transform cameraTransform;
	public Renderer crystalRenderer;
	private Material crystalMaterial;
        
    void Start()
    {
		crystalMaterial = crystalRenderer.material;
        ResetAcceptor();
		Color setPhaseColor = new Color (Mathf.Sin (2 * Mathf.PI * setPhase / 2f), 0, 0);
		crystalMaterial.SetColor ("_EmissionColor", setPhaseColor);
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
                (Mathf.Abs(other.GetComponent<LightBeam>().phase-other.GetComponent<LightBeam>().period - this.setPhase) < phaseTol))
            {
                this.currBeams++;
                GameObject.Destroy(other.gameObject);
            }
            if (currBeams == acceptorNum)
            {
				
                LightGenerator.isEnabled = false;
                ResetAcceptor();
				SceneController.Instance.LoadNewScene (nextLevel);
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
