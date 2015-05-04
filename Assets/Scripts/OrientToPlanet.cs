using UnityEngine;

public class OrientToPlanet : AffectedByGravity
{
    public float rayLength, rotationTime;
    public Rigidbody2D planet;
    public LayerMask layer;

    // Update is called once per frame
    void Update()
    {
        //If we are being attracted to a planet
        if (planet)
        {
            Vector2 dirToPlanet = (planet.position - (Vector2)transform.position).normalized;   //Take direction to planet

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
}