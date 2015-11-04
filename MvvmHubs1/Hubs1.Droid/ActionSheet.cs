using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Views.InputMethods;
using Android.Widget;

namespace Hubs1.Droid
{
    public class ActionSheet : Dialog, View.IOnClickListener
    {

        /* 控件的id */
        private const int CancelButtonId = 100;
        private const int BgViewId = 10;
        private const int TranslateDuration = 300;
        private const int AlphaDuration = 300;

        private readonly Context _mContext;
        private Attributes _mAttrs;
        private IMenuItemClickListener _mListener;
        private View _mView;
        private LinearLayout _mPanel;
        private View _mBg;
        private List<string> _items;
        private string _cancelTitle = "";
        private bool _mCancelableOnTouchOutside;
        private bool _mDismissed = true;
        private bool _isCancel = true;

        public ActionSheet(Context context) : base(context, Android.Resource.Style.ThemeLightNoTitleBar)// 全屏
        {

            this._mContext = context;
            InitViews();
            if (Window != null)
            {
                Window.SetGravity(GravityFlags.Bottom);
                Drawable drawable = new ColorDrawable();
                drawable.Alpha = 0;// 去除黑色背景
                Window.SetBackgroundDrawable(drawable);
            }
        }


        public void InitViews()
        {
            /* 隐藏软键盘 */
            InputMethodManager imm = (InputMethodManager)_mContext.GetSystemService(Context.InputMethodService);
            if (imm.IsActive)
            {
                View focusView = ((Activity)_mContext).CurrentFocus;
                if (focusView != null)
                    imm.HideSoftInputFromWindow(focusView.WindowToken, 0);
            }
            _mAttrs = ReadAttribute();// 获取主题属性
            _mView = CreateView();
            _mBg.StartAnimation(CreateAlphaInAnimation());
            _mPanel.StartAnimation(CreateTranslationInAnimation());
        }

        private Animation CreateTranslationInAnimation()
        {
            const Dimension type = Dimension.RelativeToSelf; //TranslateAnimation.RELATIVE_TO_SELF;
            var an = new TranslateAnimation(type, 0, type, 0, type, 1, type, 0)
            {
                Duration = TranslateDuration
            };
            return an;
        }

        private Animation CreateAlphaInAnimation()
        {
            var an = new AlphaAnimation(0, 1)
            {
                Duration = AlphaDuration
            };
            return an;
        }

        private Animation CreateTranslationOutAnimation()
        {
            const Dimension type = Dimension.RelativeToSelf;
            TranslateAnimation an = new TranslateAnimation(type, 0, type, 0, type, 0, type, 1)
            {
                Duration = TranslateDuration,
                FillAfter = true
            };
            return an;
        }

        private Animation CreateAlphaOutAnimation()
        {
            AlphaAnimation an = new AlphaAnimation(1, 0)
            {
                Duration = AlphaDuration,
                FillAfter = true
            };
            return an;
        }

        /**
         * 创建基本的背景视图
         */
        private View CreateView()
        {
            FrameLayout parent = new FrameLayout(_mContext);
            FrameLayout.LayoutParams parentParams =
                new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
                {
                    Gravity = GravityFlags.Bottom
                };
            parent.LayoutParameters = parentParams;
            _mBg = new View(_mContext)
            {
                LayoutParameters =
                    new ActionBar.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
            };
            _mBg.SetBackgroundColor(Color.Argb(136, 0, 0, 0));
            _mBg.Id = BgViewId;
            _mBg.SetOnClickListener(this);

            _mPanel = new LinearLayout(_mContext);
            FrameLayout.LayoutParams mPanelParams = new FrameLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
            {
                Gravity = GravityFlags.Bottom
            };
            _mPanel.LayoutParameters = mPanelParams;
            //mPanel.setOrientation(LinearLayout.VERTICAL);
            //mPanel.Orientation = Linear
            parent.AddView(_mBg);
            parent.AddView(_mPanel);
            return parent;
        }

