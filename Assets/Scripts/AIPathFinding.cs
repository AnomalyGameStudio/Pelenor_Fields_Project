﻿using UnityEngine;
using System.Collections;

using Pathfinding;


[RequireComponent(typeof(Seeker))]
//[RequireComponent(typeof(CharacterController))]
public class AIPathFinding : MonoBehaviour {

    public Transform targetPosition;

    private Seeker seeker;
    //private CharacterController controller;

    // The calculated path
    public Path path;

    // The AI's speed in meters per second
    public float speed = 2;

    // The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    // How often to recalculate the path (in seconds)
    public float repathRate = 0.5f;
    private float lastRepath = -9999;

    private GameObject[] nodes;

    Transform parentPath;
    int currentPathChild = 0;

    void Start ()
    {
        seeker = GetComponent<Seeker>();
        //controller = GetComponent<CharacterController>();
        
        // Gets a random node to go
        nodes = GameObject.FindGameObjectsWithTag("PathNode");
        getNewPath();
        
	}
	
    public void OnPathComplete(Path p)
    {
        Debug.Log("A path was calculated. Did it have an error? " + p.error);
        if(!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

    private void getNewPath()
    {
        currentPathChild = 0;
        int random = Random.Range(0, nodes.Length);
        
        parentPath = nodes[random].transform;
        targetPosition = parentPath.GetChild(currentPathChild);
    }

    public void Update()
    {
        if (Time.time - lastRepath > repathRate && seeker.IsDone())
        {
            lastRepath = Time.time + Random.value * repathRate * 0.5f;

            // Start a new path to the targetPosition, call the OnPathComplete Function
            // When the path das been calculated (which may take a few frames depending on the complexity)
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }

        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        if (currentWaypoint > path.vectorPath.Count) return;

        if (currentWaypoint == path.vectorPath.Count)
        {
            Debug.Log("End of PAth Reached");
            currentWaypoint++;

            int random = Random.Range(0, nodes.Length - 1);
            currentPathChild++;

            if(currentPathChild > parentPath.childCount-1)
            {
                getNewPath();
            }
            else
            {
                targetPosition = parentPath.GetChild(currentPathChild);
            }
            
            lastRepath = Time.time + Random.value * repathRate * 0.5f;
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);

            return;
        }

        // Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        
        // TODO Find a way to rotate
        // Rotates the Object to look at the next node
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        //Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);

        //this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, targetRotation.y, this.transform.rotation.z));




        dir = path.vectorPath[currentWaypoint] - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;
        transform.Translate(dir.normalized * distThisFrame, Space.World);
        //Quaternion targetRotation = Quaternion.LookRotation(dir);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);


        //dir *= speed;
        
        // Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
        //controller.SimpleMove(dir);
        
        // The commented line is equivalent to the one below, but the one that is used
        // Is slightly faster since it does not have to calculate a square root
        // if (Vector3.Distance(trasnform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
        if ((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance*nextWaypointDistance )
        {
            currentWaypoint++;
            return;
        }

    }
}
