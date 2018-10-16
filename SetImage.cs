using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathRPG
{
    public class SetImage : MonoBehaviour
    {
        public Image image;
        private Sprite sprite;
        public static SetImage Instance { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z)) LoadImage();
        }

        public void LoadImage()
        {
            //Sprite lol = Resources.Load<Sprite>("Enemy/a");
            //image.sprite = lol;
            //this.sprite = Resources.Load<Sprite>("Enemy/a");
            //image = this.GetComponent<Image>();
            //ue is already being filled with the inspector
            //this.image.sprite = sprite;
        }
        
    }

}

