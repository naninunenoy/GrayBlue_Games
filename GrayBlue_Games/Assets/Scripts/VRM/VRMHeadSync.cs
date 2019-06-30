using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GrayBlue;

namespace GrayBlue_Games {
    public class VRMHeadSync : GrayBlueGameSceneBase {
        [SerializeField] Transform head;

        Peripheral grayBluePeripheral;

        async void Start() {
            grayBluePeripheral = await FindFirstGrayBlueAsync();
            grayBluePeripheral.IMUSensorUpdateEvent += OnGrayBlueUpdate;
        }

        private void OnGrayBlueUpdate(IMUData data) {
            head.rotation = data.unity.quat;
        }

        void OnDestroy() {
            grayBluePeripheral.IMUSensorUpdateEvent -= OnGrayBlueUpdate;
        }
    }
}
