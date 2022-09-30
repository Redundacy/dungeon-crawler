using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CreateLobbyScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInput;

    private void Start() {
        void SetOptions(TMP_Dropdown dropdown, IEnumerable<string> values) {
            dropdown.options = values.Select(type => new TMP_Dropdown.OptionData { text = type }).ToList();
        }
    }

    public static event Action<LobbyData> LobbyCreated;

    public void OnCreateClicked() {
        var lobbyData = new LobbyData {
            Name = _nameInput.text,
            MaxPlayers = 2
        };
        LobbyCreated?.Invoke(lobbyData);
    }
}

public struct LobbyData {
    public string Name;
    public int MaxPlayers;
}