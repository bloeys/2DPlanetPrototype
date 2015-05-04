using UnityEngine;

[RequireComponent(typeof(OrientToPlanet))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;

    float horz;
    OrientToPlanet orient;

    // Use this for initialization
    void Start()
    {
        orient = GetComponent<OrientToPlanet>();
    }

    void Update()
    {
        horz = Input.GetAxis("Horizontal");

        //Face moving direction
        if (Mathf.Abs(horz) > 0.01f)
        transform.localScale = new Vector3(Mathf.Sign(horz), transform.localScale.y, transform.localScale.z);

    }

    void FixedUpdate()
    {
        orient.rb.position += (Vector2)transform.right * horz * speed * Time.fixedDeltaTime;
    }
}