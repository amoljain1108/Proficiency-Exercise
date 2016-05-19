using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace proficiencyexercise
{
    public class customeview : View
    {
        Context mContext;
        public customeview(Context context) :
			base(context)
			{
            init(context);
        }
        public customeview(Context context, IAttributeSet attrs) :
			base(context, attrs)
			{
        
            init(context);
        }

        public customeview(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
			{
            init(context);
        }

        private void init(Context ctx)
        {
            mContext = ctx;
        }
        public override bool OnDragEvent(DragEvent e)
        {
            return base.OnDragEvent(e);

        }
    }
}