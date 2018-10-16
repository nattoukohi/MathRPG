using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathRPG
{
    public class EnemyDatabase : MonoBehaviour
    {
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public static EnemyDatabase Instance { get; set; }
        // Use this for initialization
        private void Awake()
        {
            //crash if more than one
            Instance = this;

            //getcomponents return every enemy found inside
            foreach (Enemy enemy in GetComponents<Enemy>())
            {
                Debug.Log("敵を見つけた");
                Enemies.Add(enemy);
            }

        }

        public Enemy GetRandomEnemy()
        {
            //return random enemy
            return Enemies[Random.Range(0, Enemies.Count)];
        }
    }
}

