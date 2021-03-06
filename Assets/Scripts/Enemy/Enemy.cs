﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamageable
{
    //GameObject pathGO;

    //Transform targetPathNode;
    //int pathNodeIndex = 0;

    public float speed = 5f;
    public float health = 1;
    public int moneyValue = 1;

    // Use this for initialization
    void Start ()
    {
        //pathGO = GameObject.Find("Path");
	}
	
    /*
    void GetNextPathNode()
    {
        if (pathNodeIndex < pathGO.transform.childCount)
        {
            targetPathNode = pathGO.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
        }
        else
        {
            //targetPathNode = null; // Removed so the enemies keep circling the ship
            //ReachedGoal();
            pathNodeIndex = 0;
            targetPathNode = pathGO.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
            
        }
    }
    */

    
    
	// Update is called once per frame
	void Update ()
    {
        /*
	    if(targetPathNode == null)
        {
            GetNextPathNode();
            if(targetPathNode == null)
            {
                // Run out of Path;
                //ReachedGoal();
                return;
            }
        }
        */
        //Vector3 dir = targetPathNode.position - this.transform.localPosition;

        //float distThisFrame = speed * Time.deltaTime;

        /*
        if(dir.magnitude <= distThisFrame)
        {
            // We reached the node
            targetPathNode = null;

        }
        else
        {
            // TODO: Consider ways to smooth this motion.

            // Move towards node
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
        }
        */
	}

    /*
    void ReachedGoal()
    {
        GameObject.FindObjectOfType<ScoreManager>().LoseLife();
        Destroy(gameObject);
    }
    */

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ScoreManager.Instance.addMoney(moneyValue);
        Destroy(gameObject);
    }
}
