using UnityEngine;
using System.Collections;

public class PlayerCharacterMovement : CharacterMovement
{
	public	float				moveSpeed 		= 10.0f;
	public	float				jumpStrength 	= 10.0f;
	
	private CharacterController controller;
	private Vector3				oldPosition;
	private Vector3				movement;
	private Vector3				direction;
	private bool				canJump;
	private Vector3             tongue;
	
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
		this.CheckCanJump();
		this.ApplyGravity();
		this.MoveInputDirection();
		this.ApplyMovement();
		//Debug.Log(this.controller.isGrounded);
	}
	
	// Public
	public Vector3 Direction{ get{ return this.direction; } }
	
	
	// Private
	private void ApplyGravity(){
		this.movement.y += Physics.gravity.y * Time.deltaTime;
	}
	
	protected void ApplyMovement(){
		this.controller.Move(this.movement * Time.deltaTime);
		if (this.controller.isGrounded) this.movement.y = 0.0f;
		this.movement.x = 0.0f;
	}
	
	private void MoveInputDirection(){
		if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0){
			this.movement.x += this.moveSpeed * Input.GetAxis("Horizontal");
		}
		
		if (Input.GetKeyDown(KeyCode.Space)){
			if (!this.canJump){
				this.Jump();
			}
		}
	}
	
	private void CheckCanJump(){
		this.canJump = (this.controller.isGrounded) ? false : true;
	}
	
	private void Jump(){
		this.canJump = true;
		this.movement.y = this.jumpStrength;
	}
	
	private void UpdateDirection(){
		this.direction 		= (this.transform.position - this.oldPosition).normalized;
		this.oldPosition 	= this.transform.position;
	}
	
	private void CheckTongue(){
		//Input.GetButton()
	}
}

