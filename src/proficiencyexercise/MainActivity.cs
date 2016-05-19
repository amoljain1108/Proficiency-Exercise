using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;

namespace proficiencyexercise
{
    [Activity(Label = "proficiencyexercise", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, View.IOnTouchListener, Android.Hardware.ISensorEventListener
    {
        RelativeLayout layout;

        bool hasUpdated = false;
        DateTime lastUpdate;
        float last_x = 0.0f;
        float last_y = 0.0f;
        float last_z = 0.0f;

        const int ShakeDetectionTimeLapse = 250;
        const double ShakeThreshold = 800;

        private RelativeLayout mainlayout;
        private float _viewX;
        int ids;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            mainlayout = FindViewById<RelativeLayout>(Resource.Id.myView);
            ids = mainlayout.Id;
            mainlayout.SetOnTouchListener(this);


            //register event for sensermanager to get shaking functionality
            var sensorManager = GetSystemService(SensorService) as Android.Hardware.SensorManager;
            var sensor = sensorManager.GetDefaultSensor(Android.Hardware.SensorType.Accelerometer);
            sensorManager.RegisterListener(this, sensor, Android.Hardware.SensorDelay.Game);
        }
        public bool OnTouch(View v, MotionEvent e)
        {
            var x = e.GetX();
            var y = e.GetY();
            switch (e.Action)
            {
                case MotionEventActions.Move:
                    if (v.Id != ids)
                    {
                   
                        v.SetX(x);
                        v.SetY(y);
                    }
                    break;
                case MotionEventActions.Down:
                    _viewX = e.GetX();
                    if (v.Id == ids)
                    {
                        createView(x, y);
                    }
                    break;
            }
            return true;
        }



        int counter = 1;
        int pars = 3;
        bool circle = false;
        public void createView(float x, float y)
        {

            if (pars > 6 || pars < 1)
            {
                pars = 1;
            }
            layout = new RelativeLayout(this);
            //switch to generate random shapes
            switch (counter)
            {
                case 1:
                    if (circle == false)
                    {
                        layout.SetBackgroundResource(Resource.Drawable.la1);
                        circle = true;
                    }
                    else
                    {
                        layout.SetBackgroundColor(Android.Graphics.Color.Red);
                        circle = false;
                    }
                    counter++;

                    break;
                case 2:
                    if (circle == false)
                    {
                        layout.SetBackgroundResource(Resource.Drawable.la2);
                        circle = true;
                    }
                    else
                    {
                        layout.SetBackgroundColor(Android.Graphics.Color.Black);
                        circle = false;
                    }                                      
                    counter++;
                    break;
                case 3:
                    if (circle == false)
                    {
                        layout.SetBackgroundResource(Resource.Drawable.la3);
                        circle = true;
                    }
                    else
                    {
                        layout.SetBackgroundColor(Android.Graphics.Color.Yellow);
                        circle = false;
                    }                   
                    counter++;
                    break;
                case 4:
                    if (circle == false)
                    {
                        layout.SetBackgroundResource(Resource.Drawable.la4);
                        circle = true;
                    }
                    else
                    {
                        layout.SetBackgroundColor(Android.Graphics.Color.Pink);
                        circle = false;
                    }
                    
                     counter++;
                    break;
                case 5:
                    if (circle == false)
                    {
                        layout.SetBackgroundResource(Resource.Drawable.texview_design);
                        circle = true;
                    }
                    else
                    {
                        layout.SetBackgroundColor(Android.Graphics.Color.Blue);
                        circle = false;
                    }
                    
                    counter++;
                    break;
                case 6:
                    if (circle == false)
                    {
                        layout.SetBackgroundResource(Resource.Drawable.la2);
                        circle = true;
                    }
                    else
                    {
                        layout.SetBackgroundColor(Android.Graphics.Color.Brown);
                        circle = false;
                    }
                   
                    counter++;
                    break;
            }
            //switch to set randome shapes size
            switch (pars)
            {
                case 1:
                    layout.LayoutParameters = new ViewGroup.LayoutParams(50, 50);
                    pars++;

                    break;
                case 2:
                    layout.LayoutParameters = new ViewGroup.LayoutParams(150, 150);
                    pars++;
                    break;
                case 3:
                    layout.LayoutParameters = new ViewGroup.LayoutParams(40, 40);
                    pars++;
                    break;
                case 4:
                    layout.LayoutParameters = new ViewGroup.LayoutParams(75, 75);
                    pars++;

                    break;
                case 5:
                    layout.LayoutParameters = new ViewGroup.LayoutParams(120, 120);
                    pars++;
                    break;
                case 6:
                    layout.LayoutParameters = new ViewGroup.LayoutParams(130, 130);
                    pars++;
                     
                    break;
            }
            if (counter < 1 || counter > 6)
            {
                counter = 1;
            }
            
            layout.SetX(x);
            layout.SetY(y);
            layout.SetOnTouchListener(this);
            mainlayout.AddView(layout);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            throw new NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            float x = e.Values[0];
            float y = e.Values[1];
            float z = e.Values[2];

            DateTime curTime = System.DateTime.Now;
            if (hasUpdated == false)
            {
                hasUpdated = true;
                lastUpdate = curTime;
                last_x = x;
                last_y = y;
                last_z = z;
            }
            else
            {
                if ((curTime - lastUpdate).TotalMilliseconds > ShakeDetectionTimeLapse)
                {
                    float diffTime = (float)(curTime - lastUpdate).TotalMilliseconds;
                    lastUpdate = curTime;
                    float total = x + y + z - last_x - last_y - last_z;
                    float speed = Math.Abs(total) / diffTime * 10000;

                    if (speed > ShakeThreshold)
                    {
                       // Toast.MakeText(this, "shake detected w/ speed: " + speed, ToastLength.Short).Show();
                    }

                    if(speed > 2500)
                    {
                        mainlayout.RemoveAllViews();
                    }

                    last_x = x;
                    last_y = y;
                    last_z = z;
                }
            }
        }
    }
}

