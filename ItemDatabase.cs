using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MathRPG
{
    public class ItemDatabase : MonoBehaviour
    {

        public List<string> Items { get; set; } = new List<string>();
        public static ItemDatabase Instance { get; private set; }//use set only here

        //before start
        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                //only need 1, destroy the second
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
            Items.Add("金の棒");
            Items.Add("留年の証");
            Items.Add("イキスギの枝");
        }
    }
}

