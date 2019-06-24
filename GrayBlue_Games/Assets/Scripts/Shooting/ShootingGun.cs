using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GrayBlue_Games {
    public class ShootingGun : MonoBehaviour {
        [SerializeField] Image reticleImage = default;
        [SerializeField] GameObject[] targets = default;

        Collider[] colliders = default;

        void Awake() {
            colliders = new Collider[targets.Length];
            for (var i = 0; i < targets.Length; i++) {
                colliders[i] = targets[i].transform.GetChild(0).GetComponent<Collider>();
            }
        }

        void Update() {
            // ヒットする方向に向いている時、レティクルの色を変える
            bool willHit = false;
            var ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                foreach (var col in colliders) {
                    if (hit.collider == col) {
                        willHit = true;
                        break;
                    }
                }
            }
            reticleImage.color = willHit ? Color.green : Color.red;
        }

        public void Shot() {
            var ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                foreach (var col in colliders) {
                    if (hit.collider == col) {
                        col.transform.parent.gameObject.SetActive(false);
                    }
                }
            }
        }

        public void ResetAllTargets() {
            foreach (var ga in targets) {
                ga.SetActive(true);
            }
        }
    }
}
