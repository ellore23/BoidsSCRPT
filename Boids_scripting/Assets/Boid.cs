using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    private float speed;
    Vector3 direction;
    public List<Boid> boids;

    public GameObject boidLeader;

    // moving da flock to poland(center) checker
    public float centerStrength;
    public float generalBoidsDistance;
    Vector3 boidsPositionInScene;
    int closenessChecker;

    // Avoiding da plebe checker
    public float avoidnessStrenght;
    public float generalcollisionAvoidness;
    Vector3 movingFarFromdaBoid;

    // Alligning boids to poland
    public float alignBoidStrenght;
    public float generalBoidsAlignment;



    void FixedUpdate()
    {
        AlligningBoids();
        MovingBoidsClose();
        AvoidingBirds();
        transform.Translate(direction * (speed *Time.deltaTime));
    }

    void MovingBoidsClose()
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

    void AvoidingBirds()
    {
        foreach (Boid boid in boids)
        {
            float distanceBetween = Vector3.Distance(boid.transform.position, transform.position);
            if (distanceBetween <= generalcollisionAvoidness)
            {
                movingFarFromdaBoid = movingFarFromdaBoid + (transform.position - boid.transform.position);
            }
        }

        movingFarFromdaBoid = movingFarFromdaBoid.normalized;

        direction = direction + avoidnessStrenght * movingFarFromdaBoid / (avoidnessStrenght + 1);
        direction = direction.normalized;
    }

    void AlligningBoids()
    {
        
    }
}
