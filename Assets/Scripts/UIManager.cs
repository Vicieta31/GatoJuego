using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite hpFull;
    public Sprite hpNone;

    public Image[] P1_hp;
    public Image[] P2_hp;

    public void SetHpPlayer(int playerIndex, int hp)
    {
        switch (playerIndex)
        {
            default:
                break;
            case 0:
                for (int i = 0; i < P1_hp.Length; i++)
                {
                    P1_hp[i].sprite = hpFull;
                    if (i >=hp)
                    {
                        P1_hp[i].sprite = hpNone;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < P2_hp.Length; i++)
                {
                    P2_hp[i].sprite = hpFull;
                    if (i >= hp)
                    {
                        P2_hp[i].sprite = hpNone;
                    }
                }
                break;
        }
    }
    public void ShowGameEndedScreen()
    {

    }

}
