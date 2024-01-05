// RectifyStart.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "RectifyStart.h"
#include "..\RectifyControlPanel2\dui70\DirectUI\DirectUI.h"
#pragma comment(lib,"dui70.lib")
using namespace DirectUI;

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// Initialize global strings
	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_RECTIFYSTART, szWindowClass, MAX_LOADSTRING);

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
	NativeHWNDHost* pwnd;
	NativeHWNDHost::Create(
		(UCString)L"Rectify11", NULL,
		LoadIconW(hInstance, MAKEINTRESOURCE(IDI_RECTIFYSTART)),
		600, 400, 800, 600,
		WS_EX_WINDOWEDGE, WS_OVERLAPPEDWINDOW | WS_VISIBLE, 0, &pwnd);

	// Create DirectUI Parser
	DUIXmlParser* pParser;

	DUIXmlParser::Create(&pParser, NULL, NULL, NULL, NULL);

	pParser->SetParseErrorCallback(
		[](UCString err1, UCString err2, int unk, void* ctx) {
			MessageBox(NULL, std::format(L"err: {}; {}; {}\n", (LPCWSTR)err1, (LPCWSTR)err2, unk).c_str(),
			L"Error while parsing DirectUI", S_OK);
	DebugBreak();
		}, NULL);

	hr = pParser->SetXMLFromResource(IDR_UIFILE, hInstance, (HINSTANCE)hInstance);

	unsigned long defer_key;
	HWNDElement* hwnd_element;

	HWNDElement::Create(pwnd->GetHWND(), true, 0, NULL, &defer_key, (Element**)&hwnd_element);

	Element* pWizardMain;
	hr = pParser->CreateElement((UCString)L"WizardMain", hwnd_element, NULL, NULL, (Element**)&pWizardMain);



	pWizardMain->SetVisible(true);
	pWizardMain->EndDefer(defer_key);
	pwnd->Host(pWizardMain);

	pwnd->ShowWindow(SW_SHOW);

	// Start message loop
	StartMessagePump();

	// Exit
	UnInitProcessPriv(NULL);
	CoUninitialize();
	return 0;
}