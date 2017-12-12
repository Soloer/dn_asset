#include <iostream>
#include<Windows.h>
#include<time.h>
#include<string>

using namespace std;

#define MLog	'L'
#define MWarn	'W'
#define MError	'E'

typedef void(*CB)(unsigned char command, const char*);
typedef void(*DllCommand)(CB);
typedef void(*DllInitial)(char*, char*);
typedef int(*DllAdd)(int, int);
typedef int(*DllSub)(int*, int*);
typedef int(*DllReadQteTable)();
typedef int(*DllReadSuitTable)();
typedef void(*DllReadJson)(const char*);
typedef void(*DllPatch)(const char*, const char*, const char*);
typedef void(*DllVector)();
DllCommand cb;
DllInitial init;
DllAdd add;
DllSub sub;
DllReadQteTable qte;
DllReadSuitTable suit;
DllReadJson json;
DllPatch patch;
DllVector vect;

void DebugInfo()
{
	cout << "********* op *********" << endl;
	cout << "** a stands for Add **" << endl;
	cout << "** s stands for Sub **" << endl;
	cout << "** t stands for Read *" << endl;
	cout << "** j stands for Json *" << endl;
	cout << "** q stands for Quit *" << endl;
	cout << "** input your command:";
}

bool CheckIn()
{
	if (cin.fail())
	{
		cin.clear(); 
		cin.sync();  
		cout << "input format error" << endl << endl;
		return false;
	}
	return true;
}

void EAdd()
{
	int a, b, c;
	cout << "input a:";
	cin >> a;
	if (!CheckIn()) return;
	cout << "input b:";
	cin >> b;
	if (!CheckIn()) return;
	c = add(a, b);
	cout << "add result:" << c << endl << endl;
}

void ESub()
{
	int a, b, c;
	cout << "input a:";
	cin >> a;
	if (!CheckIn()) return;
	cout << "input b:";
	cin >> b;
	if (!CheckIn()) return;
	c = sub(&a, &b);
	cout << "add result:" << c << endl << endl;
}

void ERead()
{
	int len1 = qte();
	int len2 = suit();
	cout << "qtestatus table line cnt:" << len1 << endl;
	cout << "equipsuit table line cnt:" << len2 << endl;
	cout << endl << endl;
}

void OnCallback(unsigned char type, const char* cont)
{
	if (type == MLog || type == MWarn || type == MError)
		cout << "> " << cont << endl;
	else
		cout << "no parse symbol" << endl;
}

void main()
{
	cout << "**********  main **********" << endl;
	LPWSTR dll = TEXT("GameCore.dll");
	HINSTANCE hInst = LoadLibrary(dll);
	if (hInst == NULL)
	{
		cout << "hInst is null" << endl;
		return;
	}
	cout << "load library succ" << endl;
	cb = (DllCommand)GetProcAddress(hInst, "iInitCallbackCommand");
	init = (DllInitial)GetProcAddress(hInst, "iInitial");
	add = (DllAdd)GetProcAddress(hInst, "iAdd");
	sub = (DllSub)GetProcAddress(hInst, "iSub");
	qte = (DllReadQteTable)GetProcAddress(hInst, "iGetQteStatusListLength");
	suit = (DllReadSuitTable)GetProcAddress(hInst, "iGetEquipSuitLength");
	json = (DllReadJson)GetProcAddress(hInst, "iJson");
	patch = (DllPatch)GetProcAddress(hInst, "iPatch");
	vect = (DllVector)GetProcAddress(hInst, "iVector");
	cb(OnCallback);
	init("", "");

	while (true)
	{
		DebugInfo();
		char input;
		cin >> input;
		if (input >= 'A'&&input <= 'Z')
			input += 32;
		bool jump = false;
		switch (input)
		{
		case 'a':
			EAdd();
			break;
		case 's':
			ESub();
			break;
		case 't':
			ERead();
			break;
		case 'q':
			jump = true;
			break;
		case 'j':
			json("json.txt");
			cout << endl << endl;
			break;
		case 'p':
			patch("D:/projects/dn_asset/Assets/StreamingAssets/Patch/old.txt",
				"D:/projects/dn_asset/Assets/StreamingAssets/Patch/diff.patch",
				"D:/projects/dn_asset/Assets/StreamingAssets/Patch/nex.txt");
			cout << "patch finish!" << endl << endl;
			break;
		case 'v':
			vect();
			break;
		default:
			cout << "invalid command" << endl << endl;
		}
		if (jump) break;
	}

	FreeLibrary(hInst);
	system("pause");
}