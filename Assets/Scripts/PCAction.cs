using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.SelectButton;

namespace Assets.Scripts
{

    public class PCAction : MonoBehaviour
    {
        public Image targetImage;
        public Sprite[] sprites;
        public float initialDelay = 1.0f;
        public float speedDecreaseAmount = 0.1f;
        public float minChangeInterval = 0.1f;

        private int currentIndex = 0;
        private float timer = 0.0f;
        private float changeInterval;
        private int randomIndex;
        public Image image;
        public TextMeshProUGUI message;
        public List<Button> buttons;
        public GameObject result;
        public Transform resultParent;
        private int loose;
        public static int win;
        public GameObject resultPopup;

        private void OnEnable()
        {
            if (sprites.Length > 0)
            {
                targetImage.sprite = sprites[currentIndex];
                changeInterval = initialDelay;
            }
            randomIndex = UnityEngine.Random.Range(0, sprites.Length);
            image.enabled = true;
        }

        void Update()
        {

            timer += Time.deltaTime;

            if (timer >= changeInterval)
            {
                currentIndex++;
                if (currentIndex < sprites.Length)
                {
                    targetImage.sprite = sprites[currentIndex];
                }
                else
                {
                    currentIndex = 0;
                    targetImage.sprite = sprites[currentIndex];

                    changeInterval = Mathf.Max(minChangeInterval, changeInterval - speedDecreaseAmount);
                }

                timer = 0;
            }

            if (changeInterval <= minChangeInterval && currentIndex >= sprites.Length - 1)
            {
                targetImage.sprite = sprites[randomIndex];
                enabled = false;

                var acts = (acts[])Enum.GetValues(typeof(acts));
                var act = acts[randomIndex];
                bool pcWin = false;
                if (SelectButton.lastMove != act)
                {
                    switch (act)
                    {
                        case SelectButton.acts.Paper:
                            pcWin = SelectButton.lastMove != SelectButton.acts.Scissors ?
                                  true : false;
                            break;
                        case SelectButton.acts.Rock:
                            pcWin = SelectButton.lastMove != SelectButton.acts.Paper ?
                                  true : false;
                            break;
                        case SelectButton.acts.Scissors:
                            pcWin = SelectButton.lastMove != SelectButton.acts.Rock ?
                                  true : false;
                            break;
                    }

                    var resultObj = Instantiate(result, resultParent);

                    var text = resultObj.transform
                        .Find("Text")
                        .GetComponent<TextMeshProUGUI>();

                    text.text = SetText(pcWin, text);
                    if (win == 5 || loose == 5)
                        resultPopup.SetActive(true);
                    else
                        message.text = Strings.SelectAgain;
                }
                else
                {
                    message.text = Strings.Even;
                }

                foreach (var item in buttons)
                    item.interactable = true;
            }
        }

        private string SetText(bool pcWin, TextMeshProUGUI text)
        {
            if (pcWin)
            {
                text.color = Color.red;
                loose++;
                return Strings.Loose;
            }
            else
            {
                text.color = Color.green;
                win++;
                return Strings.Win;
            }
        }
    }
}
