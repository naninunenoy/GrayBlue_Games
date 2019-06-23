using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using GrayBlue;
using GrayBlue.Rx;

namespace GrayBlue_Games {

    public class GyroShootingScene : GrayBlueGameSceneBase {
        [SerializeField] Transform firstPerson;
        [Range(0.1F, 10.0F)] [SerializeField] float yawFactor;
        Peripheral peripheral;
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
                    var yaw = q.eulerAngles.y * yawFactor;
                    firstPerson.transform.rotation = Quaternion.AngleAxis(yaw, Vector3.up);
                })
                .AddTo(this);
            // 真ん中ボタン
            peripheral
                .ButtonPushObservable()
                .Subscribe(x => {
                    Debug.Log("push");
                    cameraBaseQuaternion = Quaternion.identity;
                    firstPerson.transform.rotation = Quaternion.identity;
                })
                .AddTo(this);
            peripheral.ListenEvent();
        }
    }
}
