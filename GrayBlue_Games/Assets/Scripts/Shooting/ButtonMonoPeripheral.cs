using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using GrayBlue;
using GrayBlue.Rx;

namespace GrayBlue_Games {
    public class ButtonMonoPeripheral : MonoPeripheral {

        // button state
        private readonly Dictionary<ButtonType, bool> btnStateMap = new Dictionary<ButtonType, bool> {
            { ButtonType.Btn_A, false }, { ButtonType.Btn_B, false }, { ButtonType.Btn_C, false }
        };

        void Start() {
            ListenButtonState();
        }

        void ListenButtonState() {
            var p = Peripheral;
            if (p == null) {
                return;
            }
            p.ButtonPushObservable()
             .Subscribe(x => {
                 if (btnStateMap?.ContainsKey(x) == true) {
                     btnStateMap[x] = true;
                 }
             })
             .AddTo(this);
            p.ButtonReleaseObservable()
             .Subscribe(x => {
                 if (btnStateMap?.ContainsKey(x) == true) {
                     btnStateMap[x] = false;
                 }
             })
             .AddTo(this);
        }

        public bool IsPressed(ButtonType button) {
            if (btnStateMap.ContainsKey(button)) {
                return btnStateMap[button];
            }
            return false;
        }
    }
}
