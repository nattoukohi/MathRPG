using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathRPG
{
    public class Encounter : MonoBehaviour
    {
        [SerializeField] Image image;
        private Sprite sprite;
        [SerializeField] Player player;
        //updownleftright
        [SerializeField] Button[] movingDirection;
        [SerializeField] Button[] dynamicControls;
        // Use this for initialization
        public Enemy Enemy { get; set; }
        //something like a pointer
        public delegate void OnEnemyDieHandler();
        public static OnEnemyDieHandler OnEnemyDie;
        private int trapDamage;
        //enemy images
        Sprite[] enemyImages;

        //hide and show button
        public GameObject inputFieldGameObject;
        public GameObject inputFieldGameObject2;
        public GameObject inputFieldGameObject3;

        private void Awake()
        {
            enemyImages = Resources.LoadAll<Sprite>("Enemy/");
        }

        //also call loot when enemy dies
        private void Start()
        {
            
            OnEnemyDie += Loot;
        }

        bool isActive = false;

        public void Bibun()
        {
            isActive = !isActive;
            inputFieldGameObject.SetActive(isActive);
            inputFieldGameObject2.SetActive(isActive);
            inputFieldGameObject3.SetActive(isActive);
        }

        public void ResetDynamicControls()
        {
            inputFieldGameObject.SetActive(false);
            inputFieldGameObject2.SetActive(false);
            inputFieldGameObject3.SetActive(false);
            foreach (Button button in dynamicControls)
            {
                button.interactable = false;
            }
        }

        public void AbleMovingDirection()
        {
            foreach (Button button in movingDirection)
            {
                button.interactable = true;
            }
        }

        public void DisableMovingDirection()
        {
            foreach (Button button in movingDirection)
            {
                button.interactable = false;
            }
        }

        public void LoadEnemyImage(int a)
        {
            
            image.sprite = enemyImages[a];
            //sprite = Resources.Load<Sprite>("Enemy/a");
            //image = this.GetComponent<Image>();
            //ue is already being filled with the inspector
            //image.sprite = sprite;
        }

        public void StartCombat()
        {
            
            //prevent player moving when enemy appears
            DisableMovingDirection();
            this.Enemy = player.Room.Enemy;
            LoadEnemyImage(Enemy.Number);
            dynamicControls[0].interactable = true;
            dynamicControls[1].interactable = true;
            dynamicControls[2].interactable = true;
            dynamicControls[5].interactable = true;
            UIController.OnEnemyUpdate(this.Enemy);
        }

        public void StartChest()
        {
            dynamicControls[3].interactable = true;
        }

        public void Stairs()
        {
            dynamicControls[4].interactable = true;
        }

 

        public void EmptyRoom()
        {
            dynamicControls[1].interactable = true;
            dynamicControls[2].interactable = true;
        }

        public void OpenChest()
        {
            Chest chest = player.Room.Chest;
            if (chest.Trap)
            {
                trapDamage = player.Life[0] / 5;
                player.TakeDamage(trapDamage);
                Journal.Instance.Log("罠だよ！" + trapDamage +"ダメージを受けた！");
            }
            else if(chest.Heal)
            {
                trapDamage = player.Life[0] / 10;
                player.TakeDamage(-trapDamage);
                Journal.Instance.Log("回復の泉だよ！" + trapDamage + "HPを回復した！");
            }else if (chest.Enemy)
            {
                player.Room.Enemy = chest.Enemy;
                player.Room.Chest = null;
                Journal.Instance.Log("敵が出現した！");
                //show enemy
                UIController.OnEnemyUpdate(this.Enemy);
                player.Investigate();
            }
            else
            {
                player.Gold += chest.Gold;
                player.AddItem(chest.Item);
                UIController.OnPlayerStatChange();
                UIController.OnPlayerInventoryChange();
                Journal.Instance.Log("回復の泉だよ！" + trapDamage + "ダメージを回復した！");
            }
            player.Room.Chest = null;
            dynamicControls[3].interactable = false;
        }

        public void Attack()
        {
            int playerDamageAmount = (int)(Random.value * (player.Attack*10 - Enemy.Defence* Enemy.Defence));
            int enemyDamageAmount = (int)(Random.value * (Enemy.Attack * 10 - player.Defence * player.Defence));

            if(player.Speed >= Enemy.Speed)
            {
                Journal.Instance.Log("<color=#59ffa1><b>" + playerDamageAmount + "</b>ダメージを当たえた！</color>");

                Enemy.TakeDamage(playerDamageAmount);
                //if enemy is alive
                if (Enemy)
                {
                    if (Enemy.Checker != 0 || Enemy.Life[0] > 0)
                    {
                        player.TakeDamage(enemyDamageAmount);
                        Journal.Instance.Log("<color=#59ffa1><b>" + enemyDamageAmount + "</b>ダメージを受けた！</color>");
                    }
                }
            }
            else {
                player.TakeDamage(enemyDamageAmount);
                Journal.Instance.Log("<color=#59ffa1><b>" + enemyDamageAmount + "</b>ダメージを受けた！</color>");
                //if enemy is alive
                if (player)
                {
                    if (player.Checker != 0 || player.Life[0] > 0)
                    {
                        Journal.Instance.Log("<color=#59ffa1><b>" + playerDamageAmount + "</b>ダメージを当たえた！</color>");

                        Enemy.TakeDamage(playerDamageAmount);
                        
                    }
                }

            }



        }

        public void Insert()
        {
            int playerDamageAmount = (int)(Random.value * (Mathf.Abs(Enemy.Level - player.Level) + Enemy.Defence));
            int enemyDamageAmount = (int)(Random.value * (Enemy.Attack * 10 - player.Defence * player.Defence));


            if (player.Speed >= Enemy.Speed)
            {
                Journal.Instance.Log("<color=#59ffa1><b></b>相手の変数ｘに"+ playerDamageAmount + "を代入した！</color>");

                Enemy.InsertHP(playerDamageAmount);

                //if enemy is alive
                if (Enemy)
                {
                    if (Enemy.Checker != 0 || Enemy.Life[0] > 0)
                    {
                        player.TakeDamage(enemyDamageAmount);
                        Journal.Instance.Log("<color=#59ffa1><b>" + enemyDamageAmount + "</b>ダメージを受けた！</color>");
                    }
                }
            }
            else
            {
                player.TakeDamage(enemyDamageAmount);
                Journal.Instance.Log("<color=#59ffa1><b>" + enemyDamageAmount + "</b>ダメージを受けた！</color>");
                //if player is alive
                if (player)
                {
                    if (player.Checker != 0 || player.Life[0] > 0)
                    {
                        Journal.Instance.Log("<color=#59ffa1><b></b>相手の変数ｘに" + playerDamageAmount + "を代入した！</color>");

                        Enemy.InsertHP(playerDamageAmount);



                    }
                }

            }
        }


        public void Differentiate()
        {
            int playerDamageAmount = (int)(Random.value * (player.Attack * 10 - Enemy.Defence * Enemy.Defence));
            int enemyDamageAmount = (int)(Random.value * (Enemy.Attack * 10 - player.Defence * player.Defence));

            if (player.Speed >= Enemy.Speed)
            {
                Journal.Instance.Log("<color=#59ffa1><b></b>相手を微分した！</color>");

                Enemy.DifferentiateHP();

                //if enemy is alive
                if (Enemy)
                {
                    if (Enemy.Checker != 0 || Enemy.Life[0] > 0)
                    {
                        player.TakeDamage(enemyDamageAmount);
                        Journal.Instance.Log("<color=#59ffa1><b>" + enemyDamageAmount + "</b>ダメージを受けた！</color>");
                    }
                }
            }
            else
            {
                player.TakeDamage(enemyDamageAmount);
                Journal.Instance.Log("<color=#59ffa1><b>" + enemyDamageAmount + "</b>ダメージを受けた！</color>");
                //if player is alive
                if (player)
                {
                    //Journal.Instance.Log(player.Checker.ToString());
                    //Journal.Instance.Log(player.Life[0].ToString());
                    if (player.Checker != 0 || player.Life[0] > 0)
                    {
                        Journal.Instance.Log("<color=#59ffa1><b></b>相手を微分した！</color>");

                        Enemy.DifferentiateHP();

 

                    }
                }

            }
        }

        public void Sekibun()
        {
            player.IntegrateHP();
            Journal.Instance.Log(player.Life[1].ToString());
        }

        public void Flee()
        {
            int enemyDamageAmount = (int)(Random.value * (Enemy.Attack * 20 - player.Defence * player.Defence));
            player.Room.Enemy = null;
            UIController.OnEnemyUpdate(null);
            player.TakeDamage(enemyDamageAmount);
            Journal.Instance.Log("<color=#59ffa1><b>" + enemyDamageAmount + "</b>ダメージを受けた！</color>");
            AbleMovingDirection();
            player.Investigate();
        }

        public void ExitFloor()
        {
            StartCoroutine(player.world.GenerateFloor());
            player.Floor += 1;
            Journal.Instance.Log(player.Floor + "階に進んだ");
            //went down stairs, disable it
            dynamicControls[4].interactable = false;
        }

        public void Loot()
        {
            
            player.Experience += this.Enemy.Experience;
            player.Gold += this.Enemy.Gold;
            Journal.Instance.Log(string.Format("{0}を倒した！{1}経験値と{2}ゴールドを手に入れた！",this.Enemy.Description, this.Enemy.Experience, this.Enemy.Gold));
            player.LevelUp();
            //earns item dialog
            player.AddItem(this.Enemy.Inventory[0]);
            //print(player.Gold);
            LoadEnemyImage(enemyImages.Length-1);
            player.Room.Enemy = null;
            player.Room.Empty = true;
            //hide bibun
            //Bibun();
            AbleMovingDirection();
            this.Enemy = null;
            //player.Investigate();
            //when enemy is dead, lock the attack commands
            ResetDynamicControls();
            //update the money
            UIController.OnPlayerStatChange();
            UIController.OnEnemyUpdate(this.Enemy);
            //Enemy.Energy = Enemy.MaxEnergy;
            
        }
    }
}

