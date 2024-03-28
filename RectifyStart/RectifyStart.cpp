// RectifyStart.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "RectifyStart.h"
#include "..\RectifyControlPanel2\dui70\DirectUI\DirectUI.h"
#include "..\RectifyControlPanel2\Rectify11CPL\Guid.h"
#include "..\RectifyControlPanel2\Rectify11CPL\IRectifyUtil_h.h"
#pragma comment(lib,"dui70.lib")
using namespace DirectUI;

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name
HWNDElement* hwnd_element = NULL;
NativeHWNDHost* pwnd = NULL;
static HWND hwnd = NULL;
struct EventListener : public IElementListener {

	using handler_t = std::function<void(Element*, Event*)>;

	handler_t f;

	EventListener(handler_t f) : f(f) { }

	void OnListenerAttach(Element* elem) override { }
	void OnListenerDetach(Element* elem) override { }
	bool OnPropertyChanging(Element* elem, const PropertyInfo* prop, int unk, Value* v1, Value* v2) override {
		return true;
	}
	void OnListenedPropertyChanged(Element* elem, const PropertyInfo* prop, int type, Value* v1, Value* v2) override { }
	void OnListenedEvent(Element* elem, struct Event* iev) override {
		f(elem, iev);
	}
	void OnListenedInput(Element* elem, struct InputEvent* ev) override { }
};
void SetStartup(bool enable)
{
	HKEY hKey;
	WCHAR path[MAX_PATH];
	GetModuleFileName(GetModuleHandleW(NULL), path, MAX_PATH);
	LONG lnRes = RegOpenKeyEx(HKEY_CURRENT_USER,
		TEXT("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"),
		0, KEY_WRITE,
		&hKey);
	if (ERROR_SUCCESS == lnRes)
	{
		if (enable)
		{
			lnRes = RegSetValueEx(hKey,
				TEXT("RectifyStart"),
				0,
				REG_SZ,
				(const BYTE*)path,
				(DWORD)wcslen(path) * 2);
		}
		else
		{
			lnRes = RegDeleteValue(hKey, TEXT("RectifyStart"));
		}
	}

	RegCloseKey(hKey);
}

bool GetStartup()
{
	HKEY hKey;
	LONG lnRes = RegOpenKeyEx(HKEY_CURRENT_USER,
		TEXT("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"),
		0, KEY_READ,
		&hKey);
	wchar_t buf[255] = { 0 };
	DWORD dwBufSize = sizeof(buf);
	DWORD dwType = REG_SZ;
	if (ERROR_SUCCESS == lnRes)
	{
		wchar_t buf[255] = { 0 };

		lnRes = RegQueryValueEx(hKey, TEXT("RectifyStart"), 0, NULL, reinterpret_cast<LPBYTE>(&buf), &dwBufSize);
		return lnRes == ERROR_SUCCESS ? true : false;
	}

	RegCloseKey(hKey);
	return false;
}

void ApplyThemeIfNeeded()
{
	// Apply theme after rectify11 was installed. For more details see Rectify11Installer/Core/Backend/Themes.cs
	HKEY Rectify11;
	if (RegCreateKey(HKEY_CURRENT_USER, TEXT("SOFTWARE\\Rectify11"), &Rectify11))
	{
		MessageBox(hwnd, TEXT("Failed to create rectify11 key"), TEXT("RectifyStart application"), MB_ICONERROR);
		return;
	}

	// Buffer to store string read from registry
	TCHAR szValue[1024];
	DWORD cbValueLength = sizeof(szValue);

	// Query string value
	LSTATUS hr = RegQueryValueEx(
		Rectify11,
		TEXT("ApplyThemeOnNextRun"),
		NULL,
		NULL,
		reinterpret_cast<LPBYTE>(&szValue),
		&cbValueLength);
	if (hr != ERROR_SUCCESS)
	{
		if (hr != ERROR_FILE_NOT_FOUND)
		{
			MessageBox(hwnd, TEXT("Failed to query ApplyThemeOnNextRun registry value. The rectify11 theme may not be applied correctly."), TEXT("RectifyStart application"), MB_ICONERROR);
		}
		return;
	}

	IRectifyUtil* util;
	hr = CoCreateInstance(CLSID_CRectifyUtil, NULL, CLSCTX_INPROC_SERVER, IID_IRectifyUtil,
		reinterpret_cast<void**>(&util));

	if (util == NULL || hr != S_OK)
	{
		MessageBox(hwnd, TEXT("Failed to create CRectifyUtil COM Object when trying to apply theme. Please rerun the Rectify11 installer. If that doesn't work, report an issue on github."), TEXT("RectifyStart application: Critical error"), MB_ICONERROR);
		return;
	}

	hr = util->ApplyTheme(szValue);
	if (FAILED(hr))
	{
		MessageBox(hwnd, TEXT("Failed to apply the rectify11 theme."), TEXT("RectifyStart application"), MB_ICONERROR);
		return;
	}

	util->Release();

	// we are done
	RegDeleteValue(Rectify11, TEXT("ApplyThemeOnNextRun"));
}
void HandleCloseButton(Element* elem, Event* iev)
{
	if (iev->type == TouchButton::Click)
	{
		pwnd->DestroyWindow();
	}
}
void HandleStartCheckbox(Element* elem, Event* iev)
{
	TouchCheckBox* box = (TouchCheckBox*)elem;
	if (iev->type == TouchButton::Click)
	{
		SetStartup(box->GetCheckedState() == CheckedStateFlags_CHECKED ? true : false);
	}
}

