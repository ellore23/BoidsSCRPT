using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public Boid individualBoid;
    public int boidsCreated = 50;
    private List<Boid> boidsList;

    private void Start(){
        boidsList = new List<Boid>();
        for (int i = 0; i < boidsCreated; i++){
            CreateBoid(individualBoid.gameObject, 0);
        }
    }

    private void FixedUpdate(){
        foreach (Boid boid in boidsList){
            boid.IndividualMovement(boidsList);
        }
    }

    private void CreateBoid(GameObject prefab, int swarmIndex){
        var boidCreated = Instantiate(prefab);
        boidCreated.transform.localPosition += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        var boidTransform = boidCreated.GetComponent<Boid>();
        boidsList.Add(boidTransform);
    }


}
