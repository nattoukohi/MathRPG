using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathRPG
{
    public class Hogemi : Enemy
    {

        // Use this for initialization
        void Start()
        {

            Life[0] = 30;
            Life[1] = 50;

            MaxLife[0] = 30;
            MaxLife[1] = 50;
            Number = 0;
            Energy = 30;
            MaxEnergy = 30;
            Attack = 5;
            Defence = 3;
            Speed = 5;
            Gold = 20;
            Experience = 5;
            Description = "ほげ実";
            //ItemDatabase.Instance.Items[1];
            Inventory.Add("ほげの実");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

