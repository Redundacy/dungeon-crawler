using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthenticationManager : MonoBehaviour {

    [SerializeField] private GameObject Start;

    [SerializeField] private GameObject Settings;

    [SerializeField] private GameObject SettingsMenu;

    [SerializeField] private GameObject Quit;

    public async void LoginAnonymously() {
        using (new Load("Logging you in...")) {
            await Authentication.Login();
            SceneManager.LoadSceneAsync("Lobby");
        }
    }

    public void SettingsOn(){
        Start.SetActive(false);
        Settings.SetActive(false);
        Quit.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void SettingsOff(){
        Start.SetActive(true);
        Settings.SetActive(true);
        Quit.SetActive(true);
        SettingsMenu.SetActive(false);
    }
    
    public void GameExit(){
        Application.Quit();
    }
}