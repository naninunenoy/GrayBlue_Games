using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GrayBlue;

namespace GrayBlue_Games {
    public class M5StackImage : MonoPeripheralBase {
        [SerializeField] Text messageText = default;
        [SerializeField] Image buttonAImage = default;
        [SerializeField] Image buttonBImage = default;
        [SerializeField] Image buttonCImage = default;

        void Start() {

        }

        public void SetMessage(string message) {
            messageText.text = message;
        }
    }
}
