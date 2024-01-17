namespace SalaryAndTaxCalculationConsole
{
    public class PTKP
    {
        public enum Code
        {
            TK0,
            TK1,
            TK2,
            TK3,
            K0,
            K1,
            K2,
            K3
        }
        public class Value
        {
            public const double TK0 = 54000000;
            public const double TK1 = 58500000;
            public const double TK2 = 63000000;
            public const double TK3 = 67500000;
            public const double K0 = 58500000;
            public const double K1 = 63000000;
            public const double K2 = 67500000;
            public const double K3 = 72000000;
        }

        private static readonly Dictionary<PTKP.Code, double> dictPTKP = new Dictionary<PTKP.Code, double>()
        {
            {Code.TK0, Value.TK0},
            {Code.TK1, Value.TK1},
            {Code.TK2, Value.TK2},
            {Code.TK3, Value.TK3},
            {Code.K0, Value.K0},
            {Code.K1, Value.K1},
            {Code.K2, Value.K2},
            {Code.K3, Value.K3}
        };
        public static double GetPTKP(PTKP.Code enumPTKP)
        {
            return dictPTKP[enumPTKP];
        }
        public static double GetPTKP(string strPTKP)
        {
            return Enum.TryParse<PTKP.Code>(strPTKP, out var enumPTKP) ? GetPTKP(enumPTKP) : 0.0;
        }


    }
}
