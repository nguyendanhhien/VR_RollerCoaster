using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

[ExecuteInEditMode]
[RequireComponent(typeof(SplineContainer))]
public class TaoDuongRayTuDong : MonoBehaviour
{
    [Header("1. Xếp các Cube dọc theo đường ray.")]
    [Header("2. Xoay Cube để tạo độ nghiêng (lộn ngược) cho tàu.")]
    [Header("3. Kéo tất cả các Cube đó làm 'con' của vật thể này.")]
    [Header("4. Tick vào ô bên dưới để tự động nối dây!")]

    public bool BẤM_VÀO_ĐÂY_ĐỂ_TẠO = false;

    void Update()
    {
        if (BẤM_VÀO_ĐÂY_ĐỂ_TẠO)
        {
            BẤM_VÀO_ĐÂY_ĐỂ_TẠO = false;
            TaoSplineTuCacVatTheCon();
        }
    }

    void TaoSplineTuCacVatTheCon()
    {
        // Lấy hệ thống Spline
        SplineContainer container = GetComponent<SplineContainer>();
        if (container.Splines.Count == 0) container.AddSpline();

        Spline spline = container.Spline;
        spline.Clear();

        int soCon = transform.childCount;
        if (soCon == 0)
        {
            Debug.LogWarning("Bạn chưa cho vật thể nào làm con cả!");
            return;
        }

        // Tự động quét các điểm neo và vẽ đường
        for (int i = 0; i < soCon; i++)
        {
            Transform diemNeo = transform.GetChild(i);

            // Tính toán vị trí và góc lộn vòng
            float3 viTri = container.transform.InverseTransformPoint(diemNeo.position);
            quaternion gocNghieng = math.mul(math.inverse(container.transform.rotation), diemNeo.rotation);

            BezierKnot knot = new BezierKnot(viTri, float3.zero, float3.zero, gocNghieng);
            spline.Add(knot);

            // Tự động làm mượt đường cong
            spline.SetTangentMode(i, TangentMode.AutoSmooth);
        }

        Debug.Log("🎉 Đã tự động nối " + soCon + " điểm thành đường ray!");
    }
}