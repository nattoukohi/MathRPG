using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathRPG {

    interface IBaddie
    {
        void Cry();
    }
    public class Enemy : Character, IBaddie
    {
        //敵の名前
        public string Description { get; set; }
        // Use this for initialization
        private void Start () {
            Energy = 20;
	    }

        public void Cry()
        {

        }

        //override is needed
        public override void DifferentiateHP()
        {
            base.DifferentiateHP();
            UIController.OnEnemyUpdate(this);
        }

        public override void InsertHP(int hensuu)
        {
            base.InsertHP(hensuu);
            UIController.OnEnemyUpdate(this);
        }

        public override void TakeDamage(int amount)
        {
            //UIController.OnEnemyUpdate(this);
            base.TakeDamage(amount);
            Debug.Log("only for enemy");
            //reflect the damage from player
            UIController.OnEnemyUpdate(this);

        }

        public override void Die()
        {
            Encounter.OnEnemyDie();

            Debug.Log("敵を倒した");
            //Energy = MaxEnergy;

        }

    }
}
