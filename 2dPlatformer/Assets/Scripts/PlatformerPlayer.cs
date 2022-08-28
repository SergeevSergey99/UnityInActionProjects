using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 12f;

    private Rigidbody2D rb;
    private Animator anim;

    private BoxCollider2D box;

    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        scale = transform.localScale;
    }
    

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, rb.velocity.y);
        rb.velocity = movement;

        anim.SetFloat("Speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;

        Vector2 corner1 = new Vector2(max.x, min.y - 0.05f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.13f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = hit != null;


        rb.gravityScale = grounded && Mathf.Approximately(deltaX, 0) ? 0 : 1;
        if (grounded)
        {
            if (hit.GetComponent<MovingPlatform>() != null)
            {
                transform.SetParent(hit.transform);
                transform.localScale = new Vector3(
                    scale.x / hit.transform.lossyScale.x,
                    scale.y / hit.transform.lossyScale.y,
                    scale.z / hit.transform.lossyScale.z);
            }
            else
            {
            }
                //
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            transform.SetParent(null);
            transform.localScale = scale;
        }
    }
}