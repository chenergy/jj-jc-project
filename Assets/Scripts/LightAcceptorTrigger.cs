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
	public GameObject particlePrefab;
	private Material crystalMaterial;
    private List<GameObject> connected;
        
    void Start()
    {
		crystalMaterial = crystalRenderer.material;
// 		Color setPhaseColor = new Color (Mathf.Sin (2 * Mathf.PI * setPhase / 2f), 0, 0);
		Color setPhaseColor = new Color(.5f + Mathf.Sin(2 * Mathf.PI * setPhase / LightBeam.PERIOD)/2f , .5f + Mathf.Cos(2 * Mathf.PI * setPhase / LightBeam.PERIOD)/2f, 0);
        crystalMaterial.SetColor("_EmissionColor", setPhaseColor);
        connected = new List<GameObject>();
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
				(Mathf.Abs(other.GetComponent<LightBeam>().phase-LightBeam.PERIOD - this.setPhase) < phaseTol))
            {
                this.currBeams++;
//                other.GetComponent<LightBeam>().SPEED = 0;
                Debug.Log(other.gameObject == null);
                connected.Add(other.gameObject);
                //GameObject.Destroy(other.gameObject);

				// Create a particle prefab.
				LightBeam lb = other.GetComponent<LightBeam> ();
				GameObject newParticle = Instantiate (particlePrefab, transform.position, Quaternion.identity);
				ParticleSystem.MainModule module = newParticle.GetComponent<ParticleSystem> ().main;
				module.startColor = lb.CurColor;
				Destroy (newParticle, 1.5f);
            }
			else
			{
				// Create a particle prefab.
				LightBeam lb = other.GetComponent<LightBeam> ();
				GameObject newParticle = Instantiate (particlePrefab, transform.position, Quaternion.identity);
				ParticleSystem.MainModule module = newParticle.GetComponent<ParticleSystem> ().main;
				module.startColor = lb.CurColor;
				Destroy (newParticle, 1.5f);
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
        foreach(GameObject obj in connected)
        {
            if (obj != null)
            {
                GameObject.Destroy(obj);
            }
        }
        connected.Clear();
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