        /**
         * 创建MenuItem
         */
        private void CreateItems()
        {
            //if (_items != null && _items.Count > 0)
            //    for (int i = 0; i < _items.Count; i++)
            //    {
            //        var itemButton = new Button(_mContext)
            //        {
            //            Id = CancelButtonId + i + 1
            //        };
            //        itemButton.SetOnClickListener(this);
            //        itemButton.Background = GetOtherButtonBg(_items.ToArray(), i);
            //        //itemButton.SetBackgroundDrawable(GetOtherButtonBg(_items.ToArray(), i));
            //        itemButton.Text = _items[i];
            //        itemButton.SetTextColor(new Color(_mAttrs.OtherButtonTextColor));
            //        itemButton.SetTextSize(ComplexUnitType.Dip, _mAttrs.ActionSheetTextSize);
            //        if (i > 0)
            //        {
            //            LinearLayout.LayoutParams layoutParams = CreateButtonLayoutParams();
            //            layoutParams.TopMargin = _mAttrs.OtherButtonSpacing;
            //            _mPanel.AddView(itemButton, layoutParams);
            //        }
            //        else
            //            _mPanel.AddView(itemButton);
            //    }
            Button bt = new Button(_mContext);
            bt.Paint.FakeBoldText = true;
            bt.SetTextSize(ComplexUnitType.Dip, _mAttrs.ActionSheetTextSize);
            bt.Id = CancelButtonId;
            bt.Background = _mAttrs.CancelButtonBackground;
            //bt.SetBackgroundDrawable(_mAttrs.CancelButtonBackground);
            bt.Text = _cancelTitle;
            bt.SetTextColor(new Color(_mAttrs.CancelButtonTextColor));
            bt.SetOnClickListener(this);
            LinearLayout.LayoutParams param = CreateButtonLayoutParams();
            param.TopMargin = _mAttrs.CancelButtonMarginTop;
            _mPanel.AddView(bt, param);
            _mPanel.Background = _mAttrs.Background;
            //_mPanel.SetBackgroundDrawable(_mAttrs.Background);
            _mPanel.SetPadding(_mAttrs.Padding, _mAttrs.Padding, _mAttrs.Padding, _mAttrs.Padding);
        }

        public LinearLayout.LayoutParams CreateButtonLayoutParams()
        {
            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.WrapContent);
            return layoutParams;
        }

        /**
         * item按钮的颜色
         * 
         * @param titles
         * @param i
         * @return
         */
        private Drawable GetOtherButtonBg(string[] titles, int i)
        {
            if (titles.Length == 1)
                return _mAttrs.GetOtherButtonMiddleBackground();
            else if (titles.Length == 2)
                switch (i)
                {
                    case 0:
                        return _mAttrs.GetOtherButtonMiddleBackground();
                    case 1:
                        return _mAttrs.GetOtherButtonMiddleBackground();
                }
            else if (titles.Length > 2)
            {
                if (i == 0)
                    return _mAttrs.GetOtherButtonMiddleBackground();
                else if (i == (titles.Length - 1))
                    return _mAttrs.GetOtherButtonMiddleBackground();
                return _mAttrs.GetOtherButtonMiddleBackground();
            }
            return null;
        }

        public void ShowMenu()
        {
            if (!_mDismissed)
                return;
            Show();
            Window.SetContentView(_mView);
            _mDismissed = false;
        }

        /**
         * dissmiss Menu菜单
         */
        public void DismissMenu()
        {
            if (_mDismissed)
                return;
            Dismiss();
            OnDismiss();
            _mDismissed = true;
        }

        /**
         * dismiss时的处理
         */
        private void OnDismiss()
        {
            _mPanel.StartAnimation(CreateTranslationOutAnimation());
            _mBg.StartAnimation(CreateAlphaOutAnimation());
        }

        /**
         * 取消按钮的标题文字
         * 
         * @param title
         * @return
         */
        public ActionSheet SetCancelButtonTitle(string title)
        {
            _cancelTitle = title;
            return this;
        }

        /**
         * 取消按钮的标题文字
         * 
         * @param strId
         * @return
         */
        public ActionSheet SetCancelButtonTitle(int strId)
        {
            return SetCancelButtonTitle(_mContext.GetString(strId));
        }

        /**
         * 点击外部边缘是否可取消
         * 
         * @param cancelable
         * @return
         */
        public ActionSheet SetCancelableOnTouchMenuOutside(bool cancelable)
        {
            _mCancelableOnTouchOutside = cancelable;
            return this;
        }

        public ActionSheet AddItems(string[] titles)
        {
            if (titles == null || titles.Length == 0)
                return this;
            _items = titles.ToList();
            CreateItems();
            return this;
        }

        public ActionSheet SetItemClickListener(IMenuItemClickListener listener)
        {
            this._mListener = listener;
            return this;
        }

