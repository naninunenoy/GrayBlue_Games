using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrayBlue_Games {
    public class TopScene : GrayBlueGameSceneBase {
        [SerializeField] Button grayBlueButton = default;
        [SerializeField] M5StackImage m5StackImage = default;
        [SerializeField] Button testSceneButton = default;
        [SerializeField] Button shootingSceneButton = default;

        protected override void Awake() {
            base.Awake();
            grayBlueButton?.onClick.AddListener(async () => {
                if (!m5StackImage.HasPeripheral) {
                    await BindM5StackImageAsync();
                }
            });
            testSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("BLETest");
            });
            shootingSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("GyroShooting");
            });
        }

        private async void Start() {
            await BindM5StackImageAsync();
        }

        private void OnDestroy() {
            m5StackImage?.Peripheral?.UnlistenEvent();
        }

        private async Task<bool> BindM5StackImageAsync() {
            // GrayBlueを探す
            var grayBlue = await FindFirstGrayBlueAsync();
            if (grayBlue == null) {
                return false; // 見つからなかった
            }
            m5StackImage.Peripheral = grayBlue;
            m5StackImage.Peripheral.ListenEvent();
            return true;
        }
    }
}
