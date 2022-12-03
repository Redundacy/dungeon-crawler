using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthenticationManager : MonoBehaviour {

    [SerializeField] private GameObject Start;

    [SerializeField] private GameObject Settings;

    [SerializeField] private GameObject SettingsMenu;

    [SerializeField] private GameObject Credit;

    [SerializeField] private GameObject CreditMenu;

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
        Credit.SetActive(false);
        Quit.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void SettingsOff(){
        Start.SetActive(true);
        Settings.SetActive(true);
        Credit.SetActive(true);
        Quit.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void CreditOn(){
        Start.SetActive(false);
        Settings.SetActive(false);
        Credit.SetActive(false);
        Quit.SetActive(false);
        CreditMenu.SetActive(true);
    }

    public void CreditOff(){
        Start.SetActive(true);
        Settings.SetActive(true);
        Credit.SetActive(true);
        Quit.SetActive(true);
        CreditMenu.SetActive(false);
    }
    
    public void GameExit(){
        Application.Quit();
    }
}