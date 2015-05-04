using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetControl : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }

    [Range(0.1f, 100)]
    public float gravityStrength = 0.1f;

    bool attractingObjects;
    int objectsInRange;
    float radius;
    List<AffectedByGravity> objectsAffectedByGravity = new List<AffectedByGravity>();

    void Start()
    {
        //Get radius and rigidbody
        rb = GetComponent<Rigidbody2D>();
        radius = GetComponent<CircleCollider2D>().radius;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!enabled)
        {
            return;
        }

        //If object can be affected by gravity then add it to list
        AffectedByGravity affectedByGravity = c.GetComponent<AffectedByGravity>();

        if (affectedByGravity)
            AddToBeingAttracted(affectedByGravity);
    }

    /// <summary>
    /// Adds object to list of objects to attract
    /// </summary>
    /// <param name="attractedObject">AffectedByGravity component of object to be attracted</param>
    void AddToBeingAttracted(AffectedByGravity attractedObject)
    {
        objectsInRange += 1;
        objectsAffectedByGravity.Add(attractedObject);

        //If the object wants to orient then pass it this planet
        if (attractedObject is OrientToPlanet)
        {
            OrientToPlanet orient = (OrientToPlanet)attractedObject;
            orient.planet = rb;
        }

        //If coroutine isn't already runnning then run it
        if (!attractingObjects)
            StartCoroutine(ApplyGravity());
    }

    /// <summary>
    /// Applys gravity to objects within range
    /// </summary>
    /// <returns></returns>
    IEnumerator ApplyGravity()
    {
        attractingObjects = true;

        //As long as there are objects within our area of effect
        while (objectsAffectedByGravity.Count > 0)
        {
            //Pull them in
            for (int i = 0; i < objectsAffectedByGravity.Count; i++)
            {
                Vector2 dist = rb.position - objectsAffectedByGravity[i].rb.position;

                //If object is not too close then pull
                if (dist.sqrMagnitude > radius * radius + objectsAffectedByGravity[i].gravityStoppingOffset)
                    objectsAffectedByGravity[i].rb.AddForceAtPosition(gravityStrength * Time.fixedDeltaTime * dist, objectsAffectedByGravity[i].rb.position);
                else
                    objectsAffectedByGravity[i].rb.velocity = Vector2.zero;
            }

            yield return null;
        }

        attractingObjects = false;
    }
}