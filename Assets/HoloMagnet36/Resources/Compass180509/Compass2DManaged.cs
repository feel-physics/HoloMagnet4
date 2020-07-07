using UnityEngine;
using System.Collections;

public class Compass2DManaged : MonoBehaviour
{
    /* このオブジェクトを中心に回転する。Pivot。ここから。 */
    public float gizmoSize = 0.02f;
    public Color gizmoColor = Color.yellow;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }
    /* このオブジェクトを中心に回転する。Pivot。ここまで。 */

    GameObject[] southPoles;
    GameObject[] northPoles;
    MeshRenderer meshRenderer;
    Material materialNorth;
    Material materialSouth;

    Vector3 forceResultantPrevious;

    //AudioSource audioSource;

    float brightness = 0.001f;  // 明るさの係数

    void Start()    
    {
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        materialNorth = meshRenderer.materials[0];
        materialSouth = meshRenderer.materials[1];

        //audioSource = gameObject.GetComponent<AudioSource>();

        // Update Managerに自分を登録する
        UpdateManager um = GameObject.Find("UpdateManager").GetComponent<UpdateManager>();
		um.list2D.Add(this);
    }

    public void UpdateMe()
    {
        northPoles = GameObject.FindGameObjectsWithTag("North Pole");
        southPoles = GameObject.FindGameObjectsWithTag("South Pole");

        // コンパスを回転させる
        RotateCompass();
    }

    Vector3 ForceResultant()
    {
        Vector3 forceResultant = new Vector3();

        foreach (var northPole in northPoles)
        {
            // 自身からN極への変位ベクトル
            Vector3 vectorSelfToNorthPole = northPole.transform.position - transform.position;
            // N極によるクーロン力
            Vector3 forceByNorthPole = -vectorSelfToNorthPole.normalized / vectorSelfToNorthPole.sqrMagnitude;

            forceResultant += forceByNorthPole;
        }

        foreach (var southPole in southPoles)
        {
            // 自身からS極への変位ベクトル
            Vector3 vectorSelfToSouthPole = southPole.transform.position - transform.position;
            // S極によるクーロン力
            Vector3 forceBySouthPole = vectorSelfToSouthPole.normalized / vectorSelfToSouthPole.sqrMagnitude;

            forceResultant += forceBySouthPole;
        }

        // クーロン力の合力
        return forceResultant;
    }

    void RotateCompass()
    {
        Vector3 forceResultant = ForceResultant();

        // コンパスの向きを設定する
        transform.LookAt(transform.position + forceResultant);
        transform.Rotate(-90f, 0f, 0f);

        // 合力の差分
        /*
        Vector3 difForceResultant = forceResultant - forceResultantPrevious;
        if (2.0 < difForceResultant.magnitude)
        {
            audioSource.Play();
        }
        */

        // クーロン力を格納
        forceResultantPrevious = forceResultant;

        // 合力の大きさ
        //float brightnessOfForce = forceResultant.magnitude * brightness;
        float brightnessOfForce = forceResultant.sqrMagnitude * brightness;
		//MyHelper.DebugLog(brightnessOfForce.ToString());

		Color northColor = materialNorth.GetColor("_Emission");
		Color southColor = materialSouth.GetColor("_Emission");

		// 色の強さを変える
		materialNorth.color = ColorWithBrightness(true , northColor, brightnessOfForce);
        materialSouth.color = ColorWithBrightness(false, southColor, brightnessOfForce);

        // Emissioonを変える
        // Unity5のStandardシェーダのパラメタをスクリプトからいじろうとして丸一日潰れた話 - D.N.A.のおぼえがき
        // http://dnasoftwares.hatenablog.com/entry/2015/03/19/100108
        materialNorth.SetColor("_Emission", ColorWithBrightness(true, northColor, brightnessOfForce));
        materialSouth.SetColor("_Emission", ColorWithBrightness(false, southColor, brightnessOfForce));
    }

    // カラーオブジェクトをプリロード（あらかじめ作っておく）して入れ替える
    static Color originalCompassNorthColor = new Color(1f, 0.384f, 0.196f);
    static Color originalCompassSouthColor = new Color(0.341f, 0.525f, 1f);

    Color ColorWithBrightness(bool isNorth, Color color, float brightness)
    {
        Color originalColor;
        if (isNorth)  // if文
        {
            originalColor = originalCompassNorthColor;
        }
        else
        {
            originalColor = originalCompassSouthColor;
        }
        if (1.0f < brightness)
        {
            brightness = 1.0f + (brightness - 1.0f) * 0.00050f;  // 最後の値のさじ加減が大切
        }
        else if (brightness <= 0.5f)
        {
            brightness = 0.0f + brightness; // 明るさの最低値を決める。プロジェクターに出すときは0.5くらいで。
        }
        float colorR = originalColor.r * brightness;
        float colorG = originalColor.g * brightness;
        float colorB = originalColor.b * brightness;
        return new Color(colorR, colorG, colorB);
    }

    private void OnDestroy()
    {
		// Update Managerの登録を削除する
		// このタイミングで実行が終了する場合、UpdateManagerが先に削除される可能性もあるため、nullチェックを挟んでおく.
		GameObject updateManagerObj = GameObject.Find("UpdateManager");
		if (updateManagerObj == null)
		{
			return;
		}
        UpdateManager um = updateManagerObj.GetComponent<UpdateManager>();
		if (um == null)
		{
			return;
		}
		um.list2D.Remove(this);
	}
}