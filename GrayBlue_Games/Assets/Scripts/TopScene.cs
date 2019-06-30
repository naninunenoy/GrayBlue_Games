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
        [SerializeField] Button headSceneButton = default;

        protected override void Awake() {
            base.Awake();
            grayBlueButton?.onClick.AddListener(async () => {
                if (!m5StackImage.IsConnected) {
                    await SearchAndSetM5StackAsync();
                }
            });
            testSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("BLETest");
            });
            shootingSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("GyroShooting");
            });
            headSceneButton?.onClick.AddListener(() => {
                SceneManager.LoadScene("HeadSync");
            });
        }

        private async void Start() {
            await SearchAndSetM5StackAsync();
        }

        private async Task SearchAndSetM5StackAsync() {
            m5StackImage.SetMessage("M5Stackを探しています");
            var str = await BindM5StackImageAsync();
            m5StackImage.SetMessage(str);
        }

        private async Task<string> BindM5StackImageAsync() {
            // GrayBlueを探す
            var grayBlue = await FindFirstGrayBlueAsync();
            if (grayBlue == null) {
                return "みつかりませんでした";
            }
            m5StackImage.Peripheral = grayBlue;
            m5StackImage.Peripheral.ListenEvent();
            return grayBlue.ID;
        }
    }
}
