using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathRPG
{
    public class HogeArcher : Enemy
    {

        // Use this for initialization
        void Start()
        {
            Life[0] = 303;
            Life[1] = 540;
            MaxLife[0] = 303;
            MaxLife[1] = 540;
            Number = 1;
            Energy = 50;
            MaxEnergy = 50;
            Attack = 7;
            Defence = 4;
            Speed = 15;
            Gold = 30;
            Experience = 10;
            Description = "ほげ弓兵";
            Inventory.Add("ほげの実2");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

