using UnityEngine;
using System.Collections;

public class PlayerMovement : A_CharacterMovement
{
	public	float				moveSpeed 		= 10.0f;
	private CharacterController controller;
	private Vector3				oldPosition;
	private Vector3				movement;
	private Vector3				direction;
    public GameObject rotatorBeamPrefab;
    private GameObject rotatorBeam;
    public bool hasShifter;
    public GameObject PhaseShifter;
	
	// MonoBehavior interface functions
	// Use this for initialization
	void Start () {
		this.controller 	= this.gameObject.GetComponent<CharacterController>();
		this.movement 		= Vector3.zero;
		this.oldPosition 	= this.transform.position;
		foreach ( GameObject lightbeam in GameObject.FindGameObjectsWithTag("LightBeam") ){
			GameObject.Destroy(lightbeam, 5.0f);
		}
        this.hasShifter = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.UpdateDirection();
        if (!this.CheckRotatorEnabled())
        {
            this.MoveInputDirection();
            this.ApplyMovement();
        }
    }

    // Public
    public Vector3 Direction{ get{ return this.direction; } }
	
	
	// Private
	
	private void ApplyMovement(){
        this.controller.Move(this.movement * Time.deltaTime);
	    this.movement.y = 0.0f;
		this.movement.x = 0.0f;
	}
	
	private void MoveInputDirection(){
    
		if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0){
			this.movement.x += this.moveSpeed * Input.GetAxis("Horizontal");
		}
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            this.movement.y += this.moveSpeed * Input.GetAxis("Vertical");
        }
    }
		
	private void UpdateDirection(){
		this.direction 		= (this.transform.position - this.oldPosition).normalized;
		this.oldPosition 	= this.transform.position;
	}
	
    private bool CheckRotatorEnabled() //Also handles phase shifter dropoff
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.hasShifter)
            {
                Debug.Log("Deploy shifter"); 
                GameObject.Instantiate(PhaseShifter, this.transform.position + new Vector3(1, 0 ,0), Quaternion.identity);
                this.hasShifter = false;
            } else
            {
                rotatorBeam = GameObject.Instantiate(rotatorBeamPrefab, this.transform.position, Quaternion.identity) as GameObject;

            }
            return true;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if(rotatorBeam!= null)
            {
                rotatorBeam.transform.position = this.transform.position;

            }
            return true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(rotatorBeam != null)
            {
                GameObject.Destroy(rotatorBeam);

            }
            return false;
        }
        return false;
    }
}

