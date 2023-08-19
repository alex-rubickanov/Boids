using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;
    public static List<Boid> boids;
    
    [SerializeField] private GameObject boidPrefab;
    [SerializeField] private Transform boidAnchor;
    [SerializeField] private int numBoids;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnDelay;
    
    [SerializeField] private float velocity;
    [SerializeField] private float neighborDist;
    [SerializeField] private float collDist;
    [SerializeField] private float velMatching;
    [SerializeField] private float flockCentering;
    [SerializeField] private float collAvoid;
    [SerializeField] private float attractPull;
    [SerializeField] private float attractPush;
    [SerializeField] private float attractPushDist;

    private void Awake()
    {
        Instance = this;
        boids = new List<Boid>();
        InstantiateBoid();
    }

    private void InstantiateBoid()
    {
        GameObject boidObject = Instantiate(boidPrefab, boidAnchor, false);
        Boid boid = boidObject.GetComponent<Boid>();
        boids.Add(boid);
        
        if (boids.Count < numBoids)
        {
            Invoke("InstantiateBoid", spawnDelay);
        }
    }

    public float GetSpawnerRadius()
    {
        return spawnRadius;
    }

    public float GetVelocity()
    {
        return velocity;
    }

    public float GetAttractionPushDistance()
    {
        return attractPushDist;
    }

    public float GetAttractPush()
    {
        return attractPush;
    }

    public float GetAttractPull()
    {
        return attractPull;
    }

    public float GetNeighborDistance()
    {
        return neighborDist;
    }

    public float GetColliderDistance()
    {
        return collDist;
    }

    public float GetColliderAvoidness()
    {
        return collAvoid;
    }

    public float GetVelocityMatching()
    {
        return velMatching;
    }

    public float GetCentering()
    {
        return flockCentering;
    }
}
