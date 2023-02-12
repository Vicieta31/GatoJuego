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

    public GameObject prompt1;
    public GameObject prompt2;
    public GameObject promptPrefab;
    public Sprite[] prompts;

    public GameObject WinnerPanel;
    public Text winnerText;

    public void ShowWinner(int playerIndex)
    {
        WinnerPanel.SetActive(true);
        if (playerIndex == 0)
        {
            winnerText.text = "PLAYER 1 WINS!";
        }
        else
        {
            winnerText.text = "PLAYER 2 WINS!";
        }
    }



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
     public void SetPromptsPlayer(int indexPlayer, KeyCode[] keyCodes)
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            switch (keyCodes[i])
            {
               
                case KeyCode.UpArrow:
                    SpawnPrompt(indexPlayer, prompts[0 + 4]);
                    break;
                case KeyCode.DownArrow:
                    SpawnPrompt(indexPlayer, prompts[2 + 4]);
                    break;
                case KeyCode.RightArrow:
                    SpawnPrompt(indexPlayer, prompts[1 + 4]);
                    break;
                case KeyCode.LeftArrow:
                    SpawnPrompt(indexPlayer, prompts[3 + 4]);
                    break;
                case KeyCode.W:
                    SpawnPrompt(indexPlayer, prompts[0]);
                    break;
                case KeyCode.S:
                    SpawnPrompt(indexPlayer, prompts[2]);
                    break;
                case KeyCode.A:
                    SpawnPrompt(indexPlayer, prompts[1]);
                    break;
                case KeyCode.D:
                    SpawnPrompt(indexPlayer, prompts[3]);
                    break;
                
                
                
                default:
                    break;
            }
        }
    }

    private void SpawnPrompt(int indexPlayer, Sprite sprite)
    {
        if (indexPlayer == 0)
        {
            var createImage = Instantiate(promptPrefab) as GameObject;
            createImage.transform.SetParent(prompt1.transform, false);
            createImage.GetComponent<Image>().sprite = sprite;
        }
        else
        {
            var createImage = Instantiate(promptPrefab) as GameObject;
            createImage.transform.SetParent(prompt2.transform, false);
            createImage.GetComponent<Image>().sprite = sprite;
        }
    }
    public void DeletePromptsOnScreen(int playerIndex)
    {
        if (playerIndex == 0)
        {
            foreach (Transform c in prompt1.transform)
            {
                GameObject.Destroy(c.gameObject);
            }
        }
        else
        {
            foreach (Transform c in prompt2.transform)
            {
                GameObject.Destroy(c.gameObject);
            }
        }
        
        
    }
    public void ShowGameEndedScreen()
    {

    }

}
