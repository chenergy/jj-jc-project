﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class CharacterMovement : MonoBehaviour {
	public	float				moveSpeed 		= 10.0f;
	public	float				jumpStrength 	= 10.0f;
	
	private CharacterController controller;
	private Vector3				oldPosition;
	private Vector3				movement;
	private Vector3				direction;
	private bool				canJump;
	
	// MonoBehavior interface functions
	// Use this for initialization
	void Start () {
		this.controller 	= this.gameObject.GetComponent<CharacterController>();
		this.movement 		= Vector3.zero;
		this.oldPosition 	= this.transform.position;
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
}
