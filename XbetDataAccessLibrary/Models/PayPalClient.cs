using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public static class PayPalClient
    {
        //PayPal fields
        public static string SandboxClientId { get; set; } =
                             "AYBqN4OuHgNob-xgAlid2aDweFNmAtodoHkYu8BObmrmZu3BfpSV9IUyfUXzIQcKSzAyJqdWTshCeiHe";
        public static string SandboxClientSecret { get; set; } =
                             "ENc4HtS_45_61tXN3D4yLI8UqjhfFjIbCrdjbcgyfx8HH3ntH54nSeL9hPgIuZ_U-gcX7c0339dyWzjQ";

        public static string LiveClientId { get; set; } =
                      "<alert>{PayPal LIVE Client Id}</alert>";
        public static string LiveClientSecret { get; set; } =
                      "<alert>{PayPal LIVE Client Secret}</alert>";
    }
}
