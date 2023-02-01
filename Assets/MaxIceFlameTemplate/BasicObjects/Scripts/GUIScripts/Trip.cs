using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MaxIceFlameTemplate.UI
{
    public class Trip : MonoBehaviour
    {
        public LowerSetting LowerCore;
        public Vector2 OnRect, OffRect;
        public Vector3 OnScale, OffScale;
        public Sprite OnSprite, OffSprite;
        [HideInInspector] public MenuSettings menu;

        private void Start()
        {
            menu = FindObjectOfType<MenuSettings>();
        }
        public void click()
        {
            Change();
            setting();

            LowerCore.NowIsTrip = true;
            LowerCore.NowIsSetting = false;

            GetComponent<Button>().enabled = false;
            LowerCore.Setting.GetComponent<Button>().enabled = true;

        }
        public void Change()
        {
            GetComponent<RectTransform>().DOAnchorPos(OnRect, 0.25f);//图标位移
            LowerCore.gameObject.GetComponent<RectTransform>().DOAnchorPos(LowerCore.To_Trip_Rect, 0.25f);//背景板位移
            GetComponent<Image>().sprite = OnSprite;//贴图修改
            GetComponent<RectTransform>().DOScale(OnScale, 0.25f);//图标大小修改
        }
        public void setting()
        {
            LowerCore.Setting.GetComponent<RectTransform>().DOAnchorPos(LowerCore.Setting.GetComponent<Setting>().OffRect, 0.25f);//设置图标位移
            LowerCore.Setting.GetComponent<Image>().sprite = LowerCore.Setting.GetComponent<Setting>().OffSprite;//贴图修改
            LowerCore.Setting.GetComponent<RectTransform>().DOScale(LowerCore.Setting.GetComponent<Setting>().OffScale, 0.25f);//图标大小修改
        }
    }
}