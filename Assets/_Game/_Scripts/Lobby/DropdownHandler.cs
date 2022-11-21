using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        //dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("1920 x 1080");
        items.Add("1280 x 720");
        items.Add("Fullscreen");

        // Fill the items
        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        if(index == 0){
            if(Screen.fullScreen == true) Screen.SetResolution(1920, 1080, true);
            else Screen.SetResolution(1920, 1080, false);
        }else if(index == 1){
            if(Screen.fullScreen == true) Screen.SetResolution(1280, 720, true);
            else Screen.SetResolution(1280, 720, false);
        }else{
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
