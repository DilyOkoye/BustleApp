using System;
namespace BustleApp_api.Utilities
{
    public static class Formatters
    {
        public static string FormatAmount(decimal? amount)
        {
            if (amount != null )
            {

              return  String.Format("{0:n}", amount);
            }

            return null;
        }
    }
}
