using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace RPD
{
    public class STRCTURE
    {
        public static SYS SYS = new SYS();
        public static EF EF = new EF();
        public static LL LL = new LL();
        public static MFC MFC = new MFC();
        public static PCC1 PCC1 = new PCC1();
        public static PCC2 PCC2 = new PCC2();
        public static TR TR = new TR();
        public static UL UL = new UL();
        public static DF DF = new DF();

    }

    #region System
    public class SYS
    {
        public SYS_Btn Btn = new SYS_Btn();
        public SYS_Map Map = new SYS_Map();
        public SYS_DI DI = new SYS_DI();
        public SYS_DO DO = new SYS_DO();
        public SYS_Data Data = new SYS_Data();
        public SYS_Heater Heater = new SYS_Heater();
    }

    public class SYS_Data
    {
        public int[] INTERLOCK_TABLE;
        public int[] IO_TABLE;
        public string UserID;
        public int Level;
        public int PLCAliveIndex;
        public int GUIAliveIndex;
        public bool PLCAliveAlarmRequest = false;
        public int SystemYear;
        public int SystemMonth;
        public int SystemDate;
        public int SystemHour;
        public int SystemMin;
        public int SystemSec;
        public int SystemWeek;


        public int LeakTestStartTorr;
        public int LeakTestEndTorr;
        public int LeakTestGUIFinish;

        public int[] Alarm = new int[32];
        public int[] EventLog = new int[128];
        public int[] Warning = new int[32];
        public DataTable dt_AlarmDisplay = new DataTable();
        public DataTable dt_WarningDisplay = new DataTable();
        public int[,] AlarmTemp = new int[32, 16];
        public int[,] WarningTemp = new int[32, 16];

        public double CVSpeed;
        public double CVProcessSpeed;
        public double DVOpenATM;

        public double MainWaterHigherLimit;
        public double MainWaterLowerLimit;

        public int[] EC_TABLE = new int[112]; //初始化EC

        public bool AlarmRequest;
        public bool WarningRequest;

        public bool RefreshRecipeRequest;

        public string RecipeNumber;
    }

    public class SYS_Btn
    {
        public Btns AutoModeSwitch = new Btns();
        public Btns ManualModeSwitch = new Btns();
        public Btns DummyModeSwitch = new Btns();
        public Btns StartSwitch = new Btns();
        public Btns StopSwitch = new Btns();
        public Btns ContinueSwitch = new Btns();
        public Btns PauseSwitch = new Btns();
        public Btns InitialSwitch = new Btns();
        public Btns AbortSwitch = new Btns();

        public Btns AlarmResetSwitch = new Btns();
        public Btns BuzzerOffSwitch = new Btns();
        public Btns TactTimeReaetSwitch = new Btns();

        public Btns TrayCountResetSwitch = new Btns();
        public Btns ForcedExcludeSwitch = new Btns();
        public Btns LeakTestSwitchTR = new Btns();

        public Btns StartCodeInputKeySwitch = new Btns();

        public Btns TrayDeleteSwitch = new Btns();
        public Btns TrayDataModifySwitch = new Btns();
        public Btns MachineDataSaveSwitch = new Btns();
        public Btns OutputForceOnOffSwitch = new Btns();
        public Btns CurrentRecipeChangeSwitch = new Btns();
        public Btns IOForceOnOffModeSwitch = new Btns();

        public Btns ALLPumpDownSwitch = new Btns();
        public Btns ALLVentSwitch = new Btns();
        public Btns ALLTPStartSwitch = new Btns();

        //add by william
        public Btns ALLLineInit = new Btns();

        


    }

    public class SYS_Map
    {

        public int LeakTestStartTR;
        public int LeakTestFinishTR;
        public int LeakTestTimeSetting;
        public int LeakTestWaitTimeSetting;


        public int StartCodeError;
        public int StartCodeFailure;
        public int EventLogRequest;
        public int EventLogFinish;

        public int EQStatus;

        public double LLTP_EV1_GaugeValueP;
        public double DP_EV1_GaugeValueP;
        public double UL_TP_EV2_GaugeValueP;
        public double TR_TP_EV3_GaugeValueP;

        public int CurrentTactTime;
        public int TactTime1;
        public int TactTime2;
        public int TactTime3;
        public int TactTime4;
        public int TactTime5;
        public int TrayCount;

        public double MainWaterTemp;
      

    }

    public class SYS_DI
    {
        public Btns EntryFeederStartReady = new Btns();               //X1000 
        public Btns EntryFeederTrayExistBWD = new Btns();            //X1001
        public Btns EntryFeederTrayExistFWD = new Btns();            //X1002
        public Btns DV1Open = new Btns();                            //X1003
        public Btns DV1Close = new Btns();                           //X1004
        public Btns LLHVBatATM = new Btns();                         //X1005
        public Btns LLHVBTrayExistBWD = new Btns();                  //X1006
        public Btns LLHVBTrayExistFWD = new Btns();                  //X1007
        public Btns LLHVBVV1VentOpen = new Btns();                   //X1008
        public Btns LLHVBVV1VentClose = new Btns();                  //X1009
        public Btns LLHVBRV1FastPumpValveOpen = new Btns();          //X100A
        public Btns LLHVBRV1FastPumpValveClose = new Btns();         //X100B
        public Btns LLHVBSRV1SlowPumpValveOpen = new Btns();         //X100C
        public Btns LLHVBSRV1SlowPumpValveClose = new Btns();        //X100D
        public Btns LLHVBDryBoostPumpRunning = new Btns();           //X100E
        public Btns SPARE0 = new Btns();                             //X100F
        //******************************************************************
        public Btns SPARE7 = new Btns();                             //X1010 
        public Btns LLHVBDryPumpCoolingWaterOK = new Btns();         //X1011
        public Btns DV2Open = new Btns();                            //X1012
        public Btns DV2Close = new Btns();                           //X1013
        public Btns SPARE8 = new Btns();                             //X1014
        public Btns SPARE9 = new Btns();                             //X1015
        public Btns SPARE10 = new Btns();                            //X1016
        public Btns SPARE11 = new Btns();                            //X1017
        public Btns EmergencyOK = new Btns();                        //X1018
        public Btns EntryFeederServoMotorPowerOn = new Btns();       //X1019
        public Btns LLHVBTransferMotorPowerOn = new Btns();          //X101A
        public Btns TR1TransferMotorPowerOn = new Btns();            //X101B
        public Btns TR2TransferMotorPowerOn = new Btns();            //X101C
        public Btns TR3TransferMotorPowerOn = new Btns();            //X101D
        public Btns ULHVBTransferMotorPowerOn = new Btns();          //X101E
        public Btns DeliveryFeederServoMotorPowerOn = new Btns();    //X101F
        //******************************************************************
        public Btns TRatATM = new Btns();                            //X1020
        public Btns TR1TrayExistBWD = new Btns();                    //X1021
        public Btns TR1TraySpeedUpMID = new Btns();                  //X1022
        public Btns TR1TrayExistFWD = new Btns();                    //X1023
        public Btns TR3TrayExistBWD = new Btns();                    //X1024
        public Btns TR3TraySpeedUpMID = new Btns();                  //X1025
        public Btns TR3TrayExistFWD = new Btns();                    //X1026
        public Btns LLHVBGV1Open = new Btns();                       //X1027
        public Btns LLHVBGV1Close = new Btns();                      //X1028
        public Btns LLHVBTurboPumpCoolingWaterOK = new Btns();       //X1029
        public Btns LLHVBTurboPumpRunning = new Btns();              //X102A
        public Btns LLHVBTurboPumpAlarm = new Btns();                //X102B
        public Btns LLHVBEV1Open = new Btns();                       //X102C
        public Btns LLHVBEV1Close = new Btns();                      //X102D
        public Btns RV3Open = new Btns();                            //X102E
        public Btns RV3Close = new Btns();                           //X102F
        //******************************************************************
        public Btns SPARE19 = new Btns();                                   //X1030
        public Btns BackingDryPumpCoolingWaterOK = new Btns();              //X1031
        public Btns BackingDryBoostPumpRunning = new Btns();                //X1032
        public Btns SPARE20 = new Btns();                                   //X1033
        public Btns SPARE21 = new Btns();                                   //X1034
        public Btns RPDDoorInwallCoolingWaterFlowOK = new Btns();           //X1035
        public Btns RPDG1ElectrodeCoolingWaterFlowOK = new Btns();          //X1036
        public Btns RPDG2ElectrodeCoolingWaterFlowOK = new Btns();          //X1037
        public Btns RPDSteeringCoilCoolingWaterFlowOK = new Btns();         //X1038
        public Btns RPDMainHearthCoolingWaterFlowOK = new Btns();           //X1039
        public Btns RPDBeamGuideCoolingWaterFlowOK = new Btns();            //X103A
        public Btns RPDChamberInwallCoolingWaterFlowOK = new Btns();        //X103B
        public Btns PCCGV3Open = new Btns();                                //X103C
        public Btns PCCGV3Close = new Btns();                               //X103D
        public Btns PCCTurboPumpCoolingWaterOK = new Btns();                //X103E
        public Btns PCCTurboPumpRunning = new Btns();                       //X103F
        //******************************************************************
        public Btns PCCTurboPumpAlarm = new Btns();                  //X1040 
        public Btns SPARE1 = new Btns();                             //X1041
        public Btns PCCEV2Open = new Btns();                         //X1042
        public Btns PCCEV2Close = new Btns();                        //X1043
        public Btns SPARE2 = new Btns();                             //X1044
        public Btns SPARE3 = new Btns();                             //X1045
        public Btns ULHVBTurboPumpRunning = new Btns();              //X1046
        public Btns ULHVBTurboPumpAlarm = new Btns();                //X1047
        public Btns SPARE4 = new Btns();                             //X1048
        public Btns SPARE5 = new Btns();                             //X1049
        public Btns SPARE6 = new Btns();                             //X104A
        public Btns ULHVBTurboPumpCoolingWaterOK = new Btns();       //X104B
        public Btns RPDSourceSupplyRV4Open = new Btns();             //X104C
        public Btns RPDSourceSupplyRV4Close = new Btns();            //X104D
        public Btns RPDSourceSupplyatATM = new Btns();               //X104E
        public Btns RPDSourceSupplyGV6Open = new Btns();             //X104F

        //******************************************************************
        public Btns RPDSourceSupplyGV6Close = new Btns();            //X1050
        public Btns RPDSourceSupplyTurboPumpStart = new Btns();      //X1051
        public Btns RPDSourceSupplyTurboPumpAlarm = new Btns();      //X1052
        public Btns SPARE12 = new Btns();                            //X1053
        public Btns RPDSourceSupplyEV4Open = new Btns();             //X1054
        public Btns RPDSourceSupplyEV4Close = new Btns();            //X1055
        public Btns TR2CoolerCoolingWaterOK = new Btns();            //X1056
        public Btns RPDSourceSupplyOilPumpRunning = new Btns();      //X1057
        public Btns SPARE13 = new Btns();                            //X1058
        public Btns SPARE14 = new Btns();                            //X1059
        public Btns TR2TrayExistBWD = new Btns();                    //X105A
        public Btns TR2CHBCoverCoolingWaterOK = new Btns();          //X105B
        public Btns TR3CHBCoverCoolingWaterOK = new Btns();          //X105C
        public Btns TRVV3VentOpen = new Btns();                      //X105D
        public Btns TRVV3VentClose = new Btns();                     //X105E
        public Btns TR2TrayExistFWD = new Btns();                    //X105F

        //******************************************************************
        public Btns RGAIsolationValveOpen = new Btns();              //X1060
        public Btns RGAIsolationValveClose = new Btns();             //X1061
        public Btns RPDSourceSupplyUpperGateOpenTS1 = new Btns();    //X1062
        public Btns RPDSourceSupplyUpperGateCloseTS1 = new Btns();   //X1063
        public Btns RPDSourceSupplyLowerGateOpenGV4 = new Btns();    //X1064
        public Btns RPDSourceSupplyLowerGateCloseGV4 = new Btns();   //X1065
        public Btns RPDSourceSupplyRotaryFWDPB1 = new Btns();        //X1066
        public Btns RPDSourceSupplyRotaryBWDPB1 = new Btns();        //X1067
        public Btns RPDSourceSupplyVentOpenVV4 = new Btns();         //X1068
        public Btns RPDSourceSupplyVentCloseVV4 = new Btns();        //X1069
        public Btns SPARE15 = new Btns();                            //X106A
        public Btns SPARE16 = new Btns();                            //X106B
        public Btns SPARE17 = new Btns();                            //X106C
        public Btns SPARE18 = new Btns();                            //X106D
        public Btns MainWaterPressureOK = new Btns();                //X106E
        public Btns MainCDAPressureOK = new Btns();                  //X106F


        //******************************************************************
        public Btns SPARE22 = new Btns();                            //X1070
        public Btns SPARE23 = new Btns();                            //X1071
        public Btns SPARE24 = new Btns();                            //X1072
        public Btns SPARE25 = new Btns();                            //X1073
        public Btns SPARE26 = new Btns();                            //X1074
        public Btns SPARE27 = new Btns();                            //X1075
        public Btns SPARE28 = new Btns();                            //X1076
        public Btns SPARE29 = new Btns();                            //X1077
        public Btns SPARE30 = new Btns();                            //X1078
        public Btns SPARE31 = new Btns();                            //X1079
        public Btns SPARE32 = new Btns();                            //X107A
        public Btns SPARE33 = new Btns();                            //X107B
        public Btns SPARE34 = new Btns();                            //X107C
        public Btns SPARE35 = new Btns();                            //X107D
        public Btns SPARE36 = new Btns();                            //X107E
        public Btns SPARE37 = new Btns();                            //X107F

        //******************************************************************
        public Btns DV3Open = new Btns();                            //X1080
        public Btns DV3Close = new Btns();                           //X1081
        public Btns ULHVBatATM = new Btns();                         //X1082
        public Btns DV4Open = new Btns();                            //X1083
        public Btns DV4Close = new Btns();                           //X1084
        public Btns ULHVBTrayExistFWD = new Btns();                  //X1085
        public Btns ULHVBTrayExistBWD = new Btns();                  //X1086
        public Btns DeliverFeederTrayExistBWD = new Btns();          //X1087
        public Btns DeliverFeederTrayExistFWD = new Btns();          //X1088
        public Btns DeliverFeederTakeoutReady = new Btns();          //X1089
        public Btns ULHVBSRV2SlowPumpValveOpen = new Btns();         //X108A
        public Btns ULHVBSRV2SlowPumpValveClose = new Btns();        //X108B
        public Btns ULHVBRV2FastPumpValveOpen = new Btns();          //X108C
        public Btns ULHVBRV2FastPumpValveClose = new Btns();         //X108D
        public Btns ULHVBVV2VentOpen = new Btns();                   //X108E
        public Btns ULHVBVV2VentClose = new Btns();                  //X108F

        //******************************************************************
        public Btns ULHVBDryBoostPumpRunning = new Btns();           //X1090
        public Btns ULHVBEV3Open = new Btns();                       //X1091
        public Btns ULHVBEV3Close = new Btns();                      //X1092
        public Btns ULHVBDryBoostPumpCoolingWaterOK = new Btns();    //X1093
        public Btns ULHVBGV2Open = new Btns();                       //X1094
        public Btns ULHVBGV2Close = new Btns();                      //X1095
        public Btns SPARE38 = new Btns();                            //X1096
        public Btns SPARE39 = new Btns();                            //X1097
        public Btns SPARE40 = new Btns();                            //X1098
        public Btns SPARE41 = new Btns();                            //X1099
        public Btns SPARE42 = new Btns();                            //X109A
        public Btns SPARE43 = new Btns();                            //X109B
        public Btns SPARE44 = new Btns();                            //X109C
        public Btns SPARE45 = new Btns();                            //X109D
        public Btns SPARE46 = new Btns();                            //X109E
        public Btns SPARE47 = new Btns();                            //X109F

    }

    public class SYS_DO
    {
        public Btns EntryFeederStartReady = new Btns();                  //Y1000 
        public Btns DV1Open = new Btns();                               //Y1001
        public Btns DV1Close = new Btns();                              //Y1002
        public Btns DV2Open = new Btns();                               //Y1003
        public Btns DV2Close = new Btns();                              //Y1004
        public Btns SPARE0 = new Btns();                                //Y1005
        public Btns LLHVBRV1FastPumpValveOpen = new Btns();             //Y1006
        public Btns LLHVBRV1FastPumpValveClose = new Btns();            //Y1007
        public Btns LLHVBSRV1SlowPumpValveOpen = new Btns();            //Y1008
        public Btns LLHVBSRV1SlowPumpValveClose = new Btns();           //Y1009
        public Btns LLHVBDryBoostPumpStart = new Btns();                //Y100A
        public Btns SignalTower_G = new Btns();                         //Y100B
        public Btns SignalTower_Y = new Btns();                         //Y100C
        public Btns SignalTower_R = new Btns();                         //Y100D
        public Btns SignalTower_B = new Btns();                         //Y100E
        public Btns Signal_Buzzer = new Btns();                         //Y100F
        //**********************************************************************
        public Btns LLHVBVV1VentOpen = new Btns();                       //Y1110
        public Btns MDV1O2MixArOpen = new Btns();                        //Y1111
        public Btns MDV2O2OutOpen = new Btns();                          //Y1112
        public Btns MDV3O2InOpen = new Btns();                           //Y1113
        public Btns MDV4ArOutOpen = new Btns();                          //Y1114
        public Btns MDV5ArInOpen = new Btns();                           //Y1115
        public Btns LLHVBVMV1Open = new Btns();                          //Y1116
        public Btns SPARE3 = new Btns();                                 //Y1117
        public Btns SPARE4 = new Btns();                                 //Y1118
        public Btns SPARE5 = new Btns();                                 //Y1119
        public Btns SPARE6 = new Btns();                                 //Y111A
        public Btns SPARE7 = new Btns();                                 //Y111B
        public Btns SPARE8 = new Btns();                                 //Y111C
        public Btns SPARE9 = new Btns();                                 //Y111D
        public Btns SPARE10 = new Btns();                                //Y111E
        public Btns SPARE11 = new Btns();                                //Y111F
        //**********************************************************************
        public Btns SPARE26 = new Btns();                                //Y1120
        public Btns LLHVBGV1Open = new Btns();                           //Y1121
        public Btns LLHVBGV1Close = new Btns();                          //Y1122
        public Btns LLHVBTurboPumpStart = new Btns();                    //Y1123
        public Btns LLHVBEV1Open = new Btns();                           //Y1124
        public Btns LLHVBEV1Close = new Btns();                          //Y1125
        public Btns TRRV3VentValveOpen = new Btns();                     //Y1126
        public Btns TRRV3VentValveClose = new Btns();                    //Y1127
        public Btns BackingDryBoostPumpStart = new Btns();               //Y1128
        public Btns PCCGV3Open = new Btns();                             //Y1129
        public Btns PCCGV3Close = new Btns();                            //Y112A
        public Btns PCCTurboPumpStart = new Btns();                      //Y112B
        public Btns PCCEV2Open = new Btns();                             //Y112C
        public Btns PCCEV2Close = new Btns();                            //Y112D
        public Btns DV3Open = new Btns();                                //Y112E
        public Btns DV3Close = new Btns();                               //Y112F
        //**********************************************************************
        public Btns SPARE28 = new Btns();                                //Y1130
        public Btns RGAIsolationValveOpen = new Btns();                  //Y1131
        public Btns TRVV3VentOpen = new Btns();                          //Y1132
        public Btns TRVMV3Open = new Btns();                             //Y1133
        public Btns MDV17ArOutOpen = new Btns();                         //Y1134
        public Btns MDV14O2OutOpen = new Btns();                         //Y1135
        public Btns MDV11ArOutOpen = new Btns();                         //Y1136
        public Btns MDV18ArOutOpen = new Btns();                         //Y1137
        public Btns MDV15O2OutOpen = new Btns();                         //Y1138
        public Btns MDV12ArOutOpen = new Btns();                         //Y1139
        public Btns MDV19ArInOpen = new Btns();                          //Y113A
        public Btns MDV16O2InOpen = new Btns();                          //Y113B
        public Btns MDV13ArInOpen = new Btns();                          //Y113C
        public Btns SPARE29 = new Btns();                                //Y113D
        public Btns SPARE30 = new Btns();                                //Y113E
        public Btns SPARE31 = new Btns();                                //Y113F
        //*********************************************************************
        public Btns RPDSourceSupplyGV6Open = new Btns();                 //Y1140
        public Btns RPDSourceSupplyGV6Close = new Btns();                //Y1141
        public Btns RPDSourceSupplyTurboPumpStart = new Btns();          //Y1142
        public Btns RPDSourceSupplyEV4Open = new Btns();                 //Y1143
        public Btns RPDSourceSupplyEV4Close = new Btns();                //Y1144
        public Btns RPDSourceSupplyOilPumpStart = new Btns();            //Y1145
        public Btns RPDSourceSupplyRV4Open = new Btns();                 //Y1146
        public Btns RPDSourceSupplyRV4Close = new Btns();                //Y1147
        public Btns RPDSourceSupplyUpperGateOpenTS1 = new Btns();        //Y1148
        public Btns RPDSourceSupplyUpperGateCloseTS1 = new Btns();       //Y1149
        public Btns RPDSourceSupplyLowerGateOpenGV4 = new Btns();        //Y114A
        public Btns RPDSourceSupplyLowerGateCloseGV4 = new Btns();       //Y114B
        public Btns RPDSourceSupplyRotaryFWDPB1 = new Btns();            //Y114C
        public Btns RPDSourceSupplyRotaryBWDPB1 = new Btns();            //Y114D
        public Btns SPARE1 = new Btns();                                 //Y114E
        public Btns SPARE2 = new Btns();                                 //Y114F     
        //**********************************************************************
        public Btns RPDSourceSupplyVentOpenVV4 = new Btns();             //Y1150
        public Btns RPDSourceSupplyFASTVentOpenVMV4 = new Btns();        //Y1151
        public Btns SPARE12 = new Btns();                                //Y1152
        public Btns SPARE13 = new Btns();                                //Y1153
        public Btns SPARE14 = new Btns();                                //Y1154
        public Btns SPARE15 = new Btns();                                //Y1155
        public Btns SPARE16 = new Btns();                                //Y1156
        public Btns SPARE17 = new Btns();                                //Y1157
        public Btns SPARE18 = new Btns();                                //Y1158
        public Btns SPARE19 = new Btns();                                //Y1159
        public Btns SPARE20 = new Btns();                                //Y115A
        public Btns SPARE21 = new Btns();                                //Y115B
        public Btns SPARE22 = new Btns();                                //Y115C
        public Btns SPARE23 = new Btns();                                //Y115D
        public Btns SPARE24 = new Btns();                                //Y115E
        public Btns SPARE25 = new Btns();                                //Y115F       
        //**********************************************************************
        public Btns ULHVBTurboPumpStart = new Btns();                    //Y1160
        public Btns SPARE27 = new Btns();                                //Y1161
        public Btns DV4Open = new Btns();                                //Y1162
        public Btns DV4Close = new Btns();                               //Y1163
        public Btns OutletFeederTakeOutReady = new Btns();               //Y1164
        public Btns ULHVBSRV2SlowPumpValveOpen = new Btns();             //Y1165
        public Btns ULHVBSRV2SlowPumpValveClose = new Btns();            //Y1166
        public Btns ULHVBRV2FastPumpValveOpen = new Btns();              //Y1167
        public Btns ULHVBRV2FastPumpValveClose = new Btns();             //Y1168
        public Btns MainWaterInletValve = new Btns();                    //Y1169
        public Btns MainWaterOutletValve = new Btns();                   //Y116A
        public Btns ULHVBDryBoostPumpStart = new Btns();                 //Y116B
        public Btns ULHVBEV3Open = new Btns();                           //Y116C
        public Btns ULHVBEV3Close = new Btns();                          //Y116D
        public Btns ULHVBGV2Open = new Btns();                           //Y116E
        public Btns ULHVBGV2Close = new Btns();                          //Y116F      
        //**********************************************************************
        public Btns MDV10ArOutOpen = new Btns();                         //Y1170
        public Btns MDV6O2MixArOpen = new Btns();                        //Y1171
        public Btns MDV7O2OutOpen = new Btns();                          //Y1172
        public Btns MDV9ArOutOpen = new Btns();                          //Y1173
        public Btns MDV8O2InOpen = new Btns();                           //Y1174
        public Btns ULHVBVV2VentOpen = new Btns();                       //Y1175
        public Btns ULHVBVMV2Open = new Btns();                          //Y1176
        public Btns TRCoolingWater1In = new Btns();                      //Y1177
        public Btns TRCoolingWater2In = new Btns();                      //Y1178
        public Btns TRCoolingWater3In = new Btns();                      //Y1179
        public Btns SPARE32 = new Btns();                                //Y117A
        public Btns SPARE33 = new Btns();                                //Y117B
        public Btns SPARE34 = new Btns();                                //Y117C
        public Btns SPARE35 = new Btns();                                //Y117D
        public Btns SPARE36 = new Btns();                                //Y117E
        public Btns SPARE37 = new Btns();                                //Y117F
    }

    public class SYS_Heater
    {
        public double W_1_Heater_Temp_SetValue;
        public double W_2_Heater_Temp_SetValue;
        public double W_3_Heater_Temp_SetValue;
    }


    #endregion

    #region EF
    public class EF
    {
        public EF_Data Data = new EF_Data();
        public EF_Map Map = new EF_Map();
        public EF_Btn Btn = new EF_Btn();
        public EF_DI DI = new EF_DI();
        public EF_DO DO = new EF_DO();
    }

    public class EF_Data
    {
        public double EFCVSpeedTime;
        public double EFCVDecTime;
        public double EFToLLDelayTime;

        public int TD_TrayID;
        public int TD_Spare1;
        public int TD_Spare2;
        public int TD_Spare3;
        public int TD_Spare4;
        public int TD_Spare5;

    }

    public class EF_Map
    {

        public int TaryRegistration;
        public double EFCVSpeed; 

    }

    public class EF_Btn
    {
        public Btns LDPortCVForwardSwitch = new Btns();
        public Btns LDPortCVReverseSwitch = new Btns();
        public Btns LDPortCVStopSwitch = new Btns();
    }

    public class EF_DI
    {

    }
    public class EF_DO
    {


    }

    #endregion

    #region LL
    public class LL
    {
        public LL_Data Data = new LL_Data();
        public LL_Map Map = new LL_Map();
        public LL_Btn Btn = new LL_Btn();
        public LL_DI DI = new LL_DI();
        public LL_DO DO = new LL_DO();
    }
    public class LL_Data
    {
        public double LLCVSpeedTime;
        public double LLCVDecTime;

        public int TD_TrayID;
        public int TD_Spare1;
        public int TD_Spare2;
        public int TD_Spare3;
        public int TD_Spare4;
        public int TD_Spare5;

        public double Record_LLReadyToGo_P;
        public int Record_LLMFCO2_Flow;
        public int Record_LLMFCAr_Flow;
        public double Record_LLReadyToVent_P;
        public int Record_LLPumpDownTo50m_Time;
        public int Record_LLPumpDownTo1m_Time;

        public double LLPumpFlowLowerLimit;
        public double LLTPPumpFlowLowerLimit;

        public int LL_SRVSet_Sec;

        public int LL_MFC_Pipeline_Delay_Sec;

        //Add by Henry 2016/2/18 新增LL控壓精度
        public double LLPCAccuracy;

    }
    public class LL_Map
    {
        public double LLGaugeValuePH;

        public double LLPumpFlowValue;
        public double BackingFlowValue;
        public double LL_TP_PumpFlowValue;
        public int TaryRegistration;

        public int LLMFCO2Set;
        public int LLMFCArSet;
        public int LLMFCXSet;

        public int LLMFCArFlow;
        public int LLMFCO2Flow;
        public int LLMFCXFlow;

        public int TP_LL_Status;

        public int Record_LLReadyToGo;
        public int Record_LLReadyToVent;
        public int Record_LLPumpDownTo50m;
        public int Record_LLPumpDownTo1m;

        public double LLCVSpeed; 
    }

    public class LL_Btn
    {
        public Btns LLCVForwardSwitch = new Btns();
        public Btns LLCVReverseSwitch = new Btns();
        public Btns LLCVStopSwitch = new Btns();
        public Btns DV1OpenSwitch = new Btns();
        public Btns DV1CloseSwitch = new Btns();
        public Btns SRV1OpenSwitch = new Btns();
        public Btns SRV1CloseSwitch = new Btns();
        public Btns RV1OpenSwitch = new Btns();
        public Btns RV1CloseSwitch = new Btns();
        public Btns LLBPDPRunSwitch = new Btns();
        public Btns GV1OpenSwitch = new Btns();
        public Btns GV1CloseSwitch = new Btns();
        public Btns EV1OpenSwitch = new Btns();
        public Btns EV1CloseSwitch = new Btns();
        public Btns VMV1OpenSwitch = new Btns();
        public Btns VV1OpenSwitch = new Btns();
        public Btns MDV1OpenSwitch = new Btns();
        public Btns MDV2OpenSwitch = new Btns();
        public Btns MDV3OpenSwitch = new Btns();
        public Btns MDV4OpenSwitch = new Btns();
        public Btns MDV5OpenSwitch = new Btns();

        //Add by Henry 2016/1/22 
        public Btns MDV20OpenSwitch = new Btns();
        public Btns MDV21OpenSwitch = new Btns();
       
        public Btns LLTPRunSwitch = new Btns();
        public Btns LLTPStopSwitch = new Btns();

        public Btns LLBPDPStopSwitch = new Btns();
        public Btns VMV1CloseSwitch = new Btns();
        public Btns VV1CloseSwitch = new Btns();
        public Btns MDV1CloseSwitch = new Btns();
        public Btns MDV2CloseSwitch = new Btns();
        public Btns MDV3CloseSwitch = new Btns();
        public Btns MDV4CloseSwitch = new Btns();
        public Btns MDV5CloseSwitch = new Btns();

        //Add by Henry 2016/1/22 
        public Btns MDV20CloseSwitch = new Btns();
        public Btns MDV21CloseSwitch = new Btns();
     
        public Btns LLPumpDownSwitch = new Btns();
        public Btns LLVentSwitch = new Btns();
        public Btns LLTPStartSwitch = new Btns();
      
     
        public Btns LLMFCO2Switch = new Btns();
        public Btns LLMFCArSwitch = new Btns();
        public Btns LLMFCXSwitch = new Btns();

        public Btns PMV1OpenSwitch = new Btns();
        public Btns PMV1CloseSwitch = new Btns();



    }

    public class LL_DI
    {


    }
    public class LL_DO
    {

    }

    #endregion

    #region MFC
    public class MFC
    {
        public MFC_Data Data = new MFC_Data();
        public MFC_Map Map = new MFC_Map();
        public MFC_Btn Btn = new MFC_Btn();
        public MFC_DI DI = new MFC_DI();
        public MFC_DO DO = new MFC_DO();
    }

    public class MFC_Data
    {

    }

    public class MFC_Map
    {
        //LayOut圖顯示
        public int GlassAUp = 0;        //A上層是否有片Lamp-->(B0262)
        public int GlassBUp = 0;        //B上層是否有片Lamp-->(B0263)
        public int GlassADown = 0;      //A下層是否有片Lamp-->(B0264)
        public int GlassBDown = 0;      //B下層是否有片Lamp-->(B0265)

    }

    public class MFC_Btn
    {
        //Main
        public int HeatingTimeSet_IL = 0;     //初始化Interlock-->(B0031)
        public int HeatingTimeSet_LP = 0;     //初始化Lamp-->(B0171)


    }

    public class MFC_DI
    {
        public int[] X000 = new int[16];
        public int[] X010 = new int[16];
        public int[] X020 = new int[16];
        public int[] X030 = new int[16];
        public int[] X040 = new int[16];
    }
    public class MFC_DO
    {
        public int[] Y050 = new int[16];
        public int[] Y060 = new int[16];
        public int[] Y070 = new int[16];
        public int[] Y080 = new int[16];
    }

    #endregion

    #region PCC1
    public class PCC1
    {
        public PCC1_Data Data = new PCC1_Data();
        public PCC1_Map Map = new PCC1_Map();
        public PCC1_Btn Btn = new PCC1_Btn();
        public PCC1_DI DI = new PCC1_DI();
        public PCC1_DO DO = new PCC1_DO();
    }

    public class PCC1_Data
    {
        public double RGunCurrentofHearthCoil;
        public double RGunCurrentofSteeringCoil;
        public double RGunCurrentofG2Coil;
        public double RGunVoltageofMainHearth;
        public double RGunVoltageofBeamGuide;
        public double RGunDischargeVoltage;
        public double RGunCurrentofMainHearth;
        public double RGunCurrentofBeamGuide;

        public int UseRecipeNumber;
        public int RGunGunArCoolingTimeCounter;
        public int RGunAccumulationTimeMainDischargehour;
        public int RGunAccumulationTimeMainDischargemin;
        public int RGunResistanceValueofBeamGuide_Earth;
        public int RGunResistanceValueofMainHearth_Earth;
        public int RGunResistanceValueofG1_Earth;
        public int RGunResistanceValueofG2_Earth;
        public int RGunResistanceValueofBG_BGCoil;
        public int RGunArFlowValue;
        public int ChamberO2FlowValue;
        public int ChamberArFlowValue;

        public int RGunQtyofRevolverTablet;
        public double RGunMHpush_uplevel;
        public double RGunChargingpush_uplevel;
        public int RGunRevolverangle;


        public int IgnitionRunning;
        public int IgnitionOK;
        public int IgnitionStopping;
        public int IgnitionStop;
        public int CoolingStopping;
        public int CoolingStop;
        public int MainDischargeAnodeChange;
        public int MainDischarge;
        public int BeamGuideDischargeAnodeChang;
        public int BeamGuideDischarge;
        public int AutoGunIgnitionOperation;
        public int AutoGunCoolingOperation;
        public int AutoGunCurrentChangeOperation;
        public int AutoPushupcommand;
        public int RemoteOK;

        public int IgnitionModeAutoMODE;
        public int AutoIgnitionReady;
        public int AutoCoolingReady;
        public int AutoCurrentChangeReady;
        public int CoilPSOK;
        public int SubPSOK;
        public int MainPSOK;
        public int OPOK;
        public int ResistanceMeasurement;
        public int CoatingRunningOK;
        public int ProgramInitializationComplete;
        public int COILPOWERMCCBON;
        public int MAINDISPOWERMCON;
        public int TBChargingRequest;
        public int SystemNoError;
        public int SystemAlarm;
        public int SystemWarning;

        public int TurnTableCWCommand;
        public int TurnTableCCWCommand;
        public int ChargingUpperGateOpenCommand;
        public int ChargingUpperGateClosCommand;
        public int ChargingLowerGateOpenCommand;
        public int ChargingLowerGateClosCommand;
        public int ChargingRoomVaccumCommand;
        public int ChargingRoomBreakCommand;
        public int ChargingRoomVacuumOperationCMD;
        public int ArOpenCommand;
        public int ChamberO2OpenCommand;
        public int ChamberArOpenCommand;

        public int MHPushupUpperLS;
        public int MHPushupLowerLS;
        public int ChargingPushupUpperLS;
        public int ChargingPushupUpperPosition;
        public int ChargingPushupLowerPosition;
        public int ChargingPushupLowerLS;
        public int TurnTableCWLS;
        public int TurnTableCCWLS;
        public int ChargingUpperGateOpenLS;
        public int ChargingUpperGateCloseLS;
        public int ChargingLowerGateOpenLS;
        public int ChargingLowerGateCloseLS;
        public int ChargingRoomTabletDetectSensor;
        public int HearthCleanerPositionOKDetect;

        public int ProcessCount;
        public double ProcessP;

        public int LLMFCO2Set1;
        public int LLMFCArSet1;
        public int LLMFCO2Set2;
        public int LLMFCArSet2;
        public int LLMFCO2Set3;
        public int LLMFCArSet3;


        public int ULMFCO2Set1;
        public int ULMFCArSet1;
        public int ULMFCO2Set2;
        public int ULMFCArSet2;
        public int ULMFCO2Set3;
        public int ULMFCArSet3;

        public int LLDelayTime;
        public int TRCV3DelayTime;

        public int[] PCC_TABLE;

        public bool MDAWarningRequest;

        public double PlasmaGeneratorG1FlowLowerLimit;
        public double PlasmaGeneratorG2FlowLowerLimit;
        public double PlasmaGeneratorsteeringcoilFlowLowerLimit;
        public double HearthMainFlowLowerLimit;
        public double HearthBeamFlowLowerLimit;
        public double RPDDoorInwallFlowLowerLimit;
        public double RPDChamberInwallFlowLowerLimit;

        public int IdelTime;
        public int ReadyTime;

    }

    public class PCC1_Map
    {
        public int TS1OpenLamp;
        public int TS1CloseLamp;
   
        public int PB1OpenLamp;
        public int PB1CloseLamp;

        public double PlasmaGeneratorG1FlowValue;
        public double PlasmaGeneratorG2FlowValue;
        public double PlasmaGeneratorSteeringCoilFlowValue;
        public double HearthMainFlowValue;
        public double HearthBeamFlowValue;

        public double DoorInWallFlowValue;
        public double ChamberInWallFlowValue;

        //add by william on 2016.07.20 for G! body and cooling water Temperature sensor value

        public double G1BodyTempValue;
        public double G1CWTempValue;

    }

    public class PCC1_Btn
    {

        public Btns PlasmaStart = new Btns();
        public Btns PlasmaStop = new Btns();
        public Btns MainDischargeChange = new Btns();
        public Btns BeamGuideDischargeChange = new Btns();
        public Btns ResistanceMeasurementOperation = new Btns();

    }

    public class PCC1_DI
    {

    }
    public class PCC1_DO
    {
    }

    #endregion

    #region PCC2
    public class PCC2
    {
        public PCC2_Data Data = new PCC2_Data();
        public PCC2_Map Map = new PCC2_Map();
        public PCC2_Btn Btn = new PCC2_Btn();
        public PCC2_DI DI = new PCC2_DI();
        public PCC2_DO DO = new PCC2_DO();
    }

    public class PCC2_Data
    {
       
    }

    public class PCC2_Map
    {
        public double TSS_GaugeValueP;
        public double TSS_TP_EV4_GaugeValueP;
        public double TSS_DP_EV4_GaugeValueP;

        public int TP_TSS_Status;

    }

    public class PCC2_Btn
    {
        public Btns RV4OpenSwitch = new Btns();
        public Btns RV4CloseSwitch = new Btns();
        public Btns TSSBPDPRunSwitch = new Btns();
        public Btns TSSBPDPStopSwitch = new Btns();
        public Btns GV6OpenSwitch = new Btns();
        public Btns GV6CloseSwitch = new Btns();
        public Btns EV4OpenSwitch = new Btns();
        public Btns EV4CloseSwitch = new Btns();
        public Btns VMV4OpenSwitch = new Btns();
        public Btns VV4OpenSwitch = new Btns();

        public Btns TSSTPRunSwitch = new Btns();
        public Btns TSSTPStopSwitch = new Btns();

        public Btns ULBPDPStopSwitch = new Btns();
        public Btns VMV4CloseSwitch = new Btns();
        public Btns VV4CloseSwitch = new Btns();
        public Btns GV4OpenSwitch = new Btns();
        public Btns GV4CloseSwitch = new Btns();

        public Btns TSSPumpDownSwitch = new Btns();
        public Btns TSSVentSwitch = new Btns();
        public Btns TSSTPStartSwitch = new Btns();

    }

    public class PCC2_DI
    {

    }
    public class PCC2_DO
    {


    }

    #endregion

    #region TR
    public class TR
    {
        public TR_Data Data = new TR_Data();
        public TR_Map Map = new TR_Map();
        public TR_Btn Btn = new TR_Btn();
        public TR_DI DI = new TR_DI();
        public TR_DO DO = new TR_DO();
    }

    public class TR_Data
    {
        public int TRGaugeValueP;

        public double TR1CVSpeedTime;
        public double TR1CVDecTime;
        public double TR2CVSpeedTime;
        public double TR2CVDecTime;
        public double TR3CVSpeedTime;
        public double TR3CVDecTime;
        public double TRCV1_TRCV2DelayTime;
        public double TR3CVToULDelayTime;

        public int TD_TR1_TrayID;
        public int TD_TR1_Spare1;
        public int TD_TR1_Spare2;
        public int TD_TR1_Spare3;
        public int TD_TR1_Spare4;
        public int TD_TR1_Spare5;

        public int TD_TR2_TrayID;
        public int TD_TR2_Spare1;
        public int TD_TR2_Spare2;
        public int TD_TR2_Spare3;
        public int TD_TR2_Spare4;
        public int TD_TR2_Spare5;

        public int TD_TR3_TrayID;
        public int TD_TR3_Spare1;
        public int TD_TR3_Spare2;
        public int TD_TR3_Spare3;
        public int TD_TR3_Spare4;
        public int TD_TR3_Spare5;

        public double BackingFlowLowerLimit;
        public double TR2CHBCOVERFlowLowerLimit;
        public double TRTPPumpFlowLowerLimit;
        public double TR3CHBCOVERFlowLowerLimit;
        public double TR2CoolingFlowLowerLimit;

        public double TRCgaugeOffset;
    }

    public class TR_Map
    {
        public double TRGaugeValuePH;
        public double TRGaugeValueC;
        public double TRGaugeValueC_NoOffset;


        public double TR2_CHB_COVERFlowValue;
        public double TR_TP_PumpFlowValue;
        public double TR3_CHB_COVERFlowValue;
        public double TR2_CoolerFlowValue;

        public int TR1_TaryRegistration;
        public int TR2_TaryRegistration;
        public int TR3_TaryRegistration;

        public int TRMFCAr_GunSet;
        public int TRMFCO2Set;
        public int TRMFCAr_ChamberSet;
        public int TRMFCH2OSet;

        public int TRMFCAr_Gun_Flow;
        public int TRMFCO2Flow;
        public int TRMFCAr_Chamber_Flow;
        public int TRMFCH2OFlow;

        public int TP_TR_Status;

        public double TR1CVSpeed;
        public double TR2CVSpeed;
        public double TR3CVSpeed;

        public double W_1_Heater_Temp;
        public double W_2_Heater_Temp;
        public double W_3_Heater_Temp;

    }

    public class TR_Btn
    {
        public Btns TR1CVForwardSwitch = new Btns();
        public Btns TR1CVReverseSwitch = new Btns();
        public Btns TR1CVStopSwitch = new Btns();
        public Btns TR2CVForwardSwitch = new Btns();
        public Btns TR2CVReverseSwitch = new Btns();
        public Btns TR2CVStopSwitch = new Btns();
        public Btns TR3CVForwardSwitch = new Btns();
        public Btns TR3CVReverseSwitch = new Btns();
        public Btns TR3CVStopSwitch = new Btns();
        public Btns DV2OpenSwitch = new Btns();
        public Btns DV2CloseSwitch = new Btns();
        public Btns DV3OpenSwitch = new Btns();
        public Btns DV3CloseSwitch = new Btns();

        public Btns RV3OpenSwitch = new Btns();
        public Btns RV3CloseSwitch = new Btns();
        public Btns GV3OpenSwitch = new Btns();
        public Btns GV3CloseSwitch = new Btns();
        public Btns EV3OpenSwitch = new Btns();
        public Btns EV3CloseSwitch = new Btns();
        public Btns VMV3OpenSwitch = new Btns();
        public Btns VV3OpenSwitch = new Btns();
        public Btns MDV11OpenSwitch = new Btns();
        public Btns MDV12OpenSwitch = new Btns();
        public Btns MDV13OpenSwitch = new Btns();
        public Btns MDV14OpenSwitch = new Btns();
        public Btns MDV15OpenSwitch = new Btns();
        public Btns MDV16OpenSwitch = new Btns();
        public Btns MDV17OpenSwitch = new Btns();
        public Btns MDV18OpenSwitch = new Btns();
        public Btns MDV19OpenSwitch = new Btns();

        public Btns MDV24OpenSwitch = new Btns();
        public Btns MDV25OpenSwitch = new Btns();

        public Btns TRTPRunSwitch = new Btns();
        public Btns TRTPStopSwitch = new Btns();

        public Btns DPRunSwitch = new Btns();
        public Btns DPStopSwitch = new Btns();
        public Btns VMV3CloseSwitch = new Btns();
        public Btns VV3CloseSwitch = new Btns();
        public Btns MDV11CloseSwitch = new Btns();
        public Btns MDV12CloseSwitch = new Btns();
        public Btns MDV13CloseSwitch = new Btns();
        public Btns MDV14CloseSwitch = new Btns();
        public Btns MDV15CloseSwitch = new Btns();
        public Btns MDV16CloseSwitch = new Btns();
        public Btns MDV17CloseSwitch = new Btns();
        public Btns MDV18CloseSwitch = new Btns();
        public Btns MDV19CloseSwitch = new Btns();

        public Btns MDV24CloseSwitch = new Btns();
        public Btns MDV25CloseSwitch = new Btns();

        public Btns TRPumpDownSwitch = new Btns();
        public Btns TRVentSwitch = new Btns();
        public Btns TRTPStartSwitch = new Btns();
        // add by sophia 20160822
        public Btns TRMFCTestSwitch = new Btns();

        public Btns TRMFCAr_GunSwitch = new Btns();
        public Btns TRMFCO2Switch = new Btns();
        public Btns TRMFCAr_ChamberSwitch = new Btns();
        public Btns TRMFCH2OSwitch = new Btns();
        public Btns W_1_Heater_Open = new Btns();
        public Btns W_1_Heater_Close = new Btns();
        public Btns W_2_Heater_Open = new Btns();
        public Btns W_2_Heater_Close = new Btns();
        public Btns W_3_Heater_Open = new Btns();
        public Btns W_3_Heater_Close = new Btns();
    }

    public class TR_DI
    {

    }
    public class TR_DO
    {

    }

    #endregion

    #region UL
    public class UL
    {
        public UL_Data Data = new UL_Data();
        public UL_Map Map = new UL_Map();
        public UL_Btn Btn = new UL_Btn();
        public UL_DI DI = new UL_DI();
        public UL_DO DO = new UL_DO();
    }

    public class UL_Data
    {
        public double ULCVSpeedTime;
        public double ULCVDecTime;
        public double ULToUNLDPortDelayTime;

        public int TD_TrayID;
        public int TD_Spare1;
        public int TD_Spare2;
        public int TD_Spare3;
        public int TD_Spare4;
        public int TD_Spare5;

        public double Record_ULReadyToGo_P;
        public int Record_ULMFCO2_Flow;
        public int Record_ULMFCAr_Flow;
        public double Record_ULReadyToVent_P;
        public int Record_ULPumpDownTo50m_Time;
        public int Record_ULPumpDownTo1m_Time;

        public double ULPumpFlowLowerLimit;
        public double ULTPPumpFlowLowerLimit;

        public double LLULTargetPreSet;

        public int UL_SRVSet_Sec;

        public int UL_MFC_Pipeline_Delay_Sec;

        //Add by Henry 2016/2/18 新增LL控壓精度
        public double ULPCAccuracy;
    }

    public class UL_Map
    {
        public double ULGaugeValuePH;
        public double ULPumpFlowValue;
        public double ULTPPumpFlowValue;
        public int TaryRegistration;

        public int ULMFCO2Set;
        public int ULMFCArSet;
        public int ULMFCXSet;

        public int ULMFCArFlow;
        public int ULMFCO2Flow;
        public int ULMFCXFlow;

        public int TP_UL_Status;

        public int Record_ULReadyToGo;
        public int Record_ULReadyToVent;
        public int Record_ULPumpDownTo50m;
        public int Record_ULPumpDownTo1m;

        public double ULCVSpeed;
    }

    public class UL_Btn
    {
        public Btns ULCVForwardSwitch = new Btns();
        public Btns ULCVReverseSwitch = new Btns();
        public Btns ULCVStopSwitch = new Btns();
        public Btns DV4OpenSwitch = new Btns();
        public Btns DV4CloseSwitch = new Btns();
        public Btns SRV2OpenSwitch = new Btns();
        public Btns SRV2CloseSwitch = new Btns();
        public Btns RV2OpenSwitch = new Btns();
        public Btns RV2CloseSwitch = new Btns();
        public Btns ULBPDPRunSwitch = new Btns();
        public Btns GV2OpenSwitch = new Btns();
        public Btns GV2CloseSwitch = new Btns();
        public Btns EV2OpenSwitch = new Btns();
        public Btns EV2CloseSwitch = new Btns();
        public Btns VMV2OpenSwitch = new Btns();
        public Btns VV2OpenSwitch = new Btns();
        public Btns MDV6OpenSwitch = new Btns();
        public Btns MDV7OpenSwitch = new Btns();
        public Btns MDV8OpenSwitch = new Btns();
        public Btns MDV9OpenSwitch = new Btns();
        public Btns MDV10OpenSwitch = new Btns();

        public Btns MDV22OpenSwitch = new Btns();
        public Btns MDV23OpenSwitch = new Btns();


        public Btns ULTPRunSwitch = new Btns();
        public Btns ULTPStopSwitch = new Btns();

        public Btns ULBPDPStopSwitch = new Btns();
        public Btns VMV2CloseSwitch = new Btns();
        public Btns VV2CloseSwitch = new Btns();
        public Btns MDV6CloseSwitch = new Btns();
        public Btns MDV7CloseSwitch = new Btns();
        public Btns MDV8CloseSwitch = new Btns();
        public Btns MDV9CloseSwitch = new Btns();
        public Btns MDV10CloseSwitch = new Btns();

        public Btns MDV22CloseSwitch = new Btns();
        public Btns MDV23CloseSwitch = new Btns();

        public Btns UNLDPortCVForwardSwitch = new Btns();
        public Btns UNLDPortCVReverseSwitch = new Btns();
        public Btns UNLDPortCVStopSwitch = new Btns();

        public Btns ULPumpDownSwitch = new Btns();
        public Btns ULVentSwitch = new Btns();
        public Btns ULTPStartSwitch = new Btns();

        public Btns ULMFCO2Switch = new Btns();
        public Btns ULMFCArSwitch = new Btns();
        public Btns ULMFCXSwitch = new Btns();

        public Btns PMV2OpenSwitch = new Btns();
        public Btns PMV2CloseSwitch = new Btns();

    }

    public class UL_DI
    {

    }
    public class UL_DO
    {

    }

    #endregion

    #region DF
    public class DF
    {
        public DF_Data Data = new DF_Data();
        public DF_Map Map = new DF_Map();
        public DF_Btn Btn = new DF_Btn();
        public DF_DI DI = new DF_DI();
        public DF_DO DO = new DF_DO();
    }

    public class DF_Data
    {
        public double DFCVSpeedTime;
        public double DFCVDecTime;

        public int TD_TrayID;
        public int TD_Spare1;
        public int TD_Spare2;
        public int TD_Spare3;
        public int TD_Spare4;
        public int TD_Spare5;

    }

    public class DF_Map
    {
        public int TaryRegistration;

        public double DFCVSpeed;
    }

    public class DF_Btn
    {
        public Btns UNLDPortCVForwardSwitch = new Btns();
        public Btns UNLDPortCVReverseSwitch = new Btns();
        public Btns UNLDPortCVStopSwitch = new Btns();

    }

    public class DF_DI
    {

    }
    public class DF_DO
    {
    }

    #endregion

    public class Btns
    {
        public int Lamp = 0;
        public int Interlock = 0;
        public int Switch = 0;
        public string Address = "";
    }



}
