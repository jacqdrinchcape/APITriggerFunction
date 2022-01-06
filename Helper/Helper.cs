using System;
using System.Collections.Generic;
using System.Text;

namespace APITriggerFunction.Helper
{
    public static class Helper
    {
        public static string ErrorMessage(int errorCode)
        {
            string errorMessage = string.Empty;
            switch (errorCode)
            {
                case 0:
                    errorMessage = "success";
                    break;
                case 1:
                    errorMessage = "error";
                    break;

                case 2:
                    errorMessage = "validation";
                    break;

                case 3:
                    errorMessage = "duplicate";
                    break;

                case 4:
                    errorMessage = "invalid vin number";
                    break;
                default:
                    errorMessage = "error";
                    break;
            }

            return errorMessage;

        }

    }
}
