
#ifndef Sidebar7_h
#define Sidebar7_h

#include <windows.h>

//If your application uses multiple processes please call this macro on your ui windows
//For example Google chrome uses a process for each tab. Without this function the
//7 Sidebar feature for suspending a process or muting one would be useless because
//these features only work on the process of the window the user is working on.
//If this macro is called 7 Sidebar searches for all processes of the application
//If your application starts a different exe to delegate some work (e.g. like firefox does
//with plugin-container.exe) and you would like to make your application compatible with
//7 Sidebar, please make the helper exe start with the name of the main exe. E.g. "opera.exe" and
//"opera_plugin_wrapper.exe". 7 Sidebar will find it automatically then.
//If that's not possible please contact me via the feedback function in the settings dialog.
#define SetMultiProcessApplication(hwnd) (SetPropW((hwnd), L"Sidebar7MultiProcessApplication", (HANDLE)1))


//The 7 Sidebar Gadget adds the ability to update the thumbnail of a window while it is minimized.
//This is done by restoring every window as soon as it is minimized without the users notice and moving
//it off-screen. This may break some applications which do something special when they are minimized.
//(But I'm not aware of a single application that breaks. Even not fullscreen games)
//With this API you can handle the additional state "Fakeminimized".

//In the state "Fakeminimized" the window has the following properties:
// - IsIconic() will return FALSE
// - The Window coordinates will be offscreen so you won't normally see it
// - As soon as the window position changes the window is moved back off-screen
// - The Maximized-State won't change. So if the window was maximized before fakeminimizing or was maximized
//   before minimized and then fakeminimized it will still be maximized while fakeminimized (although not visible
//   on screen)
// - Generally only windows that appear in the taskbar can be fakeminimized. 

#define IsWindowFakeMinimized(hwnd) ((BOOL)(GetPropW((hwnd), L"Sidebar7IsFakeMinimized") != 0))
//Use this macro to determine if a window is fakeminimized

#define DisableWindowFakeMinimizing(hwnd) (SetPropW((hwnd), L"Sidebar7DisableFakeMinimizing", (HANDLE)1))
//Call this macro if your application breaks when this feature is used and you are too lazy to fix it correctly.
//Call this in WM_CREATE of every window that can appear in the taskbar to ensure that it is not
//fakeminimized.

const UINT WM_FAKEMINIMIZE = RegisterWindowMessageW(L"Sidebar7FakeMinimize");
//This Message is sent to a window when it eventually was fakeminimized. Use IsWindowFakeMinimized() to be sure.
//This is usually called by the 7 Sidebar-Gadget a short time after the window has been minimized by the user.
//You can also send this message by your own in order to fakeminimize a window:
//SendMessage(hwnd, WM_FAKEMINIMIZE, 0, 0);
//Note that you have to send it with SendMessage, PostMessage won't work.
//If the 7 Sidebar-Gadget isn't running the message won't have any effect, so you need to call
//IsWindowFakeMinimized() after sending the message to find out if it worked.
//The message will do basically the following:
// - If the window is restored or maximized it is moved off-screen
// - If it is minimized it is first hidden, restored, moved off-screen and shown again. (The taskbar position is preserved)

const UINT WM_UNFAKEMINIMIZE = RegisterWindowMessageW(L"Sidebar7UnFakeMinimize");
//This message is sent to a window when it lost the FakeMinimized-State.
//This is usually sent when the user wants to restore a fakeminimized window (with the taskbar,
//alt-tab or similar)
//wParam is - FALSE when the window is now restored (or maximized). So it is visible to the user now.
//          - TRUE when the window is now minimized. This happens for example when the 7 Sidebar-Gadget
//            is closed. The user shouldn't notice anything when wParam is TRUE except that the preview isn't
//            updated anymore.
//
//You can call this message on your own in order to unfakeminimize a window:
//SendMessage(hwnd, WM_UNFAKEMINIMIZE, (BOOL)Minimize, 0);
//You have to use SendMessage, PostMessage won't work.


#endif
