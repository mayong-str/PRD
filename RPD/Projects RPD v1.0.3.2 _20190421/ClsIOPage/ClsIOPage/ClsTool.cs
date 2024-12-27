using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsIOPage
{
    class ClsTool
    {
        //異常視窗
        public static void ShowErrMsg(string sMessage)
        {
            System.Windows.Forms.MessageBox.Show(sMessage,
                                                 "RPD",
                                                 System.Windows.Forms.MessageBoxButtons.OK,
                                                 System.Windows.Forms.MessageBoxIcon.Error);
        }

        //警告視窗
        public static void ShowWarningMsg(string sMessage)
        {

            //Msg.ShowDialog();
            System.Windows.Forms.MessageBox.Show(sMessage,
                                                 "RPD",
                                                 System.Windows.Forms.MessageBoxButtons.OK,
                                                 System.Windows.Forms.MessageBoxIcon.Warning);
        }

        //提示視窗
        public static void ShowInformationMsg(string sMessage)
        {
            System.Windows.Forms.MessageBox.Show(sMessage,
                                             "RPD",
                                             System.Windows.Forms.MessageBoxButtons.OK,
                                             System.Windows.Forms.MessageBoxIcon.Information);

        }

        //選擇視窗 
        public static bool ShowQuestionMsg(string sMessage)
        {
            if (System.Windows.Forms.MessageBox.Show(sMessage,
                                                     "RPD",
                                                     System.Windows.Forms.MessageBoxButtons.OKCancel,
                                                     System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判斷值是否為數字 double
        public static bool IsNumeric(string sValue)
        {
            try
            {
                double d = Convert.ToDouble(sValue);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
