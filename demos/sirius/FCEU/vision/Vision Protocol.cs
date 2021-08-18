/// <summary>
/// command type ; 4byte
/// ACK ==> ..._OK
/// </summary>
public enum MessageProtocol
{
    UNKNOWN = 0,

    #region 모델 변경
    MODEL_LOAD = 1,//제어 -> 검사, 레이저 (model name 파일에 모델들의 인덱스(1~190)) 
    MODEL_LOAD_OK = 191,
    MODEL_LOAD_NG = 192,
    #endregion

    #region 비전 검사기
    SCAN_END = 220,//제어->검사
    SCAN_END_OK,
    SCAN_END_NG,

    SCAN_IMAGE = 230,//검사->제어 (스캔 완료시 이미지JPG 이미지만 저장 (scan_image_tmp.jpg))     
    SCAN_IMAGE_OK,
    SCAN_IMAGE_NG,

    INSP_ERR = 300,//검사->제어 (검사 중 에러 발생시)
    INSP_ERR_OK,
    INSP_RE_NEED = 310,//검사->제어 (재검사 필요할때)
    INSP_RE_NEED_OK,
    INSP_RECICP_LOAD = 320,//MODEL_LOAD 로 대체한다
    INSP_RECICP_LOAD_OK,
    INSP_RECICP_LOAD_NG,
    INSP_START = 330,//검사->제어 (상태표시)
    INSP_START_OK,
    INSP_START_NG,

    INSP_DONE = 340,//검사->제어 (검사완료 후 제어쪽에 주는 메시지
                    //            hatching할 영역이 표시된 이미지 저장(insp_image_tmp.jpg)
    INSP_DONE_OK,
    INSP_DONE_NG,

    #endregion

    #region 레이저 가공기
    LASER_STATUS_READY = 400,//제어->레이저 (레이저 준비 상태 확인용)
    LASER_STATUS_READY_OK,//레이저->제어 (준비 완료)
    LASER_STATUS_READY_NG,//레이저->제어 (준비 미완료)
    LASER_STATUS_BUSY = 410,//레이저->제어 (레이저 출사중 상태 확인용)
    LASER_STATUS_BUSY_OK,//레이저->제어 (가공중)
    LASER_STATUS_BUSY_NG,//레이저->제어 (가공중 아님)
    LASER_STATUS_ERR = 420,//레이저->제어 (레이저 에러 상태 확인용)
    LASER_STATUS_ERR_OK,//레이저->제어 (에러 없음)
    LASER_STATUS_ERR_NG,//레이저->제어 (에러 있음)
    LASER_STATUS_RESET = 430, //제어-> 레이저 (에러 상태를 리셋 시도)
    LASER_STATUS_RESET_OK, //레이저 -> 제어 (수신 성공)

    LASER_SCANNER_SYSTEM_TEACH = 450, //제어->레이저  ;스캐너 중심 위치에 특정 패턴(원형)을 가공
    LASER_SCANNER_SYSTEM_TEACH_OK, //레이저->제어
    LASER_SCANNER_SYSTEM_TEACH_NG, //레이저->제어
    LASER_SCANNER_SYSTEM_TEACH_FINISH, // 레이저 -> 제어
    LASER_SCANNER_SYSTEM_TEACH_FINISH_OK, // 제어 -> 레이저

    LASER_SCANNER_COMPENSATE = 460, //제어->레이저; 스캐너 필드 보정을 위한 가로세로 격자 마킹    
    LASER_SCANNER_COMPENSATE_OK, //레이저->제어
    LASER_SCANNER_COMPENSATE_NG, //레이저->제어
    LASER_SCANNER_COMPENSATE_FINISH, // 레이저 -> 제어
    LASER_SCANNER_COMPENSATE_FINISH_OK, // 제어 -> 레이저

    LASER_Z_CORRECT_INIT = 470, //제어->레이저; 초기상태로 변환
    LASER_Z_CORRECT_INIT_OK,
    LASER_Z_CORRECT_INIT_NG,
    LASER_Z_CORRECT,            //제어->레이저; 라인만들기
    LASER_Z_CORRECT_OK,            //레이저->제어
    LASER_Z_CORRECT_NG,            //레이저->제어
    LASER_Z_CORRECT_FINISH,        //레이저 -> 제어
    LASER_Z_CORRECT_FINISH_OK, //제어 -> 레이저

    LASER_SCANNER_COMPENSATE_READ = 490, //제어->레이저 ; 스캐너 필드 보정용 비전 측정 데이타 파일 읽기
    LASER_SCANNER_COMPENSATE_READ_OK, //레이저 ->제어
    LASER_SCANNER_COMPENSATE_READ_NG, //레이저 ->제어

