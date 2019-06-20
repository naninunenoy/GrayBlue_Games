using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GrayBlue;

namespace GrayBlue_Games {
    public class M5StackImage : MonoPeripheralBase {
        [SerializeField] Text messageText = default;
        [SerializeField] Image displayImage = default;
        [SerializeField] Image buttonAImage = default;
        [SerializeField] Image buttonBImage = default;
        [SerializeField] Image buttonCImage = default;

        Dictionary<ButtonType, Image> btnMap = default;

        private void Awake() {
            btnMap = new Dictionary<ButtonType, Image> {
                { ButtonType.Btn_A,  buttonAImage},
                { ButtonType.Btn_B,  buttonBImage},
                { ButtonType.Btn_C,  buttonCImage}
            };
        }

        public void SetMessage(string message) {
            messageText.text = message;
        }

        protected override void OnPeripheralLost() {
            base.OnPeripheralLost();
            messageText.text = "切断されました";
        }
        protected override void OnButtonPush(ButtonType button) {
            if (btnMap.ContainsKey(button)) {
                btnMap[button].color = Color.red;
            }
        }

        protected override void OnButtonRelease(ButtonType button, float pressTime) {
            if (btnMap.ContainsKey(button)) {
                btnMap[button].color = Color.white;
            }
        }
    }
}
