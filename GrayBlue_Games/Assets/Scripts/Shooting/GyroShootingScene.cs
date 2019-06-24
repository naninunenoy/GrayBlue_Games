using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using GrayBlue;
using GrayBlue.Rx;

namespace GrayBlue_Games {

    public class GyroShootingScene : GrayBlueGameSceneBase {
        [SerializeField] Transform firstPerson = default;
        [Range(0.1F, 10.0F)] [SerializeField] float yawFactor;
        [SerializeField] Toggle gyroReverse = default;
        Peripheral peripheral = default;
        Quaternion cameraBaseQuaternion = Quaternion.identity;

        async void Start() {
            peripheral = await FindFirstGrayBlueAsync();
            if (peripheral == null) {
                return;
            }
            var baseQuat = Quaternion.identity;
            // センサ値更新
            peripheral
                .ReactiveIMUData()
                .Subscribe(x => {
                    if (cameraBaseQuaternion == Quaternion.identity) {
                        cameraBaseQuaternion = Quaternion.Inverse(x.unity.quat);
                    }
                    var q = cameraBaseQuaternion * x.unity.quat;
                    var yaw = q.eulerAngles.z * yawFactor;
                    var pitch = q.eulerAngles.x;
                    if (gyroReverse?.isOn == true) {
                        yaw *= -1.0F;
                    }
                    firstPerson.transform.rotation = Quaternion.AngleAxis(pitch, Vector3.right) * Quaternion.AngleAxis(yaw, Vector3.up);
                })
                .AddTo(this);
            // 真ん中ボタン
            peripheral
                .ButtonPushObservable()
                .Subscribe(x => {
                    cameraBaseQuaternion = Quaternion.identity;
                    firstPerson.transform.rotation = Quaternion.identity;
                })
                .AddTo(this);
            peripheral.ListenEvent();
        }
    }
}
