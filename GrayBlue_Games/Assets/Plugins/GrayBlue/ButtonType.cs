using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrayBlue {
    public enum ButtonType {
        Unknown = 0,
        Btn_A = 1,
        Btn_B = 2,
        Btn_C = 3,
    }

    public static class ButtonTypeExtension {
        private static readonly Dictionary<string, ButtonType> btnMap =
            new Dictionary<string, ButtonType> {
                {"A", ButtonType.Btn_A },
                {"B", ButtonType.Btn_B },
                {"C", ButtonType.Btn_C },
            };
        public static ButtonType ToButtonType(this string str) {
            if (btnMap.ContainsKey(str)) {
                return btnMap[str];
            }
            return ButtonType.Unknown;
        }
        public static string ToCharacterStr(this ButtonType button) {
            switch (button) {
            case ButtonType.Btn_A:
                return "A";
            case ButtonType.Btn_B:
                return "B";
            case ButtonType.Btn_C:
                return "C";
            default:
                return "?";
            }
        }
    }
}
