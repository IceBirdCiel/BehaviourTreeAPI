using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGSauce.Games.DropDaBomb;

public class Besoins : MonoBehaviour
{
    public class Besoin
    {
        public string besoinName;

        public float value;

        public float drecreaseOverTime;

        public float increaseOverTime;

        public bool isIncreasing;

        public bool isDeadly;

        public Besoin(string besoinName, float value, float drecreaseOverTime, float increaseOverTime, bool isDeadly)
        {
            this.besoinName = besoinName;
            this.value = value;
            this.drecreaseOverTime = drecreaseOverTime;
            this.increaseOverTime = increaseOverTime;
            this.isIncreasing = false;
            this.isDeadly = isDeadly;
        }
    }

    public Dictionary<string, Besoin> besoins;

    public PGEventBesoins GameEvent;



    // Start is called before the first frame update
    void Start()
    {
        besoins = new Dictionary<string, Besoin>();
        besoins.Add("Appetit", new Besoin("Appetit", 100, 10f, 1, true));
        besoins.Add("Confort", new Besoin("Confort", 100, 10f, 1, false));
        besoins.Add("Petits_besoins", new Besoin("Petits_besoins", 100, 10f, 1, false));
        besoins.Add("Energie", new Besoin("Energie", 100, 10f, 1, false));
        besoins.Add("Distractions", new Besoin("Distractions", 100, 10f, 1, false));
        besoins.Add("Vie_sociale", new Besoin("Vie_sociale", 100, 10f, 1, false));
        besoins.Add("Hygiène", new Besoin("Hygiène", 100, 10f, 1, false));
        besoins.Add("Environnement", new Besoin("Environnement", 100, 10f, 1, false));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Besoin b in besoins.Values)
        {
            if (b != null)
            {
                if (b.isIncreasing)
                {
                    b.value -= b.increaseOverTime * Time.deltaTime;
                }
                else
                {
                    b.value -= b.drecreaseOverTime * Time.deltaTime;
                }
                //Debug.Log(besoinList[i].besoinName + " : " + besoinList[i].value);
            }
        }
        GameEvent.Raise(this.besoins);        
    }
}
