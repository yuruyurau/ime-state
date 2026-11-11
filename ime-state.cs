using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("imm32.dll")]
    static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    const uint WM_IME_CONTROL = 0x0283;
    const int IMC_GETOPENSTATUS = 0x0005;
    const int IMC_SETOPENSTATUS = 0x0006;

    static void Main(string[] args)
    {
        // 現在アクティブな（最前面の）ウィンドウを取得
        IntPtr foregroundWnd = GetForegroundWindow();
        if (foregroundWnd == IntPtr.Zero) return;

        // そのウィンドウに紐づくデフォルトIMEウィンドウを取得
        IntPtr imeWnd = ImmGetDefaultIMEWnd(foregroundWnd);
        if (imeWnd == IntPtr.Zero) return;

        if (args.Length == 0)
        {
            // 引数なし：現在のIME状態を取得（1ならON、0ならOFF）
            IntPtr status = SendMessage(imeWnd, WM_IME_CONTROL, (IntPtr)IMC_GETOPENSTATUS, IntPtr.Zero);
            Console.WriteLine(status != IntPtr.Zero ? "ON" : "OFF");
        }
        else
        {
            // 引数あり：ON / OFF を切り替え
            string cmd = args[0].ToLower();
            if (cmd == "on" || cmd == "1")
            {
                SendMessage(imeWnd, WM_IME_CONTROL, (IntPtr)IMC_SETOPENSTATUS, (IntPtr)1);
            }
            else if (cmd == "off" || cmd == "0")
            {
                SendMessage(imeWnd, WM_IME_CONTROL, (IntPtr)IMC_SETOPENSTATUS, IntPtr.Zero);
            }
        }
    }
}
