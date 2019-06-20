using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using GrayBlue;
using GrayBlue.Rx;

namespace GrayBlue_Games {
    public class BLETestScene : GrayBlueGameSceneBase {
        [SerializeField] Transform cube;
        Peripheral peripheral;

        async void Start() {
            peripheral = await FindFirstGrayBlueAsync();
            if (peripheral == null) {
                return;
            }
            var baseQuat = Quaternion.identity;
            peripheral
                .ReactiveIMUData()
                .Take(1)
                .Subscribe(x => { baseQuat = Quaternion.Inverse(x.unity.quat); })
                .AddTo(this);
            peripheral
                .ReactiveIMUData()
                .Skip(1)
                .Subscribe(x => { cube.transform.rotation = baseQuat * x.unity.quat; })
                .AddTo(this);
            peripheral.ListenEvent();
        }
    }
}
