//!  iTweenEx.cs
/*!
 * \details iTweenの文字列を使いやすくしたクラス
 * \author  Shoki Tsuneyama
 * \date    15.11.04　新規作成
 */
public class CiTweenEx {
	public const string EASE_TYPE = "easetype";
	public const string LOOP_TYPE = "looptype";
	public enum EaseType {
		InQuad,    OutQuad,    InOutQuad,
		InCubic,   OutCubic,   InOutCubic,
		InQuart,   OutQuart,   InOutQuart,
		InQuint,   OutQuint,   InOutQuint,
		InSine,    OutSine,    InOutSine,
		InExpo,    OutExpo,    InOutExpo,
		InCirc,    OutCirc,    InOutCirc,
		InBounce,  OutBounce,  InOutBounce,
		InBack,    OutBack,    InOutBack,
		InElastic, OutElastic, InOutElastic,
		Linear,    Spring,
	}
	public enum LoopType {
		None, Loop, PingPong,
	}
	public static string ToString(EaseType t) {
		string[] s = new string[]{
			"easeInQuad", "easeOutQuad", "easeInOutQuad",
			"easeInCubic", "easeOutCubic", "easeInOutCubic",
			"easeInQuart", "easeOutQuart", "easeInOutQuart",
			"easeInQuint", "easeOutQuint", "easeInoutQuint",
			"easeInSine", "easeOutSine", "easeInOutSine",
			"easeInExpo", "easeOutExpo", "easeInOutExpo",
			"easeInCirc", "easeOutCirc", "easeInOutCirc",
			"easeInBounce", "easeOutBounce", "easeInOutBounce",
			"easeInBack", "easeOutBack", "easeInOutBack",
			"easeInElastic", "easeOutElastic", "easeInOutElastic",
			"linear", "spring",
		};
		return s[(int)t];
	}
	public static string ToString(LoopType t) {
		switch (t) {
		case LoopType.None: return "none";
		case LoopType.Loop: return "loop";
		case LoopType.PingPong: return "pingpong";
		}
		return null;
	}
}
