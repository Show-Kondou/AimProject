using UnityEngine;
using System.Collections;

public enum EEaseType
{
    InQuad,
    OutQuad,
    InOutQuad,
    InCubic,
    OutCubic,
    InOutCubic,
    InQuart,
    OutQuart,
    InOutQuart,
    InQuint,
    OutQuint,
    InOutQuint,
    InSine,
    OutSine,
    InOutSine,
    InExpo,
    OutExpo,
    InOutExpo,
    InCirc,
    OutCirc,
    InOutCirc,
    Linear,
    Spring,
    InBounce,
    OutBounce,
    InOutBounce,
    InBack,
    OutBack,
    InOutBack,
    InElastic,
    OutElastic,
    InOutElastic,
    Punch
}

public delegate float FEase(float start, float end, float value);
//!  CEase.cs
/*!
 * \details CEase	補完系
 * \author  Shoki Tsuneyama
 * \date    2016/10/22	新規作成

 */
public class CEase
{
    static public FEase LINER
    {
        get { return new FEase(linear); }
    }
    static public FEase CUBIC
    {
        get { return new FEase(easeInOutCubic); }
    }
    static public FEase SPRING
    {
        get { return new FEase(spring); }
    }
    
    //補完を取得
    static public FEase GetEasingFunction(EEaseType easeType)
    {
        FEase ease = LINER;
        switch (easeType)
        {
            case EEaseType.InQuad:
                ease = new FEase(easeInQuad);
                break;
            case EEaseType.OutQuad:
                ease = new FEase(easeOutQuad);
                break;
            case EEaseType.InOutQuad:
                ease = new FEase(easeInOutQuad);
                break;
            case EEaseType.InCubic:
                ease = new FEase(easeInCubic);
                break;
            case EEaseType.OutCubic:
                ease = new FEase(easeOutCubic);
                break;
            case EEaseType.InOutCubic:
                ease = new FEase(easeInOutCubic);
                break;
            case EEaseType.InQuart:
                ease = new FEase(easeInQuart);
                break;
            case EEaseType.OutQuart:
                ease = new FEase(easeOutQuart);
                break;
            case EEaseType.InOutQuart:
                ease = new FEase(easeInOutQuart);
                break;
            case EEaseType.InQuint:
                ease = new FEase(easeInQuint);
                break;
            case EEaseType.OutQuint:
                ease = new FEase(easeOutQuint);
                break;
            case EEaseType.InOutQuint:
                ease = new FEase(easeInOutQuint);
                break;
            case EEaseType.InSine:
                ease = new FEase(easeInSine);
                break;
            case EEaseType.OutSine:
                ease = new FEase(easeOutSine);
                break;
            case EEaseType.InOutSine:
                ease = new FEase(easeInOutSine);
                break;
            case EEaseType.InExpo:
                ease = new FEase(easeInExpo);
                break;
            case EEaseType.OutExpo:
                ease = new FEase(easeOutExpo);
                break;
            case EEaseType.InOutExpo:
                ease = new FEase(easeInOutExpo);
                break;
            case EEaseType.InCirc:
                ease = new FEase(easeInCirc);
                break;
            case EEaseType.OutCirc:
                ease = new FEase(easeOutCirc);
                break;
            case EEaseType.InOutCirc:
                ease = new FEase(easeInOutCirc);
                break;
            case EEaseType.Linear:
                ease = new FEase(linear);
                break;
            case EEaseType.Spring:
                ease = new FEase(spring);
                break;
            case EEaseType.InBounce:
                ease = new FEase(easeInBounce);
                break;
            case EEaseType.OutBounce:
                ease = new FEase(easeOutBounce);
                break;
            case EEaseType.InOutBounce:
                ease = new FEase(easeInOutBounce);
                break;
            /* GFX47 MOD END */
            case EEaseType.InBack:
                ease = new FEase(easeInBack);
                break;
            case EEaseType.OutBack:
                ease = new FEase(easeOutBack);
                break;
            case EEaseType.InOutBack:
                ease = new FEase(easeInOutBack);
                break;
            /* GFX47 MOD START */
            /*case EaseType.elastic:
                ease = new EasingFunction(elastic);
                break;*/
            case EEaseType.InElastic:
                ease = new FEase(easeInElastic);
                break;
            case EEaseType.OutElastic:
                ease = new FEase(easeOutElastic);
                break;
            case EEaseType.InOutElastic:
                ease = new FEase(easeInOutElastic);
                break;
            /* GFX47 MOD END */
        }
        return ease;
    }

