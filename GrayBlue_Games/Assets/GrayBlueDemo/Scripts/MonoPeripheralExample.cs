using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GrayBlue;

namespace GrayBlueDemo {
    public class MonoPeripheralExample : MonoPeripheral {
        Quaternion baseRotarion = Quaternion.identity;

        void OnDisable() {
            Peripheral?.UnlistenEvent();
        }

        void OnDestroy() {
            Peripheral?.Dispose();
        }

        protected override void OnPeripheralLost() {
            Destroy(gameObject);
        }

        protected override void OnIMUSensorUpdate(IMUData imu) {
            var q = imu.unity.quat;
            if (baseRotarion == Quaternion.identity) {
                baseRotarion = q;
            }
            transform.rotation = Quaternion.Inverse(baseRotarion) * q;
        }

        protected override void OnButtonPush(ButtonType button) {
            // base pose reset
            baseRotarion = Quaternion.identity;
        }
    }
}
