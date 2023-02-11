using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSkillCheck : MonoBehaviour
{
    public KeyCode[] keys;
    KeyCode[] keys2;
    public Vector3 vectorForce = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        keys2 = Translate();
    }
    KeyCode[] Translate()
    {
        KeyCode[] keysa = new KeyCode[keys.Length];
        for (int i = 0; i < keys.Length; i++)
        {
            switch (keys[i])
            {
                default:
                    break;
                case KeyCode.A:
                    keysa[i] = KeyCode.LeftArrow;
                    break;
                case KeyCode.S:
                    keysa[i] = KeyCode.DownArrow;
                    break;
                case KeyCode.D:
                    keysa[i] = KeyCode.RightArrow;
                    break;
                case KeyCode.W:
                    keysa[i] = KeyCode.UpArrow;
                    break;
            }
        }
        return keysa;
    }
    public KeyCode[] GetKeys(int indexPlayer)
    {
        if (indexPlayer == 0)
        {
            return keys;
        }
        return keys2;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("In");
            PlayerActor player = collision.gameObject.GetComponent<PlayerActor>();
            player.skillCheck = this;
            player.isInSkillCheck = true;
            player.SetSkillCheckArray(keys.Length);
        }
    }
    
}
