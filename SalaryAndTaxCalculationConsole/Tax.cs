namespace SalaryAndTaxCalculationConsole
{
    public class Tax
    {

        public enum Ter
        {
            A,
            B,
            C,
        }

        public class Constant
        {
            public const double TIER_1_MAX = 60000000;
            public const double TIER_2_MAX = 250000000;
            public const double TIER_3_MAX = 500000000;
            public const double TIER_4_MAX = 5000000000;

            public const double TIER_1_PERCENTAGE = 0.05; //5%
            public const double TIER_2_PERCENTAGE = 0.15; //15%
            public const double TIER_3_PERCENTAGE = 0.25; //25%
            public const double TIER_4_PERCENTAGE = 0.30; //30%
            public const double TIER_5_PERCENTAGE = 0.35; //35%
        }

        public static double TaxPercentage(double value)
        {
            //TIER: 4
            if (value > Constant.TIER_3_MAX)
            {
                return Constant.TIER_4_PERCENTAGE;
            }

            //TIER: 3
            if (value > Constant.TIER_2_MAX)
            {
                return Constant.TIER_3_PERCENTAGE;
            }

            //TIER: 2
            if (value > Constant.TIER_1_MAX)
            {
                return Constant.TIER_2_PERCENTAGE;
            }

            //TIER: 1
            if (value <= Constant.TIER_1_MAX)
            {
                return Constant.TIER_1_PERCENTAGE;
            }

            return 0;
        }

        public static double GrossUpNatura(double annualBIK, double annualSalary)
        {
            double percentage = TaxPercentage(annualSalary);
            return annualBIK / (1 - percentage) - annualBIK;
        }

        public static Ter GetTerByPTKP(PTKP.Code code)
        {
            if (code == PTKP.Code.TK0 || code == PTKP.Code.TK1 || code == PTKP.Code.K0)
            {
                return Ter.A;
            }

            if (code == PTKP.Code.TK2 || code == PTKP.Code.K1 || code == PTKP.Code.TK3 || code == PTKP.Code.K2)
            {
                return Ter.B;
            }

            //PTKP.Code.K3
            return Ter.C;
        }

        public static List<Tuple<double, double, double>>? GetTerTuple(Ter ter)
        {
            switch (ter)
            {
                case (Ter.A):
                    return TerA;
                case (Ter.B):
                    return TerB;
                case (Ter.C):
                    return TerC;
                default:
                    return null;
            }
        }

        public static double TaxTerCalculation(double grossSalary, double annualTax, double percentage, double currentMonth)
        {
            double monthlyTaxEffective = (grossSalary * percentage) / 100;
            if (currentMonth == 12)
            {
                return annualTax - (11 * monthlyTaxEffective);
            }

            return monthlyTaxEffective;
        }

        public static List<Tuple<double, double, double>> TerA = new List<Tuple<double, double, double>>
        {
            Tuple.Create(0.0, 5400000.0, 0.00),
            Tuple.Create(5400001.0, 5650000.0, 0.25),
            Tuple.Create(5650001.0, 5950000.0, 0.50),
            Tuple.Create(5950001.0, 6300000.0, 0.75),
            Tuple.Create(6300001.0, 6750000.0, 1.00),
            Tuple.Create(6750001.0, 7500000.0, 1.25),
            Tuple.Create(7500001.0, 8550000.0, 1.50),
            Tuple.Create(8550001.0, 9650000.0, 1.75),
            Tuple.Create(9650001.0, 10050000.0, 2.00),
            Tuple.Create(10050001.0, 10350000.0, 2.25),
            Tuple.Create(10350001.0, 10700000.0, 2.50),
            Tuple.Create(10700001.0, 11050000.0, 3.00),
            Tuple.Create(11050001.0, 11600000.0, 3.50),
            Tuple.Create(11600001.0, 12500000.0, 4.00),
            Tuple.Create(12500001.0, 13750000.0, 5.00),
            Tuple.Create(13750001.0, 15100000.0, 6.00),
            Tuple.Create(15100001.0, 16950000.0, 7.00),
            Tuple.Create(16950001.0, 19750000.0, 8.00),
            Tuple.Create(19750001.0, 24150000.0, 9.00),
            Tuple.Create(24150001.0, 26450000.0, 10.00),
            Tuple.Create(26450001.0, 28000000.0, 11.00),
            Tuple.Create(28000001.0, 30050000.0, 12.00),
            Tuple.Create(30050001.0, 32400000.0, 13.00),
            Tuple.Create(32400001.0, 35400000.0, 14.00),
            Tuple.Create(35400001.0, 39100000.0, 15.00),
            Tuple.Create(39100001.0, 43850000.0, 16.00),
            Tuple.Create(43850001.0, 47800000.0, 17.00),
            Tuple.Create(47800001.0, 51400000.0, 18.00),
            Tuple.Create(51400001.0, 56300000.0, 19.00),
            Tuple.Create(56300001.0, 62200000.0, 20.00),
            Tuple.Create(62200001.0, 68600000.0, 21.00),
            Tuple.Create(68600001.0, 77500000.0, 22.00),
            Tuple.Create(77500001.0, 89000000.0, 23.00),
            Tuple.Create(89000001.0, 103000000.0, 24.00),
            Tuple.Create(103000001.0, 125000000.0, 25.00),
            Tuple.Create(125000001.0, 157000000.0, 26.00),
            Tuple.Create(157000001.0, 206000000.0, 27.00),
            Tuple.Create(206000001.0, 337000000.0, 28.00),
            Tuple.Create(337000001.0, 454000000.0, 29.00),
            Tuple.Create(454000001.0, 550000000.0, 30.00),
            Tuple.Create(550000001.0, 695000000.0, 31.00),
            Tuple.Create(695000001.0, 910000000.0, 32.00),
            Tuple.Create(910000001.0, 1400000000.0, 33.00),
            Tuple.Create(1400000001.0, double.MaxValue, 34.00)
        };

        public static List<Tuple<double, double, double>> TerB = new List<Tuple<double, double, double>>
        {
            Tuple.Create(0.0, 6200000.0, 0.00),
            Tuple.Create(6200001.0, 6500000.0, 0.25),
            Tuple.Create(6500001.0, 6850000.0, 0.50),
            Tuple.Create(6850001.0, 7300000.0, 0.75),
            Tuple.Create(7300001.0, 9200000.0, 1.00),
            Tuple.Create(9200001.0, 10750000.0, 1.50),
            Tuple.Create(10750001.0, 11250000.0, 2.00),
            Tuple.Create(11250001.0, 11600000.0, 2.50),
            Tuple.Create(11600001.0, 12600000.0, 3.00),
            Tuple.Create(12600001.0, 13600000.0, 4.00),
            Tuple.Create(13600001.0, 14950000.0, 5.00),
            Tuple.Create(14950001.0, 16400000.0, 6.00),
            Tuple.Create(16400001.0, 18450000.0, 7.00),
            Tuple.Create(18450001.0, 21850000.0, 8.00),
            Tuple.Create(21850001.0, 26000000.0, 9.00),
            Tuple.Create(26000001.0, 27700000.0, 10.00),
            Tuple.Create(27700001.0, 29350000.0, 11.00),
            Tuple.Create(29350001.0, 31450000.0, 12.00),
            Tuple.Create(31450001.0, 33950000.0, 13.00),
            Tuple.Create(33950001.0, 37100000.0, 14.00),
            Tuple.Create(37100001.0, 41100000.0, 15.00),
            Tuple.Create(41100001.0, 45800000.0, 16.00),
            Tuple.Create(45800001.0, 49500000.0, 17.00),
            Tuple.Create(49500001.0, 53800000.0, 18.00),
            Tuple.Create(53800001.0, 58500000.0, 19.00),
            Tuple.Create(58500001.0, 64000000.0, 20.00),
            Tuple.Create(64000001.0, 71000000.0, 21.00),
            Tuple.Create(71000001.0, 80000000.0, 22.00),
            Tuple.Create(80000001.0, 93000000.0, 23.00),
            Tuple.Create(93000001.0, 109000000.0, 24.00),
            Tuple.Create(109000001.0, 129000000.0, 25.00),
            Tuple.Create(129000001.0, 163000000.0, 26.00),
            Tuple.Create(163000001.0, 211000000.0, 27.00),
            Tuple.Create(211000001.0, 374000000.0, 28.00),
            Tuple.Create(374000001.0, 459000000.0, 29.00),
            Tuple.Create(459000001.0, 555000000.0, 30.00),
            Tuple.Create(555000001.0, 704000000.0, 31.00),
            Tuple.Create(704000001.0, 957000000.0, 32.00),
            Tuple.Create(957000001.0, 1405000000.0, 33.00),
            Tuple.Create(1405000001.0, double.MaxValue, 34.00)
        };

        public static List<Tuple<double, double, double>> TerC = new List<Tuple<double, double, double>>
        {
            Tuple.Create(0.0, 6600000.0, 0.00),
            Tuple.Create(6600001.0, 6950000.0, 0.25),
            Tuple.Create(6950001.0, 7350000.0, 0.50),
            Tuple.Create(7350001.0, 7800000.0, 0.75),
            Tuple.Create(7800001.0, 8850000.0, 1.00),
            Tuple.Create(8850001.0, 9800000.0, 1.25),
            Tuple.Create(9800001.0, 10950000.0, 1.50),
            Tuple.Create(10950001.0, 11200000.0, 1.75),
            Tuple.Create(11200001.0, 12050000.0, 2.00),
            Tuple.Create(12050001.0, 12950000.0, 3.00),
            Tuple.Create(12950001.0, 14150000.0, 4.00),
            Tuple.Create(14150001.0, 15550000.0, 5.00),
            Tuple.Create(15550001.0, 17050000.0, 6.00),
            Tuple.Create(17050001.0, 19500000.0, 7.00),
            Tuple.Create(19500001.0, 22700000.0, 8.00),
            Tuple.Create(22700001.0, 26600000.0, 9.00),
            Tuple.Create(26600001.0, 28100000.0, 10.00),
            Tuple.Create(28100001.0, 30100000.0, 11.00),
            Tuple.Create(30100001.0, 32600000.0, 12.00),
            Tuple.Create(32600001.0, 35400000.0, 13.00),
            Tuple.Create(35400001.0, 38900000.0, 14.00),
            Tuple.Create(38900001.0, 43000000.0, 15.00),
            Tuple.Create(43000001.0, 47400000.0, 16.00),
            Tuple.Create(47400001.0, 51200000.0, 17.00),
            Tuple.Create(51200001.0, 55800000.0, 18.00),
            Tuple.Create(55800001.0, 60400000.0, 19.00),
            Tuple.Create(60400001.0, 66700000.0, 20.00),
            Tuple.Create(66700001.0, 74500000.0, 21.00),
            Tuple.Create(74500001.0, 83200000.0, 22.00),
            Tuple.Create(83200001.0, 95600000.0, 23.00),
            Tuple.Create(95600001.0, 110000000.0, 24.00),
            Tuple.Create(110000001.0, 134000000.0, 25.00),
            Tuple.Create(134000001.0, 169000000.0, 26.00),
            Tuple.Create(169000001.0, 221000000.0, 27.00),
            Tuple.Create(221000001.0, 390000000.0, 28.00),
            Tuple.Create(390000001.0, 463000000.0, 29.00),
            Tuple.Create(463000001.0, 561000000.0, 30.00),
            Tuple.Create(561000001.0, 709000000.0, 31.00),
            Tuple.Create(709000001.0, 965000000.0, 32.00),
            Tuple.Create(965000001.0, 1419000000.0, 33.00),
            Tuple.Create(1419000001.0, double.MaxValue, 34.00)
        };
    }
}
