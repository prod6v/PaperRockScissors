using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SelectButton : MonoBehaviour
    {
        public enum acts { Paper, Rock, Scissors };
        public int act;
        public Image myBoard;
        public List<Sprite> actsList = new List<Sprite>();
        public TextMeshProUGUI message;
        public static acts lastMove;
        public PCAction pcMove;

        protected void Start()
        {
            act = (int)Enum.Parse(typeof(acts), gameObject.name);
        }

        public void Execute()
        {
            myBoard.sprite = actsList[act];
            myBoard.gameObject.SetActive(true);
            var acts = (acts[])Enum.GetValues(typeof(acts));
            lastMove = acts[act];
            message.text = Strings.Wait;
            StartCoroutine(PCMove());
            foreach (var item in pcMove.buttons)
                item.interactable = false;
        }

        private IEnumerator PCMove()
        {
            yield return new WaitForSeconds(1);

            pcMove.enabled = true;
        }
    }
}