    /// <summary>
    /// 레이저 레시피의 구성
    /// 레이어 1 (도면 우측) : LASER_SCANNER_REF_01_IMAGE 의해 가공됨
    /// 레이어 2 (도면 좌측) : LASER_SCANNER_REF_02_IMAGE 의해 가공됨
    /// 레이어 3 (비전으로부터의 불량 정보 우측) : LASER_READ_INSPECT_01 의해 불러짐
    ///  -> 실제 가공 데이타는 LASER_READ_HATCHING_01 으로 불러진 데이타 사용
    /// 레이어 4 (비전으로부터의 불량 정보 좌측) : LASER_READ_INSPECT_02 의해 불러짐
    ///  -> 실제 가공 데이타는 LASER_READ_HATCHING_02 으로 불러진 데이타 사용
    /// </summary>
    LASER_SCANNER_REF_01_IMAGE = 500, //제어->레이저  ;모델 정보중 오른쪽 레이어를 ps plate에 해칭
    LASER_SCANNER_REF_01_IMAGE_OK, //레이저->제어 (도면 기준 데이타로 가공)
    LASER_SCANNER_REF_01_IMAGE_NG, //레이저->제어
    LASER_SCANNER_REF_01_IMAGE_FINISH, // 레이저 -> 제어
    LASER_SCANNER_REF_01_IMAGE_FINISH_OK, // 제어 -> 레이저

    LASER_SCANNER_REF_02_IMAGE = 510, //제어->레이저  ;모델 정보중 왼쪽 레이어를 ps plate에 해칭
    LASER_SCANNER_REF_02_IMAGE_OK, //레이저->제어 (도면 기준 데이타로 가공)
    LASER_SCANNER_REF_02_IMAGE_NG, //레이저->제어
    LASER_SCANNER_REF_02_IMAGE_FINISH, // 레이저 -> 제어
    LASER_SCANNER_REF_02_IMAGE_FINISH_OK, // 제어 -> 레이저

    //미사용?
    LASER_READ_INSPECT_01 = 600, //제어->레이저 ; 가공 정보 데이타 파일 읽기 (도면 기준이며 화면에 가공 정보를 보여주기 위함)
    LASER_READ_INSPECT_01_OK, //레이저 ->제어
    LASER_READ_INSPECT_01_NG, //레이저 ->제어
    LASER_READ_INSPECT_01_FINISH, // 레이저 -> 제어
    LASER_READ_INSPECT_01_FINISH_OK, // 제어 -> 레이저

    //미사용?
    LASER_READ_INSPECT_02 = 610, //제어->레이저 ; 가공 정보 데이타 파일 읽기  (도면 기준이며 화면에 가공 정보를 보여주기 위함)
    LASER_READ_INSPECT_02_OK, //레이저 ->제어
    LASER_READ_INSPECT_02_NG, //레이저 ->제어
    LASER_READ_INSPECT_02_FINISH, // 레이저 -> 제어
    LASER_READ_INSPECT_02_FINISH_OK, // 제어 -> 레이저

    //우
    LASER_READ_HATCHING_01 = 650, //제어->레이저 ; 가공 정보 데이타 파일 읽기 (자재 기준이며 실제 가공 위치를 조정하기 위함)
    LASER_READ_HATCHING_01_OK, //레이저 ->제어
    LASER_READ_HATCHING_01_NG, //레이저 ->제어
    LASER_READ_HATCHING_01_FINISH, // 레이저 -> 제어
    LASER_READ_HATCHING_01_FINISH_OK, // 제어 -> 레이저

    //좌
    LASER_READ_HATCHING_02 = 660, //제어->레이저 ; 가공 정보 데이타 파일 읽기 (자재 기준이며 실제 가공 위치를 조정하기 위함)
    LASER_READ_HATCHING_02_OK, //레이저 ->제어
    LASER_READ_HATCHING_02_NG, //레이저 ->제어
    LASER_READ_HATCHING_02_FINISH, // 레이저 -> 제어
    LASER_READ_HATCHING_02_FINISH_OK, // 제어 -> 레이저

    //우
    MOVE_HATCHING_01_POITION_DONE = 700, //제어->레이저 (오른쪽 가공 위치로 이동 완료)
    MOVE_HATCHING_01_POITION_DONE_OK, //레이저->제어
    DO_HATCHING_01_START, //제어 -> 레이저 (자재 기준 데이타로 가공)
    DO_HATCHING_01_START_OK, //레이저-> 제어
    DO_HATCHING_01_START_NG, //레이저-> 제어
    DO_HATCHING_01_FINISH, //레이저->제어
    DO_HATCHING_01_FINISH_OK, //제어 -> 레이저

    //좌
    MOVE_HATCHING_02_POITION_DONE = 800, //제어->레이저 (왼쪽 가공 위치로 이동 완료)
    MOVE_HATCHING_02_POITION_DONE_OK, //레이저->제어
    DO_HATCHING_02_START, //제어 -> 레이저 (자재 기준 데이타로 가공)
    DO_HATCHING_02_START_OK, //레이저-> 제어
    DO_HATCHING_02_START_NG, //레이저-> 제어
    DO_HATCHING_02_FINISH, //레이저->제어
    DO_HATCHING_02_FINISH_OK, //제어 -> 레이저 
    #endregion

    IM_INSP = 990, //client는 접속되면 이메시지를 서버에 보낸다.
    IM_LASER = 991,
    WHO_ARE_YOU = 999,
}