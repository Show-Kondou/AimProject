

//!  CCandlePL.cs
/*!
 * \details CCandlePL	キャンドルのポイントライト設定
 * \author  show kondou
 * \date    2016 10 / 18	新規作成

 */
using UnityEngine;
using System.Collections;



//!  CCnadlePL.cs
/*!
 * \details CCnadlePL	ポイントライト操作クラス
 * \author  Show Kondou
 * \date    2016 10 / 18	新規作成

 */
public class CCandlePL : MonoBehaviour {
	// =====  メンバ変数 =====
	private const float m_flashSpeed = 0.08F;
	private float m_Range;
	private Light m_pointLight;


	// ===== メンバ関数 =====
	/*!  Start()
	*!   \details	オブジェクト生成時呼び出し関数
	*!
	*!   \return	none
	*/
	void Start () {
		m_pointLight = GetComponent<Light>();
		if( !m_pointLight ) {
			Debug.Log( "ポイントライトの取得に失敗。" );
		}
		m_Range = m_pointLight.range;
	}



	/*!  Update()
	*!   \details	更新時呼び出し関数
	*!
	*!   \return	none
	*/
	void Update () {
		var f = Mathf.Sin( Time.frameCount * m_flashSpeed ) * m_Range + m_Range * 2.0F;
		// Debug.Log( f );
		m_pointLight.range = f;
	}
}