void HandleOpenCpl(Element* elem, Event* iev)
{
	TouchCheckBox* box = (TouchCheckBox*)elem;
	if (iev->type == TouchButton::Click)
	{
		ShellExecute(hwnd_element->GetHWND(), NULL, _T("control.exe"), _T("/name Rectify11.SettingsCPL"), NULL, SW_SHOW);
	}
}

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// Initialize global strings
	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);

	HRESULT hr = CoInitializeEx(NULL, 0);
	if (FAILED(hr) && hr != ERROR_ALREADY_INITIALIZED)
	{
		return -1;
	}

	// Initialize DirectUI
	InitProcessPriv(14, NULL, 0, true);
	InitThread(2);
	RegisterAllControls();

	// Create the main window
	NativeHWNDHost::Create(
		(UCString)L"Rectify11", NULL,
		LoadIconW(hInstance, MAKEINTRESOURCE(IDI_RECTIFYSTART)),
		CW_USEDEFAULT, CW_USEDEFAULT, 880, 720,
		0, WS_EX_TOOLWINDOW | WS_VISIBLE | WS_SYSMENU, 0, &pwnd);

	hwnd = pwnd->GetHWND();

	// apply theme after installation
	ApplyThemeIfNeeded();

	// Center the window on the screen
	RECT rc;
	GetWindowRect(hwnd, &rc);

	int xPos = (GetSystemMetrics(SM_CXSCREEN) - (rc.right - rc.left)) / 2;
	int yPos = (GetSystemMetrics(SM_CYSCREEN) - (rc.bottom - rc.top)) / 2;
	SetWindowPos(hwnd, 0, xPos, yPos, 0, 0, SWP_NOZORDER | SWP_NOSIZE);

	// Create DirectUI Parser
	DUIXmlParser* pParser = NULL;

	DUIXmlParser::Create(&pParser, NULL, NULL, NULL, NULL);
	pParser->SetParseErrorCallback(
		[](UCString err1, UCString err2, int unk, void* ctx) {
			MessageBox(hwnd, std::format(L"err: {}; {}; {}\n", (LPCWSTR)err1, (LPCWSTR)err2, unk).c_str(),
			L"Error while parsing DirectUI", S_OK);
		}, NULL);

	hr = pParser->SetXMLFromResource(IDR_UIFILE, hInstance, (HINSTANCE)hInstance);

	unsigned long defer_key = 0;
	HWNDElement::Create(pwnd->GetHWND(), true, 0, NULL, &defer_key, (Element**)&hwnd_element);

	// Create the root element
	Element* pWizardMain;
	hr = pParser->CreateElement((UCString)L"WizardMain", hwnd_element, NULL, NULL, (Element**)&pWizardMain);

	// Host the window with the element
	pWizardMain->SetVisible(true);
	pWizardMain->EndDefer(defer_key);
	pwnd->Host(pWizardMain);
	pwnd->ShowWindow(SW_SHOW);

	// Add listeners. TODO: use custom control
	TouchCheckBox* startChk = (TouchCheckBox*)pWizardMain->FindDescendent(StrToID((UCString)L"SXWizardCheckbox"));
	TouchButton* closeBtn = (TouchButton*)pWizardMain->FindDescendent(StrToID((UCString)L"SXWizardDefaultButton"));
	TouchButton* BtnOpenCpl = (TouchButton*)pWizardMain->FindDescendent(StrToID((UCString)L"BtnOpenCpl"));
	startChk->SetToggleOnClick(true);
	closeBtn->AddListener(new EventListener(HandleCloseButton));
	startChk->AddListener(new EventListener(HandleStartCheckbox));
	BtnOpenCpl->AddListener(new EventListener(HandleOpenCpl));

	// Setup startchk
	startChk->SetCheckedState(GetStartup() ? CheckedStateFlags_CHECKED : CheckedStateFlags_NONE);

	// Start message loop
	StartMessagePump();

	// Exit
	UnInitProcessPriv(NULL);
	CoUninitialize();
	return 0;
}