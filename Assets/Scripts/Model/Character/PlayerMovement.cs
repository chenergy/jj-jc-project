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
	
	// MonoBehavior interface functions
	// Use this for initialization
	void Start () {
		this.controller 	= this.gameObject.GetComponent<CharacterController>();
		this.movement 		= Vector3.zero;
		this.oldPosition 	= this.transform.position;
		foreach ( GameObject lightbeam in GameObject.FindGameObjectsWithTag("LightBeam") ){
			GameObject.Destroy(lightbeam, 5.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.UpdateDirection();
 		this.MoveInputDirection();
		this.ApplyMovement();
        this.CheckRotatorEnabled();

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
	
    private void CheckRotatorEnabled()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotatorBeam = GameObject.Instantiate(rotatorBeamPrefab, this.transform.position, Quaternion.identity) as GameObject;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            rotatorBeam.transform.position = this.transform.position;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject.Destroy(rotatorBeam);
        }
    }
}

