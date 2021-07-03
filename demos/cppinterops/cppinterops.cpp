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
 *
 * C++ 콘솔 프로그램에서 dll 의 COM 인터페이스에 접근하여 사용하는 예제
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
 *
 */

#include <windows.h>
#include <tchar.h>

 //tlb 파일의 경로를 변경해 사용
#import "..\..\packages\sirius.rtc\spirallab.core.tlb" raw_interfaces_only
#import "..\..\packages\sirius.rtc\spirallab.sirius.rtc.tlb" raw_interfaces_only

using namespace spirallab_core;
using namespace spirallab_sirius_rtc;


bool static DrawCircle(ILaserPtr laser, IRtcPtr rtc, float radius)
{
    HRESULT hr = S_OK;
    VARIANT_BOOL vRet = false;
    hr = rtc->ListBegin(laser, ListType::ListType_Auto, &vRet);
    hr = rtc->ListJump_2(radius, 0, 1.0f, &vRet);
    hr = rtc->ListArc_2(0,0, 360, &vRet);
    hr = rtc->ListEnd(&vRet);
    hr = rtc->ListExecute(VARIANT_BOOL(true), &vRet);
    return vRet == true;
}

bool static DrawLine(ILaserPtr laser, IRtcPtr rtc, float x1, float y1, float x2, float y2)
{
    HRESULT hr = S_OK;
    VARIANT_BOOL vRet = false;
    hr = rtc->ListBegin(laser, ListType::ListType_Auto, &vRet);
    hr = rtc->ListJump_2(x1, y1, 1.0f, &vRet);
    hr = rtc->ListMark_2(x2, y2, 1.0f, &vRet);
    hr = rtc->ListEnd(&vRet);
    hr = rtc->ListExecute(VARIANT_BOOL(true), &vRet);
    return vRet == true;
}

int _tmain(int argc, _TCHAR* argv[])
{
    //COM 초기화
    HRESULT hr = CoInitialize(NULL);

    VARIANT_BOOL vRet = false;
    //코어 객체 생성
    ICorePtr pCore(__uuidof( Core));
    //초기화
    hr = pCore->InitializeEngine(&vRet);
    long lResult = 0;
    
    //Rtc5 객체 생성
    IRtcPtr pRtc(__uuidof(Rtc5));
    float kFactor =(float) pow(2,20) / 60.0f;
    //초기화
    hr = pRtc->Initialize(kFactor, LaserMode::LaserMode_Yag1, _bstr_t(_T("correction\\Cor_1to1.ct5")), &vRet);

    //레이저(가상) 객체 생성
    ILaserPtr pLaser(__uuidof(LaserVirtual));
    VARIANT_BOOL busy = false;
    //초기화
    hr = pLaser->Initialize(&vRet);

    //선 그리기
    DrawLine(pLaser, pRtc, -10, -10, 10, 10);

    //원 그리기
    DrawCircle(pLaser, pRtc, 10);

    printf("\r\nPress any key to terminate ... \r\n");
    getchar();

    //COM 해제
    CoUninitialize();
    return 0;
}

