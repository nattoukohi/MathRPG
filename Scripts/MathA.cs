using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathRPG
{
    public class MathA : Enemy
    {

        // Use this for initialization
        void Start()
        {

            Life[0] = 20;
            

            MaxLife[0] = 30;
            Number = 0;
            Energy = 30;
            MaxEnergy = 30;
            Attack = 3;
            Defence = 2;
            Speed = 5;
            Gold = 10;
            Experience = 3;
            Description = "数学A";
            //ItemDatabase.Instance.Items[1];
            Inventory.Add("参考書");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

