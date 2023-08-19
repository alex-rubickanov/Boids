using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    private List<Boid> neighbors;
    private SphereCollider coll;


    private void Awake()
    {
        neighbors = new List<Boid>();
    }

    private void Start()
    {
        coll = GetComponent<SphereCollider>();
        coll.radius = Spawner.Instance.GetNeighborDistance() / 2;
    }

    private void FixedUpdate()
    {
        if (coll.radius != Spawner.Instance.GetNeighborDistance() / 2)
        {
            coll.radius = Spawner.Instance.GetNeighborDistance() / 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Boid b = other.GetComponent<Boid>();
        if (b == null) return;
        if (neighbors.IndexOf(b) == -1)
        {
            neighbors.Add(b);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Boid b = other.GetComponent<Boid>();
        if (b == null) return;
        if (neighbors.IndexOf(b) != -1)
        {
            neighbors.Remove(b);
        }
    }

    public Vector3 avgPos
    {
        get
        {
            Vector3 avg = Vector3.zero;

            if (neighbors.Count == 0) return avg;

            for (int i = 0; i < neighbors.Count; i++)
            {
                avg += neighbors[i].GetPosition();
            }

            avg /= neighbors.Count;

            return avg;
        } 
    }

    public Vector3 avgVel
    {
        get
        {
            Vector3 avg = Vector3.zero;
            if (neighbors.Count == 0) return avg;

            for (int i = 0; i < neighbors.Count; i++)
            {
                avg += neighbors[i].GetVelocity();
            }

            avg /= neighbors.Count;

            return avg;
        }
    }

    public Vector3 avgClosePos
    {
        get
        {
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            int nearCount = 0;
            for (int i = 0; i < neighbors.Count; i++)
            {
                delta = neighbors[i].GetPosition() - transform.position;
                if (delta.magnitude <= Spawner.Instance.GetColliderDistance())
                {
                    avg += neighbors[i].GetPosition();
                    nearCount++;
                }
            }

            if (nearCount == 0) return avg;

            avg /= nearCount;
            return avg;
        }
    }
}
