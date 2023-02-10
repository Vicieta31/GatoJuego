using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{

    public Sprite gfx_Cat;
    public SpriteRenderer renderer;
    Rigidbody rig;
    public float velocity = 0.1f;
    public int playerIndex = 0;
    public Camera camera;

    int vida = 3;   

    // Start is called before the first frame update
    void Start()
    {
        if (gfx_Cat != null && renderer != null)
        {
            renderer.sprite = gfx_Cat;
        }
        rig = GetComponent<Rigidbody>();
        if (playerIndex == 1)
        {
            camera.rect = new Rect(0.5f, 0f, 0.5f, 1f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.AddForce(Vector2.right * velocity, ForceMode.Impulse);
    }

    

    void Update()
    {
        if (gameObject.transform.position.y == -7) 
        {
            vida--;
        }
    }

}