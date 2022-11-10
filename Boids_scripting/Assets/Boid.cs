using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    private float speed;
    Vector3 direction;
    public List<Boid> boids;

    public GameObject boidLeader;

    // Boids close checker
    public float centerStrength;
    public float generalBoidsDistance;
    Vector3 boidsPositionInScene;
    int closenessChecker;


    void FixedUpdate()
    {
        KeepFlockClose();
        transform.Translate(direction * (speed *Time.deltaTime));
    }

    void KeepFlockClose()
    {    
        foreach(Boid boid in boids)
        {
            float distanceBetween = Vector3.Distance(boid.transform.position, transform.position);
            if(distanceBetween <= generalBoidsDistance)
            {
                boidsPositionInScene += boid.transform.position;
                closenessChecker++;
            }
        }

        Vector3 averagePosition = boidsPositionInScene / closenessChecker;
        averagePosition = averagePosition.normalized;
        Vector3 faceDirection = (averagePosition - transform.position).normalized;

        float time = centerStrength * Time.deltaTime;
        direction = direction + time * faceDirection / (time + 1);
        direction = direction.normalized;

    }
}
