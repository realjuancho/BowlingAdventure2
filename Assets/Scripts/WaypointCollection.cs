using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollection : MonoBehaviour {


	public List<Waypoint> waypoints;


	public float directionToWaypoint;

	void Start () {

		Waypoint[] myWaypoints = gameObject.GetComponentsInChildren<Waypoint>();

		foreach(Waypoint w in myWaypoints)
		{
			waypoints.Add(w);
		}

	}


	public Waypoint FirstWaypoint()
	{
		return waypoints[0];
	}

	public Waypoint NextWaypoint(Waypoint currentWaypoint, Vector3 position)
	{

		directionToWaypoint = (currentWaypoint.transform.position - position).sqrMagnitude;

		if(directionToWaypoint <= 1)
		{

			int i = waypoints.IndexOf(currentWaypoint);

			if(waypoints.Count > i+1)
				return waypoints[i + 1];
			else
			 	currentWaypoint.IsLast = true;

		}
		return currentWaypoint;
	}
}
