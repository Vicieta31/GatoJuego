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


    public Sprite gfx_Cat1;
    public Sprite gfx_Cat2;

    public SpriteRenderer renderer;
    Rigidbody rig;
    public float velocity = 0.1f;
    public int playerIndex = 0;
    public Camera camera;

    float velMax = 30;
    int vida = 7;
    int vidaMax;
    public bool stopPlayer = false;
    public TriggerSkillCheck skillCheck;
    public bool isInSkillCheck = false;
    public bool isPlayerFinished = false;
    private KeyCode[] latestKeys;
    private Countdown countdown;
    public float greacePrriod = 0.2f;

    public GameManger gm;

    public RuntimeAnimatorController ControllerP1;
    public RuntimeAnimatorController ControllerP2;

    private Animator animator;
    private float distToGround;
    private bool hasJumped;

    void Start()
    {
        vidaMax = vida;
        if (gfx_Cat1 != null && renderer != null)
        {
            renderer.sprite = gfx_Cat1;
        }
        rig = GetComponent<Rigidbody>();
        setPlayerIndex(playerIndex);
        countdown = gameObject.AddComponent<Countdown>();
        countdown.SetCuntdown(greacePrriod);
        countdown.StartTimer();
        animator = GetComponent<Animator>();
        distToGround = 18f;
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
            animator.SetBool("Moving", true);
            rig.AddForce(Vector2.right * velocity, ForceMode.Impulse);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void Restart()
    {
        ReduceHP();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 20f, gameObject.transform.position.x + 60f, gameObject.transform.position.z);
    }

    public void AumentaHp()
    {
        if (countdown.HasCompleted())
        {
            if (vida < vidaMax)
            {
                vida++;
            }
            countdown.ResetTimer();
            countdown.StartTimer();


        }
    }

    public int GetHp()
    {
        return vida;
    }

    public void ReduceHP()
    {
        if (countdown.HasCompleted())
        {
            countdown.ResetTimer();
            countdown.StartTimer();
            vida--;
        }

    }
    public bool muerto()
    {
        if (vida <= 0)
        {
            animator.SetBool("Die", true);
            return true;
        }
        return false;
    }

    //
    public void setPlayerIndex(int ind)
    {
        playerIndex = ind;
        renderer.color = Color.white;
        if (playerIndex == 0)
        {
            //animator.runtimeAnimatorController = ControllerP1;
            renderer.sprite = gfx_Cat1;
            camera.rect = new Rect(0f, 0f, 1f, 0.5f);
        }
        else
        {
            // animator.runtimeAnimatorController = ControllerP2;
            renderer.sprite = gfx_Cat2;
            camera.rect = new Rect(0f, 0.5f, 1f, 0.5f);
        }
    }

    //Atacar con espacio al enemigo


    public void StopPlayer()
    {
        stopPlayer = true;
        rig.velocity = Vector3.zero;
        animator.SetBool("Moving", false);
    }
    public void MakePlayerRun()
    {
        animator.SetBool("Moving", true);
        stopPlayer = false;
    }
    public void SetPlayerFinished()
    {
        isPlayerFinished = true;
    }
    bool IsGrounded()
    {
        bool ground = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        return ground;
    }
    void Update()
    {
        if (IsGrounded() && hasJumped)
        {
            animator.SetBool("IsJump", false);
        }
        if (vida <= 0)
        {
            //Animacion vida
            animator.SetBool("Die", true);
            StopPlayer();
        }
        if (rig.velocity.magnitude > velMax)
        {
            Vector3 newVel = rig.velocity.normalized;
            newVel *= velMax;
            rig.velocity = newVel;
        }

        if (isInSkillCheck)
        {
            SaveKeyState();



            //comprovar amb el objecte original si son iguals
            // - Son iguals fer salt SALT()
            KeyCode[] e = skillCheck.GetKeys(playerIndex);
            int a = 0;
            bool equalInputs = true;
            for (int i = e.Length - 1; i > 0; i--)
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
        hasJumped = true;
        animator.SetBool("IsJump", true);
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