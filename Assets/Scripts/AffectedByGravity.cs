using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AffectedByGravity : MonoBehaviour
{
    public float gravityStoppingOffset;
    public Rigidbody2D rb { get; private set; }

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}