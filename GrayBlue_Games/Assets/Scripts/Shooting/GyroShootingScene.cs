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
        Dictionary<ButtonType, bool> btnOnMap = default;

        protected override void Awake() {
            base.Awake();
            btnOnMap = new Dictionary<ButtonType, bool> {
                { ButtonType.Btn_A, false}, { ButtonType.Btn_B, false}, { ButtonType.Btn_C, false}
            };
        }

        async void Start() {
            peripheral = await FindFirstGrayBlueAsync();
            if (peripheral == null) {
                return;
            }
            // toggle変更時にカメラリセット
            gyroReverse?.onValueChanged.AddListener(_ => { ResetCameraRotation(); });
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
            // ボタンの挙動
            peripheral
                .ButtonPushObservable()
                .Subscribe(btn => {
                    if (btnOnMap.ContainsKey(btn)) {
                        btnOnMap[btn] = true;
                    }
                    // 両端ボタン同時押しでリセット
                    if (btnOnMap[ButtonType.Btn_A] && btnOnMap[ButtonType.Btn_C]) {
                        ResetCameraRotation();
                    }
                    // 真ん中ボタンでシュート
                    if (btn == ButtonType.Btn_B) {
                        Debug.Log("shot");
                    }
                })
                .AddTo(this);
            peripheral
                .ButtonReleaseObservable()
                .Subscribe(btn => {
                    if (btnOnMap.ContainsKey(btn)) {
                        btnOnMap[btn] = false;
                    }
                })
                .AddTo(this);
            peripheral.ListenEvent();
        }

        void ResetCameraRotation() {
            cameraBaseQuaternion = Quaternion.identity;
            firstPerson.transform.rotation = Quaternion.identity;
        }
    }
}
