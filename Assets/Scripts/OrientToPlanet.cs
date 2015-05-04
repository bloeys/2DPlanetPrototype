using UnityEngine;
using System.Collections.Generic;

public class OrientToPlanet : AffectedByGravity
{
    public float rayLength, rotationTime;
    public List<Rigidbody2D> planets = new List<Rigidbody2D>();
    public LayerMask layer;

    // Update is called once per frame
    void Update()
    {
        //If we are being attracted to a planet
        if (planets.Count > 0)
        {
            int closestPlanet = ClosestPlanet();

            Vector2 dirToPlanet = (planets[closestPlanet].position - (Vector2)transform.position).normalized;   //Take direction to planet

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlanet, rayLength, layer);    //Shoot ray in direction of planet
            Debug.DrawRay(transform.position, dirToPlanet * rayLength); //Debug ray

            //If we hit the planet
            if (hit)
            {
                //Rotate to land on it
                Quaternion rot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                rot.eulerAngles = new Vector3(0, 0, rot.eulerAngles.z);

                transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationTime * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// Gets the index of the closest planet
    /// </summary>
    /// <returns></returns>
    int ClosestPlanet()
    {
        float closestDist = Mathf.Infinity;
        int closestIndex = -1;

        for (int i = 0; i < planets.Count; i++)
        {
            float dist = (planets[i].position - rb.position).sqrMagnitude;
            if (dist < closestDist)
            {
                closestDist = dist;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}