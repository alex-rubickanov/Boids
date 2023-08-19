using UnityEngine;

public class Boid : MonoBehaviour
{
    private Neighborhood neighborhood;
    private Rigidbody rb;

    private Vector3 pos
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void Awake()
    {
        neighborhood = GetComponent<Neighborhood>();
        rb = GetComponent<Rigidbody>();

        pos = Random.insideUnitSphere * Spawner.Instance.GetSpawnerRadius();

        Vector3 vel = Random.onUnitSphere * Spawner.Instance.GetVelocity();
        rb.velocity = vel;

        LookAhead();

        Color randColor = Color.black;
        randColor = new Color(Random.value, Random.value, Random.value);

        TrailRenderer tRend = GetComponent<TrailRenderer>();
        tRend.material.SetColor("_TintColor", randColor);

    }

    private void LookAhead()
    {
        transform.LookAt(pos + rb.velocity);
    }


    private void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        Spawner spawner = Spawner.Instance;
        
        // AVOIDING
        Vector3 velAvoid = Vector3.zero;
        Vector3 tooClosePos = neighborhood.avgClosePos;
        if (tooClosePos != Vector3.zero)
        {
            velAvoid = pos - tooClosePos;
            velAvoid.Normalize();
            velAvoid *= spawner.GetVelocity();
        }
        
        // ALLIGNING
        Vector3 velAlign = neighborhood.avgVel;
        if (velAlign != Vector3.zero)
        {
            velAlign.Normalize();
            velAlign *= spawner.GetVelocity();
        }
        
        // CENTERING
        Vector3 velCenter = neighborhood.avgPos;
        if (velCenter != Vector3.zero)
        {
            velCenter -= transform.position;
            velCenter.Normalize();
            velCenter *= spawner.GetVelocity();
        }
        
        // MOVING TO ATTRACTOR
        Vector3 delta = Attractor.Pos - pos;
        bool attracted = (delta.magnitude > spawner.GetAttractionPushDistance());
        Vector3 velAttract = delta.normalized * spawner.GetVelocity();


        float fdt = Time.deltaTime;
        if (velAvoid != Vector3.zero)
        {
            vel = Vector3.Lerp(vel, velAvoid, spawner.GetColliderAvoidness() * fdt);
        }
        else
        {
            if (velAlign != Vector3.zero)
            {
                vel = Vector3.Lerp(vel, velAlign, spawner.GetVelocityMatching() * fdt);
            }

            if (velCenter != Vector3.zero)
            {
                vel = Vector3.Lerp(vel, velAlign, spawner.GetCentering() * fdt);
            }

            if (velAttract != Vector3.zero)
            {
                if (attracted)
                {
                    vel = Vector3.Lerp(vel, velAttract, spawner.GetAttractPull() * fdt);
                }
                else
                {
                    vel = Vector3.Lerp(vel, -velAttract, spawner.GetAttractPush() * fdt);
                }
            }
        }


        vel = vel.normalized * spawner.GetVelocity();
        rb.velocity = vel;
        LookAhead();
    }

    public Vector3 GetPosition()
    {
        return pos;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
