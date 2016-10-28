//!  CMath.cs
/*!
 * \details 便利関数を集約するクラス
 * \author  Shoki Tsuneyama
 * \date    2015/05/20　新規作成
 * \date    2015/05/24  ビルドに失敗したのでプリプロセッサ追加　Shoki Tsuneyama
 * \date    2015/10/08  みそてん用に編集・２D専用機能の排除、数学関連に特化
 * \date    2015/11/15  Int用の乗算を追加
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//! 数学関数を集約するクラス
public class CMath
{
    private const float m_byPixel = 64;

    //! 画面上のピクセル数をメートルに変換
    public static float PixelToMatle(float pixel)
    {
        return pixel / m_byPixel;
    }

    //! 指定された格率配列のうち1つをランダムに選ぶ
    public static int Choose(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        if (total == 0) return Random.Range(0, probs.Length - 1);

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    //! あるベクトルをあるベクトルに向けて回転させたものを取得する
    public static Vector3 GetRotatedVector3(
        Vector3 from,
        Vector3 to, 
        Vector3 zeroDegreeVector, 
        Vector3 axis,
        float maxDegree, 
        float maxDegreeDelta) 
    {
        float angleToTarget = Vector3.Angle(to, zeroDegreeVector);
        float angleNow = Vector3.Angle(from, zeroDegreeVector);

        if (angleToTarget > maxDegree)
            angleToTarget = maxDegree;
        if (to.y < 0)
            axis = -axis;

        if (angleToTarget - angleNow > maxDegreeDelta * Time.deltaTime) angleToTarget = angleNow + maxDegreeDelta * Time.deltaTime;
        if (angleToTarget - angleNow < -maxDegreeDelta * Time.deltaTime) angleToTarget = angleNow - maxDegreeDelta * Time.deltaTime;

        return Quaternion.AngleAxis(angleToTarget, axis) * zeroDegreeVector; 
    }

    public static float IntPow( float value , uint pow )
    {
        float inValue = value;
        for(int i=0; i<pow-1;++i)
        {
            value *= inValue;
        }
        return value;
    }

    //エルミート曲線
    public static Vector3 Hermitian(
        Vector3 inPos, 
        Vector3 inVec, 
        Vector3 toPos, 
        Vector3 toVec, 
        float t)
    {
        float[] h = new float[4];
        h[0] = 2 * (t * t * t) - 3 * (t * t) + 1;
        h[1] = -2 * (t * t * t) + 3 * (t * t);
        h[2] = (t * t * t) - 2 * (t * t) + t;
        h[3] = (t * t * t) - (t * t);

        return 
            (h[0] * inPos) + 
            (h[1] * toPos) + 
            (h[2] * inVec) + 
            (h[3] * toVec);
    }
    public static float Liner (float t,bool isAcce)
    {
        if (isAcce)
            return (t * t);
        else
            return t * (2 - t);
    }

    //tを3次曲線に変換
    public static float Cubic(float t)
    {
        return -2 * ( t*t*t ) + 3 * ( t*t );
    }

    //時間をtに変換
    public static float ClampTime( float time ,float slice = 1)
    {
        return Mathf.Clamp((Time.time - time)/slice,0,1);
    }

    //! Sin波の指定した時間における振幅を取得する
    public static float CalcAmplitudeOfSinWave(
        float frequency,
        float time,
        float phase = 0)
    {
        return Mathf.Sin(2 * Mathf.PI * frequency * time + phase);
    }

    //! 線形補完
    public static float Lerp( float v0 , float v1 , float t )
    {
        return Mathf.Lerp(v0, v1, t);
    }

    //! 線形補完
    public static Vector3 Lerp(Vector3 v0, Vector3 v1, float t)
    {
        Vector3 ret = new Vector3();
        ret.x = Mathf.Lerp(v0.x, v1.x, t);
        ret.y = Mathf.Lerp(v0.y, v1.y, t);
        ret.z = Mathf.Lerp(v0.z, v1.z, t);
        return ret;
    }
    //! 線形補完
    public static Color Lerp(Color v0, Color v1, float t)
    {
        Color ret = new Color();
        ret.r = Mathf.Lerp(v0.r, v1.r, t);
        ret.g = Mathf.Lerp(v0.g, v1.g, t);
        ret.b = Mathf.Lerp(v0.b, v1.b, t);
        ret.a = Mathf.Lerp(v0.a, v1.a, t);
        return ret;
    }

	/*!	
	*	\brief	始まりがゆっくりになる２次補間
	*/
	public static float EaseIn(float s)
	{
		return s * s;
	}
			
	/*!	
	*	\brief	終わりがゆっくりになる２次補間
	*/
	public static float EaseOut(float s)
	{
		return s * (2 - s);
	}
			
	/*!	
	*	\brief	始まりと終わりがゆっくりになる２次補間
	*/
	public static float EaseInEaseOut(float s)
	{
		return s * s * (3 - 2 * s);
	}

    /*!	
    *	\brief	自由落下
        *	\param  t           時間(秒)
        *	\param  gravity     重力
    */
    public static float FreeFall( float t , float gravity = -9.87f )
    {
        return 0.5f * gravity * (t * t);
    }

}