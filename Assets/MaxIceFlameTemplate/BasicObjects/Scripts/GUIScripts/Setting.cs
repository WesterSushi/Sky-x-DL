using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MaxIceFlameTemplate.UI
{
    public class Setting : MonoBehaviour
    {
        public LowerSetting LowerCore;
        public Vector2 OnRect, OffRect;
        public Vector3 OnScale, OffScale;
        public Sprite OnSprite, OffSprite;

        public void click()
        {
            changer();
            trip();
            LowerCore.NowIsTrip = false;
            LowerCore.NowIsSetting = true;
            GetComponent<Button>().enabled = false;
            LowerCore.Trip.GetComponent<Button>().enabled = true;
        }
        public void changer()
        {
            GetComponent<RectTransform>().DOAnchorPos(OnRect, 0.25f);
            LowerCore.gameObject.GetComponent<RectTransform>().DOAnchorPos(LowerCore.To_Setting_Rect, 0.25f);
            GetComponent<Image>().sprite = OnSprite;
            GetComponent<RectTransform>().DOScale(OnScale, 0.25f);
        }
        public void trip()
        {
            LowerCore.Trip.GetComponent<RectTransform>().DOAnchorPos(LowerCore.Trip.GetComponent<Trip>().OffRect, 0.25f);//设置图标位移
            LowerCore.Trip.GetComponent<Image>().sprite = LowerCore.Trip.GetComponent<Trip>().OffSprite;//贴图修改
            LowerCore.Trip.GetComponent<RectTransform>().DOScale(LowerCore.Trip.GetComponent<Trip>().OffScale, 0.25f);//图标大小修改
        }
    }
}