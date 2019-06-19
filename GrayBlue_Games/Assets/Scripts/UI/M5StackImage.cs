using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GrayBlue;

namespace GrayBlue_Games {
    public class M5StackImage : MonoPeripheralBase {
        [SerializeField] Text messageText;
        [SerializeField] Image buttonAImage;
        [SerializeField] Image buttonBImage;
        [SerializeField] Image buttonCImage;

        void Start() {

        }

        public void SetMessage(string message) {
            messageText.text = message;
        }
    }
}
