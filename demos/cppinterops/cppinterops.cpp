/*
 *                                                            ,--,      ,--,
 *             ,-.----.                                     ,---.'|   ,---.'|
 *   .--.--.   \    /  \     ,---,,-.----.      ,---,       |   | :   |   | :      ,---,           ,---,.
 *  /  /    '. |   :    \ ,`--.' |\    /  \    '  .' \      :   : |   :   : |     '  .' \        ,'  .'  \
 * |  :  /`. / |   |  .\ :|   :  :;   :    \  /  ;    '.    |   ' :   |   ' :    /  ;    '.    ,---.' .' |
 * ;  |  |--`  .   :  |: |:   |  '|   | .\ : :  :       \   ;   ; '   ;   ; '   :  :       \   |   |  |: |
 * |  :  ;_    |   |   \ :|   :  |.   : |: | :  |   /\   \  '   | |__ '   | |__ :  |   /\   \  :   :  :  /
 *  \  \    `. |   : .   /'   '  ;|   |  \ : |  :  ' ;.   : |   | :.'||   | :.'||  :  ' ;.   : :   |    ;
 *   `----.   \;   | |`-' |   |  ||   : .  / |  |  ;/  \   \'   :    ;'   :    ;|  |  ;/  \   \|   :     \
 *   __ \  \  ||   | ;    '   :  ;;   | |  \ '  :  | \  \ ,'|   |  ./ |   |  ./ '  :  | \  \ ,'|   |   . |
 *  /  /`--'  /:   ' |    |   |  '|   | ;\  \|  |  '  '--'  ;   : ;   ;   : ;   |  |  '  '--'  '   :  '; |
 * '--'.     / :   : :    '   :  |:   ' | \.'|  :  :        |   ,/    |   ,/    |  :  :        |   |  | ;
 *   `--'---'  |   | :    ;   |.' :   : :-'  |  | ,'        '---'     '---'     |  | ,'        |   :   /
 *             `---'.|    '---'   |   |.'    `--''                              `--''          |   | ,'
 *               `---`            `---'                                                        `----'
 *
 *
 * c++ 환경에서 .NET 어셈블리의 COM 노출 인터페이스를 접근하여 사용하는 방법
 * RTC5 제어기 및 레이저 소스를 생성/초기화 하고 도형 마킹을 실시
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
 *
 */

#include <windows.h>
#include <tchar.h>

 //tlb 파일의 경로를 변경해 사용
#import "..\..\bin\spirallab.core.tlb" raw_interfaces_only
#import "..\..\bin\spirallab.sirius.rtc.tlb" raw_interfaces_only

using namespace spirallab_core;
using namespace spirallab_sirius_rtc;

#include <cassert>

bool static COMInit()
{
    //COM 초기화
    HRESULT hr = CoInitialize(NULL);

    //코어 객체 생성
    ICorePtr pCore(__uuidof(Core));
    
    //코어 엔진 초기화
    VARIANT_BOOL vRet = VARIANT_TRUE;
    hr = pCore->InitializeEngine(&vRet);
    assert(hr == S_OK);
    assert(vRet == VARIANT_TRUE);
    return true;
}

bool static COMCleanUp()
{
    //COM 해제
    CoUninitialize();
    return true;
}

bool static DrawCircle(ILaserPtr laser, IRtcPtr rtc, float radius)
{
    HRESULT hr = S_OK;
    VARIANT_BOOL vRet = 0;
    hr = rtc->ListBegin(laser, ListType::ListType_Auto, &vRet);
    hr = rtc->ListFrequency(50 * 1000, 2, &vRet);
    hr = rtc->ListSpeed(500, 500, &vRet);
    hr = rtc->ListDelay(10, 50, 350, 200, 0, &vRet);
    hr = rtc->ListJump_2(radius, 0, 1.0f, &vRet);
    hr = rtc->ListArc_2(0,0, 360, 1.0, &vRet);
    hr = rtc->ListEnd(&vRet);
    hr = rtc->ListExecute(VARIANT_TRUE, &vRet);
    assert(hr == S_OK);
    return vRet == VARIANT_TRUE;
}

bool static DrawLine(ILaserPtr laser, IRtcPtr rtc, float x1, float y1, float x2, float y2)
{
    HRESULT hr = S_OK;
    VARIANT_BOOL vRet = 0;
    hr = rtc->ListBegin(laser, ListType::ListType_Auto, &vRet);    
    hr = rtc->ListFrequency(50*1000, 2, &vRet);
    hr = rtc->ListSpeed(100, 200, &vRet);
    hr = rtc->ListDelay(10, 50, 350, 200, 0, &vRet);
    hr = rtc->ListJump_2(x1, y1, 1.0f, &vRet);
    hr = rtc->ListMark_2(x2, y2, 1.0f, &vRet);
    hr = rtc->ListEnd(&vRet);
    hr = rtc->ListExecute(VARIANT_TRUE, &vRet);
    assert(hr == S_OK);
    return vRet == VARIANT_TRUE;
}


int _tmain(int argc, _TCHAR* argv[])
{
    COMInit();

    HRESULT hr;
    VARIANT_BOOL vRet = VARIANT_TRUE;

    // Rtc5 객체 생성
    IRtcPtr pRtc(__uuidof(Rtc5));

    // 초기화
    float kFactor = (float) pow(2,20) / 60.0f;
    TCHAR szCurrentPath[MAX_PATH] = { 0, };
    ::GetCurrentDirectory(MAX_PATH, szCurrentPath);
    _tcscat_s(szCurrentPath, _T("\\correction\\Cor_1to1.ct5"));
    BSTR bstr = ::SysAllocString(szCurrentPath);
    hr = pRtc->Initialize(kFactor, LaserMode::LaserMode_Yag1, bstr, &vRet);
    ::SysFreeString(bstr);
    assert(hr == S_OK);
    assert(vRet == VARIANT_TRUE);

    // 레이저(가상) 객체 생성
    ILaserPtr pLaser(__uuidof(LaserVirtual));
    //초기화
    pLaser->putref_Rtc(pRtc);
    hr = pLaser->Initialize(&vRet);
    assert(hr == S_OK);
    assert(vRet == VARIANT_TRUE);

    // 선 그리기
    printf("\r\nPress any key to draw line ... \r\n");
    getchar();
    DrawLine(pLaser, pRtc, -10, 10, 10, -10);

    // 원 그리기
    printf("\r\nPress any key to draw circle ... \r\n");
    getchar();
    DrawCircle(pLaser, pRtc, 10);

    printf("\r\nPress any key to terminate program ... \r\n");
    
    getchar();
    pLaser->Release();
    pLaser = NULL;
    pRtc->Release();
    pRtc = NULL;
   
    COMCleanUp();
    return 0;
}