    #region Easing Curves
    static private float linear(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value);
    }

    static private float clerp(float start, float end, float value)
    {
        float min = 0.0f;
        float max = 360.0f;
        float half = Mathf.Abs((max - min) * 0.5f);
        float retval = 0.0f;
        float diff = 0.0f;
        if ((end - start) < -half)
        {
            diff = ((max - start) + end) * value;
            retval = start + diff;
        }
        else if ((end - start) > half)
        {
            diff = -((max - end) + start) * value;
            retval = start + diff;
        }
        else retval = start + (end - start) * value;
        return retval;
    }

    static private float spring(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }

    static private float easeInQuad(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    static private float easeOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

    static private float easeInOutQuad(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value + start;
        value--;
        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }

    static private float easeInCubic(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value + start;
    }

    static private float easeOutCubic(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value + 1) + start;
    }

    static private float easeInOutCubic(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value + start;
        value -= 2;
        return end * 0.5f * (value * value * value + 2) + start;
    }

    static private float easeInQuart(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value + start;
    }

    static private float easeOutQuart(float start, float end, float value)
    {
        value--;
        end -= start;
        return -end * (value * value * value * value - 1) + start;
    }

    static private float easeInOutQuart(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value + start;
        value -= 2;
        return -end * 0.5f * (value * value * value * value - 2) + start;
    }

    static private float easeInQuint(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value * value + start;
    }

    static private float easeOutQuint(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value * value * value + 1) + start;
    }

    static private float easeInOutQuint(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value * value + start;
        value -= 2;
        return end * 0.5f * (value * value * value * value * value + 2) + start;
    }

    static private float easeInSine(float start, float end, float value)
    {
        end -= start;
        return -end * Mathf.Cos(value * (Mathf.PI * 0.5f)) + end + start;
    }

    static private float easeOutSine(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Sin(value * (Mathf.PI * 0.5f)) + start;
    }

    static private float easeInOutSine(float start, float end, float value)
    {
        end -= start;
        return -end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) + start;
    }

    static private float easeInExpo(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Pow(2, 10 * (value - 1)) + start;
    }

    static private float easeOutExpo(float start, float end, float value)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * value) + 1) + start;
    }

    static private float easeInOutExpo(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * Mathf.Pow(2, 10 * (value - 1)) + start;
        value--;
        return end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2) + start;
    }

    static private float easeInCirc(float start, float end, float value)
    {
        end -= start;
        return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
    }

    static private float easeOutCirc(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * Mathf.Sqrt(1 - value * value) + start;
    }

    static private float easeInOutCirc(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return -end * 0.5f * (Mathf.Sqrt(1 - value * value) - 1) + start;
        value -= 2;
        return end * 0.5f * (Mathf.Sqrt(1 - value * value) + 1) + start;
    }

    static private float easeInBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        return end - easeOutBounce(0, end, d - value) + start;
    }

    static private float easeOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }

    static private float easeInOutBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        if (value < d * 0.5f) return easeInBounce(0, end, value * 2) * 0.5f + start;
        else return easeOutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
    }

    static private float easeInBack(float start, float end, float value)
    {
        end -= start;
        value /= 1;
        float s = 1.70158f;
        return end * (value) * value * ((s + 1) * value - s) + start;
    }

    static private float easeOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value = (value) - 1;
        return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
    }

    static private float easeInOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value /= .5f;
        if ((value) < 1)
        {
            s *= (1.525f);
            return end * 0.5f * (value * value * (((s) + 1) * value - s)) + start;
        }
        value -= 2;
        s *= (1.525f);
        return end * 0.5f * ((value) * value * (((s) + 1) * value + s) + 2) + start;
    }

    static private float punch(float amplitude, float value)
    {
        float s = 9;
        if (value == 0)
        {
            return 0;
        }
        else if (value == 1)
        {
            return 0;
        }
        float period = 1 * 0.3f;
        s = period / (2 * Mathf.PI) * Mathf.Asin(0);
        return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
    }

    static private float easeInElastic(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d) == 1) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
    }

    static private float easeOutElastic(float start, float end, float value)
    {
        /* GFX47 MOD END */
        //Thank you _in rafael.marteleto for fixing this as a port over from Pedro's UnityTween
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d) == 1) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p * 0.25f;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
    }

    static private float easeInOutElastic(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d * 0.5f) == 2) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        if (value < 1) return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
    }		
	#endregion	
}
