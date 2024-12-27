using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace RPD_1GUN
{
    class clsPLC
    {
        private static int _iPlcSN = 1; //Default Value 1 ---- 指定 PLC Logical Station Number 必須與 Communication Setup Utility 一致才能連線
        private static ACTMULTILib.ActEasyIF _objPLC1 = new ACTMULTILib.ActEasyIF(); //引用三菱dll       
        public static bool _bConn = false;  //連線狀態
        private static System.Timers.Timer timRead = new System.Timers.Timer();
        private static string MyPath = Application.StartupPath + "\\ReadPLCError\\";
        int _GUIAliveReCount = 0;

        //System.Threading.Timer _ThreadTimer = null;
        //private static System.Threading.Mutex Timermutex = new System.Threading.Mutex();



        public clsPLC(int iPlcSN)
        {
            _iPlcSN = iPlcSN;

            #region 初始化 Switch 的點位

            RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Address = "M15001";
            RPD.STRCTURE.SYS.Btn.ManualModeSwitch.Address = "M15002";
            RPD.STRCTURE.SYS.Btn.DummyModeSwitch.Address = "M15003";
            RPD.STRCTURE.SYS.Btn.StartSwitch.Address = "M15004";
            RPD.STRCTURE.SYS.Btn.StopSwitch.Address = "M15005";
            RPD.STRCTURE.SYS.Btn.InitialSwitch.Address = "M15008";
            RPD.STRCTURE.SYS.Btn.AbortSwitch.Address = "M15009";
            RPD.STRCTURE.SYS.Btn.AlarmResetSwitch.Address = "M15010";
            RPD.STRCTURE.SYS.Btn.BuzzerOffSwitch.Address = "M15011";
            RPD.STRCTURE.SYS.Btn.TactTimeReaetSwitch.Address = "M15012";
            RPD.STRCTURE.SYS.Btn.TrayCountResetSwitch.Address = "M15016";
            RPD.STRCTURE.SYS.Btn.ForcedExcludeSwitch.Address = "M15017";
            RPD.STRCTURE.SYS.Btn.LeakTestSwitchTR.Address = "M15018";
            //add by sophia
            RPD.STRCTURE.TR.Btn.TRMFCTestSwitch.Address = "M15029";

            RPD.STRCTURE.SYS.Btn.StartCodeInputKeySwitch.Address = "M15030";

            //add by william
            RPD.STRCTURE.SYS.Btn.ALLLineInit.Address = "M15040";


            RPD.STRCTURE.SYS.Btn.TrayDeleteSwitch.Address = "M15051";
            RPD.STRCTURE.SYS.Btn.TrayDataModifySwitch.Address = "M15052";
            RPD.STRCTURE.SYS.Btn.MachineDataSaveSwitch.Address = "M15053";
            RPD.STRCTURE.SYS.Btn.OutputForceOnOffSwitch.Address = "M15054";
            RPD.STRCTURE.SYS.Btn.CurrentRecipeChangeSwitch.Address = "M15055";

            RPD.STRCTURE.PCC1.Btn.PlasmaStart.Address = "M15060";
            RPD.STRCTURE.PCC1.Btn.PlasmaStop.Address = "M15061";
            RPD.STRCTURE.PCC1.Btn.MainDischargeChange.Address = "M15062";
            RPD.STRCTURE.PCC1.Btn.BeamGuideDischargeChange.Address = "M15063";
            RPD.STRCTURE.PCC1.Btn.ResistanceMeasurementOperation.Address = "M15064";

            RPD.STRCTURE.SYS.Btn.ALLPumpDownSwitch.Address = "M15101";
            RPD.STRCTURE.LL.Btn.LLPumpDownSwitch.Address = "M15103";
            RPD.STRCTURE.TR.Btn.TRPumpDownSwitch.Address = "M15105";
            RPD.STRCTURE.UL.Btn.ULPumpDownSwitch.Address = "M15107";
            RPD.STRCTURE.PCC2.Btn.TSSPumpDownSwitch.Address = "M15109";
            RPD.STRCTURE.SYS.Btn.ALLVentSwitch.Address = "M15111";
            RPD.STRCTURE.LL.Btn.LLVentSwitch.Address = "M15113";
            RPD.STRCTURE.TR.Btn.TRVentSwitch.Address = "M15115";
            RPD.STRCTURE.UL.Btn.ULVentSwitch.Address = "M15117";
            RPD.STRCTURE.PCC2.Btn.TSSVentSwitch.Address = "M15119";
            RPD.STRCTURE.SYS.Btn.ALLTPStartSwitch.Address = "M15121";
            RPD.STRCTURE.LL.Btn.LLTPStartSwitch.Address = "M15123";
            RPD.STRCTURE.TR.Btn.TRTPStartSwitch.Address = "M15125";
            RPD.STRCTURE.UL.Btn.ULTPStartSwitch.Address = "M15127";
            RPD.STRCTURE.PCC2.Btn.TSSTPStartSwitch.Address = "M15129";

            RPD.STRCTURE.LL.Btn.LLMFCO2Switch.Address = "M15132";
            RPD.STRCTURE.LL.Btn.LLMFCArSwitch.Address = "M15133";

            RPD.STRCTURE.TR.Btn.TRMFCH2OSwitch.Address = "M15141";


            RPD.STRCTURE.UL.Btn.ULMFCO2Switch.Address = "M15134";
            RPD.STRCTURE.UL.Btn.ULMFCArSwitch.Address = "M15135";


            RPD.STRCTURE.TR.Btn.TRMFCAr_GunSwitch.Address = "M15136";
            RPD.STRCTURE.TR.Btn.TRMFCO2Switch.Address = "M15137";
            RPD.STRCTURE.TR.Btn.TRMFCAr_ChamberSwitch.Address = "M15138";

            RPD.STRCTURE.EF.Btn.LDPortCVForwardSwitch.Address = "M15201";
            RPD.STRCTURE.EF.Btn.LDPortCVReverseSwitch.Address = "M15202";
            RPD.STRCTURE.EF.Btn.LDPortCVStopSwitch.Address = "M15203";

            RPD.STRCTURE.LL.Btn.LLCVForwardSwitch.Address = "M15251";
            RPD.STRCTURE.LL.Btn.LLCVReverseSwitch.Address = "M15252";
            RPD.STRCTURE.LL.Btn.LLCVStopSwitch.Address = "M15253";
            RPD.STRCTURE.LL.Btn.DV1OpenSwitch.Address = "M15254";
            RPD.STRCTURE.LL.Btn.DV1CloseSwitch.Address = "M15255";
            RPD.STRCTURE.LL.Btn.SRV1OpenSwitch.Address = "M15256";
            RPD.STRCTURE.LL.Btn.SRV1CloseSwitch.Address = "M15257";
            RPD.STRCTURE.LL.Btn.RV1OpenSwitch.Address = "M15258";
            RPD.STRCTURE.LL.Btn.RV1CloseSwitch.Address = "M15259";
            RPD.STRCTURE.LL.Btn.LLBPDPRunSwitch.Address = "M15260";
            RPD.STRCTURE.LL.Btn.GV1OpenSwitch.Address = "M15261";
            RPD.STRCTURE.LL.Btn.GV1CloseSwitch.Address = "M15262";
            RPD.STRCTURE.LL.Btn.EV1OpenSwitch.Address = "M15263";
            RPD.STRCTURE.LL.Btn.EV1CloseSwitch.Address = "M15264";
            RPD.STRCTURE.LL.Btn.VMV1OpenSwitch.Address = "M15265";
            RPD.STRCTURE.LL.Btn.VV1OpenSwitch.Address = "M15266";
            RPD.STRCTURE.LL.Btn.MDV1OpenSwitch.Address = "M15267";
            RPD.STRCTURE.LL.Btn.MDV2OpenSwitch.Address = "M15268";
            RPD.STRCTURE.LL.Btn.MDV3OpenSwitch.Address = "M15269";
            RPD.STRCTURE.LL.Btn.MDV4OpenSwitch.Address = "M15270";
            RPD.STRCTURE.LL.Btn.MDV5OpenSwitch.Address = "M15271";
            RPD.STRCTURE.LL.Btn.LLBPDPStopSwitch.Address = "M15273";
            RPD.STRCTURE.LL.Btn.VMV1CloseSwitch.Address = "M15274";
            RPD.STRCTURE.LL.Btn.VV1CloseSwitch.Address = "M15275";
            RPD.STRCTURE.LL.Btn.MDV1CloseSwitch.Address = "M15276";
            RPD.STRCTURE.LL.Btn.MDV2CloseSwitch.Address = "M15277";
            RPD.STRCTURE.LL.Btn.MDV3CloseSwitch.Address = "M15278";
            RPD.STRCTURE.LL.Btn.MDV4CloseSwitch.Address = "M15279";
            RPD.STRCTURE.LL.Btn.MDV5CloseSwitch.Address = "M15280";

            //Add by Henry 2016/1/22
            RPD.STRCTURE.LL.Btn.MDV20OpenSwitch.Address = "M15281";
            RPD.STRCTURE.LL.Btn.MDV21OpenSwitch.Address = "M15282";
            RPD.STRCTURE.LL.Btn.MDV20CloseSwitch.Address = "M15283";
            RPD.STRCTURE.LL.Btn.MDV21CloseSwitch.Address = "M15284";

            RPD.STRCTURE.LL.Btn.LLTPStopSwitch.Address = "M15299";
            
            // Add by Albert 201905
            RPD.STRCTURE.TR.Btn.W_1_Heater_Open.Address = "M15317";
            RPD.STRCTURE.TR.Btn.W_1_Heater_Close.Address = "M15318";
            RPD.STRCTURE.TR.Btn.W_2_Heater_Open.Address = "M15319";
            RPD.STRCTURE.TR.Btn.W_2_Heater_Close.Address = "M15320";
            RPD.STRCTURE.TR.Btn.W_3_Heater_Open.Address = "M15321";
            RPD.STRCTURE.TR.Btn.W_3_Heater_Close.Address = "M15322";


            RPD.STRCTURE.TR.Btn.TR1CVForwardSwitch.Address = "M15351";
            RPD.STRCTURE.TR.Btn.TR1CVReverseSwitch.Address = "M15352";
            RPD.STRCTURE.TR.Btn.TR1CVStopSwitch.Address = "M15353";
            RPD.STRCTURE.TR.Btn.TR2CVForwardSwitch.Address = "M15354";
            RPD.STRCTURE.TR.Btn.TR2CVReverseSwitch.Address = "M15355";
            RPD.STRCTURE.TR.Btn.TR2CVStopSwitch.Address = "M15356";
            RPD.STRCTURE.TR.Btn.TR3CVForwardSwitch.Address = "M15357";
            RPD.STRCTURE.TR.Btn.TR3CVReverseSwitch.Address = "M15358";
            RPD.STRCTURE.TR.Btn.TR3CVStopSwitch.Address = "M15359";
            RPD.STRCTURE.TR.Btn.DV2OpenSwitch.Address = "M15360";
            RPD.STRCTURE.TR.Btn.DV2CloseSwitch.Address = "M15361";
            RPD.STRCTURE.TR.Btn.DV3OpenSwitch.Address = "M15362";
            RPD.STRCTURE.TR.Btn.DV3CloseSwitch.Address = "M15363";
            RPD.STRCTURE.TR.Btn.RV3OpenSwitch.Address = "M15366";
            RPD.STRCTURE.TR.Btn.RV3CloseSwitch.Address = "M15367";
            RPD.STRCTURE.TR.Btn.GV3OpenSwitch.Address = "M15368";
            RPD.STRCTURE.TR.Btn.GV3CloseSwitch.Address = "M15369";
            RPD.STRCTURE.TR.Btn.EV3OpenSwitch.Address = "M15370";
            RPD.STRCTURE.TR.Btn.EV3CloseSwitch.Address = "M15371";
            RPD.STRCTURE.TR.Btn.VMV3OpenSwitch.Address = "M15372";
            RPD.STRCTURE.TR.Btn.VV3OpenSwitch.Address = "M15373";
            RPD.STRCTURE.TR.Btn.MDV11OpenSwitch.Address = "M15374";
            RPD.STRCTURE.TR.Btn.MDV12OpenSwitch.Address = "M15375";
            RPD.STRCTURE.TR.Btn.MDV13OpenSwitch.Address = "M15376";
            RPD.STRCTURE.TR.Btn.MDV14OpenSwitch.Address = "M15377";
            RPD.STRCTURE.TR.Btn.MDV15OpenSwitch.Address = "M15378";
            RPD.STRCTURE.TR.Btn.MDV16OpenSwitch.Address = "M15379";
            RPD.STRCTURE.TR.Btn.MDV17OpenSwitch.Address = "M15380";
            RPD.STRCTURE.TR.Btn.MDV18OpenSwitch.Address = "M15381";
            RPD.STRCTURE.TR.Btn.MDV19OpenSwitch.Address = "M15382";

            RPD.STRCTURE.TR.Btn.DPRunSwitch.Address = "M15384";
            RPD.STRCTURE.TR.Btn.DPStopSwitch.Address = "M15385";
            RPD.STRCTURE.TR.Btn.VMV3CloseSwitch.Address = "M15386";
            RPD.STRCTURE.TR.Btn.VV3CloseSwitch.Address = "M15387";
            RPD.STRCTURE.TR.Btn.MDV11CloseSwitch.Address = "M15388";
            RPD.STRCTURE.TR.Btn.MDV12CloseSwitch.Address = "M15389";
            RPD.STRCTURE.TR.Btn.MDV13CloseSwitch.Address = "M15390";
            RPD.STRCTURE.TR.Btn.MDV14CloseSwitch.Address = "M15391";
            RPD.STRCTURE.TR.Btn.MDV15CloseSwitch.Address = "M15392";
            RPD.STRCTURE.TR.Btn.MDV16CloseSwitch.Address = "M15393";
            RPD.STRCTURE.TR.Btn.MDV17CloseSwitch.Address = "M15394";
            RPD.STRCTURE.TR.Btn.MDV18CloseSwitch.Address = "M15395";
            RPD.STRCTURE.TR.Btn.MDV19CloseSwitch.Address = "M15396";

            RPD.STRCTURE.TR.Btn.TRTPStopSwitch.Address = "M15399";

            //Add by Henry 2016/1/22
            RPD.STRCTURE.TR.Btn.MDV24OpenSwitch.Address = "M15400";
            RPD.STRCTURE.TR.Btn.MDV25OpenSwitch.Address = "M15401";
            RPD.STRCTURE.TR.Btn.MDV24CloseSwitch.Address = "M15402";
            RPD.STRCTURE.TR.Btn.MDV25CloseSwitch.Address = "M15403";

            RPD.STRCTURE.UL.Btn.ULCVForwardSwitch.Address = "M15451";
            RPD.STRCTURE.UL.Btn.ULCVReverseSwitch.Address = "M15452";
            RPD.STRCTURE.UL.Btn.ULCVStopSwitch.Address = "M15453";
            RPD.STRCTURE.UL.Btn.DV4OpenSwitch.Address = "M15454";
            RPD.STRCTURE.UL.Btn.DV4CloseSwitch.Address = "M15455";
            RPD.STRCTURE.UL.Btn.SRV2OpenSwitch.Address = "M15456";
            RPD.STRCTURE.UL.Btn.SRV2CloseSwitch.Address = "M15457";
            RPD.STRCTURE.UL.Btn.RV2OpenSwitch.Address = "M15458";
            RPD.STRCTURE.UL.Btn.RV2CloseSwitch.Address = "M15459";
            RPD.STRCTURE.UL.Btn.ULBPDPRunSwitch.Address = "M15460";
            RPD.STRCTURE.UL.Btn.GV2OpenSwitch.Address = "M15461";
            RPD.STRCTURE.UL.Btn.GV2CloseSwitch.Address = "M15462";
            RPD.STRCTURE.UL.Btn.EV2OpenSwitch.Address = "M15463";
            RPD.STRCTURE.UL.Btn.EV2CloseSwitch.Address = "M15464";
            RPD.STRCTURE.UL.Btn.VMV2OpenSwitch.Address = "M15465";
            RPD.STRCTURE.UL.Btn.VV2OpenSwitch.Address = "M15466";
            RPD.STRCTURE.UL.Btn.MDV6OpenSwitch.Address = "M15467";
            RPD.STRCTURE.UL.Btn.MDV7OpenSwitch.Address = "M15468";
            RPD.STRCTURE.UL.Btn.MDV8OpenSwitch.Address = "M15469";
            RPD.STRCTURE.UL.Btn.MDV9OpenSwitch.Address = "M15470";
            RPD.STRCTURE.UL.Btn.MDV10OpenSwitch.Address = "M15471";
            RPD.STRCTURE.UL.Btn.ULBPDPStopSwitch.Address = "M15473";
            RPD.STRCTURE.UL.Btn.VMV2CloseSwitch.Address = "M15474";
            RPD.STRCTURE.UL.Btn.VV2CloseSwitch.Address = "M15475";
            RPD.STRCTURE.UL.Btn.MDV6CloseSwitch.Address = "M15476";
            RPD.STRCTURE.UL.Btn.MDV7CloseSwitch.Address = "M15477";
            RPD.STRCTURE.UL.Btn.MDV8CloseSwitch.Address = "M15478";
            RPD.STRCTURE.UL.Btn.MDV9CloseSwitch.Address = "M15479";
            RPD.STRCTURE.UL.Btn.MDV10CloseSwitch.Address = "M15480";

            RPD.STRCTURE.UL.Btn.MDV22OpenSwitch.Address = "M15481";
            RPD.STRCTURE.UL.Btn.MDV23OpenSwitch.Address = "M15482";
            RPD.STRCTURE.UL.Btn.MDV22CloseSwitch.Address = "M15483";
            RPD.STRCTURE.UL.Btn.MDV23CloseSwitch.Address = "M15484";


            RPD.STRCTURE.UL.Btn.ULTPStopSwitch.Address = "M15499";

            RPD.STRCTURE.DF.Btn.UNLDPortCVForwardSwitch.Address = "M15501";
            RPD.STRCTURE.DF.Btn.UNLDPortCVReverseSwitch.Address = "M15502";
            RPD.STRCTURE.DF.Btn.UNLDPortCVStopSwitch.Address = "M15503";

            RPD.STRCTURE.PCC2.Btn.RV4OpenSwitch.Address = "M15556";
            RPD.STRCTURE.PCC2.Btn.RV4CloseSwitch.Address = "M15557";
            RPD.STRCTURE.PCC2.Btn.TSSBPDPRunSwitch.Address = "M15558";
            RPD.STRCTURE.PCC2.Btn.GV6OpenSwitch.Address = "M15559";
            RPD.STRCTURE.PCC2.Btn.GV6CloseSwitch.Address = "M15560";
            RPD.STRCTURE.PCC2.Btn.EV4OpenSwitch.Address = "M15561";
            RPD.STRCTURE.PCC2.Btn.EV4CloseSwitch.Address = "M15562";
            RPD.STRCTURE.PCC2.Btn.VMV4OpenSwitch.Address = "M15563";
            RPD.STRCTURE.PCC2.Btn.VV4OpenSwitch.Address = "M15564";
            RPD.STRCTURE.PCC2.Btn.ULBPDPStopSwitch.Address = "M15566";
            RPD.STRCTURE.PCC2.Btn.VMV4CloseSwitch.Address = "M15567";
            RPD.STRCTURE.PCC2.Btn.VV4CloseSwitch.Address = "M15568";
            RPD.STRCTURE.PCC2.Btn.TSSBPDPStopSwitch.Address = "M15569";

            RPD.STRCTURE.PCC2.Btn.TSSTPStopSwitch.Address = "M15599";

            RPD.STRCTURE.SYS.DO.EntryFeederStartReady.Address = "M15701";
            RPD.STRCTURE.SYS.DO.DV1Open.Address = "M15702";
            RPD.STRCTURE.SYS.DO.DV1Close.Address = "M15703";
            RPD.STRCTURE.SYS.DO.DV2Open.Address = "M15704";
            RPD.STRCTURE.SYS.DO.DV2Close.Address = "M15705";
            RPD.STRCTURE.SYS.DO.LLHVBRV1FastPumpValveOpen.Address = "M15707";
            RPD.STRCTURE.SYS.DO.LLHVBRV1FastPumpValveClose.Address = "M15708";
            RPD.STRCTURE.SYS.DO.LLHVBSRV1SlowPumpValveOpen.Address = "M15709";
            RPD.STRCTURE.SYS.DO.LLHVBSRV1SlowPumpValveClose.Address = "M15710";
            RPD.STRCTURE.SYS.DO.LLHVBDryBoostPumpStart.Address = "M15711";
            RPD.STRCTURE.SYS.DO.SignalTower_G.Address = "M15712";
            RPD.STRCTURE.SYS.DO.SignalTower_Y.Address = "M15713";
            RPD.STRCTURE.SYS.DO.SignalTower_R.Address = "M15714";
            RPD.STRCTURE.SYS.DO.SignalTower_B.Address = "M15715";
            RPD.STRCTURE.SYS.DO.Signal_Buzzer.Address = "M15716";
            RPD.STRCTURE.SYS.DO.LLHVBVV1VentOpen.Address = "M15717";
            RPD.STRCTURE.SYS.DO.MDV1O2MixArOpen.Address = "M15718";
            RPD.STRCTURE.SYS.DO.MDV2O2OutOpen.Address = "M15719";
            RPD.STRCTURE.SYS.DO.MDV3O2InOpen.Address = "M15720";
            RPD.STRCTURE.SYS.DO.MDV4ArOutOpen.Address = "M15721";
            RPD.STRCTURE.SYS.DO.MDV5ArInOpen.Address = "M15722";
            RPD.STRCTURE.SYS.DO.LLHVBVMV1Open.Address = "M15723";
            RPD.STRCTURE.SYS.DO.LLHVBGV1Open.Address = "M15734";
            RPD.STRCTURE.SYS.DO.LLHVBGV1Close.Address = "M15735";
            RPD.STRCTURE.SYS.DO.LLHVBTurboPumpStart.Address = "M15736";
            RPD.STRCTURE.SYS.DO.LLHVBEV1Open.Address = "M15737";
            RPD.STRCTURE.SYS.DO.LLHVBEV1Close.Address = "M15738";
            RPD.STRCTURE.SYS.DO.TRRV3VentValveOpen.Address = "M15739";
            RPD.STRCTURE.SYS.DO.TRRV3VentValveClose.Address = "M15740";
            RPD.STRCTURE.SYS.DO.BackingDryBoostPumpStart.Address = "M15741";
            RPD.STRCTURE.SYS.DO.PCCGV3Open.Address = "M15742";
            RPD.STRCTURE.SYS.DO.PCCGV3Close.Address = "M15743";
            RPD.STRCTURE.SYS.DO.PCCTurboPumpStart.Address = "M15744";
            RPD.STRCTURE.SYS.DO.PCCEV2Open.Address = "M15745";
            RPD.STRCTURE.SYS.DO.PCCEV2Close.Address = "M15746";
            RPD.STRCTURE.SYS.DO.DV3Open.Address = "M15747";
            RPD.STRCTURE.SYS.DO.DV3Close.Address = "M15748";
            RPD.STRCTURE.SYS.DO.RGAIsolationValveOpen.Address = "M15750";
            RPD.STRCTURE.SYS.DO.TRVV3VentOpen.Address = "M15751";
            RPD.STRCTURE.SYS.DO.TRVMV3Open.Address = "M15752";
            RPD.STRCTURE.SYS.DO.MDV17ArOutOpen.Address = "M15753";
            RPD.STRCTURE.SYS.DO.MDV14O2OutOpen.Address = "M15754";
            RPD.STRCTURE.SYS.DO.MDV11ArOutOpen.Address = "M15755";
            RPD.STRCTURE.SYS.DO.MDV18ArOutOpen.Address = "M15756";
            RPD.STRCTURE.SYS.DO.MDV15O2OutOpen.Address = "M15757";
            RPD.STRCTURE.SYS.DO.MDV12ArOutOpen.Address = "M15758";
            RPD.STRCTURE.SYS.DO.MDV19ArInOpen.Address = "M15759";
            RPD.STRCTURE.SYS.DO.MDV16O2InOpen.Address = "M15760";
            RPD.STRCTURE.SYS.DO.MDV13ArInOpen.Address = "M15761";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyGV6Open.Address = "M15765";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyGV6Close.Address = "M15766";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyTurboPumpStart.Address = "M15767";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyEV4Open.Address = "M15768";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyEV4Close.Address = "M15769";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyOilPumpStart.Address = "M15770";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyRV4Open.Address = "M15771";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyRV4Close.Address = "M15772";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyUpperGateOpenTS1.Address = "M15773";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyUpperGateCloseTS1.Address = "M15774";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyLowerGateOpenGV4.Address = "M15775";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyLowerGateCloseGV4.Address = "M15776";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyRotaryFWDPB1.Address = "M15777";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyRotaryBWDPB1.Address = "M15778";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyVentOpenVV4.Address = "M15781";
            RPD.STRCTURE.SYS.DO.RPDSourceSupplyFASTVentOpenVMV4.Address = "M15782";
            RPD.STRCTURE.SYS.DO.ULHVBTurboPumpStart.Address = "M15797";
            RPD.STRCTURE.SYS.DO.DV4Open.Address = "M15799";
            RPD.STRCTURE.SYS.DO.DV4Close.Address = "M15800";
            RPD.STRCTURE.SYS.DO.OutletFeederTakeOutReady.Address = "M15801";
            RPD.STRCTURE.SYS.DO.ULHVBSRV2SlowPumpValveOpen.Address = "M15802";
            RPD.STRCTURE.SYS.DO.ULHVBSRV2SlowPumpValveClose.Address = "M15803";
            RPD.STRCTURE.SYS.DO.ULHVBRV2FastPumpValveOpen.Address = "M15804";
            RPD.STRCTURE.SYS.DO.ULHVBRV2FastPumpValveClose.Address = "M15805";
            RPD.STRCTURE.SYS.DO.MainWaterInletValve.Address = "M15806";
            RPD.STRCTURE.SYS.DO.MainWaterOutletValve.Address = "M15807";
            RPD.STRCTURE.SYS.DO.ULHVBDryBoostPumpStart.Address = "M15808";
            RPD.STRCTURE.SYS.DO.ULHVBEV3Open.Address = "M15809";
            RPD.STRCTURE.SYS.DO.ULHVBEV3Close.Address = "M15810";
            RPD.STRCTURE.SYS.DO.ULHVBGV2Open.Address = "M15811";
            RPD.STRCTURE.SYS.DO.ULHVBGV2Close.Address = "M15812";
            RPD.STRCTURE.SYS.DO.MDV10ArOutOpen.Address = "M15813";
            RPD.STRCTURE.SYS.DO.MDV6O2MixArOpen.Address = "M15814";
            RPD.STRCTURE.SYS.DO.MDV7O2OutOpen.Address = "M15815";
            RPD.STRCTURE.SYS.DO.MDV9ArOutOpen.Address = "M15816";
            RPD.STRCTURE.SYS.DO.MDV8O2InOpen.Address = "M15817";
            RPD.STRCTURE.SYS.DO.ULHVBVV2VentOpen.Address = "M15818";
            RPD.STRCTURE.SYS.DO.ULHVBVMV2Open.Address = "M15819";
            RPD.STRCTURE.SYS.DO.TRCoolingWater1In.Address = "M15820";
            RPD.STRCTURE.SYS.DO.TRCoolingWater2In.Address = "M15821";
            RPD.STRCTURE.SYS.DO.TRCoolingWater3In.Address = "M15822";



            #endregion

            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);

            //_ThreadTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimerThread_Tick), "Timer", 0, 200); //宣告Timer

            Thread T_PLC = new Thread(new ThreadStart(TimerThread_Tick));

            T_PLC.Start();

        }

        public class ThreadPulse
        {
            private string sAddr;

            public ThreadPulse(string text)
            {
                sAddr = text;
            }

            //要丟給執行緒執行的方法，無 return value 就是為了能讓ThreadStart來呼叫
            public void ThreadProc()
            {
                Thread.Sleep(500);
                WriPLC_Bit(sAddr, false);

            }

        }

        private void TimerThread_Tick()
        {
            //Timermutex.WaitOne();
            while (true)
            {
                //lock (_ThreadTimer)
                //{
                    //StreamWriter sw_t = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    //sw_t.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    //sw_t.Close();

                    try
                    {
                        if (_GUIAliveReCount >= 4)
                        {
                            if (RPD.STRCTURE.SYS.Data.GUIAliveIndex == 65535)
                                RPD.STRCTURE.SYS.Data.GUIAliveIndex = 0;

                            RPD.STRCTURE.SYS.Data.GUIAliveIndex += 1;

                            FunSingleWriPLC("D35000", RPD.STRCTURE.SYS.Data.GUIAliveIndex);
                            _GUIAliveReCount = 0;
                        }

                        _GUIAliveReCount++;


                        ReadPLC_E2G_Bit_Interlock();
                        ReadPLC_E2G_Bit_Lamp();
                        ReadPLC_E2G_Word();
                    }
                    catch (Exception Ex)
                    {
                        StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + Ex.ToString());
                        sw.Close();
                    }
                    finally
                    {

                    }

                //}

                Thread.Sleep(200);
            }

            //Timermutex.ReleaseMutex();

        }


        #region Batch Read & Write

        public void ReadPLC_E2G_Bit_Interlock()
        {
            int[] iRetData = new int[125];   //M16992 後開始 2000個位址
            bool bRtn;
            bRtn = FunBatchReadPLC("M16992", ref iRetData); //M16992


            if (bRtn == true)
            {
                RPD.STRCTURE.SYS.Data.INTERLOCK_TABLE = iRetData;
                RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Interlock = ((iRetData[0] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                 //M17001
                RPD.STRCTURE.SYS.Btn.ManualModeSwitch.Interlock = ((iRetData[0] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);               //M17002
                RPD.STRCTURE.SYS.Btn.DummyModeSwitch.Interlock = ((iRetData[0] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                //M17003
                RPD.STRCTURE.SYS.Btn.StartSwitch.Interlock = ((iRetData[0] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                    //M17004
                RPD.STRCTURE.SYS.Btn.StopSwitch.Interlock = ((iRetData[0] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                     //M17005
                RPD.STRCTURE.SYS.Btn.ContinueSwitch.Interlock = ((iRetData[0] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                 //M17006
                RPD.STRCTURE.SYS.Btn.PauseSwitch.Interlock = ((iRetData[0] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                    //M17007
                RPD.STRCTURE.SYS.Btn.InitialSwitch.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                  //M17008

                RPD.STRCTURE.SYS.Btn.AbortSwitch.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M17009
                RPD.STRCTURE.SYS.Btn.AlarmResetSwitch.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);              //M17010
                RPD.STRCTURE.SYS.Btn.BuzzerOffSwitch.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);               //M17011
                RPD.STRCTURE.SYS.Btn.TactTimeReaetSwitch.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);           //M17012

                RPD.STRCTURE.SYS.Btn.TrayCountResetSwitch.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);           //M17016
                RPD.STRCTURE.SYS.Btn.ForcedExcludeSwitch.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);            //M17017
                RPD.STRCTURE.SYS.Btn.LeakTestSwitchTR.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);               //M17018

                //add by sophia 20160822 
                RPD.STRCTURE.TR.Btn.TRMFCTestSwitch.Interlock = ((iRetData[2] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                 //M17029
                RPD.STRCTURE.SYS.Btn.StartCodeInputKeySwitch.Interlock = ((iRetData[2] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);       //M17030

                //add by william 
                RPD.STRCTURE.SYS.Btn.ALLLineInit.Interlock = ((iRetData[3] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                    //M17040

                RPD.STRCTURE.SYS.Btn.TrayDeleteSwitch.Interlock = ((iRetData[3] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);               //M17051
                RPD.STRCTURE.SYS.Btn.TrayDataModifySwitch.Interlock = ((iRetData[3] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);           //M17052
                RPD.STRCTURE.SYS.Btn.MachineDataSaveSwitch.Interlock = ((iRetData[3] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);          //M17053
                RPD.STRCTURE.SYS.Btn.OutputForceOnOffSwitch.Interlock = ((iRetData[3] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);         //M17054
                RPD.STRCTURE.SYS.Btn.CurrentRecipeChangeSwitch.Interlock = ((iRetData[3] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);      //M17055

                RPD.STRCTURE.PCC1.Btn.PlasmaStart.Interlock = ((iRetData[4] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                    //M17060
                RPD.STRCTURE.PCC1.Btn.PlasmaStop.Interlock = ((iRetData[4] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                     //M17061
                RPD.STRCTURE.PCC1.Btn.MainDischargeChange.Interlock = ((iRetData[4] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);            //M17062
                RPD.STRCTURE.PCC1.Btn.BeamGuideDischargeChange.Interlock = ((iRetData[4] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);       //M17063
                RPD.STRCTURE.PCC1.Btn.ResistanceMeasurementOperation.Interlock = ((iRetData[4] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1); //M17064


                RPD.STRCTURE.SYS.Btn.ALLPumpDownSwitch.Interlock = ((iRetData[6] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);              //M17101

                //add by william 

                RPD.STRCTURE.LL.Btn.LLPumpDownSwitch.Interlock = ((iRetData[6] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M17103
                RPD.STRCTURE.TR.Btn.TRPumpDownSwitch.Interlock = ((iRetData[7] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                //M17105 
                RPD.STRCTURE.UL.Btn.ULPumpDownSwitch.Interlock = ((iRetData[7] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);               //M17107
                RPD.STRCTURE.PCC2.Btn.TSSPumpDownSwitch.Interlock = ((iRetData[7] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);            //M17109
                RPD.STRCTURE.SYS.Btn.ALLVentSwitch.Interlock = ((iRetData[7] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                 //M17111
                RPD.STRCTURE.LL.Btn.LLVentSwitch.Interlock = ((iRetData[7] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                    //M17113
                RPD.STRCTURE.TR.Btn.TRVentSwitch.Interlock = ((iRetData[7] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                    //M17115
                RPD.STRCTURE.UL.Btn.ULVentSwitch.Interlock = ((iRetData[7] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                    //M17117
                RPD.STRCTURE.PCC2.Btn.TSSVentSwitch.Interlock = ((iRetData[7] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                 //M17119
                RPD.STRCTURE.SYS.Btn.ALLTPStartSwitch.Interlock = ((iRetData[8] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);               //M17121
                RPD.STRCTURE.LL.Btn.LLTPStartSwitch.Interlock = ((iRetData[8] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M17123
                RPD.STRCTURE.TR.Btn.TRTPStartSwitch.Interlock = ((iRetData[8] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                 //M17125
                RPD.STRCTURE.UL.Btn.ULTPStartSwitch.Interlock = ((iRetData[8] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                 //M17127
                RPD.STRCTURE.PCC2.Btn.TSSTPStartSwitch.Interlock = ((iRetData[8] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);              //M17129

                RPD.STRCTURE.LL.Btn.LLMFCO2Switch.Interlock = ((iRetData[8] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                   //M17132
                RPD.STRCTURE.LL.Btn.LLMFCArSwitch.Interlock = ((iRetData[8] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                   //M17133
            
                RPD.STRCTURE.UL.Btn.ULMFCO2Switch.Interlock = ((iRetData[8] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                   //M17134
                RPD.STRCTURE.UL.Btn.ULMFCArSwitch.Interlock = ((iRetData[8] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                   //M17135
                RPD.STRCTURE.TR.Btn.TRMFCAr_GunSwitch.Interlock = ((iRetData[9] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                //M17136
                RPD.STRCTURE.TR.Btn.TRMFCO2Switch.Interlock = ((iRetData[9] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M17137
                RPD.STRCTURE.TR.Btn.TRMFCAr_ChamberSwitch.Interlock = ((iRetData[9] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);            //M17138

                //Add by Henry 2016/1/22
                RPD.STRCTURE.TR.Btn.TRMFCH2OSwitch.Interlock = ((iRetData[9] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                     //M17141

                RPD.STRCTURE.EF.Btn.LDPortCVForwardSwitch.Interlock = ((iRetData[13] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);          //M17201
                RPD.STRCTURE.EF.Btn.LDPortCVReverseSwitch.Interlock = ((iRetData[13] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);         //M17202
                RPD.STRCTURE.EF.Btn.LDPortCVStopSwitch.Interlock = ((iRetData[13] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);            //M17203

                RPD.STRCTURE.LL.Btn.LLCVForwardSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);             //M17251
                RPD.STRCTURE.LL.Btn.LLCVReverseSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);             //M17252
                RPD.STRCTURE.LL.Btn.LLCVStopSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                //M17253
                RPD.STRCTURE.LL.Btn.DV1OpenSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                 //M17254
                RPD.STRCTURE.LL.Btn.DV1CloseSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M17255
                RPD.STRCTURE.LL.Btn.SRV1OpenSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                 //M17256
                RPD.STRCTURE.LL.Btn.SRV1CloseSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                //M17257
                RPD.STRCTURE.LL.Btn.RV1OpenSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                  //M17258
                RPD.STRCTURE.LL.Btn.RV1CloseSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                 //M17259
                RPD.STRCTURE.LL.Btn.LLBPDPRunSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                //M17260
                RPD.STRCTURE.LL.Btn.GV1OpenSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M17261
                RPD.STRCTURE.LL.Btn.GV1CloseSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                 //M17262
                RPD.STRCTURE.LL.Btn.EV1OpenSwitch.Interlock = ((iRetData[16] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                  //M17263
                RPD.STRCTURE.LL.Btn.EV1CloseSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                 //M17264
                RPD.STRCTURE.LL.Btn.VMV1OpenSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                 //M17265
                RPD.STRCTURE.LL.Btn.VV1OpenSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                 //M17266
                RPD.STRCTURE.LL.Btn.MDV1OpenSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M17267
                RPD.STRCTURE.LL.Btn.MDV2OpenSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                //M17268
                RPD.STRCTURE.LL.Btn.MDV3OpenSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                //M17269
                RPD.STRCTURE.LL.Btn.MDV4OpenSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                //M17270
                RPD.STRCTURE.LL.Btn.MDV5OpenSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M17271
                RPD.STRCTURE.LL.Btn.LLTPRunSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                  //M17272                
                RPD.STRCTURE.LL.Btn.LLBPDPStopSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);               //M17273
                RPD.STRCTURE.LL.Btn.VMV1CloseSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                 //M17274
                RPD.STRCTURE.LL.Btn.VV1CloseSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                 //M17275
                RPD.STRCTURE.LL.Btn.MDV1CloseSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                //M17276
                RPD.STRCTURE.LL.Btn.MDV2CloseSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                //M17277
                RPD.STRCTURE.LL.Btn.MDV3CloseSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                //M17278
                RPD.STRCTURE.LL.Btn.MDV4CloseSwitch.Interlock = ((iRetData[17] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M17279
                RPD.STRCTURE.LL.Btn.MDV5CloseSwitch.Interlock = ((iRetData[18] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                //M17280

                //Add by Henry 2016/1/22
                RPD.STRCTURE.LL.Btn.MDV20OpenSwitch.Interlock = ((iRetData[18] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                //M17281
                RPD.STRCTURE.LL.Btn.MDV21OpenSwitch.Interlock = ((iRetData[18] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                //M17282
                RPD.STRCTURE.LL.Btn.MDV20CloseSwitch.Interlock = ((iRetData[18] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M17283
                RPD.STRCTURE.LL.Btn.MDV21CloseSwitch.Interlock = ((iRetData[18] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                //M17284


                RPD.STRCTURE.LL.Btn.LLTPStopSwitch.Interlock = ((iRetData[19] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M17299

                RPD.STRCTURE.TR.Btn.TR1CVForwardSwitch.Interlock = ((iRetData[22] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);            //M17351
                RPD.STRCTURE.TR.Btn.TR1CVReverseSwitch.Interlock = ((iRetData[22] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);             //M17352
                RPD.STRCTURE.TR.Btn.TR1CVStopSwitch.Interlock = ((iRetData[22] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                //M17353
                RPD.STRCTURE.TR.Btn.TR2CVForwardSwitch.Interlock = ((iRetData[22] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);             //M17354
                RPD.STRCTURE.TR.Btn.TR2CVReverseSwitch.Interlock = ((iRetData[22] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);             //M17355
                RPD.STRCTURE.TR.Btn.TR2CVStopSwitch.Interlock = ((iRetData[22] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                //M17356
                RPD.STRCTURE.TR.Btn.TR3CVForwardSwitch.Interlock = ((iRetData[22] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);             //M17357
                RPD.STRCTURE.TR.Btn.TR3CVReverseSwitch.Interlock = ((iRetData[22] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);             //M17358
                RPD.STRCTURE.TR.Btn.TR3CVStopSwitch.Interlock = ((iRetData[22] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M17359
                RPD.STRCTURE.TR.Btn.DV2OpenSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                  //M17360
                RPD.STRCTURE.TR.Btn.DV2CloseSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                 //M17361
                RPD.STRCTURE.TR.Btn.DV3OpenSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                 //M17362
                RPD.STRCTURE.TR.Btn.DV3CloseSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M17363

                RPD.STRCTURE.TR.Btn.RV3OpenSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                 //M17366
                RPD.STRCTURE.TR.Btn.RV3CloseSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M17367
                RPD.STRCTURE.TR.Btn.GV3OpenSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                  //M17368
                RPD.STRCTURE.TR.Btn.GV3CloseSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                 //M17369
                RPD.STRCTURE.TR.Btn.EV3OpenSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                  //M17370
                RPD.STRCTURE.TR.Btn.EV3CloseSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                 //M17371
                RPD.STRCTURE.TR.Btn.VMV3OpenSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                 //M17372
                RPD.STRCTURE.TR.Btn.VV3OpenSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M17373
                RPD.STRCTURE.TR.Btn.MDV11OpenSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                //M17374
                RPD.STRCTURE.TR.Btn.MDV12OpenSwitch.Interlock = ((iRetData[23] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M17375
                RPD.STRCTURE.TR.Btn.MDV13OpenSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                //M17376
                RPD.STRCTURE.TR.Btn.MDV14OpenSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                //M17377
                RPD.STRCTURE.TR.Btn.MDV15OpenSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);               //M17378
                RPD.STRCTURE.TR.Btn.MDV16OpenSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);               //M17379
                RPD.STRCTURE.TR.Btn.MDV17OpenSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);               //M17380
                RPD.STRCTURE.TR.Btn.MDV18OpenSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M17381
                RPD.STRCTURE.TR.Btn.MDV19OpenSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);               //M17382
                RPD.STRCTURE.TR.Btn.TRTPRunSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                 //M17383
                RPD.STRCTURE.TR.Btn.DPRunSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                    //M17384
                RPD.STRCTURE.TR.Btn.DPStopSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                   //M17385
                RPD.STRCTURE.TR.Btn.VMV3CloseSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                //M17386
                RPD.STRCTURE.TR.Btn.VV3CloseSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                 //M17387
                RPD.STRCTURE.TR.Btn.MDV11CloseSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);               //M17388
                RPD.STRCTURE.TR.Btn.MDV12CloseSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);               //M17389
                RPD.STRCTURE.TR.Btn.MDV13CloseSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);               //M17390
                RPD.STRCTURE.TR.Btn.MDV14CloseSwitch.Interlock = ((iRetData[24] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);               //M17391
                RPD.STRCTURE.TR.Btn.MDV15CloseSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);               //M17392
                RPD.STRCTURE.TR.Btn.MDV16CloseSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);               //M17393
                RPD.STRCTURE.TR.Btn.MDV17CloseSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);              //M17394
                RPD.STRCTURE.TR.Btn.MDV18CloseSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);              //M17395
                RPD.STRCTURE.TR.Btn.MDV19CloseSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);              //M17396

                RPD.STRCTURE.TR.Btn.TRTPStopSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M17399

                //Add by Henry 2016/1/22
                RPD.STRCTURE.TR.Btn.MDV24OpenSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                //M17400
                RPD.STRCTURE.TR.Btn.MDV25OpenSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                //M17401
                RPD.STRCTURE.TR.Btn.MDV24CloseSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);              //M17402
                RPD.STRCTURE.TR.Btn.MDV25CloseSwitch.Interlock = ((iRetData[25] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);              //M17403


                RPD.STRCTURE.UL.Btn.ULCVForwardSwitch.Interlock = ((iRetData[28] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);              //M17451
                RPD.STRCTURE.UL.Btn.ULCVReverseSwitch.Interlock = ((iRetData[28] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);              //M17452
                RPD.STRCTURE.UL.Btn.ULCVStopSwitch.Interlock = ((iRetData[28] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                 //M17453
                RPD.STRCTURE.UL.Btn.DV4OpenSwitch.Interlock = ((iRetData[28] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                  //M17454
                RPD.STRCTURE.UL.Btn.DV4CloseSwitch.Interlock = ((iRetData[28] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                 //M17455
                RPD.STRCTURE.UL.Btn.SRV2OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                 //M17456
                RPD.STRCTURE.UL.Btn.SRV2CloseSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                //M17457
                RPD.STRCTURE.UL.Btn.RV2OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                 //M17458
                RPD.STRCTURE.UL.Btn.RV2CloseSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M17459
                RPD.STRCTURE.UL.Btn.ULBPDPRunSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);               //M17460
                RPD.STRCTURE.UL.Btn.GV2OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                 //M17461
                RPD.STRCTURE.UL.Btn.GV2CloseSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                //M17462
                RPD.STRCTURE.UL.Btn.EV2OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                 //M17463
                RPD.STRCTURE.UL.Btn.EV2CloseSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                 //M17464
                RPD.STRCTURE.UL.Btn.VMV2OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                 //M17465
                RPD.STRCTURE.UL.Btn.VV2OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                  //M17466
                RPD.STRCTURE.UL.Btn.MDV6OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                 //M17467
                RPD.STRCTURE.UL.Btn.MDV7OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                 //M17468
                RPD.STRCTURE.UL.Btn.MDV8OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                 //M17469
                RPD.STRCTURE.UL.Btn.MDV9OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                 //M17470
                RPD.STRCTURE.UL.Btn.MDV10OpenSwitch.Interlock = ((iRetData[29] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M17471
                RPD.STRCTURE.UL.Btn.ULTPRunSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                  //M17472
                RPD.STRCTURE.UL.Btn.ULBPDPStopSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);               //M17473
                RPD.STRCTURE.UL.Btn.VMV2CloseSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);               //M17474
                RPD.STRCTURE.UL.Btn.VV2CloseSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M17475
                RPD.STRCTURE.UL.Btn.MDV6CloseSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);               //M17476
                RPD.STRCTURE.UL.Btn.MDV7CloseSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M17477
                RPD.STRCTURE.UL.Btn.MDV8CloseSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);               //M17478
                RPD.STRCTURE.UL.Btn.MDV9CloseSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);               //M17479
                RPD.STRCTURE.UL.Btn.MDV10CloseSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);               //M17480

                //Add by Henry 2016/1/22
                RPD.STRCTURE.UL.Btn.MDV22OpenSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);               //M17481
                RPD.STRCTURE.UL.Btn.MDV23OpenSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);              //M17482
                RPD.STRCTURE.UL.Btn.MDV22CloseSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);             //M17483
                RPD.STRCTURE.UL.Btn.MDV23CloseSwitch.Interlock = ((iRetData[30] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);             //M17484


                RPD.STRCTURE.UL.Btn.ULTPStopSwitch.Interlock = ((iRetData[31] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                //M17499

                RPD.STRCTURE.DF.Btn.UNLDPortCVForwardSwitch.Interlock = ((iRetData[31] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);               //M17501
                RPD.STRCTURE.DF.Btn.UNLDPortCVReverseSwitch.Interlock = ((iRetData[31] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);               //M17502
                RPD.STRCTURE.DF.Btn.UNLDPortCVStopSwitch.Interlock = ((iRetData[31] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                  //M17503

                RPD.STRCTURE.PCC2.Btn.RV4OpenSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                //M17556
                RPD.STRCTURE.PCC2.Btn.RV4CloseSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M17557
                RPD.STRCTURE.PCC2.Btn.TSSBPDPRunSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);             //M17558
                RPD.STRCTURE.PCC2.Btn.GV6OpenSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M17559
                RPD.STRCTURE.PCC2.Btn.GV6CloseSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                //M17560
                RPD.STRCTURE.PCC2.Btn.EV4OpenSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                 //M17561
                RPD.STRCTURE.PCC2.Btn.EV4CloseSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                //M17562
                RPD.STRCTURE.PCC2.Btn.VMV4OpenSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                //M17563
                RPD.STRCTURE.PCC2.Btn.VV4OpenSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                 //M17564
                RPD.STRCTURE.PCC2.Btn.TSSTPRunSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                //M17565
                RPD.STRCTURE.PCC2.Btn.ULBPDPStopSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);              //M17566
                RPD.STRCTURE.PCC2.Btn.VMV4CloseSwitch.Interlock = ((iRetData[35] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);               //M17567
                RPD.STRCTURE.PCC2.Btn.VV4CloseSwitch.Interlock = ((iRetData[36] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                //M17568
                RPD.STRCTURE.PCC2.Btn.TSSBPDPStopSwitch.Interlock = ((iRetData[36] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);             //M17569

                RPD.STRCTURE.PCC2.Btn.TSSTPStopSwitch.Interlock = ((iRetData[37] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M17599

                RPD.STRCTURE.SYS.Btn.IOForceOnOffModeSwitch.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);        //M17700
                RPD.STRCTURE.SYS.DO.EntryFeederStartReady.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);           //M17701
                RPD.STRCTURE.SYS.DO.DV1Open.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                        //M17702
                RPD.STRCTURE.SYS.DO.DV1Close.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                       //M17703
                RPD.STRCTURE.SYS.DO.DV2Open.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                         //M17704
                RPD.STRCTURE.SYS.DO.DV2Close.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                        //M17705
                RPD.STRCTURE.SYS.DO.SPARE0.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                          //M17706
                RPD.STRCTURE.SYS.DO.LLHVBRV1FastPumpValveOpen.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);       //M17707
                RPD.STRCTURE.SYS.DO.LLHVBRV1FastPumpValveClose.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);      //M17708
                RPD.STRCTURE.SYS.DO.LLHVBSRV1SlowPumpValveOpen.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);      //M17709
                RPD.STRCTURE.SYS.DO.LLHVBSRV1SlowPumpValveClose.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);     //M17710
                RPD.STRCTURE.SYS.DO.LLHVBDryBoostPumpStart.Interlock = ((iRetData[44] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);          //M17711
                RPD.STRCTURE.SYS.DO.SignalTower_G.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                   //M17712
                RPD.STRCTURE.SYS.DO.SignalTower_Y.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                   //M17713
                RPD.STRCTURE.SYS.DO.SignalTower_R.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                  //M17714
                RPD.STRCTURE.SYS.DO.SignalTower_B.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                  //M17715
                RPD.STRCTURE.SYS.DO.Signal_Buzzer.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                  //M17716

                RPD.STRCTURE.SYS.DO.LLHVBVV1VentOpen.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M17717
                RPD.STRCTURE.SYS.DO.MDV1O2MixArOpen.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                //M17718
                RPD.STRCTURE.SYS.DO.MDV2O2OutOpen.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                  //M17719
                RPD.STRCTURE.SYS.DO.MDV3O2InOpen.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                    //M17720
                RPD.STRCTURE.SYS.DO.MDV4ArOutOpen.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                   //M17721
                RPD.STRCTURE.SYS.DO.MDV5ArInOpen.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                    //M17722
                RPD.STRCTURE.SYS.DO.LLHVBVMV1Open.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                   //M17723
                RPD.STRCTURE.SYS.DO.SPARE3.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                          //M17724
                RPD.STRCTURE.SYS.DO.SPARE4.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                          //M17725
                RPD.STRCTURE.SYS.DO.SPARE5.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                          //M17726
                RPD.STRCTURE.SYS.DO.SPARE6.Interlock = ((iRetData[45] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                          //M17727
                RPD.STRCTURE.SYS.DO.SPARE7.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                          //M17728
                RPD.STRCTURE.SYS.DO.SPARE8.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                          //M17729
                RPD.STRCTURE.SYS.DO.SPARE9.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                         //M17730
                RPD.STRCTURE.SYS.DO.SPARE10.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                        //M17731
                RPD.STRCTURE.SYS.DO.SPARE11.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                        //M17732

                RPD.STRCTURE.SYS.DO.SPARE26.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                        //M17733
                RPD.STRCTURE.SYS.DO.LLHVBGV1Open.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                   //M17734
                RPD.STRCTURE.SYS.DO.LLHVBGV1Close.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                  //M17735
                RPD.STRCTURE.SYS.DO.LLHVBTurboPumpStart.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);             //M17736
                RPD.STRCTURE.SYS.DO.LLHVBEV1Open.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                    //M17737
                RPD.STRCTURE.SYS.DO.LLHVBEV1Close.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                   //M17738
                RPD.STRCTURE.SYS.DO.TRRV3VentValveOpen.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);              //M17739
                RPD.STRCTURE.SYS.DO.TRRV3VentValveClose.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);             //M17740
                RPD.STRCTURE.SYS.DO.BackingDryBoostPumpStart.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);        //M17741
                RPD.STRCTURE.SYS.DO.PCCGV3Open.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                      //M17742
                RPD.STRCTURE.SYS.DO.PCCGV3Close.Interlock = ((iRetData[46] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                     //M17743
                RPD.STRCTURE.SYS.DO.PCCTurboPumpStart.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);               //M17744
                RPD.STRCTURE.SYS.DO.PCCEV2Open.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                      //M17745
                RPD.STRCTURE.SYS.DO.PCCEV2Close.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                    //M17746
                RPD.STRCTURE.SYS.DO.DV3Open.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                        //M17747
                RPD.STRCTURE.SYS.DO.DV3Close.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                       //M17748

                RPD.STRCTURE.SYS.DO.SPARE28.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                        //M17749
                RPD.STRCTURE.SYS.DO.RGAIsolationValveOpen.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);          //M17750
                RPD.STRCTURE.SYS.DO.TRVV3VentOpen.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                  //M17751
                RPD.STRCTURE.SYS.DO.TRVMV3Open.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                      //M17752
                RPD.STRCTURE.SYS.DO.MDV17ArOutOpen.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                  //M17753
                RPD.STRCTURE.SYS.DO.MDV14O2OutOpen.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                  //M17754
                RPD.STRCTURE.SYS.DO.MDV11ArOutOpen.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                  //M17755
                RPD.STRCTURE.SYS.DO.MDV18ArOutOpen.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                  //M17756
                RPD.STRCTURE.SYS.DO.MDV15O2OutOpen.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M17757
                RPD.STRCTURE.SYS.DO.MDV12ArOutOpen.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                  //M17758
                RPD.STRCTURE.SYS.DO.MDV19ArInOpen.Interlock = ((iRetData[47] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                   //M17759
                RPD.STRCTURE.SYS.DO.MDV16O2InOpen.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                   //M17760
                RPD.STRCTURE.SYS.DO.MDV13ArInOpen.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                   //M17761
                RPD.STRCTURE.SYS.DO.SPARE29.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                        //M17762
                RPD.STRCTURE.SYS.DO.SPARE30.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                        //M17763
                RPD.STRCTURE.SYS.DO.SPARE31.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                        //M17764

                RPD.STRCTURE.SYS.DO.RPDSourceSupplyGV6Open.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);          //M17765
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyGV6Close.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);         //M17766
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyTurboPumpStart.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);   //M17767
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyEV4Open.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);           //M17768
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyEV4Close.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);          //M17769
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyOilPumpStart.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);      //M17770
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyRV4Open.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);           //M17771
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyRV4Close.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);          //M17772
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyUpperGateOpenTS1.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);  //M17773
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyUpperGateCloseTS1.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1); //M17774
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyLowerGateOpenGV4.Interlock = ((iRetData[48] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);  //M17775
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyLowerGateCloseGV4.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1); //M17776
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyRotaryFWDPB1.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);      //M17777
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyRotaryBWDPB1.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);     //M17778
                RPD.STRCTURE.SYS.DO.SPARE1.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                          //M17779
                RPD.STRCTURE.SYS.DO.SPARE2.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                          //M17780

                RPD.STRCTURE.SYS.DO.RPDSourceSupplyVentOpenVV4.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);        //M17781
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyFASTVentOpenVMV4.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);   //M17782
                RPD.STRCTURE.SYS.DO.SPARE12.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                           //M17783
                RPD.STRCTURE.SYS.DO.SPARE13.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                            //M17784
                RPD.STRCTURE.SYS.DO.SPARE14.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                            //M17785
                RPD.STRCTURE.SYS.DO.SPARE15.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                            //M17786
                RPD.STRCTURE.SYS.DO.SPARE16.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                            //M17787
                RPD.STRCTURE.SYS.DO.SPARE17.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                            //M17788
                RPD.STRCTURE.SYS.DO.SPARE18.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                            //M17789
                RPD.STRCTURE.SYS.DO.SPARE19.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                            //M17790
                RPD.STRCTURE.SYS.DO.SPARE20.Interlock = ((iRetData[49] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                            //M17791
                RPD.STRCTURE.SYS.DO.SPARE21.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                            //M17792
                RPD.STRCTURE.SYS.DO.SPARE22.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                            //M17793
                RPD.STRCTURE.SYS.DO.SPARE23.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                           //M17794
                RPD.STRCTURE.SYS.DO.SPARE24.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                           //M17795
                RPD.STRCTURE.SYS.DO.SPARE25.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                           //M17796

                RPD.STRCTURE.SYS.DO.ULHVBTurboPumpStart.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M17797
                RPD.STRCTURE.SYS.DO.SPARE27.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                           //M17798
                RPD.STRCTURE.SYS.DO.DV4Open.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                           //M17799
                RPD.STRCTURE.SYS.DO.DV4Close.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                           //M17800
                RPD.STRCTURE.SYS.DO.OutletFeederTakeOutReady.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);           //M17801
                RPD.STRCTURE.SYS.DO.ULHVBSRV2SlowPumpValveOpen.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);         //M17802
                RPD.STRCTURE.SYS.DO.ULHVBSRV2SlowPumpValveClose.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);        //M17803
                RPD.STRCTURE.SYS.DO.ULHVBRV2FastPumpValveOpen.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);          //M17804
                RPD.STRCTURE.SYS.DO.ULHVBRV2FastPumpValveClose.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);         //M17805
                RPD.STRCTURE.SYS.DO.MainWaterInletValve.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                //M17806
                RPD.STRCTURE.SYS.DO.MainWaterOutletValve.Interlock = ((iRetData[50] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);               //M17807
                RPD.STRCTURE.SYS.DO.ULHVBDryBoostPumpStart.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);             //M17808
                RPD.STRCTURE.SYS.DO.ULHVBEV3Open.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                       //M17809
                RPD.STRCTURE.SYS.DO.ULHVBEV3Close.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                     //M17810
                RPD.STRCTURE.SYS.DO.ULHVBGV2Open.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                      //M17811
                RPD.STRCTURE.SYS.DO.ULHVBGV2Close.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                     //M17812

                RPD.STRCTURE.SYS.DO.MDV10ArOutOpen.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                    //M17813
                RPD.STRCTURE.SYS.DO.MDV6O2MixArOpen.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                   //M17814
                RPD.STRCTURE.SYS.DO.MDV7O2OutOpen.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                     //M17815
                RPD.STRCTURE.SYS.DO.MDV9ArOutOpen.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                      //M17816
                RPD.STRCTURE.SYS.DO.MDV8O2InOpen.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                       //M17817
                RPD.STRCTURE.SYS.DO.ULHVBVV2VentOpen.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                   //M17818
                RPD.STRCTURE.SYS.DO.ULHVBVMV2Open.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                      //M17819
                RPD.STRCTURE.SYS.DO.TRCoolingWater1In.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                  //M17820
                RPD.STRCTURE.SYS.DO.TRCoolingWater2In.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M17821
                RPD.STRCTURE.SYS.DO.TRCoolingWater3In.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                  //M17822
                RPD.STRCTURE.SYS.DO.SPARE32.Interlock = ((iRetData[51] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                            //M17823
                RPD.STRCTURE.SYS.DO.SPARE33.Interlock = ((iRetData[52] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                            //M17824
                RPD.STRCTURE.SYS.DO.SPARE34.Interlock = ((iRetData[52] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                            //M17825
                RPD.STRCTURE.SYS.DO.SPARE35.Interlock = ((iRetData[52] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                           //M17826
                RPD.STRCTURE.SYS.DO.SPARE36.Interlock = ((iRetData[52] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                           //M17827
                RPD.STRCTURE.SYS.DO.SPARE37.Interlock = ((iRetData[52] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                           //M17828

                RPD.STRCTURE.TR.Btn.W_1_Heater_Open.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M17009
                RPD.STRCTURE.TR.Btn.W_1_Heater_Close.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M17009
                RPD.STRCTURE.TR.Btn.W_2_Heater_Open.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M17009
                RPD.STRCTURE.TR.Btn.W_2_Heater_Close.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M17009
                RPD.STRCTURE.TR.Btn.W_3_Heater_Open.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M17009
                RPD.STRCTURE.TR.Btn.W_3_Heater_Close.Interlock = ((iRetData[1] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M17009
            }
            else
            {
                _bConn = false;
            }

        }
        public void ReadPLC_E2G_Bit_Lamp()
        {
            int[] iRetData = new int[125];   //M18992 後開始 2000個位址
            bool bRtn;
            bRtn = FunBatchReadPLC("M18992", ref iRetData); //M18992
            if (bRtn == true)
            {
                RPD.STRCTURE.SYS.Data.IO_TABLE = iRetData;
                RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp = ((iRetData[0] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                 //M19001
                RPD.STRCTURE.SYS.Btn.ManualModeSwitch.Lamp = ((iRetData[0] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);               //M19002
                RPD.STRCTURE.SYS.Btn.DummyModeSwitch.Lamp = ((iRetData[0] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                //M19003
                RPD.STRCTURE.SYS.Btn.StartSwitch.Lamp = ((iRetData[0] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                    //M19004
                RPD.STRCTURE.SYS.Btn.StopSwitch.Lamp = ((iRetData[0] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                     //M19005
                RPD.STRCTURE.SYS.Btn.ContinueSwitch.Lamp = ((iRetData[0] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                 //M19006
                RPD.STRCTURE.SYS.Btn.PauseSwitch.Lamp = ((iRetData[0] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                    //M19007
                RPD.STRCTURE.SYS.Btn.InitialSwitch.Lamp = ((iRetData[1] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                  //M19008
                RPD.STRCTURE.SYS.Btn.AbortSwitch.Lamp = ((iRetData[1] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M19009
                RPD.STRCTURE.SYS.Btn.AlarmResetSwitch.Lamp = ((iRetData[1] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);              //M19010
                RPD.STRCTURE.SYS.Btn.BuzzerOffSwitch.Lamp = ((iRetData[1] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);               //M19011
                RPD.STRCTURE.SYS.Btn.TactTimeReaetSwitch.Lamp = ((iRetData[1] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);           //M19012

                RPD.STRCTURE.SYS.Btn.TrayCountResetSwitch.Lamp = ((iRetData[1] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);           //M19016
                RPD.STRCTURE.SYS.Btn.ForcedExcludeSwitch.Lamp = ((iRetData[1] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);            //M19017
                RPD.STRCTURE.SYS.Btn.LeakTestSwitchTR.Lamp = ((iRetData[1] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);               //M19018
                RPD.STRCTURE.SYS.Map.LeakTestStartTR = ((iRetData[1] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                     //M19019
                RPD.STRCTURE.SYS.Map.LeakTestFinishTR = ((iRetData[1] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                    //M19020

                //add by sophia 20160822
                RPD.STRCTURE.TR.Btn.TRMFCTestSwitch.Lamp = ((iRetData[2] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                //M19029

                RPD.STRCTURE.SYS.Btn.StartCodeInputKeySwitch.Lamp = ((iRetData[2] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);       //M19030
                RPD.STRCTURE.SYS.Map.StartCodeError = ((iRetData[2] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                     //M19031
                RPD.STRCTURE.SYS.Map.StartCodeFailure = ((iRetData[2] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                    //M19032

                //add by william
                RPD.STRCTURE.SYS.Btn.ALLLineInit.Lamp = ((iRetData[3] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);       //M19040

                RPD.STRCTURE.SYS.Btn.TrayDeleteSwitch.Lamp = ((iRetData[3] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);               //M19051
                RPD.STRCTURE.SYS.Btn.TrayDataModifySwitch.Lamp = ((iRetData[3] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);           //M19052
                RPD.STRCTURE.SYS.Btn.MachineDataSaveSwitch.Lamp = ((iRetData[3] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);          //M19053
                RPD.STRCTURE.SYS.Btn.OutputForceOnOffSwitch.Lamp = ((iRetData[3] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);         //M19054
                RPD.STRCTURE.SYS.Btn.CurrentRecipeChangeSwitch.Lamp = ((iRetData[3] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);      //M19055

                RPD.STRCTURE.PCC1.Btn.PlasmaStart.Lamp = ((iRetData[4] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                    //M19060
                RPD.STRCTURE.PCC1.Btn.PlasmaStop.Lamp = ((iRetData[4] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                     //M19061
                RPD.STRCTURE.PCC1.Btn.MainDischargeChange.Lamp = ((iRetData[4] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);            //M19062
                RPD.STRCTURE.PCC1.Btn.BeamGuideDischargeChange.Lamp = ((iRetData[4] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);       //M19063
                RPD.STRCTURE.PCC1.Btn.ResistanceMeasurementOperation.Lamp = ((iRetData[4] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1); //M19064

                #region Menu Mode Btns
                RPD.STRCTURE.SYS.Btn.ALLPumpDownSwitch.Lamp = ((iRetData[6] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);              //M19101
                RPD.STRCTURE.LL.Btn.LLPumpDownSwitch.Lamp = ((iRetData[6] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M19103
                RPD.STRCTURE.TR.Btn.TRPumpDownSwitch.Lamp = ((iRetData[7] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                //M19105
                RPD.STRCTURE.UL.Btn.ULPumpDownSwitch.Lamp = ((iRetData[7] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);               //M19107
                RPD.STRCTURE.PCC2.Btn.TSSPumpDownSwitch.Lamp = ((iRetData[7] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);            //M19109
                RPD.STRCTURE.SYS.Btn.ALLVentSwitch.Lamp = ((iRetData[7] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                 //M19111
                RPD.STRCTURE.LL.Btn.LLVentSwitch.Lamp = ((iRetData[7] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                    //M19113
                RPD.STRCTURE.TR.Btn.TRVentSwitch.Lamp = ((iRetData[7] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                    //M19115
                RPD.STRCTURE.UL.Btn.ULVentSwitch.Lamp = ((iRetData[7] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                    //M19117
                RPD.STRCTURE.PCC2.Btn.TSSVentSwitch.Lamp = ((iRetData[7] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                 //M19119
                RPD.STRCTURE.SYS.Btn.ALLTPStartSwitch.Lamp = ((iRetData[8] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);               //M19121
                RPD.STRCTURE.LL.Btn.LLTPStartSwitch.Lamp = ((iRetData[8] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M19123
                RPD.STRCTURE.TR.Btn.TRTPStartSwitch.Lamp = ((iRetData[8] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                //M19125
                RPD.STRCTURE.UL.Btn.ULTPStartSwitch.Lamp = ((iRetData[8] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M19127
                RPD.STRCTURE.PCC2.Btn.TSSTPStartSwitch.Lamp = ((iRetData[8] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);              //M19129
                #endregion

                RPD.STRCTURE.LL.Btn.LLMFCO2Switch.Lamp = ((iRetData[8] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                   //M19132
                RPD.STRCTURE.LL.Btn.LLMFCArSwitch.Lamp = ((iRetData[8] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                   //M19133
                RPD.STRCTURE.UL.Btn.ULMFCO2Switch.Lamp = ((iRetData[8] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                   //M19134
                RPD.STRCTURE.UL.Btn.ULMFCArSwitch.Lamp = ((iRetData[8] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                   //M19135
                RPD.STRCTURE.TR.Btn.TRMFCAr_GunSwitch.Lamp = ((iRetData[9] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                   //M19136
                RPD.STRCTURE.TR.Btn.TRMFCO2Switch.Lamp = ((iRetData[9] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M19137
                RPD.STRCTURE.TR.Btn.TRMFCAr_ChamberSwitch.Lamp = ((iRetData[9] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                   //M19138

                RPD.STRCTURE.TR.Btn.TRMFCH2OSwitch.Lamp = ((iRetData[9] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                   //M19141

                RPD.STRCTURE.EF.Btn.LDPortCVForwardSwitch.Lamp = ((iRetData[13] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);          //M19201
                RPD.STRCTURE.EF.Btn.LDPortCVReverseSwitch.Lamp = ((iRetData[13] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);         //M19202
                RPD.STRCTURE.EF.Btn.LDPortCVStopSwitch.Lamp = ((iRetData[13] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);            //M19203

                RPD.STRCTURE.LL.Btn.LLCVForwardSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);             //M19251
                RPD.STRCTURE.LL.Btn.LLCVReverseSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);             //M19252
                RPD.STRCTURE.LL.Btn.LLCVStopSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                //M19253
                RPD.STRCTURE.LL.Btn.DV1OpenSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                 //M19254
                RPD.STRCTURE.LL.Btn.DV1CloseSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M19255
                RPD.STRCTURE.LL.Btn.SRV1OpenSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                 //M19256
                RPD.STRCTURE.LL.Btn.SRV1CloseSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                //M19257
                RPD.STRCTURE.LL.Btn.RV1OpenSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                  //M19258
                RPD.STRCTURE.LL.Btn.RV1CloseSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                 //M19259
                RPD.STRCTURE.LL.Btn.LLBPDPRunSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                //M19260
                RPD.STRCTURE.LL.Btn.GV1OpenSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M19261
                RPD.STRCTURE.LL.Btn.GV1CloseSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                 //M19262
                RPD.STRCTURE.LL.Btn.EV1OpenSwitch.Lamp = ((iRetData[16] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                  //M19263
                RPD.STRCTURE.LL.Btn.EV1CloseSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                 //M19264
                RPD.STRCTURE.LL.Btn.VMV1OpenSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                 //M19265
                RPD.STRCTURE.LL.Btn.VV1OpenSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                 //M19266
                RPD.STRCTURE.LL.Btn.MDV1OpenSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M19267
                RPD.STRCTURE.LL.Btn.MDV2OpenSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                //M19268
                RPD.STRCTURE.LL.Btn.MDV3OpenSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                //M19269
                RPD.STRCTURE.LL.Btn.MDV4OpenSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                //M19270
                RPD.STRCTURE.LL.Btn.MDV5OpenSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M19271
                RPD.STRCTURE.LL.Btn.LLTPRunSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                  //M19272               
                RPD.STRCTURE.LL.Btn.LLBPDPStopSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);               //M19273       

                //Add by Henry 2016/1/22
                RPD.STRCTURE.LL.Btn.MDV20OpenSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);               //M19274
                RPD.STRCTURE.LL.Btn.MDV21OpenSwitch.Lamp = ((iRetData[17] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);               //M19275


                //Add by Albert 201905
                RPD.STRCTURE.TR.Btn.W_1_Heater_Open.Lamp = ((iRetData[20] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                             //M19318
                RPD.STRCTURE.TR.Btn.W_1_Heater_Close.Lamp = RPD.STRCTURE.TR.Btn.W_1_Heater_Open.Lamp;

                RPD.STRCTURE.TR.Btn.W_2_Heater_Open.Lamp = ((iRetData[20] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                            //M19320
                RPD.STRCTURE.TR.Btn.W_2_Heater_Close.Lamp = RPD.STRCTURE.TR.Btn.W_2_Heater_Open.Lamp;

                RPD.STRCTURE.TR.Btn.W_3_Heater_Open.Lamp = ((iRetData[20] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                           //M19322
                RPD.STRCTURE.TR.Btn.W_3_Heater_Close.Lamp = RPD.STRCTURE.TR.Btn.W_3_Heater_Open.Lamp;

                RPD.STRCTURE.TR.Btn.TR1CVForwardSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);            //M19351
                RPD.STRCTURE.TR.Btn.TR1CVReverseSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);             //M19352
                RPD.STRCTURE.TR.Btn.TR1CVStopSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                //M19353
                RPD.STRCTURE.TR.Btn.TR2CVForwardSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);             //M19354
                RPD.STRCTURE.TR.Btn.TR2CVReverseSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);             //M19355
                RPD.STRCTURE.TR.Btn.TR2CVStopSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                //M19356
                RPD.STRCTURE.TR.Btn.TR3CVForwardSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);             //M19357
                RPD.STRCTURE.TR.Btn.TR3CVReverseSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);             //M19358
                RPD.STRCTURE.TR.Btn.TR3CVStopSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M19359
                RPD.STRCTURE.TR.Btn.DV2OpenSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                  //M19360
                RPD.STRCTURE.TR.Btn.DV2CloseSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                 //M19361
                RPD.STRCTURE.TR.Btn.DV3OpenSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                 //M19362
                RPD.STRCTURE.TR.Btn.DV3CloseSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M19363

                RPD.STRCTURE.TR.Btn.RV3OpenSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                 //M19366
                RPD.STRCTURE.TR.Btn.RV3CloseSwitch.Lamp = ((iRetData[22] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M19367
                RPD.STRCTURE.TR.Btn.GV3OpenSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                  //M19368
                RPD.STRCTURE.TR.Btn.GV3CloseSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                 //M19369
                RPD.STRCTURE.TR.Btn.EV3OpenSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                  //M19370
                RPD.STRCTURE.TR.Btn.EV3CloseSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                 //M19371
                RPD.STRCTURE.TR.Btn.VMV3OpenSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                 //M19372
                RPD.STRCTURE.TR.Btn.VV3OpenSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M19373
                RPD.STRCTURE.TR.Btn.MDV11OpenSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                //M19374
                RPD.STRCTURE.TR.Btn.MDV12OpenSwitch.Lamp = ((iRetData[23] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M19375
                RPD.STRCTURE.TR.Btn.MDV13OpenSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                //M19376
                RPD.STRCTURE.TR.Btn.MDV14OpenSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                //M19377
                RPD.STRCTURE.TR.Btn.MDV15OpenSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);               //M19378
                RPD.STRCTURE.TR.Btn.MDV16OpenSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);               //M19379
                RPD.STRCTURE.TR.Btn.MDV17OpenSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);               //M19380
                RPD.STRCTURE.TR.Btn.MDV18OpenSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M19381
                RPD.STRCTURE.TR.Btn.MDV19OpenSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);               //M19382
                RPD.STRCTURE.TR.Btn.TRTPRunSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                 //M19383
                RPD.STRCTURE.TR.Btn.DPRunSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                    //M19384
                RPD.STRCTURE.TR.Btn.DPStopSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                   //M19385

                //Add by Henry 2016/1/22
                RPD.STRCTURE.TR.Btn.MDV24OpenSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);               //M19388
                RPD.STRCTURE.TR.Btn.MDV25OpenSwitch.Lamp = ((iRetData[24] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);               //M19389

                RPD.STRCTURE.UL.Btn.ULCVForwardSwitch.Lamp = ((iRetData[28] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);              //M19451
                RPD.STRCTURE.UL.Btn.ULCVReverseSwitch.Lamp = ((iRetData[28] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);              //M19452
                RPD.STRCTURE.UL.Btn.ULCVStopSwitch.Lamp = ((iRetData[28] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                 //M19453
                RPD.STRCTURE.UL.Btn.DV4OpenSwitch.Lamp = ((iRetData[28] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                  //M19454
                RPD.STRCTURE.UL.Btn.DV4CloseSwitch.Lamp = ((iRetData[28] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                 //M19455
                RPD.STRCTURE.UL.Btn.SRV2OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                 //M19456
                RPD.STRCTURE.UL.Btn.SRV2CloseSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                //M19457
                RPD.STRCTURE.UL.Btn.RV2OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                 //M19458
                RPD.STRCTURE.UL.Btn.RV2CloseSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M19459
                RPD.STRCTURE.UL.Btn.ULBPDPRunSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);               //M19460
                RPD.STRCTURE.UL.Btn.GV2OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                 //M19461
                RPD.STRCTURE.UL.Btn.GV2CloseSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                //M19462
                RPD.STRCTURE.UL.Btn.EV2OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                 //M19463
                RPD.STRCTURE.UL.Btn.EV2CloseSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                 //M19464
                RPD.STRCTURE.UL.Btn.VMV2OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                 //M19465
                RPD.STRCTURE.UL.Btn.VV2OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                  //M19466
                RPD.STRCTURE.UL.Btn.MDV6OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                 //M19467
                RPD.STRCTURE.UL.Btn.MDV7OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                 //M19468
                RPD.STRCTURE.UL.Btn.MDV8OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                 //M19469
                RPD.STRCTURE.UL.Btn.MDV9OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                 //M19470
                RPD.STRCTURE.UL.Btn.MDV10OpenSwitch.Lamp = ((iRetData[29] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                //M19471
                RPD.STRCTURE.UL.Btn.ULTPRunSwitch.Lamp = ((iRetData[30] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                  //M19472
                RPD.STRCTURE.UL.Btn.ULBPDPStopSwitch.Lamp = ((iRetData[30] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);               //M19473

                //Add by Henry 2016/1/22
                RPD.STRCTURE.UL.Btn.MDV22OpenSwitch.Lamp = ((iRetData[30] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);               //M19481
                RPD.STRCTURE.UL.Btn.MDV23OpenSwitch.Lamp = ((iRetData[30] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);              //M19482


                RPD.STRCTURE.DF.Btn.UNLDPortCVForwardSwitch.Lamp = ((iRetData[31] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);        //M19501
                RPD.STRCTURE.DF.Btn.UNLDPortCVReverseSwitch.Lamp = ((iRetData[31] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);        //M19502
                RPD.STRCTURE.DF.Btn.UNLDPortCVStopSwitch.Lamp = ((iRetData[31] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);           //M19503

                RPD.STRCTURE.PCC2.Btn.RV4OpenSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                //M19556
                RPD.STRCTURE.PCC2.Btn.RV4CloseSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M19557
                RPD.STRCTURE.PCC2.Btn.TSSBPDPRunSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);             //M19558
                RPD.STRCTURE.PCC2.Btn.GV6OpenSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                //M19559
                RPD.STRCTURE.PCC2.Btn.GV6CloseSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                //M19560
                RPD.STRCTURE.PCC2.Btn.EV4OpenSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                 //M19561
                RPD.STRCTURE.PCC2.Btn.EV4CloseSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                //M19562
                RPD.STRCTURE.PCC2.Btn.VMV4OpenSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                //M19563
                RPD.STRCTURE.PCC2.Btn.VV4OpenSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                 //M19564
                RPD.STRCTURE.PCC2.Btn.TSSTPRunSwitch.Lamp = ((iRetData[35] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                //M19565

                RPD.STRCTURE.PCC2.Btn.VV4CloseSwitch.Lamp = ((iRetData[36] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                //M19568

                RPD.STRCTURE.PCC1.Map.TS1OpenLamp = ((iRetData[36] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                       //M19569
                RPD.STRCTURE.PCC1.Map.TS1CloseLamp = ((iRetData[36] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                      //M19570
                RPD.STRCTURE.PCC2.Btn.GV4OpenSwitch.Lamp = ((iRetData[36] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                //M19571
                RPD.STRCTURE.PCC2.Btn.GV4CloseSwitch.Lamp = ((iRetData[36] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);               //M19572
                RPD.STRCTURE.PCC1.Map.PB1OpenLamp = ((iRetData[36] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                       //M19573
                RPD.STRCTURE.PCC1.Map.PB1CloseLamp = ((iRetData[36] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                      //M19574
                RPD.STRCTURE.PCC2.Btn.TSSBPDPStopSwitch.Lamp = ((iRetData[36] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);            //M19575

                //DI & DO
                RPD.STRCTURE.SYS.Btn.IOForceOnOffModeSwitch.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);        //M19700
                RPD.STRCTURE.SYS.DO.EntryFeederStartReady.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);          //M19701
                RPD.STRCTURE.SYS.DO.DV1Open.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                        //M19702
                RPD.STRCTURE.SYS.DO.DV1Close.Lamp = ((iRetData[43] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                       //M19703
                RPD.STRCTURE.SYS.DO.DV2Open.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                         //M19704
                RPD.STRCTURE.SYS.DO.DV2Close.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                        //M19705
                RPD.STRCTURE.SYS.DO.SPARE0.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                          //M19706
                RPD.STRCTURE.SYS.DO.LLHVBRV1FastPumpValveOpen.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);       //M19707
                RPD.STRCTURE.SYS.DO.LLHVBRV1FastPumpValveClose.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);      //M19708
                RPD.STRCTURE.SYS.DO.LLHVBSRV1SlowPumpValveOpen.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);      //M19709
                RPD.STRCTURE.SYS.DO.LLHVBSRV1SlowPumpValveClose.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);     //M19710
                RPD.STRCTURE.SYS.DO.LLHVBDryBoostPumpStart.Lamp = ((iRetData[44] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);          //M19711
                RPD.STRCTURE.SYS.DO.SignalTower_G.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                   //M19712
                RPD.STRCTURE.SYS.DO.SignalTower_Y.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                   //M19713
                RPD.STRCTURE.SYS.DO.SignalTower_R.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                  //M19714
                RPD.STRCTURE.SYS.DO.SignalTower_B.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                  //M19715
                RPD.STRCTURE.SYS.DO.Signal_Buzzer.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                  //M19716

                RPD.STRCTURE.SYS.DO.LLHVBVV1VentOpen.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M19717
                RPD.STRCTURE.SYS.DO.MDV1O2MixArOpen.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                //M19718
                RPD.STRCTURE.SYS.DO.MDV2O2OutOpen.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                  //M19719
                RPD.STRCTURE.SYS.DO.MDV3O2InOpen.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                    //M19720
                RPD.STRCTURE.SYS.DO.MDV4ArOutOpen.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                   //M19721
                RPD.STRCTURE.SYS.DO.MDV5ArInOpen.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                    //M19722
                RPD.STRCTURE.SYS.DO.LLHVBVMV1Open.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                   //M19723
                RPD.STRCTURE.SYS.DO.SPARE3.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                          //M19724
                RPD.STRCTURE.SYS.DO.SPARE4.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                          //M19725
                RPD.STRCTURE.SYS.DO.SPARE5.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                          //M19726
                RPD.STRCTURE.SYS.DO.SPARE6.Lamp = ((iRetData[45] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                          //M19727
                RPD.STRCTURE.SYS.DO.SPARE7.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                          //M19728
                RPD.STRCTURE.SYS.DO.SPARE8.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                          //M19729
                RPD.STRCTURE.SYS.DO.SPARE9.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                         //M19730
                RPD.STRCTURE.SYS.DO.SPARE10.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                        //M19731
                RPD.STRCTURE.SYS.DO.SPARE11.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                        //M19732

                RPD.STRCTURE.SYS.DO.SPARE26.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                        //M19733
                RPD.STRCTURE.SYS.DO.LLHVBGV1Open.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                   //M19734
                RPD.STRCTURE.SYS.DO.LLHVBGV1Close.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                  //M19735
                RPD.STRCTURE.SYS.DO.LLHVBTurboPumpStart.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);             //M19736
                RPD.STRCTURE.SYS.DO.LLHVBEV1Open.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                    //M19737
                RPD.STRCTURE.SYS.DO.LLHVBEV1Close.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                   //M19738
                RPD.STRCTURE.SYS.DO.TRRV3VentValveOpen.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);              //M19739
                RPD.STRCTURE.SYS.DO.TRRV3VentValveClose.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);             //M19740
                RPD.STRCTURE.SYS.DO.BackingDryBoostPumpStart.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);        //M19741
                RPD.STRCTURE.SYS.DO.PCCGV3Open.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                      //M19742
                RPD.STRCTURE.SYS.DO.PCCGV3Close.Lamp = ((iRetData[46] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                     //M19743
                RPD.STRCTURE.SYS.DO.PCCTurboPumpStart.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);               //M19744
                RPD.STRCTURE.SYS.DO.PCCEV2Open.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                      //M19745
                RPD.STRCTURE.SYS.DO.PCCEV2Close.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                    //M19746
                RPD.STRCTURE.SYS.DO.DV3Open.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                        //M19747
                RPD.STRCTURE.SYS.DO.DV3Close.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                       //M19748

                RPD.STRCTURE.SYS.DO.SPARE28.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                        //M19749
                RPD.STRCTURE.SYS.DO.RGAIsolationValveOpen.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);          //M19750
                RPD.STRCTURE.SYS.DO.TRVV3VentOpen.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                  //M19751
                RPD.STRCTURE.SYS.DO.TRVMV3Open.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                      //M19752
                RPD.STRCTURE.SYS.DO.MDV17ArOutOpen.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                  //M19753
                RPD.STRCTURE.SYS.DO.MDV14O2OutOpen.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                  //M19754
                RPD.STRCTURE.SYS.DO.MDV11ArOutOpen.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                  //M19755
                RPD.STRCTURE.SYS.DO.MDV18ArOutOpen.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                  //M19756
                RPD.STRCTURE.SYS.DO.MDV15O2OutOpen.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M19757
                RPD.STRCTURE.SYS.DO.MDV12ArOutOpen.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                  //M19758
                RPD.STRCTURE.SYS.DO.MDV19ArInOpen.Lamp = ((iRetData[47] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                   //M19759
                RPD.STRCTURE.SYS.DO.MDV16O2InOpen.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                   //M19760
                RPD.STRCTURE.SYS.DO.MDV13ArInOpen.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                   //M19761
                RPD.STRCTURE.SYS.DO.SPARE29.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                        //M19762
                RPD.STRCTURE.SYS.DO.SPARE30.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                        //M19763
                RPD.STRCTURE.SYS.DO.SPARE31.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                        //M19764

                RPD.STRCTURE.SYS.DO.RPDSourceSupplyGV6Open.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);          //M19765
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyGV6Close.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);         //M19766
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyTurboPumpStart.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);   //M19767
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyEV4Open.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);           //M19768
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyEV4Close.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);          //M19769
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyOilPumpStart.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);      //M19770
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyRV4Open.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);           //M19771
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyRV4Close.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);          //M19772
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyUpperGateOpenTS1.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);  //M19773
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyUpperGateCloseTS1.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1); //M19774
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyLowerGateOpenGV4.Lamp = ((iRetData[48] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);  //M19775
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyLowerGateCloseGV4.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1); //M19776
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyRotaryFWDPB1.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);      //M19777
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyRotaryBWDPB1.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);     //M19778
                RPD.STRCTURE.SYS.DO.SPARE1.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                          //M19779
                RPD.STRCTURE.SYS.DO.SPARE2.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                          //M19780

                RPD.STRCTURE.SYS.DO.RPDSourceSupplyVentOpenVV4.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);        //M19781
                RPD.STRCTURE.SYS.DO.RPDSourceSupplyFASTVentOpenVMV4.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);   //M19782
                RPD.STRCTURE.SYS.DO.SPARE12.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                           //M19783
                RPD.STRCTURE.LL.Btn.PMV1OpenSwitch.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                     //M19784
                RPD.STRCTURE.UL.Btn.PMV2OpenSwitch.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                     //M19785
                RPD.STRCTURE.SYS.DO.SPARE15.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                            //M19786
                RPD.STRCTURE.SYS.DO.SPARE16.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                            //M19787
                RPD.STRCTURE.SYS.DO.SPARE17.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                            //M19788
                RPD.STRCTURE.SYS.DO.SPARE18.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                            //M19789
                RPD.STRCTURE.SYS.DO.SPARE19.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                            //M19790
                RPD.STRCTURE.SYS.DO.SPARE20.Lamp = ((iRetData[49] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                            //M19791
                RPD.STRCTURE.SYS.DO.SPARE21.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                            //M19792
                RPD.STRCTURE.SYS.DO.SPARE22.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                            //M19793
                RPD.STRCTURE.SYS.DO.SPARE23.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                           //M19794
                RPD.STRCTURE.SYS.DO.SPARE24.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                           //M19795
                RPD.STRCTURE.SYS.DO.SPARE25.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                           //M19796

                RPD.STRCTURE.SYS.DO.ULHVBTurboPumpStart.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M19797
                RPD.STRCTURE.SYS.DO.SPARE27.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                           //M19798
                RPD.STRCTURE.SYS.DO.DV4Open.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                           //M19799
                RPD.STRCTURE.SYS.DO.DV4Close.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                           //M19800
                RPD.STRCTURE.SYS.DO.OutletFeederTakeOutReady.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);           //M19801
                RPD.STRCTURE.SYS.DO.ULHVBSRV2SlowPumpValveOpen.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);         //M19802
                RPD.STRCTURE.SYS.DO.ULHVBSRV2SlowPumpValveClose.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);        //M19803
                RPD.STRCTURE.SYS.DO.ULHVBRV2FastPumpValveOpen.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);          //M19804
                RPD.STRCTURE.SYS.DO.ULHVBRV2FastPumpValveClose.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);         //M19805
                RPD.STRCTURE.SYS.DO.MainWaterInletValve.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                //M19806
                RPD.STRCTURE.SYS.DO.MainWaterOutletValve.Lamp = ((iRetData[50] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);               //M19807
                RPD.STRCTURE.SYS.DO.ULHVBDryBoostPumpStart.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);             //M19808
                RPD.STRCTURE.SYS.DO.ULHVBEV3Open.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                       //M19809
                RPD.STRCTURE.SYS.DO.ULHVBEV3Close.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                     //M19810
                RPD.STRCTURE.SYS.DO.ULHVBGV2Open.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                      //M19811
                RPD.STRCTURE.SYS.DO.ULHVBGV2Close.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                     //M19812

                RPD.STRCTURE.SYS.DO.MDV10ArOutOpen.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                    //M19813
                RPD.STRCTURE.SYS.DO.MDV6O2MixArOpen.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                   //M19814
                RPD.STRCTURE.SYS.DO.MDV7O2OutOpen.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                     //M19815
                RPD.STRCTURE.SYS.DO.MDV9ArOutOpen.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                      //M19816
                RPD.STRCTURE.SYS.DO.MDV8O2InOpen.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                       //M19817
                RPD.STRCTURE.SYS.DO.ULHVBVV2VentOpen.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                   //M19818
                RPD.STRCTURE.SYS.DO.ULHVBVMV2Open.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                      //M19819
                RPD.STRCTURE.SYS.DO.TRCoolingWater1In.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                  //M19820
                RPD.STRCTURE.SYS.DO.TRCoolingWater2In.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M19821
                RPD.STRCTURE.SYS.DO.TRCoolingWater3In.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                  //M19822
                RPD.STRCTURE.SYS.DO.SPARE32.Lamp = ((iRetData[51] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                            //M19823
                RPD.STRCTURE.SYS.DO.SPARE33.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                            //M19824
                RPD.STRCTURE.SYS.DO.SPARE34.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                            //M19825
                RPD.STRCTURE.SYS.DO.SPARE35.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                           //M19826
                RPD.STRCTURE.SYS.DO.SPARE36.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                           //M19827
                RPD.STRCTURE.SYS.DO.SPARE37.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                           //M19828

                RPD.STRCTURE.SYS.DI.EntryFeederStartReady.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);              //M19831
                RPD.STRCTURE.SYS.DI.EntryFeederTrayExistBWD.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);            //M19832
                RPD.STRCTURE.SYS.DI.EntryFeederTrayExistFWD.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);            //M19833
                RPD.STRCTURE.SYS.DI.DV1Open.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                            //M19834
                RPD.STRCTURE.SYS.DI.DV1Close.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                           //M19835
                RPD.STRCTURE.SYS.DI.LLHVBatATM.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                         //M19836
                RPD.STRCTURE.SYS.DI.LLHVBTrayExistBWD.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M19837
                RPD.STRCTURE.SYS.DI.LLHVBTrayExistFWD.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                  //M19838
                RPD.STRCTURE.SYS.DI.LLHVBVV1VentOpen.Lamp = ((iRetData[52] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                   //M19839
                RPD.STRCTURE.SYS.DI.LLHVBVV1VentClose.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                  //M19840
                RPD.STRCTURE.SYS.DI.LLHVBRV1FastPumpValveOpen.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);          //M19841
                RPD.STRCTURE.SYS.DI.LLHVBRV1FastPumpValveClose.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);        //M19842
                RPD.STRCTURE.SYS.DI.LLHVBSRV1SlowPumpValveOpen.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);        //M19843
                RPD.STRCTURE.SYS.DI.LLHVBSRV1SlowPumpValveClose.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);       //M19844
                RPD.STRCTURE.SYS.DI.LLHVBTurboPumpRunning.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);             //M19845
                RPD.STRCTURE.SYS.DI.SPARE0.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                            //M19846

                RPD.STRCTURE.SYS.DI.SPARE7.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                            //M19847
                RPD.STRCTURE.SYS.DI.LLHVBDryPumpCoolingWaterOK.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);         //M19848
                RPD.STRCTURE.SYS.DI.DV2Open.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                            //M19849
                RPD.STRCTURE.SYS.DI.DV2Close.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                           //M19850
                RPD.STRCTURE.SYS.DI.SPARE8.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                             //M19851
                RPD.STRCTURE.SYS.DI.SPARE9.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                             //M19852
                RPD.STRCTURE.SYS.DI.SPARE10.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                            //M19853
                RPD.STRCTURE.SYS.DI.SPARE11.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                            //M19854
                RPD.STRCTURE.SYS.DI.EmergencyOK.Lamp = ((iRetData[53] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                        //M19855
                RPD.STRCTURE.SYS.DI.EntryFeederServoMotorPowerOn.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);       //M19856
                RPD.STRCTURE.SYS.DI.LLHVBTransferMotorPowerOn.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);          //M19857
                RPD.STRCTURE.SYS.DI.TR1TransferMotorPowerOn.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);           //M19858
                RPD.STRCTURE.SYS.DI.TR2TransferMotorPowerOn.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);           //M19859
                RPD.STRCTURE.SYS.DI.TR3TransferMotorPowerOn.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);           //M19860
                RPD.STRCTURE.SYS.DI.ULHVBTransferMotorPowerOn.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);         //M19861
                RPD.STRCTURE.SYS.DI.DeliveryFeederServoMotorPowerOn.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);   //M19862

                RPD.STRCTURE.SYS.DI.TRatATM.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                           //M19863
                RPD.STRCTURE.SYS.DI.TR1TrayExistBWD.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                    //M19864
                RPD.STRCTURE.SYS.DI.TR1TraySpeedUpMID.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                  //M19865
                RPD.STRCTURE.SYS.DI.TR1TrayExistFWD.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                    //M19866
                RPD.STRCTURE.SYS.DI.TR3TrayExistBWD.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                    //M19867
                RPD.STRCTURE.SYS.DI.TR3TraySpeedUpMID.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                  //M19868
                RPD.STRCTURE.SYS.DI.TR3TrayExistFWD.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                    //M19869
                RPD.STRCTURE.SYS.DI.LLHVBGV1Open.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                       //M19870
                RPD.STRCTURE.SYS.DI.LLHVBGV1Close.Lamp = ((iRetData[54] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                      //M19871
                RPD.STRCTURE.SYS.DI.LLHVBTurboPumpCoolingWaterOK.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);       //M19872
                RPD.STRCTURE.SYS.DI.LLHVBTurboPumpRunning.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);              //M19873
                RPD.STRCTURE.SYS.DI.LLHVBTurboPumpAlarm.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);               //M19874
                RPD.STRCTURE.SYS.DI.LLHVBEV1Open.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                      //M19875
                RPD.STRCTURE.SYS.DI.LLHVBEV1Close.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                     //M19876
                RPD.STRCTURE.SYS.DI.RV3Open.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                           //M19877
                RPD.STRCTURE.SYS.DI.RV3Close.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                          //M19878

                RPD.STRCTURE.SYS.DI.SPARE19.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                           //M19879
                RPD.STRCTURE.SYS.DI.BackingDryPumpCoolingWaterOK.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);       //M19880
                RPD.STRCTURE.SYS.DI.BackingDryBoostPumpRunning.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);         //M19881
                RPD.STRCTURE.SYS.DI.SPARE20.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                            //M19882
                RPD.STRCTURE.SYS.DI.SPARE21.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                            //M19883
                RPD.STRCTURE.SYS.DI.RPDDoorInwallCoolingWaterFlowOK.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);    //M19884
                RPD.STRCTURE.SYS.DI.RPDG1ElectrodeCoolingWaterFlowOK.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);   //M19885
                RPD.STRCTURE.SYS.DI.RPDG2ElectrodeCoolingWaterFlowOK.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);   //M19886
                RPD.STRCTURE.SYS.DI.RPDSteeringCoilCoolingWaterFlowOK.Lamp = ((iRetData[55] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);  //M19887
                RPD.STRCTURE.SYS.DI.RPDMainHearthCoolingWaterFlowOK.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);    //M19888
                RPD.STRCTURE.SYS.DI.RPDBeamGuideCoolingWaterFlowOK.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);     //M19889
                RPD.STRCTURE.SYS.DI.RPDChamberInwallCoolingWaterFlowOK.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);//M19890
                RPD.STRCTURE.SYS.DI.PCCGV3Open.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                        //M19891
                RPD.STRCTURE.SYS.DI.PCCGV3Close.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                       //M19892
                RPD.STRCTURE.SYS.DI.PCCTurboPumpCoolingWaterOK.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);        //M19893
                RPD.STRCTURE.SYS.DI.PCCTurboPumpRunning.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);               //M19894

                RPD.STRCTURE.SYS.DI.PCCTurboPumpAlarm.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                 //M19895
                RPD.STRCTURE.SYS.DI.SPARE1.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                             //M19896
                RPD.STRCTURE.SYS.DI.PCCEV2Open.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                         //M19897
                RPD.STRCTURE.SYS.DI.PCCEV2Close.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                        //M19898
                RPD.STRCTURE.SYS.DI.SPARE2.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                             //M19899
                RPD.STRCTURE.SYS.DI.SPARE3.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                             //M19900
                RPD.STRCTURE.SYS.DI.ULHVBTurboPumpRunning.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);              //M19901
                RPD.STRCTURE.SYS.DI.ULHVBTurboPumpAlarm.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                //M19902
                RPD.STRCTURE.SYS.DI.SPARE4.Lamp = ((iRetData[56] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                             //M19903
                RPD.STRCTURE.SYS.DI.SPARE5.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                             //M19904
                RPD.STRCTURE.SYS.DI.SPARE6.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                             //M19905
                RPD.STRCTURE.SYS.DI.ULHVBTurboPumpCoolingWaterOK.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);      //M19906
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyRV4Open.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);            //M19907
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyRV4Close.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);           //M19908
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyatATM.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);              //M19909
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyGV6Open.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);            //M19910

                RPD.STRCTURE.SYS.DI.RPDSourceSupplyGV6Close.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);           //M19911
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyTurboPumpStart.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);      //M19912
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyTurboPumpAlarm.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);      //M19913
                RPD.STRCTURE.SYS.DI.SPARE12.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                            //M19914
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyEV4Open.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);             //M19915
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyEV4Close.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);            //M19916
                RPD.STRCTURE.SYS.DI.TR2CoolerCoolingWaterOK.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);            //M19917
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyOilPumpRunning.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);      //M19918
                RPD.STRCTURE.SYS.DI.SPARE13.Lamp = ((iRetData[57] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                            //M19919
                RPD.STRCTURE.SYS.DI.SPARE14.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                            //M19920
                RPD.STRCTURE.SYS.DI.TR2TrayExistBWD.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                    //M19921
                RPD.STRCTURE.SYS.DI.TR2CHBCoverCoolingWaterOK.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);         //M19922
                RPD.STRCTURE.SYS.DI.TR3CHBCoverCoolingWaterOK.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);         //M19923
                RPD.STRCTURE.SYS.DI.TRVV3VentOpen.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                     //M19924
                RPD.STRCTURE.SYS.DI.TRVV3VentClose.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                    //M19925
                RPD.STRCTURE.SYS.DI.TR2TrayExistFWD.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                   //M19926

                RPD.STRCTURE.SYS.DI.RGAIsolationValveOpen.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);             //M19927
                RPD.STRCTURE.SYS.DI.RGAIsolationValveClose.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);             //M19928
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyUpperGateOpenTS1.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);    //M19929
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyUpperGateCloseTS1.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);   //M19930
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyLowerGateOpenGV4.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);    //M19931
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyLowerGateCloseGV4.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);   //M19932
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyRotaryFWDPB1.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);        //M19933
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyRotaryBWDPB1.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);        //M19934
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyVentOpenVV4.Lamp = ((iRetData[58] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);         //M19935
                RPD.STRCTURE.SYS.DI.RPDSourceSupplyVentCloseVV4.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);        //M19936
                RPD.STRCTURE.SYS.DI.SPARE15.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                            //M19937
                RPD.STRCTURE.SYS.DI.SPARE16.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                           //M19938
                RPD.STRCTURE.SYS.DI.SPARE17.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                           //M19939
                RPD.STRCTURE.SYS.DI.SPARE18.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                           //M19940
                RPD.STRCTURE.SYS.DI.MainWaterPressureOK.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);               //M19941
                RPD.STRCTURE.SYS.DI.MainCDAPressureOK.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                 //M19942

                RPD.STRCTURE.SYS.DI.SPARE22.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                           //M19943
                RPD.STRCTURE.SYS.DI.SPARE23.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                            //M19944
                RPD.STRCTURE.SYS.DI.SPARE24.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                            //M19945
                RPD.STRCTURE.SYS.DI.SPARE25.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                            //M19946
                RPD.STRCTURE.SYS.DI.SPARE26.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                            //M19947
                RPD.STRCTURE.SYS.DI.SPARE27.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                            //M19948
                RPD.STRCTURE.SYS.DI.SPARE28.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                            //M19949
                RPD.STRCTURE.SYS.DI.SPARE29.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                            //M19950
                RPD.STRCTURE.SYS.DI.SPARE30.Lamp = ((iRetData[59] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                            //M19951
                RPD.STRCTURE.SYS.DI.SPARE31.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                            //M19952
                RPD.STRCTURE.SYS.DI.SPARE32.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                            //M19953
                RPD.STRCTURE.SYS.DI.SPARE33.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                           //M19954
                RPD.STRCTURE.SYS.DI.SPARE34.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                           //M19955
                RPD.STRCTURE.SYS.DI.SPARE35.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                           //M19956
                RPD.STRCTURE.SYS.DI.SPARE36.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                           //M19957
                RPD.STRCTURE.SYS.DI.SPARE37.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                           //M19958

                RPD.STRCTURE.SYS.DI.DV3Open.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);                           //M19959
                RPD.STRCTURE.SYS.DI.DV3Close.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                           //M19960
                RPD.STRCTURE.SYS.DI.ULHVBatATM.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                         //M19961
                RPD.STRCTURE.SYS.DI.DV4Open.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                            //M19962
                RPD.STRCTURE.SYS.DI.DV4Close.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                           //M19963
                RPD.STRCTURE.SYS.DI.ULHVBTrayExistFWD.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                  //M19964
                RPD.STRCTURE.SYS.DI.ULHVBTrayExistBWD.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                  //M19965
                RPD.STRCTURE.SYS.DI.DeliverFeederTrayExistBWD.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);          //M19966
                RPD.STRCTURE.SYS.DI.DeliverFeederTrayExistFWD.Lamp = ((iRetData[60] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);          //M19967
                RPD.STRCTURE.SYS.DI.DeliverFeederTakeoutReady.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);          //M19968
                RPD.STRCTURE.SYS.DI.ULHVBSRV2SlowPumpValveOpen.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);         //M19969
                RPD.STRCTURE.SYS.DI.ULHVBSRV2SlowPumpValveClose.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);       //M19970
                RPD.STRCTURE.SYS.DI.ULHVBRV2FastPumpValveOpen.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);         //M19971
                RPD.STRCTURE.SYS.DI.ULHVBRV2FastPumpValveClose.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);        //M19972
                RPD.STRCTURE.SYS.DI.ULHVBVV2VentOpen.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                  //M19973
                RPD.STRCTURE.SYS.DI.ULHVBVV2VentClose.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                 //M19974

                RPD.STRCTURE.SYS.DI.ULHVBDryBoostPumpRunning.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);          //M19975
                RPD.STRCTURE.SYS.DI.ULHVBEV3Open.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                       //M19976
                RPD.STRCTURE.SYS.DI.ULHVBEV3Close.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);                      //M19977
                RPD.STRCTURE.SYS.DI.ULHVBDryBoostPumpCoolingWaterOK.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);    //M19978
                RPD.STRCTURE.SYS.DI.ULHVBGV2Open.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                       //M19979
                RPD.STRCTURE.SYS.DI.ULHVBGV2Close.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);                      //M19980
                RPD.STRCTURE.SYS.DI.SPARE38.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);                            //M19981
                RPD.STRCTURE.SYS.DI.SPARE39.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                            //M19982
                RPD.STRCTURE.SYS.DI.SPARE40.Lamp = ((iRetData[61] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);                            //M19983
                RPD.STRCTURE.SYS.DI.SPARE41.Lamp = ((iRetData[62] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                            //M19984
                RPD.STRCTURE.SYS.DI.SPARE42.Lamp = ((iRetData[62] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                            //M19985
                RPD.STRCTURE.SYS.DI.SPARE43.Lamp = ((iRetData[62] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);                           //M19986
                RPD.STRCTURE.SYS.DI.SPARE44.Lamp = ((iRetData[62] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);                           //M19987
                RPD.STRCTURE.SYS.DI.SPARE45.Lamp = ((iRetData[62] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                           //M19988
                RPD.STRCTURE.SYS.DI.SPARE46.Lamp = ((iRetData[62] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);                           //M19989
                RPD.STRCTURE.SYS.DI.SPARE47.Lamp = ((iRetData[62] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);                           //M19990

                RPD.STRCTURE.SYS.Map.EventLogRequest = ((iRetData[63] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                       //M20000

                RPD.STRCTURE.LL.Map.Record_LLReadyToGo = ((iRetData[63] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                    //M20010
                RPD.STRCTURE.LL.Map.Record_LLReadyToVent = ((iRetData[63] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);                  //M20014
                RPD.STRCTURE.LL.Map.Record_LLPumpDownTo50m = ((iRetData[64] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);                 //M20016
                RPD.STRCTURE.LL.Map.Record_LLPumpDownTo1m = ((iRetData[64] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);                  //M20017

                RPD.STRCTURE.UL.Map.Record_ULReadyToGo = ((iRetData[64] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);                     //M20020
                RPD.STRCTURE.UL.Map.Record_ULReadyToVent = ((iRetData[64] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);                   //M20024
                RPD.STRCTURE.UL.Map.Record_ULPumpDownTo50m = ((iRetData[64] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);                //M20026
                RPD.STRCTURE.UL.Map.Record_ULPumpDownTo1m = ((iRetData[64] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);                 //M20027


            }
            else
            {
                _bConn = false;
            }
        }
        public void ReadPLC_E2G_Word()
        {
            int[] iRetData = new int[2000];   //D38000 後開始 2000個位址
            bool bRtn;
            bRtn = FunBatchReadPLC("D38000", ref iRetData);//D38000 
            if (bRtn == true)
            {
                RPD.STRCTURE.PCC1.Data.PCC_TABLE = iRetData;
                RPD.STRCTURE.SYS.Data.PLCAliveIndex = iRetData[0];                         //D38000 
                RPD.STRCTURE.SYS.Data.SystemYear = iRetData[1];                            //D38001
                RPD.STRCTURE.SYS.Data.SystemMonth = iRetData[2];                           //D38002
                RPD.STRCTURE.SYS.Data.SystemDate = iRetData[3];                            //D38003
                RPD.STRCTURE.SYS.Data.SystemHour = iRetData[4];                            //D38004
                RPD.STRCTURE.SYS.Data.SystemMin = iRetData[5];                             //D38005
                RPD.STRCTURE.SYS.Data.SystemSec = iRetData[6];                             //D38006
                RPD.STRCTURE.SYS.Data.SystemWeek = iRetData[7];                            //D38007

                RPD.STRCTURE.SYS.Map.EQStatus = iRetData[9];                               //D38009
                RPD.STRCTURE.SYS.Data.LeakTestStartTorr = iRetData[10];                    //D38010
                RPD.STRCTURE.SYS.Data.LeakTestEndTorr = iRetData[11];                      //D38011


                RPD.STRCTURE.SYS.Map.MainWaterTemp = (double)iRetData[15] / 10;                         //D38015;

                //add by william for G1 body and cooling water temperature sensor value display on 2016.7.20
                //RPD.STRCTURE.PCC1.Map.G1BodyTempValue = (double)iRetData[16] / 10;             //D38016
                //RPD.STRCTURE.PCC1.Map.G1CWTempValue = (double)iRetData[17] / 10;             //D38017


                RPD.STRCTURE.SYS.Map.CurrentTactTime = iRetData[20] / 10;                    //D38020
                RPD.STRCTURE.SYS.Map.TactTime1 = iRetData[21] / 10;                   //D38021            
                RPD.STRCTURE.SYS.Map.TactTime2 = iRetData[22] / 10;                   //D38022
                RPD.STRCTURE.SYS.Map.TactTime3 = iRetData[23] / 10;                   //D38023
                RPD.STRCTURE.SYS.Map.TactTime4 = iRetData[24] / 10;                   //D38024
                RPD.STRCTURE.SYS.Map.TactTime5 = iRetData[25] / 10;                   //D38025

                //Add by Henry 2016/1/22
                RPD.STRCTURE.TR.Map.TRMFCH2OSet = iRetData[29];                        //D38029

                RPD.STRCTURE.SYS.Map.TrayCount = iRetData[30];                        //D38030

                RPD.STRCTURE.LL.Map.LLMFCO2Set = iRetData[32];                        //D38032
                RPD.STRCTURE.LL.Map.LLMFCArSet = iRetData[33];                        //D38033
                RPD.STRCTURE.UL.Map.ULMFCO2Set = iRetData[34];                        //D38034
                RPD.STRCTURE.UL.Map.ULMFCArSet = iRetData[35];                        //D38035
                RPD.STRCTURE.TR.Map.TRMFCAr_GunSet = iRetData[36];                    //D38036
                RPD.STRCTURE.TR.Map.TRMFCO2Set = iRetData[37];                        //D38037
                RPD.STRCTURE.TR.Map.TRMFCAr_ChamberSet = iRetData[38];                //D38038

                RPD.STRCTURE.LL.Map.LLMFCO2Flow = iRetData[40];                       //D38040
                RPD.STRCTURE.LL.Map.LLMFCArFlow = iRetData[41];                       //D38041
                RPD.STRCTURE.UL.Map.ULMFCO2Flow = iRetData[42];                       //D38042
                RPD.STRCTURE.UL.Map.ULMFCArFlow = iRetData[43];                       //D38043

                RPD.STRCTURE.TR.Map.TRMFCAr_Gun_Flow = iRetData[44];                  //D38044
                RPD.STRCTURE.TR.Map.TRMFCO2Flow = iRetData[45];                       //D38045
                RPD.STRCTURE.TR.Map.TRMFCAr_Chamber_Flow = iRetData[46];              //D38046

                //Add by Henry 2016/1/22
                RPD.STRCTURE.TR.Map.TRMFCH2OFlow = iRetData[49];                        //D38049

                RPD.STRCTURE.LL.Map.LLGaugeValuePH = (double)(MakeDWord((ushort)iRetData[50], (ushort)iRetData[51])) / 1000000;             //D38050 & D38051 除以10 * 6 次方
                RPD.STRCTURE.TR.Map.TRGaugeValuePH = (double)(MakeDWord((ushort)iRetData[54], (ushort)iRetData[55])) / 1000000;             //D38054 & D38055 除以10 * 6 次方 
                RPD.STRCTURE.TR.Map.TRGaugeValueC_NoOffset = (double)(MakeDWord((ushort)iRetData[56], (ushort)iRetData[57])) / 1000000;     //D38056 & D38057 除以10 * 6 次方 
                RPD.STRCTURE.UL.Map.ULGaugeValuePH = (double)(MakeDWord((ushort)iRetData[58], (ushort)iRetData[59])) / 1000000;             //D38058 & D38059 除以10 * 6 次方 

                RPD.STRCTURE.SYS.Map.LLTP_EV1_GaugeValueP = (double)(MakeDWord((ushort)iRetData[60], (ushort)iRetData[61])) / 10000;        //D38060 & D38061 除以10 * 4 次方 
                RPD.STRCTURE.SYS.Map.DP_EV1_GaugeValueP = (double)(MakeDWord((ushort)iRetData[62], (ushort)iRetData[63])) / 10000;          //D38062 & D38063 除以10 * 4 次方 
                RPD.STRCTURE.SYS.Map.UL_TP_EV2_GaugeValueP = (double)(MakeDWord((ushort)iRetData[64], (ushort)iRetData[65])) / 10000;       //D38064 & D38065 除以10 * 4 次方 
                RPD.STRCTURE.SYS.Map.TR_TP_EV3_GaugeValueP = (double)(MakeDWord((ushort)iRetData[66], (ushort)iRetData[67])) / 10000;       //D38066 & D38067 除以10 * 4 次方 
                RPD.STRCTURE.PCC2.Map.TSS_GaugeValueP = (double)(MakeDWord((ushort)iRetData[68], (ushort)iRetData[69])) / 1000000;          //D38068 & D38069 除以10 * 6 次方 
                RPD.STRCTURE.PCC2.Map.TSS_TP_EV4_GaugeValueP = (double)(MakeDWord((ushort)iRetData[70], (ushort)iRetData[71])) / 10000;     //D38070 & D38071 除以10 * 4 次方 
                RPD.STRCTURE.PCC2.Map.TSS_DP_EV4_GaugeValueP = (double)(MakeDWord((ushort)iRetData[72], (ushort)iRetData[73])) / 10000;     //D38072 & D38073 除以10 * 4 次方 
                RPD.STRCTURE.TR.Map.TRGaugeValueC = (double)(MakeDWord((ushort)iRetData[75], (ushort)iRetData[76])) / 1000000;               //D38075 & D38076 除以10 * 6 次方 

                //Add By Henry 2015/12/28 新增TR Gauge Value (PH) 極小值顯示
                if (RPD.STRCTURE.TR.Map.TRGaugeValuePH == 0)
                    RPD.STRCTURE.TR.Map.TRGaugeValuePH = (double)(MakeDWord((ushort)iRetData[77], (ushort)iRetData[78])) / 10000000000;               //D38077 & D38078 除以10 * 10 次方 


                RPD.STRCTURE.LL.Map.LLPumpFlowValue = (double)iRetData[80] / 10;                          //D38080
                RPD.STRCTURE.LL.Map.BackingFlowValue = (double)iRetData[81] / 10;                         //D38081
                RPD.STRCTURE.UL.Map.ULPumpFlowValue = (double)iRetData[82] / 10;                          //D38082
                RPD.STRCTURE.LL.Map.LL_TP_PumpFlowValue = (double)iRetData[83] / 10;                      //D38083
                RPD.STRCTURE.TR.Map.TR2_CHB_COVERFlowValue = (double)iRetData[84] / 10;                   //D38084
                RPD.STRCTURE.TR.Map.TR_TP_PumpFlowValue = (double)iRetData[85] / 10;                      //D38085
                RPD.STRCTURE.TR.Map.TR3_CHB_COVERFlowValue = (double)iRetData[86] / 10;                   //D38086
                RPD.STRCTURE.TR.Map.TR2_CoolerFlowValue = (double)iRetData[87] / 10;                      //D38087

                RPD.STRCTURE.PCC1.Map.PlasmaGeneratorG1FlowValue = (double)iRetData[88] / 10;             //D38088
                RPD.STRCTURE.PCC1.Map.PlasmaGeneratorG2FlowValue = (double)iRetData[89] / 10;             //D38089
                RPD.STRCTURE.PCC1.Map.PlasmaGeneratorSteeringCoilFlowValue = (double)iRetData[90] / 10;   //D38090
                RPD.STRCTURE.PCC1.Map.HearthMainFlowValue = (double)iRetData[91] / 10;                    //D38091
                RPD.STRCTURE.PCC1.Map.HearthBeamFlowValue = (double)iRetData[92] / 10;                    //D38092

                RPD.STRCTURE.PCC1.Map.DoorInWallFlowValue = (double)iRetData[93] / 10;                    //D38093
                RPD.STRCTURE.PCC1.Map.ChamberInWallFlowValue = (double)iRetData[94] / 10;                 //D38094





                RPD.STRCTURE.UL.Map.ULTPPumpFlowValue = (double)iRetData[95] / 10;                        //D38095

                RPD.STRCTURE.LL.Map.TP_LL_Status = iRetData[96];                                          //D38096
                RPD.STRCTURE.TR.Map.TP_TR_Status = iRetData[97];                                          //D38097
                RPD.STRCTURE.UL.Map.TP_UL_Status = iRetData[98];                                          //D38098
                RPD.STRCTURE.PCC2.Map.TP_TSS_Status = iRetData[99];                                       //D38099


                RPD.STRCTURE.SYS.Data.Alarm[0] = iRetData[100];                              //D38100
                RPD.STRCTURE.SYS.Data.Alarm[1] = iRetData[101];                             //D38101
                RPD.STRCTURE.SYS.Data.Alarm[2] = iRetData[102];                             //D38102
                RPD.STRCTURE.SYS.Data.Alarm[3] = iRetData[103];                             //D38103
                RPD.STRCTURE.SYS.Data.Alarm[4] = iRetData[104];                             //D38104
                RPD.STRCTURE.SYS.Data.Alarm[5] = iRetData[105];                             //D38105
                RPD.STRCTURE.SYS.Data.Alarm[6] = iRetData[106];                            //D38106
                RPD.STRCTURE.SYS.Data.Alarm[7] = iRetData[107];                           //D38107
                RPD.STRCTURE.SYS.Data.Alarm[8] = iRetData[108];                           //D38108
                RPD.STRCTURE.SYS.Data.Alarm[9] = iRetData[109];                           //D38109
                RPD.STRCTURE.SYS.Data.Alarm[10] = iRetData[110];                           //D38110
                RPD.STRCTURE.SYS.Data.Alarm[11] = iRetData[111];                           //D38111
                RPD.STRCTURE.SYS.Data.Alarm[12] = iRetData[112];                           //D38112
                RPD.STRCTURE.SYS.Data.Alarm[13] = iRetData[113];                           //D38113
                RPD.STRCTURE.SYS.Data.Alarm[14] = iRetData[114];                           //D38114
                RPD.STRCTURE.SYS.Data.Alarm[15] = iRetData[115];                           //D38115
                RPD.STRCTURE.SYS.Data.Alarm[16] = iRetData[116];                           //D38116
                RPD.STRCTURE.SYS.Data.Alarm[17] = iRetData[117];                           //D38117
                RPD.STRCTURE.SYS.Data.Alarm[18] = iRetData[118];                           //D38118
                RPD.STRCTURE.SYS.Data.Alarm[19] = iRetData[119];                           //D38119
                RPD.STRCTURE.SYS.Data.Alarm[20] = iRetData[120];                           //D38120
                RPD.STRCTURE.SYS.Data.Alarm[21] = iRetData[121];                           //D38121
                RPD.STRCTURE.SYS.Data.Alarm[22] = iRetData[122];                           //D38122
                RPD.STRCTURE.SYS.Data.Alarm[23] = iRetData[123];                           //D38123
                RPD.STRCTURE.SYS.Data.Alarm[24] = iRetData[124];                           //D38124
                RPD.STRCTURE.SYS.Data.Alarm[25] = iRetData[125];                           //D38125
                RPD.STRCTURE.SYS.Data.Alarm[26] = iRetData[126];                           //D38126
                RPD.STRCTURE.SYS.Data.Alarm[27] = iRetData[127];                           //D38127
                RPD.STRCTURE.SYS.Data.Alarm[28] = iRetData[128];                           //D38128
                RPD.STRCTURE.SYS.Data.Alarm[29] = iRetData[129];                           //D38129
                RPD.STRCTURE.SYS.Data.Alarm[30] = iRetData[130];                           //D38130
                RPD.STRCTURE.SYS.Data.Alarm[31] = iRetData[131];                           //D38131


                RPD.STRCTURE.SYS.Data.EventLog[0] = iRetData[140];                            //D38140
                RPD.STRCTURE.SYS.Data.EventLog[1] = iRetData[141];                            //D38141
                RPD.STRCTURE.SYS.Data.EventLog[2] = iRetData[142];                            //D38142
                RPD.STRCTURE.SYS.Data.EventLog[3] = iRetData[143];                            //D38143
                RPD.STRCTURE.SYS.Data.EventLog[4] = iRetData[144];                            //D38144
                RPD.STRCTURE.SYS.Data.EventLog[5] = iRetData[145];                            //D38145
                RPD.STRCTURE.SYS.Data.EventLog[6] = iRetData[146];                            //D38146
                RPD.STRCTURE.SYS.Data.EventLog[7] = iRetData[147];                            //D38147
                RPD.STRCTURE.SYS.Data.EventLog[8] = iRetData[148];                            //D38148
                RPD.STRCTURE.SYS.Data.EventLog[9] = iRetData[149];                            //D38149
                RPD.STRCTURE.SYS.Data.EventLog[10] = iRetData[150];                           //D38150
                RPD.STRCTURE.SYS.Data.EventLog[11] = iRetData[151];                           //D38151
                RPD.STRCTURE.SYS.Data.EventLog[12] = iRetData[152];                           //D38152
                RPD.STRCTURE.SYS.Data.EventLog[13] = iRetData[153];                           //D38153
                RPD.STRCTURE.SYS.Data.EventLog[14] = iRetData[154];                           //D38154
                RPD.STRCTURE.SYS.Data.EventLog[15] = iRetData[155];                           //D38155
                RPD.STRCTURE.SYS.Data.EventLog[16] = iRetData[156];                           //D38156
                RPD.STRCTURE.SYS.Data.EventLog[17] = iRetData[157];                           //D38157
                RPD.STRCTURE.SYS.Data.EventLog[18] = iRetData[158];                           //D38158
                RPD.STRCTURE.SYS.Data.EventLog[19] = iRetData[159];                           //D38159
                RPD.STRCTURE.SYS.Data.EventLog[20] = iRetData[160];                           //D38160
                RPD.STRCTURE.SYS.Data.EventLog[21] = iRetData[161];                           //D38161
                RPD.STRCTURE.SYS.Data.EventLog[22] = iRetData[162];                           //D38162
                RPD.STRCTURE.SYS.Data.EventLog[23] = iRetData[163];                           //D38163
                RPD.STRCTURE.SYS.Data.EventLog[24] = iRetData[164];                           //D38164
                RPD.STRCTURE.SYS.Data.EventLog[25] = iRetData[165];                           //D38165
                RPD.STRCTURE.SYS.Data.EventLog[26] = iRetData[166];                           //D38166
                RPD.STRCTURE.SYS.Data.EventLog[27] = iRetData[167];                           //D38167
                RPD.STRCTURE.SYS.Data.EventLog[28] = iRetData[168];                           //D38168
                RPD.STRCTURE.SYS.Data.EventLog[29] = iRetData[169];                           //D38169
                RPD.STRCTURE.SYS.Data.EventLog[30] = iRetData[170];                           //D38170
                RPD.STRCTURE.SYS.Data.EventLog[31] = iRetData[171];                           //D38171
                RPD.STRCTURE.SYS.Data.EventLog[32] = iRetData[172];                           //D38172
                RPD.STRCTURE.SYS.Data.EventLog[33] = iRetData[173];                           //D38173
                RPD.STRCTURE.SYS.Data.EventLog[34] = iRetData[174];                           //D38174
                RPD.STRCTURE.SYS.Data.EventLog[35] = iRetData[175];                           //D38175
                RPD.STRCTURE.SYS.Data.EventLog[36] = iRetData[176];                           //D38176
                RPD.STRCTURE.SYS.Data.EventLog[37] = iRetData[177];                           //D38177
                RPD.STRCTURE.SYS.Data.EventLog[38] = iRetData[178];                           //D38178
                RPD.STRCTURE.SYS.Data.EventLog[39] = iRetData[179];                           //D38179
                RPD.STRCTURE.SYS.Data.EventLog[40] = iRetData[180];                           //D38180
                RPD.STRCTURE.SYS.Data.EventLog[41] = iRetData[181];                           //D38181
                RPD.STRCTURE.SYS.Data.EventLog[42] = iRetData[182];                           //D38182
                RPD.STRCTURE.SYS.Data.EventLog[43] = iRetData[183];                           //D38183
                RPD.STRCTURE.SYS.Data.EventLog[44] = iRetData[184];                           //D38184
                RPD.STRCTURE.SYS.Data.EventLog[45] = iRetData[185];                           //D38185
                RPD.STRCTURE.SYS.Data.EventLog[46] = iRetData[186];                           //D38186
                RPD.STRCTURE.SYS.Data.EventLog[47] = iRetData[187];                           //D38187
                RPD.STRCTURE.SYS.Data.EventLog[48] = iRetData[188];                           //D38188
                RPD.STRCTURE.SYS.Data.EventLog[49] = iRetData[189];                           //D38189
                RPD.STRCTURE.SYS.Data.EventLog[50] = iRetData[190];                           //D38190
                RPD.STRCTURE.SYS.Data.EventLog[51] = iRetData[191];                           //D38191
                RPD.STRCTURE.SYS.Data.EventLog[52] = iRetData[192];                           //D38192
                RPD.STRCTURE.SYS.Data.EventLog[53] = iRetData[193];                           //D38193
                RPD.STRCTURE.SYS.Data.EventLog[54] = iRetData[194];                           //D38194
                RPD.STRCTURE.SYS.Data.EventLog[55] = iRetData[195];                           //D38195
                RPD.STRCTURE.SYS.Data.EventLog[56] = iRetData[196];                           //D38196
                RPD.STRCTURE.SYS.Data.EventLog[57] = iRetData[197];                           //D38197
                RPD.STRCTURE.SYS.Data.EventLog[58] = iRetData[198];                           //D38198
                RPD.STRCTURE.SYS.Data.EventLog[59] = iRetData[199];                           //D38199
                RPD.STRCTURE.SYS.Data.EventLog[60] = iRetData[200];                           //D38200
                RPD.STRCTURE.SYS.Data.EventLog[61] = iRetData[201];                           //D38201
                RPD.STRCTURE.SYS.Data.EventLog[62] = iRetData[202];                           //D38202
                RPD.STRCTURE.SYS.Data.EventLog[63] = iRetData[203];                           //D38203
                RPD.STRCTURE.SYS.Data.EventLog[64] = iRetData[204];                           //D38204
                RPD.STRCTURE.SYS.Data.EventLog[65] = iRetData[205];                           //D38205
                RPD.STRCTURE.SYS.Data.EventLog[66] = iRetData[206];                           //D38206
                RPD.STRCTURE.SYS.Data.EventLog[67] = iRetData[207];                           //D38207
                RPD.STRCTURE.SYS.Data.EventLog[68] = iRetData[208];                           //D38208
                RPD.STRCTURE.SYS.Data.EventLog[69] = iRetData[209];                           //D38209
                RPD.STRCTURE.SYS.Data.EventLog[70] = iRetData[210];                           //D38210
                RPD.STRCTURE.SYS.Data.EventLog[71] = iRetData[211];                           //D38211
                RPD.STRCTURE.SYS.Data.EventLog[72] = iRetData[212];                           //D38212
                RPD.STRCTURE.SYS.Data.EventLog[73] = iRetData[213];                           //D38213
                RPD.STRCTURE.SYS.Data.EventLog[74] = iRetData[214];                           //D38214
                RPD.STRCTURE.SYS.Data.EventLog[75] = iRetData[215];                           //D38215
                RPD.STRCTURE.SYS.Data.EventLog[76] = iRetData[216];                           //D38216
                RPD.STRCTURE.SYS.Data.EventLog[77] = iRetData[217];                           //D38217
                RPD.STRCTURE.SYS.Data.EventLog[78] = iRetData[218];                           //D38218
                RPD.STRCTURE.SYS.Data.EventLog[79] = iRetData[219];                           //D38219
                RPD.STRCTURE.SYS.Data.EventLog[80] = iRetData[220];                           //D38220
                RPD.STRCTURE.SYS.Data.EventLog[81] = iRetData[221];                           //D38221
                RPD.STRCTURE.SYS.Data.EventLog[82] = iRetData[222];                           //D38222
                RPD.STRCTURE.SYS.Data.EventLog[83] = iRetData[223];                           //D38223
                RPD.STRCTURE.SYS.Data.EventLog[84] = iRetData[224];                           //D38224
                RPD.STRCTURE.SYS.Data.EventLog[85] = iRetData[225];                           //D38225
                RPD.STRCTURE.SYS.Data.EventLog[86] = iRetData[226];                           //D38226
                RPD.STRCTURE.SYS.Data.EventLog[87] = iRetData[227];                           //D38227
                RPD.STRCTURE.SYS.Data.EventLog[88] = iRetData[228];                           //D38228
                RPD.STRCTURE.SYS.Data.EventLog[89] = iRetData[229];                           //D38229
                RPD.STRCTURE.SYS.Data.EventLog[90] = iRetData[230];                           //D38230
                RPD.STRCTURE.SYS.Data.EventLog[91] = iRetData[231];                           //D38231
                RPD.STRCTURE.SYS.Data.EventLog[92] = iRetData[232];                           //D38232
                RPD.STRCTURE.SYS.Data.EventLog[93] = iRetData[233];                           //D38233
                RPD.STRCTURE.SYS.Data.EventLog[94] = iRetData[234];                           //D38234
                RPD.STRCTURE.SYS.Data.EventLog[95] = iRetData[235];                           //D38235
                RPD.STRCTURE.SYS.Data.EventLog[96] = iRetData[236];                           //D38236
                RPD.STRCTURE.SYS.Data.EventLog[97] = iRetData[237];                           //D38237
                RPD.STRCTURE.SYS.Data.EventLog[98] = iRetData[238];                           //D38238
                RPD.STRCTURE.SYS.Data.EventLog[99] = iRetData[239];                           //D38239
                RPD.STRCTURE.SYS.Data.EventLog[100] = iRetData[240];                          //D38240
                RPD.STRCTURE.SYS.Data.EventLog[101] = iRetData[241];                          //D38241
                RPD.STRCTURE.SYS.Data.EventLog[102] = iRetData[242];                          //D38242
                RPD.STRCTURE.SYS.Data.EventLog[103] = iRetData[243];                          //D38243
                RPD.STRCTURE.SYS.Data.EventLog[104] = iRetData[244];                          //D38244
                RPD.STRCTURE.SYS.Data.EventLog[105] = iRetData[245];                          //D38245
                RPD.STRCTURE.SYS.Data.EventLog[106] = iRetData[246];                          //D38246
                RPD.STRCTURE.SYS.Data.EventLog[107] = iRetData[247];                          //D38247
                RPD.STRCTURE.SYS.Data.EventLog[108] = iRetData[248];                          //D38248
                RPD.STRCTURE.SYS.Data.EventLog[109] = iRetData[249];                          //D38249
                RPD.STRCTURE.SYS.Data.EventLog[110] = iRetData[250];                          //D38250
                RPD.STRCTURE.SYS.Data.EventLog[111] = iRetData[251];                          //D38251
                RPD.STRCTURE.SYS.Data.EventLog[112] = iRetData[252];                          //D38252
                RPD.STRCTURE.SYS.Data.EventLog[113] = iRetData[253];                          //D38253
                RPD.STRCTURE.SYS.Data.EventLog[114] = iRetData[254];                          //D38254
                RPD.STRCTURE.SYS.Data.EventLog[115] = iRetData[255];                          //D38255
                RPD.STRCTURE.SYS.Data.EventLog[116] = iRetData[256];                          //D38256
                RPD.STRCTURE.SYS.Data.EventLog[117] = iRetData[257];                          //D38257
                RPD.STRCTURE.SYS.Data.EventLog[118] = iRetData[258];                          //D38258
                RPD.STRCTURE.SYS.Data.EventLog[119] = iRetData[259];                          //D38259
                RPD.STRCTURE.SYS.Data.EventLog[120] = iRetData[260];                          //D38260
                RPD.STRCTURE.SYS.Data.EventLog[121] = iRetData[261];                          //D38261
                RPD.STRCTURE.SYS.Data.EventLog[122] = iRetData[262];                          //D38262
                RPD.STRCTURE.SYS.Data.EventLog[123] = iRetData[263];                          //D38263
                RPD.STRCTURE.SYS.Data.EventLog[124] = iRetData[264];                          //D38264
                RPD.STRCTURE.SYS.Data.EventLog[125] = iRetData[265];                          //D38265
                RPD.STRCTURE.SYS.Data.EventLog[126] = iRetData[266];                          //D38266
                RPD.STRCTURE.SYS.Data.EventLog[127] = iRetData[267];                          //D38267

                RPD.STRCTURE.SYS.Data.Warning[0] = iRetData[270];                            //D38270
                RPD.STRCTURE.SYS.Data.Warning[1] = iRetData[271];                            //D38271
                RPD.STRCTURE.SYS.Data.Warning[2] = iRetData[272];                            //D38272
                RPD.STRCTURE.SYS.Data.Warning[3] = iRetData[273];                            //D38273
                RPD.STRCTURE.SYS.Data.Warning[4] = iRetData[274];                            //D38274
                RPD.STRCTURE.SYS.Data.Warning[5] = iRetData[275];                            //D38275
                RPD.STRCTURE.SYS.Data.Warning[6] = iRetData[276];                            //D38276
                RPD.STRCTURE.SYS.Data.Warning[7] = iRetData[277];                            //D38277
                RPD.STRCTURE.SYS.Data.Warning[8] = iRetData[278];                            //D38278
                RPD.STRCTURE.SYS.Data.Warning[9] = iRetData[279];                            //D38279
                RPD.STRCTURE.SYS.Data.Warning[10] = iRetData[280];                           //D38280
                RPD.STRCTURE.SYS.Data.Warning[11] = iRetData[281];                           //D38281
                RPD.STRCTURE.SYS.Data.Warning[12] = iRetData[282];                           //D38282
                RPD.STRCTURE.SYS.Data.Warning[13] = iRetData[283];                           //D38283
                RPD.STRCTURE.SYS.Data.Warning[14] = iRetData[284];                           //D38284
                RPD.STRCTURE.SYS.Data.Warning[15] = iRetData[285];                           //D38285
                RPD.STRCTURE.SYS.Data.Warning[16] = iRetData[286];                           //D38286
                RPD.STRCTURE.SYS.Data.Warning[17] = iRetData[287];                           //D38287
                RPD.STRCTURE.SYS.Data.Warning[18] = iRetData[288];                           //D38288
                RPD.STRCTURE.SYS.Data.Warning[19] = iRetData[289];                           //D38289
                RPD.STRCTURE.SYS.Data.Warning[20] = iRetData[290];                           //D38290
                RPD.STRCTURE.SYS.Data.Warning[21] = iRetData[291];                           //D38291
                RPD.STRCTURE.SYS.Data.Warning[22] = iRetData[292];                           //D38292
                RPD.STRCTURE.SYS.Data.Warning[23] = iRetData[293];                           //D38293
                RPD.STRCTURE.SYS.Data.Warning[24] = iRetData[294];                           //D38294
                RPD.STRCTURE.SYS.Data.Warning[25] = iRetData[295];                           //D38295
                RPD.STRCTURE.SYS.Data.Warning[26] = iRetData[296];                           //D38296
                RPD.STRCTURE.SYS.Data.Warning[27] = iRetData[297];                           //D38297
                RPD.STRCTURE.SYS.Data.Warning[28] = iRetData[298];                           //D38298
                RPD.STRCTURE.SYS.Data.Warning[29] = iRetData[299];                           //D38299
                RPD.STRCTURE.SYS.Data.Warning[30] = iRetData[300];                           //D38300
                RPD.STRCTURE.SYS.Data.Warning[31] = iRetData[301];                           //D38301

                RPD.STRCTURE.SYS.Data.CVSpeed = (double)(MakeDWord((ushort)iRetData[401], (ushort)iRetData[402])) / 100;                    //D38401 & D38402 除以100
                RPD.STRCTURE.SYS.Data.CVProcessSpeed = (double)(MakeDWord((ushort)iRetData[403], (ushort)iRetData[404])) / 100;             //D38403 & D38404 除以100
                RPD.STRCTURE.SYS.Data.DVOpenATM = (double)(MakeDWord((ushort)iRetData[405], (ushort)iRetData[406])) / 1000000;              //D38405 & D38406 除以10 * 6 次方
                RPD.STRCTURE.TR.Data.TRCV1_TRCV2DelayTime = (double)iRetData[407] / 100;                                                             //D38407 除以 10
                RPD.STRCTURE.EF.Data.EFCVSpeedTime = (double)(MakeDWord((ushort)iRetData[408], (ushort)iRetData[409])) / 1000;              //D38408 & D38409 除以1000
                RPD.STRCTURE.EF.Data.EFCVDecTime = (double)(MakeDWord((ushort)iRetData[410], (ushort)iRetData[411])) / 1000;                //D38410 & D38411 除以1000
                RPD.STRCTURE.LL.Data.LLCVSpeedTime = (double)(MakeDWord((ushort)iRetData[412], (ushort)iRetData[413])) / 1000;              //D38412 & D38413 除以1000
                RPD.STRCTURE.LL.Data.LLCVDecTime = (double)(MakeDWord((ushort)iRetData[414], (ushort)iRetData[415])) / 1000;                //D38414 & D38415 除以1000
                RPD.STRCTURE.TR.Data.TR1CVSpeedTime = (double)(MakeDWord((ushort)iRetData[416], (ushort)iRetData[417])) / 1000;             //D38416 & D38417 除以1000
                RPD.STRCTURE.TR.Data.TR1CVDecTime = (double)(MakeDWord((ushort)iRetData[418], (ushort)iRetData[419])) / 1000;               //D38418 & D38419 除以1000
                RPD.STRCTURE.TR.Data.TR2CVSpeedTime = (double)(MakeDWord((ushort)iRetData[420], (ushort)iRetData[421])) / 1000;             //D38420 & D38421 除以1000
                RPD.STRCTURE.TR.Data.TR2CVDecTime = (double)(MakeDWord((ushort)iRetData[422], (ushort)iRetData[423])) / 1000;               //D38422 & D38423 除以1000
                RPD.STRCTURE.TR.Data.TR3CVSpeedTime = (double)(MakeDWord((ushort)iRetData[424], (ushort)iRetData[425])) / 1000;             //D38424 & D38425 除以1000
                RPD.STRCTURE.TR.Data.TR3CVDecTime = (double)(MakeDWord((ushort)iRetData[426], (ushort)iRetData[427])) / 1000;               //D38426 & D38427 除以1000
                RPD.STRCTURE.UL.Data.ULCVSpeedTime = (double)(MakeDWord((ushort)iRetData[428], (ushort)iRetData[429])) / 1000;              //D38428 & D38429 除以1000
                RPD.STRCTURE.UL.Data.ULCVDecTime = (double)(MakeDWord((ushort)iRetData[430], (ushort)iRetData[431])) / 1000;                //D38430 & D38431 除以1000
                RPD.STRCTURE.DF.Data.DFCVSpeedTime = (double)(MakeDWord((ushort)iRetData[432], (ushort)iRetData[433])) / 1000;              //D38432 & D38433 除以1000
                RPD.STRCTURE.DF.Data.DFCVDecTime = (double)(MakeDWord((ushort)iRetData[434], (ushort)iRetData[435])) / 1000;                //D38434 & D38435 除以1000

                //Add by Henry 2016-1-19
                RPD.STRCTURE.TR.Data.TRCgaugeOffset = (double)(MakeDWord((ushort)iRetData[479], (ushort)iRetData[480])) / 1000000;          //D38479 & D38480 除以10 * 6 次方

                for (int i = 0; i < 112; i++)
                {
                    RPD.STRCTURE.SYS.Data.EC_TABLE[i] = iRetData[400 + i];
                }



                RPD.STRCTURE.EF.Data.EFToLLDelayTime = (double)iRetData[436] / 100;                    //D38436
                RPD.STRCTURE.TR.Data.TR3CVToULDelayTime = (double)iRetData[437] / 100;                 //D38437
                RPD.STRCTURE.UL.Data.ULToUNLDPortDelayTime = (double)iRetData[438] / 100;              //D38438  

                RPD.STRCTURE.SYS.Data.MainWaterHigherLimit = (double)iRetData[439] / 10;                            //D38439
                RPD.STRCTURE.SYS.Data.MainWaterLowerLimit = (double)iRetData[440] / 10;                            //D38440

                RPD.STRCTURE.LL.Data.LLPumpFlowLowerLimit = iRetData[442] / 10;                             //D38442
                RPD.STRCTURE.TR.Data.BackingFlowLowerLimit = iRetData[444] / 10;                            //D38444
                RPD.STRCTURE.UL.Data.ULPumpFlowLowerLimit = iRetData[446] / 10;                             //D38446
                RPD.STRCTURE.LL.Data.LLTPPumpFlowLowerLimit = iRetData[448] / 10;                           //D38448
                RPD.STRCTURE.TR.Data.TR2CHBCOVERFlowLowerLimit = iRetData[450] / 10;                        //D38450
                RPD.STRCTURE.TR.Data.TRTPPumpFlowLowerLimit = iRetData[452] / 10;                           //D38452
                RPD.STRCTURE.TR.Data.TR3CHBCOVERFlowLowerLimit = iRetData[454] / 10;                        //D38454
                RPD.STRCTURE.TR.Data.TR2CoolingFlowLowerLimit = iRetData[456] / 10;                         //D38456
                RPD.STRCTURE.PCC1.Data.PlasmaGeneratorG1FlowLowerLimit = iRetData[458] / 10;                //D38458
                RPD.STRCTURE.PCC1.Data.PlasmaGeneratorG2FlowLowerLimit = iRetData[460] / 10;                //D38460
                RPD.STRCTURE.PCC1.Data.PlasmaGeneratorsteeringcoilFlowLowerLimit = iRetData[462] / 10;      //D38462
                RPD.STRCTURE.PCC1.Data.HearthMainFlowLowerLimit = iRetData[464] / 10;                       //D38464
                RPD.STRCTURE.PCC1.Data.HearthBeamFlowLowerLimit = iRetData[466] / 10;                       //D38466
                RPD.STRCTURE.PCC1.Data.RPDDoorInwallFlowLowerLimit = iRetData[468] / 10;                    //D38468
                RPD.STRCTURE.PCC1.Data.RPDChamberInwallFlowLowerLimit = iRetData[470] / 10;                 //D38470
                RPD.STRCTURE.UL.Data.ULTPPumpFlowLowerLimit = iRetData[472] / 10;                           //D38472

                RPD.STRCTURE.PCC1.Data.IdelTime = iRetData[473] / 10 / 60;                                  //D38473            
                RPD.STRCTURE.PCC1.Data.ReadyTime = iRetData[474] / 10 / 60;                                 //D38474

                RPD.STRCTURE.UL.Data.LLULTargetPreSet = (double)(MakeDWord((ushort)iRetData[475], (ushort)iRetData[476])) / 1000000;              //D38475 & D38476 除以10 * 6 次方

                //Add by Henry 2016/1/12 新增LL/UL SRV 設定/秒
                RPD.STRCTURE.LL.Data.LL_SRVSet_Sec = iRetData[477] / 10;                                   //D38477
                RPD.STRCTURE.UL.Data.UL_SRVSet_Sec = iRetData[478] / 10;                                   //D38478

                //Add by Henry 2016/1/25 新增MFC管線延遲/秒
                RPD.STRCTURE.LL.Data.LL_MFC_Pipeline_Delay_Sec = iRetData[485] / 10;                       //D38485
                RPD.STRCTURE.UL.Data.UL_MFC_Pipeline_Delay_Sec = iRetData[486] / 10;                       //D38486

                 //Add by Henry 2016/2/18 新增LL控壓精度
                RPD.STRCTURE.LL.Data.LLPCAccuracy = iRetData[487];                                         //D38487
                RPD.STRCTURE.UL.Data.ULPCAccuracy = iRetData[488];                                         //D38488

                RPD.STRCTURE.PCC1.Data.RGunCurrentofHearthCoil = (double)iRetData[500] / 10;               //D38500
                RPD.STRCTURE.PCC1.Data.RGunCurrentofSteeringCoil = (double)iRetData[501] / 10;             //D38501
                RPD.STRCTURE.PCC1.Data.RGunCurrentofG2Coil = (double)iRetData[502] / 10;                   //D38502

                int V_Temp = Convert.ToInt16(Convert.ToString(iRetData[503], 2), 2);
                RPD.STRCTURE.PCC1.Data.RGunVoltageofMainHearth = ((double)V_Temp / 10);                    //D38503
                V_Temp = Convert.ToInt16(Convert.ToString(iRetData[504], 2), 2);
                RPD.STRCTURE.PCC1.Data.RGunVoltageofBeamGuide = ((double)V_Temp / 10);                     //D38504
                V_Temp = Convert.ToInt16(Convert.ToString(iRetData[505], 2), 2);
                RPD.STRCTURE.PCC1.Data.RGunDischargeVoltage = ((double)V_Temp / 10);                       //D38505
                RPD.STRCTURE.PCC1.Data.RGunCurrentofMainHearth = (double)iRetData[506] / 10;               //D38506
                RPD.STRCTURE.PCC1.Data.RGunCurrentofBeamGuide = (double)iRetData[507] / 10;                //D38507

                RPD.STRCTURE.PCC1.Data.UseRecipeNumber = iRetData[508];                       //D38508
                RPD.STRCTURE.PCC1.Data.RGunGunArCoolingTimeCounter = iRetData[509];           //D38509
                RPD.STRCTURE.PCC1.Data.RGunAccumulationTimeMainDischargehour = iRetData[510]; //D38510
                RPD.STRCTURE.PCC1.Data.RGunAccumulationTimeMainDischargemin = iRetData[511];  //D38511
                RPD.STRCTURE.PCC1.Data.RGunResistanceValueofBeamGuide_Earth = iRetData[512];  //D38512
                RPD.STRCTURE.PCC1.Data.RGunResistanceValueofMainHearth_Earth = iRetData[513]; //D38513
                RPD.STRCTURE.PCC1.Data.RGunResistanceValueofG1_Earth = iRetData[514];         //D38514
                RPD.STRCTURE.PCC1.Data.RGunResistanceValueofG2_Earth = iRetData[515];         //D38515
                RPD.STRCTURE.PCC1.Data.RGunResistanceValueofBG_BGCoil = iRetData[516];        //D38516
                RPD.STRCTURE.PCC1.Data.RGunArFlowValue = iRetData[517];                       //D38517              
                RPD.STRCTURE.PCC1.Data.ChamberO2FlowValue = iRetData[518];                    //D38518
                RPD.STRCTURE.PCC1.Data.ChamberArFlowValue = iRetData[519];                    //D38519

                RPD.STRCTURE.PCC1.Data.IgnitionRunning = ((iRetData[522] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.IgnitionOK = ((iRetData[522] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.IgnitionStopping = ((iRetData[522] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.IgnitionStop = ((iRetData[522] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.CoolingStopping = ((iRetData[522] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.CoolingStop = ((iRetData[522] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.MainDischargeAnodeChange = ((iRetData[522] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.MainDischarge = ((iRetData[522] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.BeamGuideDischargeAnodeChang = ((iRetData[522] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.BeamGuideDischarge = ((iRetData[522] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.AutoGunIgnitionOperation = ((iRetData[522] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.AutoGunCoolingOperation = ((iRetData[522] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.AutoGunCurrentChangeOperation = ((iRetData[522] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.AutoPushupcommand = ((iRetData[522] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.RemoteOK = ((iRetData[522] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);

                RPD.STRCTURE.PCC1.Data.IgnitionModeAutoMODE = ((iRetData[523] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.AutoIgnitionReady = ((iRetData[523] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.AutoCoolingReady = ((iRetData[523] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.AutoCurrentChangeReady = ((iRetData[523] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.CoilPSOK = ((iRetData[523] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.SubPSOK = ((iRetData[523] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.MainPSOK = ((iRetData[523] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.OPOK = ((iRetData[523] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ResistanceMeasurement = ((iRetData[523] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.CoatingRunningOK = ((iRetData[523] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);

                RPD.STRCTURE.PCC1.Data.ProgramInitializationComplete = ((iRetData[524] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.COILPOWERMCCBON = ((iRetData[524] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.MAINDISPOWERMCON = ((iRetData[524] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.TBChargingRequest = ((iRetData[524] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.SystemNoError = ((iRetData[524] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.SystemAlarm = ((iRetData[524] & ((int)(Math.Pow(2, 14)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.SystemWarning = ((iRetData[524] & ((int)(Math.Pow(2, 15)))) == 0 ? 0 : 1);

                RPD.STRCTURE.PCC1.Data.TurnTableCWCommand = ((iRetData[525] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.TurnTableCCWCommand = ((iRetData[525] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingUpperGateOpenCommand = ((iRetData[525] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingUpperGateClosCommand = ((iRetData[525] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingLowerGateOpenCommand = ((iRetData[525] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingLowerGateClosCommand = ((iRetData[525] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingRoomVaccumCommand = ((iRetData[525] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingRoomBreakCommand = ((iRetData[525] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingRoomVacuumOperationCMD = ((iRetData[525] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ArOpenCommand = ((iRetData[525] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChamberO2OpenCommand = ((iRetData[525] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChamberArOpenCommand = ((iRetData[525] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);

                RPD.STRCTURE.PCC1.Data.MHPushupUpperLS = ((iRetData[526] & ((int)(Math.Pow(2, 0)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.MHPushupLowerLS = ((iRetData[526] & ((int)(Math.Pow(2, 1)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingPushupUpperLS = ((iRetData[526] & ((int)(Math.Pow(2, 2)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingPushupUpperPosition = ((iRetData[526] & ((int)(Math.Pow(2, 3)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingPushupLowerPosition = ((iRetData[526] & ((int)(Math.Pow(2, 4)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingPushupLowerLS = ((iRetData[526] & ((int)(Math.Pow(2, 5)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.TurnTableCWLS = ((iRetData[526] & ((int)(Math.Pow(2, 6)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.TurnTableCCWLS = ((iRetData[526] & ((int)(Math.Pow(2, 7)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingUpperGateOpenLS = ((iRetData[526] & ((int)(Math.Pow(2, 8)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingUpperGateCloseLS = ((iRetData[526] & ((int)(Math.Pow(2, 9)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingLowerGateOpenLS = ((iRetData[526] & ((int)(Math.Pow(2, 10)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingLowerGateCloseLS = ((iRetData[526] & ((int)(Math.Pow(2, 11)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.ChargingRoomTabletDetectSensor = ((iRetData[526] & ((int)(Math.Pow(2, 12)))) == 0 ? 0 : 1);
                RPD.STRCTURE.PCC1.Data.HearthCleanerPositionOKDetect = ((iRetData[526] & ((int)(Math.Pow(2, 13)))) == 0 ? 0 : 1);

                RPD.STRCTURE.PCC1.Data.RGunQtyofRevolverTablet = iRetData[528];                                //D38528
                RPD.STRCTURE.PCC1.Data.RGunMHpush_uplevel = (double)iRetData[529] / 100;                       //D38529
                RPD.STRCTURE.PCC1.Data.RGunChargingpush_uplevel = (double)iRetData[530] / 100;                 //D38530
                RPD.STRCTURE.PCC1.Data.RGunRevolverangle = iRetData[531];                                      //D38531
                RPD.STRCTURE.PCC1.Data.ProcessCount = iRetData[532];                                           //D38532
                RPD.STRCTURE.PCC1.Data.ProcessP = (double)(MakeDWord((ushort)iRetData[535], (ushort)iRetData[536])); //D38535 & D38536   

                RPD.STRCTURE.PCC1.Data.LLMFCO2Set1 = iRetData[540];                                            //D38540
                RPD.STRCTURE.PCC1.Data.LLMFCArSet1 = iRetData[541];                                            //D38541
                RPD.STRCTURE.PCC1.Data.LLMFCO2Set2 = iRetData[542];                                            //D38542
                RPD.STRCTURE.PCC1.Data.LLMFCArSet2 = iRetData[543];                                            //D38543
                RPD.STRCTURE.PCC1.Data.LLMFCO2Set3 = iRetData[544];                                            //D38544
                RPD.STRCTURE.PCC1.Data.LLMFCArSet3 = iRetData[545];                                            //D38545

                RPD.STRCTURE.PCC1.Data.ULMFCO2Set1 = iRetData[567];                                            //D38567
                RPD.STRCTURE.PCC1.Data.ULMFCArSet1 = iRetData[568];                                            //D38568
                RPD.STRCTURE.PCC1.Data.ULMFCO2Set2 = iRetData[569];                                            //D38569
                RPD.STRCTURE.PCC1.Data.ULMFCArSet2 = iRetData[570];                                            //D38570
                RPD.STRCTURE.PCC1.Data.ULMFCO2Set3 = iRetData[571];                                            //D38571
                RPD.STRCTURE.PCC1.Data.ULMFCArSet3 = iRetData[572];                                            //D38572

                RPD.STRCTURE.EF.Map.EFCVSpeed = (double)(MakeDWord((ushort)iRetData[600], (ushort)iRetData[601])) / 100;             //D38600 & D38601 除以100
                RPD.STRCTURE.LL.Map.LLCVSpeed = (double)(MakeDWord((ushort)iRetData[602], (ushort)iRetData[603])) / 100;             //D38602 & D38603 除以100
                RPD.STRCTURE.TR.Map.TR1CVSpeed = (double)(MakeDWord((ushort)iRetData[604], (ushort)iRetData[605])) / 100;            //D38604 & D38605 除以100
                RPD.STRCTURE.TR.Map.TR2CVSpeed = (double)(MakeDWord((ushort)iRetData[606], (ushort)iRetData[607])) / 100;            //D38606 & D38607 除以100
                RPD.STRCTURE.TR.Map.TR3CVSpeed = (double)(MakeDWord((ushort)iRetData[608], (ushort)iRetData[609])) / 100;            //D38608 & D38609 除以100
                RPD.STRCTURE.UL.Map.ULCVSpeed = (double)(MakeDWord((ushort)iRetData[610], (ushort)iRetData[611])) / 100;             //D38610 & D386011 除以100
                RPD.STRCTURE.DF.Map.DFCVSpeed = (double)(MakeDWord((ushort)iRetData[612], (ushort)iRetData[613])) / 100;             //D38612 & D386013 除以100


                RPD.STRCTURE.EF.Map.TaryRegistration = iRetData[1000];                              //D39000
                RPD.STRCTURE.LL.Map.TaryRegistration = iRetData[1100];                              //D39100
                RPD.STRCTURE.TR.Map.TR1_TaryRegistration = iRetData[1300];                          //D39300
                RPD.STRCTURE.TR.Map.TR2_TaryRegistration = iRetData[1400];                          //D39400
                RPD.STRCTURE.TR.Map.TR3_TaryRegistration = iRetData[1500];                          //D39500
                RPD.STRCTURE.UL.Map.TaryRegistration = iRetData[1700];                              //D39700
                RPD.STRCTURE.DF.Map.TaryRegistration = iRetData[1800];                              //D39800

                RPD.STRCTURE.EF.Data.TD_TrayID = iRetData[1001];                                    //D39001
                RPD.STRCTURE.EF.Data.TD_Spare1 = iRetData[1002];                                    //D39002
                RPD.STRCTURE.EF.Data.TD_Spare2 = iRetData[1003];                                    //D39003
                RPD.STRCTURE.EF.Data.TD_Spare3 = iRetData[1004];                                    //D39004
                RPD.STRCTURE.EF.Data.TD_Spare4 = iRetData[1005];                                    //D39005
                RPD.STRCTURE.EF.Data.TD_Spare5 = iRetData[1006];                                    //D39006

                RPD.STRCTURE.LL.Data.TD_TrayID = iRetData[1101];                                    //D39101
                RPD.STRCTURE.LL.Data.TD_Spare1 = iRetData[1102];                                    //D39102
                RPD.STRCTURE.LL.Data.TD_Spare2 = iRetData[1103];                                    //D39103
                RPD.STRCTURE.LL.Data.TD_Spare3 = iRetData[1104];                                    //D39104
                RPD.STRCTURE.LL.Data.TD_Spare4 = iRetData[1105];                                    //D39105
                RPD.STRCTURE.LL.Data.TD_Spare5 = iRetData[1106];                                    //D39106

                RPD.STRCTURE.TR.Data.TD_TR1_TrayID = iRetData[1301];                                //D39301
                RPD.STRCTURE.TR.Data.TD_TR1_Spare1 = iRetData[1302];                                //D39202
                RPD.STRCTURE.TR.Data.TD_TR1_Spare2 = iRetData[1303];                                //D39203
                RPD.STRCTURE.TR.Data.TD_TR1_Spare3 = iRetData[1304];                                //D39204
                RPD.STRCTURE.TR.Data.TD_TR1_Spare4 = iRetData[1305];                                //D39205
                RPD.STRCTURE.TR.Data.TD_TR1_Spare5 = iRetData[1306];                                //D39206

                RPD.STRCTURE.TR.Data.TD_TR2_TrayID = iRetData[1401];                                //D39401
                RPD.STRCTURE.TR.Data.TD_TR2_Spare1 = iRetData[1402];                                //D39402
                RPD.STRCTURE.TR.Data.TD_TR2_Spare2 = iRetData[1403];                                //D39403
                RPD.STRCTURE.TR.Data.TD_TR2_Spare3 = iRetData[1404];                                //D39404
                RPD.STRCTURE.TR.Data.TD_TR2_Spare4 = iRetData[1405];                                //D39405
                RPD.STRCTURE.TR.Data.TD_TR2_Spare5 = iRetData[1406];                                //D39406

                RPD.STRCTURE.TR.Data.TD_TR3_TrayID = iRetData[1501];                                //D39501
                RPD.STRCTURE.TR.Data.TD_TR3_Spare1 = iRetData[1502];                                //D39502
                RPD.STRCTURE.TR.Data.TD_TR3_Spare2 = iRetData[1503];                                //D39503
                RPD.STRCTURE.TR.Data.TD_TR3_Spare3 = iRetData[1504];                                //D39504
                RPD.STRCTURE.TR.Data.TD_TR3_Spare4 = iRetData[1505];                                //D39505
                RPD.STRCTURE.TR.Data.TD_TR3_Spare5 = iRetData[1506];                                //D39506

                RPD.STRCTURE.UL.Data.TD_TrayID = iRetData[1701];                                    //D39701
                RPD.STRCTURE.UL.Data.TD_Spare1 = iRetData[1702];                                    //D39702
                RPD.STRCTURE.UL.Data.TD_Spare2 = iRetData[1703];                                    //D39703
                RPD.STRCTURE.UL.Data.TD_Spare3 = iRetData[1704];                                    //D39704
                RPD.STRCTURE.UL.Data.TD_Spare4 = iRetData[1705];                                    //D39705
                RPD.STRCTURE.UL.Data.TD_Spare5 = iRetData[1706];                                    //D39706

                RPD.STRCTURE.DF.Data.TD_TrayID = iRetData[1801];                                    //D39801
                RPD.STRCTURE.DF.Data.TD_Spare1 = iRetData[1802];                                    //D39802
                RPD.STRCTURE.DF.Data.TD_Spare2 = iRetData[1803];                                    //D39803
                RPD.STRCTURE.DF.Data.TD_Spare3 = iRetData[1804];                                    //D39804
                RPD.STRCTURE.DF.Data.TD_Spare4 = iRetData[1805];                                    //D39805
                RPD.STRCTURE.DF.Data.TD_Spare5 = iRetData[1806];                                    //D39806

                RPD.STRCTURE.LL.Data.Record_LLReadyToGo_P = (double)(MakeDWord((ushort)iRetData[1950], (ushort)iRetData[1951])) / 1000000;              //D39950 & D39951 除以10 * 6 次方
                RPD.STRCTURE.LL.Data.Record_LLMFCO2_Flow = iRetData[1952];                          //D39952
                RPD.STRCTURE.LL.Data.Record_LLMFCAr_Flow = iRetData[1953];                          //D39953
                RPD.STRCTURE.LL.Data.Record_LLReadyToVent_P = (double)(MakeDWord((ushort)iRetData[1954], (ushort)iRetData[1955])) / 1000000;            //D39954 & D39955 除以10 * 6 次方
                RPD.STRCTURE.LL.Data.Record_LLPumpDownTo50m_Time = iRetData[1956] / 10;                  //D39956
                RPD.STRCTURE.LL.Data.Record_LLPumpDownTo1m_Time = iRetData[1957] / 10;                   //D39957

                RPD.STRCTURE.UL.Data.Record_ULReadyToGo_P = (double)(MakeDWord((ushort)iRetData[1960], (ushort)iRetData[1961])) / 1000000;              //D39960 & D39961 除以10 * 6 次方
                RPD.STRCTURE.UL.Data.Record_ULMFCO2_Flow = iRetData[1962];                           //D39962
                RPD.STRCTURE.UL.Data.Record_ULMFCAr_Flow = iRetData[1963];                          //D39963
                RPD.STRCTURE.UL.Data.Record_ULReadyToVent_P = (double)(MakeDWord((ushort)iRetData[1964], (ushort)iRetData[1965])) / 1000000;            //D39964 & D39965 除以10 * 6 次方
                RPD.STRCTURE.UL.Data.Record_ULPumpDownTo50m_Time = iRetData[1966] / 10;                  //D39966
                RPD.STRCTURE.UL.Data.Record_ULPumpDownTo1m_Time = iRetData[1967] / 10;                   //D39967

                // Add by Albert 201905
                RPD.STRCTURE.TR.Map.W_1_Heater_Temp = iRetData[1990] / 10;                               //D39990
                RPD.STRCTURE.TR.Map.W_2_Heater_Temp = iRetData[1991] / 10;                               //D39991
                RPD.STRCTURE.TR.Map.W_3_Heater_Temp = iRetData[1992] / 10;                               //D39992
                //

                
            }
            else
            {
                _bConn = false;
            }

        }



        #endregion

        #region Methods


        /// Triggle 目標 Bit ON or Off
        /// <summary>
        /// Triggle 目標 Bit
        /// </summary>
        /// <param name="sAddr">目標位置</param>
        /// <param name="iValue">Bit 0 or 1</param>
        public static void WriPLC_Bit(string sAddr, bool iValue)
        {
            lock (_objPLC1)
            {
                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }

                switch (iValue)
                {
                    case true:
                        if (FunSingleWriPLC(sAddr, 1) == true)
                        {
                            FunWriteLog(string.Format("WriPLC_Bit 成功[Addr:{0},Value:{1}]", sAddr, 1));
                        }
                        else
                        {
                            FunWriteLog(string.Format("WriPLC_Bit 失敗[Addr:{0},Value:{1}]", sAddr, 1));
                        }
                        break;
                    case false:
                        if (FunSingleWriPLC(sAddr, 0) == true)
                        {
                            FunWriteLog(string.Format("WriPLC_Bit 成功[Addr:{0},Value:{1}]", sAddr, 0));
                        }
                        else
                        {
                            FunWriteLog(string.Format("WriPLC_Bit 失敗[Addr:{0},Value:{1}]", sAddr, 0));
                        }
                        break;
                }
            }
        }

        /// Triggle 目標 Bit ON to Off one plus 0.5 sec
        /// <summary>
        /// Triggle 目標 Bit ON to Off one plus 0.5 sec
        /// </summary>
        /// <param name="sAddr">目標位置</param>
        public static void WriPLC_Pulse(string sAddr)
        {
            lock (_objPLC1)
            {
                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }

                if (FunSingleWriPLC(sAddr, 1) == true)
                {
                    ThreadPulse T_PulseAddr = new ThreadPulse(sAddr);
                    Thread T_Pulse = new Thread(new ThreadStart(T_PulseAddr.ThreadProc));
                    T_Pulse.Start();
                }
                else
                {
                    FunWriteLog(string.Format("WriPLC_Plus 失敗[Addr:{0}]", sAddr));
                }



            }
        }

        /// Write 目標 Word Value
        /// <summary>
        /// Write 目標 Word Value
        /// </summary>
        /// <param name="sAddr">目標位置</param>
        /// <param name="iValue">Value</param>
        public static void WriPLC_Word(string sAddr, string sValue)
        {
            lock (_objPLC1)
            {
                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }
                if (FunSingleWriPLC(sAddr, int.Parse(sValue)) == true)
                {
                    FunWriteLog(string.Format("WriPLC_Word 成功[Addr:{0},Value:{1}]", sAddr, sValue));
                }
                else
                {
                    FunWriteLog(string.Format("WriPLC_Word 失敗[Addr:{0},Value:{1}]", sAddr, sValue));
                }
            }
        }

        /// 批次 Write 目標 Word Value 
        /// <summary>
        /// 批次 目標 Word Value
        /// </summary>
        /// <param name="sAddr">目標位置</param>
        /// <param name="iValue">Value</param>
        public static void WriPLCBatch_Word(string sAddr, ref int[] iRetData)
        {
            lock (_objPLC1)
            {
                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }
                if (FunBatchWriPLC(sAddr, ref iRetData) == true)
                {
                    FunWriteLog(string.Format("WriPLCBatch_Word 成功[起始位置:{0},Length:{1}]", sAddr, iRetData.Length));
                }
                else
                {
                    FunWriteLog(string.Format("WriPLCBatch_Word 失敗[起始位置:{0},Length:{1}]", sAddr, iRetData.Length));
                }
            }
        }

        /// 批次 讀取 目標 Word Value 
        /// <summary>
        /// 批次 讀取 Word Value
        /// </summary>
        /// <param name="sAddr">目標位置</param>
        /// <param name="iValue">Value</param>
        public static bool ReadPLCBatch_Word(string sAddr, ref int[] iRetData)
        {
            lock (_objPLC1)
            {
                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }
                if (FunBatchReadPLC(sAddr, ref iRetData) == true)
                {
                    //FunWriteLog(string.Format("ReadPLCBatch_Word 成功[起始位置:{0},Length:{1}]", sAddr, iRetData.Length));
                    return true;
                }
                else
                {
                    FunWriteLog(string.Format("ReadPLCBatch_Word 失敗[起始位置:{0},Length:{1}]", sAddr, iRetData.Length));
                    return false;
                }
            }
        }


        /// 讀取 目標 Bit Value
        /// <summary>
        /// 讀取 目標 Bit Value
        /// </summary>
        /// <param name="sAddr">目標位置</param>
        /// <returns>Value</returns>
        public static int ReadPLC_Bit(string sAddr)
        {
            int iValue = 0;
            lock (_objPLC1)
            {
                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }

                if (FunSingleReadPLC(sAddr, ref iValue) == true)
                {
                    FunWriteLog(string.Format("ReadPLC_Bit 成功[Addr:{0},Value:{1}]", sAddr, iValue));
                    return iValue;
                }
                else
                {
                    FunWriteLog(string.Format("ReadPLC_Bit 失敗[Addr:{0},Value:{1}]", sAddr, iValue));
                    return iValue;
                }
            }
        }

        /// 讀取 目標 Word Value
        /// <summary>
        /// 讀取 目標 Word Value
        /// </summary>
        /// <param name="sAddr">目標位置</param>
        /// <returns>字串</returns>
        public static string ReadPLC_Word(string sAddr)
        {
            int sValue = 0;
            lock (_objPLC1)
            {
                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }
                if (FunSingleReadPLC(sAddr, ref sValue) == true)
                {
                    FunWriteLog(string.Format("ReadPLC_Bit 成功[Addr:{0},Value:{1}]", sAddr, sValue));
                    return sValue.ToString();
                }
                else
                {
                    FunWriteLog(string.Format("ReadPLC_Bit 失敗[Addr:{0},Value:{1}]", sAddr, sValue));
                    return sValue.ToString();
                }
            }
        }

        /// 轉換 目標為 Double Word
        /// <summary>
        /// 取得 Double Word
        /// </summary>
        /// <param name="W1">第一個word</param>
        /// <param name="W2">第二個word</param>
        /// <returns>Double Word</returns>
        public static uint MakeDWord(ushort a, ushort b)
        {
            return (uint)(a + (b << 16));
        }

        public static void IntToWord(Int32 Value, string AddressLow, string AddressHigh)
        {
            string sValue = Convert.ToString(Value, 2).PadLeft(32, '0');
            clsPLC.WriPLC_Word(AddressHigh, Convert.ToInt32(sValue.Substring(0, 16), 2).ToString());  //High
            clsPLC.WriPLC_Word(AddressLow, Convert.ToInt32(sValue.Substring(16, 16), 2).ToString()); //Low
        }

        #endregion

        #region dll functions

        /// 開啟 Plc 連線
        /// <summary>
        /// 開啟 Plc 連線
        /// </summary>
        /// <returns>成功與否</returns>
        private static bool FunOpenPLC()
        {
            try
            {

                _objPLC1.ActLogicalStationNumber = _iPlcSN;

                if (_objPLC1.Open() == 0) //開啟成功
                {
                    FunWriteLog("開啟 PLC 連線成功");
                    _bConn = true;
                    return true;
                }
                else
                { throw new Exception(); }

            }
            catch
            {
                FunWriteLog("開啟 PLC 連線失敗請檢查 MX component Logical Station Number");
                return false;
            }
        }

        /// 關閉 Plc 連線
        /// <summary>
        /// 關閉 Plc 連線
        /// </summary>
        /// <returns>成功與否</returns>
        private static bool FunClosePLC()
        {
            try
            {

                _objPLC1.ActLogicalStationNumber = _iPlcSN;

                if (_objPLC1.Close() == 0) //關閉成功
                {
                    FunWriteLog("關閉 PLC 連線成功");
                    _bConn = false;
                    return true;
                }
                else
                { throw new Exception(); }

            }
            catch
            {
                FunWriteLog("關閉 PLC 連線失敗請檢查 MX component Logical Station Number");
                return false;
            }
        }

        /// 單次讀取 Plc Data
        /// <summary>
        /// 單次讀取 Plc Data
        /// </summary>
        /// <param name="sAddr">點位</param>
        /// <param name="iRetData">讀取值放置位址</param>
        /// <returns>成功與否</returns>
        private static bool FunSingleReadPLC(string sAddr, ref int iRetData)
        {
            try
            {

                int iRtn;

                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }

                iRtn = _objPLC1.GetDevice(sAddr, out iRetData);
                if (iRtn == 0)
                {
                    FunWriteLog(string.Format("FunSingleReadPLC 成功[Addr:{0},iRetData:{1}]", sAddr, iRetData));
                    return true;
                }
                else
                {
                    FunWriteLog(string.Format("FunSingleReadPLC 失敗[Addr:{0},iRetData:{1}]", sAddr, iRetData));
                    return false;
                }

            }
            catch (Exception ex)
            {
                FunWriteLog(string.Format("FunSingleReadPLC 失敗[Addr:{0},iRetData:{1}]", sAddr, iRetData));
                return false;
            }
        }

        /// 批次讀取 Plc Data
        /// <summary>
        /// 批次讀取 Plc Data
        /// </summary>
        /// <param name="sAddr">起始位置</param>
        /// <param name="iRetData">寫入目標陣列</param>
        /// <returns>成功與否</returns>
        private static bool FunBatchReadPLC(string sAddr, ref int[] iRetData)
        {
            try
            {
                int iLength = 1;
                int iRtn;
                iLength = iRetData.Length;

                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }

                iRtn = _objPLC1.ReadDeviceBlock(sAddr, iLength, out iRetData[0]);
                if (iRtn == 0)
                {
                    _bConn = true;

                    //FunWriteLog(string.Format("FunReadPLC 成功[Addr:{0},Length:{1}]", sAddr, iLength));
                    return true;
                }
                else
                {
                    FunWriteLog(string.Format("FunReadPLC 失敗[Addr:{0},Length:{1}]", sAddr, iLength));
                    return false;
                }

            }
            catch (Exception ex)
            {
                FunWriteLog(string.Format("FunReadPLC 失敗[Addr:{0},Message:{1}]", sAddr, ex.Message));
                return false;
            }
        }

        /// 單次寫入 Plc Data
        /// <summary>
        /// 單次寫入 Plc Data
        /// </summary>
        /// <param name="sAddr">點位</param>
        /// <param name="iRetData">寫入數值</param>
        /// <returns>成功與否</returns>
        private static bool FunSingleWriPLC(string sAddr, int iSetData)
        {
            try
            {

                int iRtn;

                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }

                iRtn = _objPLC1.SetDevice(sAddr, iSetData);
                if (iRtn == 0)
                {
                    FunWriteLog("FunSingleWriPLC 成功[" + sAddr + ":" + iSetData.ToString() + "]");
                    return true;
                }
                else
                {
                    FunWriteLog("FunSingleWriPLC 失敗[" + sAddr + ":" + iSetData.ToString() + "]");
                    return false;
                }

            }
            catch
            {
                return false;
            }

        }

        /// 批次寫入 Plc Data
        /// <summary>
        /// 批次寫入 Plc Data
        /// </summary>
        /// <param name="sAddr">起始位置</param>
        /// <param name="iRetData">寫入目標陣列</param>
        /// <returns>成功與否</returns>
        private static bool FunBatchWriPLC(string sAddr, ref int[] iRetData)
        {
            try
            {
                int iLength = 1;
                int iRtn;
                iLength = iRetData.Length;

                if (!_bConn) //判斷若斷線則重啟 PLC 連線
                {
                    FunOpenPLC();
                }

                iRtn = _objPLC1.WriteDeviceBlock(sAddr, iLength, ref iRetData[0]);
                if (iRtn == 0)
                {
                    FunWriteLog(string.Format("FunBatchWriPLC 成功[Addr:{0},Length:{1}]", sAddr, iLength));
                    return true;
                }
                else
                {
                    FunWriteLog(string.Format("FunBatchWriPLC 失敗[Addr:{0},Length:{1}]", sAddr, iLength));
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        ///寫 Plc Log Function
        /// <summary>
        /// 寫入 Plc Log 至txt檔
        /// </summary>
        /// <param name="sTemp">內容</param>
        private static void FunWriteLog(string sTemp)
        {
            try
            {
                string sFileName = null;
                sFileName = System.AppDomain.CurrentDomain.BaseDirectory;
                sFileName = sFileName + "\\WriPlcTestlog";
                if (System.IO.Directory.Exists(sFileName) == false)
                {
                    System.IO.Directory.CreateDirectory(sFileName);
                }

                string sFile = "";
                sFile = System.DateTime.Now.ToString("yyyyMMdd") + ".log";
                sFileName = sFileName + "\\" + sFile;

                sTemp = "[" + System.DateTime.Now.ToString("HH:mm:ss.fff") + "] " + sTemp;
                if (System.IO.File.Exists(sFileName) == false)
                {
                    using (System.IO.StreamWriter sw = System.IO.File.CreateText(sFileName))
                    {
                        sw.WriteLine(sTemp);
                        sw.Close();
                    }
                }
                else
                {
                    using (System.IO.StreamWriter sw = System.IO.File.AppendText(sFileName))
                    {
                        sw.WriteLine(sTemp);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch
            {

            }
        }

        #endregion



    }





}