        private Attributes ReadAttribute()
        {
            Attributes attrs = new Attributes(_mContext);
            TypedArray a = _mContext.Theme.ObtainStyledAttributes(null, Resource.Styleable.ActionSheet,
                    Resource.Attribute.actionSheetStyle, 0);
            Drawable background = a.GetDrawable(Resource.Styleable.ActionSheet_actionSheetBackground);
            if (background != null)
                attrs.Background = background;
            Drawable cancelButtonBackground = a.GetDrawable(Resource.Styleable.ActionSheet_cancelButtonBackground);
            if (cancelButtonBackground != null)
                attrs.CancelButtonBackground = cancelButtonBackground;
            Drawable otherButtonTopBackground = a.GetDrawable(Resource.Styleable.ActionSheet_otherButtonTopBackground);
            if (otherButtonTopBackground != null)
                attrs.OtherButtonTopBackground = otherButtonTopBackground;
            Drawable otherButtonMiddleBackground = a
                    .GetDrawable(Resource.Styleable.ActionSheet_otherButtonMiddleBackground);
            if (otherButtonMiddleBackground != null)
                attrs.OtherButtonMiddleBackground = otherButtonMiddleBackground;
            Drawable otherButtonBottomBackground = a
                    .GetDrawable(Resource.Styleable.ActionSheet_otherButtonBottomBackground);
            if (otherButtonBottomBackground != null)
                attrs.OtherButtonBottomBackground = otherButtonBottomBackground;
            Drawable otherButtonSingleBackground = a
                    .GetDrawable(Resource.Styleable.ActionSheet_otherButtonSingleBackground);
            if (otherButtonSingleBackground != null)
                attrs.OtherButtonSingleBackground = otherButtonSingleBackground;
            attrs.CancelButtonTextColor = a.GetColor(Resource.Styleable.ActionSheet_cancelButtonTextColor,
                    attrs.CancelButtonTextColor);
            attrs.OtherButtonTextColor = a.GetColor(Resource.Styleable.ActionSheet_otherButtonTextColor,
                    attrs.OtherButtonTextColor);
            attrs.Padding = (int)a.GetDimension(Resource.Styleable.ActionSheet_actionSheetPadding, attrs.Padding);
            attrs.OtherButtonSpacing = (int)a.GetDimension(Resource.Styleable.ActionSheet_otherButtonSpacing,
                    attrs.OtherButtonSpacing);
            attrs.CancelButtonMarginTop = (int)a.GetDimension(Resource.Styleable.ActionSheet_cancelButtonMarginTop,
                    attrs.CancelButtonMarginTop);
            attrs.ActionSheetTextSize = a.GetDimensionPixelSize(Resource.Styleable.ActionSheet_actionSheetTextSize,
                    (int)attrs.ActionSheetTextSize);

            a.Recycle();
            return attrs;
        }


        public void OnClick(View v)
        {
            if (v.Id == BgViewId && !_mCancelableOnTouchOutside)
                return;
            DismissMenu();
            if (v.Id == CancelButtonId || v.Id == BgViewId) return;
            if (_mListener != null)
                _mListener.OnItemClick(v.Id - CancelButtonId - 1);
            _isCancel = false;
        }

        /**
         * 自定义属性的控件主题
         * 
         * @author Mr.Zheng
         * @date 2014年9月7日 下午10:47:06
         */
        private class Attributes
        {
            private readonly Context _mContext;

            public Drawable Background { get; set; }
            public Drawable CancelButtonBackground { get; set; }
            public Drawable OtherButtonTopBackground { get; set; }
            public Drawable OtherButtonMiddleBackground { get; set; }
            public Drawable OtherButtonBottomBackground { get; set; }
            public Drawable OtherButtonSingleBackground { get; set; }
            public int CancelButtonTextColor { get; set; }
            public int OtherButtonTextColor { get; set; }
            public int Padding { get; set; }
            public int OtherButtonSpacing { get; set; }
            public int CancelButtonMarginTop { get; set; }
            public float ActionSheetTextSize { get; set; }

            public Attributes(Context context)
            {
                _mContext = context;
                Background = new ColorDrawable(Color.Transparent);
                CancelButtonBackground = new ColorDrawable(Color.Black);
                ColorDrawable gray = new ColorDrawable(Color.Gray);
                OtherButtonTopBackground = gray;
                OtherButtonMiddleBackground = gray;
                OtherButtonBottomBackground = gray;
                OtherButtonSingleBackground = gray;
                CancelButtonTextColor = Color.White;
                OtherButtonTextColor = Color.Black;
                Padding = Dp2Px(20);
                OtherButtonSpacing = Dp2Px(2);
                CancelButtonMarginTop = Dp2Px(10);
                ActionSheetTextSize = Dp2Px(16);
            }

            private int Dp2Px(int dp)
            {
                return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, _mContext.Resources.DisplayMetrics);
            }

            public Drawable GetOtherButtonMiddleBackground()
            {
                if (OtherButtonMiddleBackground is StateListDrawable)
                {
                    TypedArray a = _mContext.Theme.ObtainStyledAttributes(null, Resource.Styleable.ActionSheet,
                            Resource.Attribute.actionSheetStyle, 0);
                    OtherButtonMiddleBackground = a.GetDrawable(Resource.Styleable.ActionSheet_otherButtonMiddleBackground);
                    a.Recycle();
                }
                return OtherButtonMiddleBackground;
            }

        }

       
        
    }
}