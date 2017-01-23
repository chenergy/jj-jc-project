using UnityEngine;
using System.Collections;

[RequireComponent(typeof(A_CharacterMovement))]
public abstract class A_Character : MonoBehaviour
{
	protected A_CharacterMovement movement;

	// Use this for initialization
	protected virtual void Start ()
	{
		this.movement = this.GetComponent<A_CharacterMovement>();
	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
	}
}

