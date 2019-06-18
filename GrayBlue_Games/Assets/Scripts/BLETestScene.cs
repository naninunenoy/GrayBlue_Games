using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GrayBlue_Games {
    public class BLETestScene : GrayBlueGameScene {
        protected override void Awake() {
            base.Awake();    
        }

        async void Start() {
            await SetUpGrayBlueAsync();
        }
    }
}
