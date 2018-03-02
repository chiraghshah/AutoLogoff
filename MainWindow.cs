 using System.Windows.Interop;
 private int logOffTime = 0;

        private void InitializeAutoLogoff()
        {
            try
            {
                try
                {
                    logOffTime = 7200;//Enter Log off time in seconds..
                }
                catch (Exception ex)
                {
                    Log.Error("ReadConfig :: Exception in ReadConfig :: " + ex.Message);
                }
                HwndSource windowSpecificOSMessageListener = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
                windowSpecificOSMessageListener.AddHook(new HwndSourceHook(CallBackMethod));
                //AutoLogOff.MakeAutoLogOffEvent += new AutoLogOff.MakeAutoLogOff(AutoLogOff_MakeAutoLogOffEvent);
                AutoLogOff.LogOffTime = logOffTime;
                AutoLogOff.MakeAutoLogOffEvent += new AutoLogOff.MakeAutoLogOff(AutoLogOffHelper_MakeAutoLogOffEvent);
                AutoLogOff.StartAutoLogoffOption();
            }
            catch (Exception ex)
            {
                Log.Error("InitializeAutoLogoff :: Exception in InitializeAutoLogoff :: " + ex.Message);
            }
        }

        private IntPtr CallBackMethod(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //  Listening OS message to test whether it is a user activity
            //Log.Debug("MainWindow :: CallBackMethod");
            try
            {
                if ((msg >= 0x0200 && msg <= 0x020A) || (msg <= 0x0106 && msg >= 0x00A0) || msg == 0x0021)
                {
                    AutoLogOff.ResetLogoffTimer();
                 }
                else
                {
                    // If this auto logoff does not work for some user activity, you can detect the integer code of that activity  using the following line.
                    //Then All you need to do is adding this integer code to the above if condition.
                    System.Diagnostics.Debug.WriteLine(msg.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Error("CallBackMethod :: Exception in CallBackMethod :: " + ex.Message);
            }
            return IntPtr.Zero;
        }

        private void Logoff()
        {
            //Log.Debug("MainWindow :: Logoff");
            try
            {
                //do log off things here..example: grid.clear(); or something like that..
            }
            catch (Exception ex)
            {
                Log.Error("Logoff :: Exception : " + ex.Message);
            }
        }

        void AutoLogOffHelper_MakeAutoLogOffEvent()
        {
            //Log.Debug("MainWindow :: AutoLogOffHelper_MakeAutoLogOffEvent");
            try
            {
                Logoff();
            }
            catch (Exception ex)
            {
                Log.Error("AutoLogOffHelper_MakeAutoLogOffEvent :: Exception in AutoLogOffHelper_MakeAutoLogOffEvent :: " + ex.Message);
            }
        }
