using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] public float charSpeed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * charSpeed, body.velocity.y);
    }

}
