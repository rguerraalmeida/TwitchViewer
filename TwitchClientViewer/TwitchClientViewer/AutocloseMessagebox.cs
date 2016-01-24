using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;



namespace TwitchClientViewer
{
    internal class AutoClosingMessageBox
    {
        private const int WM_CLOSE = 16;

        private Timer _timeoutTimer;

        private string _caption;

        private AutoClosingMessageBox(string text, string caption, int timeout)
        {
            this._caption = caption;
            this._timeoutTimer = new Timer(new TimerCallback(this.OnTimerElapsed), null, timeout, -1);
            MessageBox.Show(text, caption);
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private void OnTimerElapsed(object state)
        {
            IntPtr intPtr = AutoClosingMessageBox.FindWindow(null, this._caption);
            if (intPtr != IntPtr.Zero)
            {
                AutoClosingMessageBox.SendMessage(intPtr, 16, IntPtr.Zero, IntPtr.Zero);
            }
            this._timeoutTimer.Dispose();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static void Show(string text, string caption, int timeout)
        {
            AutoClosingMessageBox autoClosingMessageBox = new AutoClosingMessageBox(text, caption, timeout);
        }
    }
}
