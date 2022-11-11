using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    private float speed = 10;
    Vector3 direction;
    

    // moving da flock to poland(center) checker
    public float centerStrength = 3;
    public float generalBoidsDistance = 3;
    Vector3 boidsPositionInScene;
    int closenessChecker;

    // Avoiding da plebe checker
    public float avoidnessStrenght = 3;
    public float generalcollisionAvoidness = 3;
    Vector3 movingFarFromdaBoid;

    // Alligning boids to poland
    public float alignBoidStrenght = 3;
    public float generalBoidsAlignment = 3;
    Vector3 directionAvg;
    int otherclosenessChecker;

    public void BoidsMovement(List<Boid> boidsList)
    {
        AlligningBoids(boidsList);
        MovingBoidsClose(boidsList);
        AvoidingBirds(boidsList);
        this.transform.Translate(direction * (speed * Time.deltaTime));
    }

    void MovingBoidsClose(List<Boid> boidsList)
    {
        foreach (Boid boid in boidsList)
        {
            float distanceBetween = Vector3.Distance(boid.transform.position, transform.position);
            if (distanceBetween <= generalBoidsDistance)
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

    void AvoidingBirds(List<Boid> boidsList)
    {
        foreach (Boid boid in boidsList)
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

    void AlligningBoids(List<Boid> boidsList)
    {
        foreach (Boid boid in boidsList)
        {
            float distanceBetween = Vector3.Distance(boid.transform.position, transform.position);
            if (distanceBetween <= generalBoidsAlignment)
            {
                directionAvg += boid.direction;
                otherclosenessChecker++;
            }

        }

        Vector3 dir = directionAvg / otherclosenessChecker;
        dir = dir.normalized;

        float time = alignBoidStrenght * Time.deltaTime;
        direction = direction + time * dir / (time + 1);
        direction = direction.normalized;
    }

    
}