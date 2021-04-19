using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Shop_Bridge.Utility
{
    public class Util
    {
        /*'*********************************************************************************
         * 'Function Name  - SpecialCharCheck()
         * 'Parameters     - Input
         * 'Description    - Code to check for special character in JSON input
         * '*********************************************************************************/
        public bool SpecialCharCheck(dynamic input)
        {
            bool spccharcheck = false;
            string strInp = Convert.ToString(input);
            string specialChar = @"|=?»«£§€;'<>`";
            foreach (var item in specialChar)
            {
                if (strInp.Contains(item))
                {
                    spccharcheck = true;
                    break;
                }
                else
                {
                    spccharcheck = false;
                }
            }
            return spccharcheck;
        }

        /*'*********************************************************************************
        * 'Function Name  - emailAdressCheck()
        * 'Parameters     - emailID
        * 'Description    - Code to check for EmaiID Validations
        * '*********************************************************************************/
        public bool emailAdressCheck(string emailID)
        {
            bool IsValid = false;
            string emailRegex = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
            Regex re = new Regex(emailRegex);
            if (!re.IsMatch(emailID))
            {
                return IsValid;
            }
            else
            {
                IsValid = true;
            }
            return IsValid;
            
        }
    }
}