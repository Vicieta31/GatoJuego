using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActor : MonoBehaviour
{


    /// <summary>
    /// NO AFAGEIXIS RE EN PLAYER
    /// FES EL NIVELL I COSES DE ANIMACIO EN TOT CAS
    /// ---OSIGUI APLIQUEU ELS SPRITES EN EL NIVELL I FES UN MENU PRINCIPAL PER INICIAR EL JOC
    /// </summary>

        
    public Sprite gfx_Cat;
    public SpriteRenderer renderer;
    Rigidbody rig;
    public float velocity = 0.1f;
    public int playerIndex = 0;
    public Camera camera;

    int vida = 7;
    bool stopPlayer = false;
    public TriggerSkillCheck skillCheck;
    public bool isInSkillCheck = false;

    private KeyCode[] latestKeys;
    private Countdown countdown;
    void Start()
    {
        if (gfx_Cat != null && renderer != null)
        {
            renderer.sprite = gfx_Cat;
        }
        rig = GetComponent<Rigidbody>();
        if (playerIndex == 1)
        {
            camera.rect = new Rect(0f, 0.5f, 1f, 0.5f);
        }
    }


    /// <summary>
    /// NO AFAGEIXIS RE EN PLAYER
    /// FES EL NIVELL I COSES DE ANIMACIO EN TOT CAS
    /// ---OSIGUI APLIQUEU ELS SPRITES EN EL NIVELL I FES UN MENU PRINCIPAL PER INICIAR EL JOC
    /// </summary>


    public void SetSkillCheckArray(int lenght)
    {
        latestKeys = new KeyCode[lenght];
        for (int i = 0; i < latestKeys.Length; i++)
        {
            latestKeys[i] = 0x000;
        }
    }
    void FixedUpdate()
    {
        if (!stopPlayer)
        {
            rig.AddForce(Vector2.right * velocity, ForceMode.Impulse);
        }
    }



    /// <summary>
    /// NO AFAGEIXIS RE EN PLAYER
    /// FES EL NIVELL I COSES DE ANIMACIO EN TOT CAS
    /// ---OSIGUI APLIQUEU ELS SPRITES EN EL NIVELL I FES UN MENU PRINCIPAL PER INICIAR EL JOC
    /// </summary>
    

    private void Restart()
    {
        ReduceHP();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 20f, gameObject.transform.position.x + 60f, gameObject.transform.position.z);
    }

    public void ReduceHP()
    {
        vida--;
    }
    public void StopPlayer()
    {
        stopPlayer = true;
    }
    public void MakePlayerRun()
    {
        stopPlayer = false;
    }
    void Update()
    {

       

        if (isInSkillCheck)
        {
            SaveKeyState();



            //comprovar amb el objecte original si son iguals
            // - Son iguals fer salt SALT()
            KeyCode[] e = skillCheck.GetKeys(playerIndex);
            int a = 0;
            bool equalInputs = true;
            for (int i = e.Length-1; i > 0; i--)
            {
                if (latestKeys[a] != e[i])
                {
                    equalInputs = false;
                }
                a++;
            }
            //Debug.Log("Player" + latestKeys[0] +" - " + latestKeys[1] + " - " + latestKeys[2] + " \n Skill: "+ e[0] + " - "+e[1] + " - " + e[2]);
            if (equalInputs)
            {
                isInSkillCheck = false;
                Jump();
            }
        }

    }

    private void SaveKeyState()
    {

        //grabar els botons a array
        KeyCode[] e = new KeyCode[skillCheck.keys.Length];
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (vKey == KeyCode.None)
            {
                continue;
            }
            if (Input.GetKeyDown(vKey))
            {
                for (int i = 1; i < latestKeys.Length; i++)
                {
                    e[i] = latestKeys[i - 1];
                    //Debug.Log(latestKeys[i - 1]);
                }
                latestKeys = e;
                latestKeys[0] = vKey;
            }
        }
        //Debug.Log(latestKeys[0] +" - " + latestKeys[1] + " - " + latestKeys[2]);
    }

    private void Jump()
    {
        Debug.Log("IN22");
        rig.AddForce(skillCheck.vectorForce, ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "yDeath")
        {
            Restart();
        }

    }
}