using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    private float speed;
    Vector3 direction;
    public List<Boid> boidsList;
    public int birdsInBoid = 50;

    public Boid boidGameObject;

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
    Vector3 directionAvg;
    int otherclosenessChecker;

    private void Start()
    {
        boidsList = new List<Boid>();

        for(int i = 0; i<birdsInBoid; i++)
        {
            CreateBoid(boidGameObject.gameObject, 0);
        }
    }

    void FixedUpdate()
    {
        AlligningBoids();
        MovingBoidsClose();
        AvoidingBirds();
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    void MovingBoidsClose()
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

    void AvoidingBirds()
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

    void AlligningBoids()
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

    void CreateBoid(GameObject prefab, int swarmindex) {
        var boidCreated = Instantiate(prefab);
        boidCreated.transform.localPosition += new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));
        Boid boidScript = boidCreated.GetComponent<Boid>();

        boidsList.Add(boidScript);
    
    }
}