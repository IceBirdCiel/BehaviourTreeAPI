using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour
{

    public Slider appetit;
    public Slider confort;
    public Slider petitsBesoins;
    public Slider energie;
    public Slider distractions;
    public Slider vieSociale;
    public Slider hygiène;
    public Slider environnement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reactToEvent(Dictionary<string, Besoins.Besoin> besoins)
    {
        foreach (Besoins.Besoin besoin in besoins.Values)
        {
            switch (besoin.besoinName)
            {
                case "Appetit":
                    appetit.value = besoin.value / 100;
                    changeColor(appetit);
                    break;
                case "Confort":
                    confort.value = besoin.value / 100;
                    changeColor(confort);
                    break;
                case "Petits_besoins":
                    petitsBesoins.value = besoin.value / 100; 
                    changeColor(petitsBesoins);
                    break;
                case "Energie":
                    energie.value = besoin.value / 100;
                    changeColor(energie);
                    break;
                case "Distractions":
                    distractions.value = besoin.value / 100;
                    changeColor(distractions);
                    break;
                case "Vie_sociale":
                    vieSociale.value = besoin.value / 100;
                    changeColor(vieSociale);
                    break;
                case "Hygiene":
                    hygiène.value = besoin.value / 100;
                    changeColor(hygiène);
                    break;
                case "Environnement":
                    environnement.value = besoin.value / 100;
                    changeColor(environnement);
                    break;
            }
        }
    }

    private void changeColor(Slider slider)
    {
        if (slider.value < 0.55)
        {
            if (slider.value < 0.25)
                slider.fillRect.gameObject.GetComponent<Image>().color = Color.red;
            else
                slider.fillRect.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            slider.fillRect.gameObject.GetComponent<Image>().color = Color.green;
        }
    }
}
