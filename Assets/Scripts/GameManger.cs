using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject spawnPoint;
    public UIManager UIManager;
    PlayerActor player1;
    PlayerActor player2;
    private void Start()
    {
        GameStart();
    }
    void GameStart()
    {
        
        player1 = Instantiate(playerPrefab, new Vector3(
            spawnPoint.transform.position.x,
            spawnPoint.transform.position.y,
            spawnPoint.transform.position.z),
            spawnPoint.transform.rotation).GetComponent<PlayerActor>();
        player2 = Instantiate(playerPrefab, new Vector3(
            spawnPoint.transform.position.x, 
            spawnPoint.transform.position.y, 
            spawnPoint.transform.position.z+10f), 
            spawnPoint.transform.rotation).GetComponent<PlayerActor>();
        player2.setPlayerIndex(0);
        player2.setPlayerIndex(1);
        player1.gm = this;
        player2.gm = this;

    }
    private void Update()
    {
        if (player1.muerto())
        {
            Time.timeScale = 0.1f;
            //Jugador 1 muerto
            UIManager.ShowWinner(0);
        }
        if (player2.muerto())
        {
            Time.timeScale = 0.1f;
            //Jugador 2 muerto
            UIManager.ShowWinner(1);
        }
        if (player1.isPlayerFinished)
        {
            Time.timeScale = 0.1f;
            //Jugador 1 muerto
            UIManager.ShowWinner(0);
        }
        if (player2.isPlayerFinished)
        {
            Time.timeScale = 0.1f;
            //Jugador 2 muerto
            UIManager.ShowWinner(1);
        }
        UIManager.SetHpPlayer(0, player1.GetHp());
        UIManager.SetHpPlayer(1, player2.GetHp());

    }
}
