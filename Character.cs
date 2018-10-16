using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathRPG
{


public class Character : MonoBehaviour
    {
        public int [] Life { get; set; } = new int [100];
        public int [] MaxLife { get; set; } = new int[100];
        public int Number { get; set; }// like a zukan
        public int Level { get; set; }//
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }//
        public int Experience { get; set; }
        public int Gold { get; set; }
        public Vector2 RoomIndex { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();

        //calculate the sum of hp, if zero then dead
        public int Checker { get; set; } = 1;
    
        //
        public virtual void TakeDamage(int amount)
        {
            Life[0] -= amount;


            Checker = 0;
            for (int x = 1; x < Life.Length - 1; x++)
            {
                if (Life[x] != 0) Checker += Mathf.Abs(Life[x]);

            }


            if (Checker == 0&&Life[0] <= 0)
            {
                Die();
            }
        }

        public virtual void InsertHP(int hensuu)
        {
            for (int x = 1; x < Life.Length - 1; x++)
            {

                Life[0] += Life[x]*(int)Mathf.Pow(hensuu,x);
                Life[x] = 0;

            }

            if (Life[0] <= 0)
            {
                Die();
                Checker = 0;
            }

        }

        public virtual void DifferentiateHP()
        {
            Checker = 0;
            for (int x = 0; x < Life.Length-1; x++) {
                
                    Life[x] = (x+1)*Life[x + 1];

            }
            for (int x = 1; x < Life.Length - 1; x++)
            {
                if (Life[x] != 0) Checker += Mathf.Abs(Life[x]);

            }



            if (Checker == 0 && Life[0] <= 0)
            {
                Die();
            }

                
        }

        public virtual void IntegrateHP()
        {
            Checker = 1;
            for (int x = Life.Length-1; x > 0; x--)
            {
                Life[x] = Life[x - 1]/(x);

            }
            Life[0] = 0;
        }

        public virtual void Die()
         {
            Journal.Instance.Log("あなたは死にました");
         }

    }

}
