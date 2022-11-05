using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] GameObject chooseMenu; 

    [SerializeField] private PlayerController _playerPrefabChoice;
    public void Choosing(){
        gameManager._playerPrefab = _playerPrefabChoice;
        chooseMenu.SetActive(false);
    }
}
