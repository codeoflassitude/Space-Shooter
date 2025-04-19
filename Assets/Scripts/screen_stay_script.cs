using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screen_stay_script : MonoBehaviour
{
    public GameObject bullet;
    private Rigidbody2D rb;
    private Vector2 v;

    //padding ensures the ship doesnt leave the screen. can be tweaked in isnpector
    public float xPadding = 0.5f;
    public float yPadding = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //handle movement input
        v.x = Input.GetAxis("Horizontal") * 10f;
        v.y = Input.GetAxis("Vertical")   * 10f;
        rb.velocity = v;

        //fire
        if (Input.GetKeyDown("space"))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }

        //clamp to the screen
        ClampToScreen();
    }

    private void ClampToScreen()
    {
        //get the world space corners of the camera
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min.x + xPadding, max.x - xPadding);
        pos.y = Mathf.Clamp(pos.y, min.y + yPadding, max.y - yPadding);
        transform.position = pos;
    }
}