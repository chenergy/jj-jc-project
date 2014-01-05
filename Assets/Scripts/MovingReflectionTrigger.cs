using UnityEngine;
using System.Collections;



public class MovingReflectionTrigger: BasicReflectionTrigger
{
	public float speed;
	public float maxSpeed;
	public Transform[] waypoints;
	
	private Vector3 targetPosition;
	private int currentWaypoint = 0;

	protected override void Start(){
		this.targetPosition = this.waypoints [0].transform.position;
	}

	protected override void Update(){
		Vector3 currentPosition = this.transform.position;
		if ((currentPosition - this.targetPosition).magnitude < 0.1f) {
			this.currentWaypoint++;
			this.currentWaypoint = this.currentWaypoint % waypoints.Length;
			this.targetPosition = this.waypoints [this.currentWaypoint].transform.position;
		} else {
			/*this.transform.position = Vector3.Lerp (this.transform.position, this.targetPosition,
				Time.deltaTime * this.speed);*/
			this.transform.position += (this.targetPosition - this.transform.position).normalized * Time.deltaTime * this.speed;
		}
	}
}
