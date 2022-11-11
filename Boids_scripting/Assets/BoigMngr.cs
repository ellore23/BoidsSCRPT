using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoigMngr : MonoBehaviour
{
    public List<Boid> boidsList;
    public int birdsInBoid = 50;

    public Boid boidGameObject;
    void Start()
    {
        boidsList = new List<Boid>();

        for (int i = 0; i < birdsInBoid; i++)
        {
            CreateBoid(boidGameObject.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Boid boid in boidsList)
        {
            boid.BoidsMovement(boidsList);
        }
    }
    void CreateBoid(GameObject prefab)
    {
        var boidCreated = Instantiate(prefab);
        boidCreated.transform.localPosition += new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));
        Boid boidScript = boidCreated.GetComponent<Boid>();

        boidsList.Add(boidScript);

    }
}
