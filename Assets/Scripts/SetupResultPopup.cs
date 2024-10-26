using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class SetupResultPopup:MonoBehaviour
    {
        public TextMeshProUGUI text;

        private void OnEnable()
        {
            if (PCAction.win == 5)
                text.text = Strings.WinMessage;
            else
                text.text = Strings.LooseMessage;
        }
    }
}
