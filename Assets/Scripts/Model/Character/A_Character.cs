using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMovement))]
public abstract class A_Character : MonoBehaviour
{
	protected CharacterMovement movement;

	// Use this for initialization
	protected virtual void Start ()
	{
		this.movement = this.GetComponent<CharacterMovement>();
	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
	}
}

