using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{      
    Vector3 steering = Vector3.zero;
    Vector3 separationDirection = Vector3.zero;
    float separationCount = 0;
    Vector3 alignmentDirection = Vector3.zero;
    float alignmentCount = 0;
    Vector3 cohesionDirection = Vector3.zero;
    float cohesionCount = 0;
    public int index = 0;
    

    public Flyweight scriptCont;

    public void IndividualMovement(List<Boid> other){
        foreach (Boid boid in other){
            if (boid == this)
                continue;
            float distance = Vector3.Distance(boid.transform.position, this.transform.position);
            if (distance < scriptCont.noClumping){
                separationDirection += boid.transform.position - transform.position;
                separationCount++;
            }
            if (distance < scriptCont.localArea && boid.index == this.index){
                alignmentDirection += boid.transform.forward;
                alignmentCount++;
                cohesionDirection += boid.transform.position - transform.position;
                cohesionCount++;
            }
        }

        if (separationCount > 0){
            separationDirection /= separationCount;           
        }
        separationDirection = -separationDirection;
        if (alignmentCount > 0){
            alignmentDirection /= alignmentCount;
        }            

        if (cohesionCount > 0){
            cohesionDirection /= cohesionCount;
        }
         cohesionDirection -= transform.position;

        steering += separationDirection.normalized;
        steering += alignmentDirection.normalized;
        steering += cohesionDirection.normalized;

        if (steering != Vector3.zero){
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(steering), scriptCont.steeringSpeed * Time.deltaTime);
        }           

        transform.position += transform.TransformDirection(new Vector3(0, 0, scriptCont.speed)) * Time.deltaTime;

    }

}