namespace SalaryAndTaxCalculationConsole
{
    public class BPJS
    {
        public class Constant
        {
            public const string NAME = "BPJS";
            public const string BY_COMPANY = "COMPANY";
            public const string BY_EMPLOYEE = "EMPLOYEE";
        }
        public class JKK
        {
            public class Constant
            {
                public const string NAME = "JAMINAN KECELAKAAN KERJA";
                public const double JKK_PERCENTAGE = 0.0024; //0.24%
            }
            public static double Calculate(double salary)
            {

                return Constant.JKK_PERCENTAGE * salary;
            }
        }

        public class JKM
        {
            public class Constant
            {
                public const string NAME = "JAMINAN KEMATIAN";
                public const double JKM_PERCENTAGE = 0.003; //0.3%
            }

            public static double Calculate(double salary)
            {
                //0.3%
                return 0.003 * salary;
            }
        }

        public class BPJSK
        {
            public class Constant
            {
                public const string NAME = "BPJS KETENAGAERJAAN";
                public const double BPJSK_CAP = 12000000;
                public const double BPJSK_BY_COMPANY_PERCENTAGE = 0.04; //4%
                public const double BPJSK_BY_EMPLOYEE_PERCENTAGE = 0.01; //1%
            }

            public static double Calculate(double salary, bool IsJP, string by)
            {
                if (!IsJP)
                    return 0.0;

                if (by == BPJS.Constant.BY_COMPANY)
                    return Constant.BPJSK_BY_COMPANY_PERCENTAGE * (salary <= Constant.BPJSK_CAP ? salary : Constant.BPJSK_CAP);

                if (by == BPJS.Constant.BY_EMPLOYEE)
                    return Constant.BPJSK_BY_EMPLOYEE_PERCENTAGE * (salary <= Constant.BPJSK_CAP ? salary : Constant.BPJSK_CAP);

                return 0.0;
            }
        }

        public class JHT
        {
            public class Constant
            {
                public const string NAME = "JAMINAN HARI TUA";
                public const double JHT_BY_COMPANY_PERCENTAGE = 0.037; //3.7%
                public const double JHT_BY_EMPLOYEE_PERCENTAGE = 0.02; //2%
            }

            public static double Calculate(double salary, string by)
            {
                if (by == BPJS.Constant.BY_COMPANY)
                    return Constant.JHT_BY_COMPANY_PERCENTAGE * salary;

                if (by == BPJS.Constant.BY_EMPLOYEE)
                    return Constant.JHT_BY_EMPLOYEE_PERCENTAGE * salary;

                return 0.0;
            }
        }

        public class JP
        {
            public class Constant
            {
                public const string NAME = "JAMINAN PENSIUN";
                public const double JP_BY_COMPANY_CAP = 8754600;
                public const double JP_BY_EMPLOYEE_CAP = 9559600;

                public const double JP_BY_COMPANY_PERCENTAGE = 0.02; //2% (source: https://glints.com/id/lowongan/perhitungan-bpjs-ketenagakerjaan/#:~:text=Nah%2C%20besar%20iuran%20dari%20JP,754.600%2C%20alias%20sisanya%20tidak%20dihitung.)
                public const double JP_BY_EMPLOYEE_PERCENTAGE = 0.01;
            }

            public static double Calculate(double salary, bool IsJHT, string by)
            {
                if (!IsJHT)
                    return 0;

                if (by == BPJS.Constant.BY_COMPANY)
                    return 0.02 * (salary <= Constant.JP_BY_COMPANY_CAP ? salary : Constant.JP_BY_COMPANY_CAP);

                if (by == BPJS.Constant.BY_EMPLOYEE)
                    return 0.01 * (salary <= Constant.JP_BY_EMPLOYEE_CAP ? salary : Constant.JP_BY_EMPLOYEE_CAP);

                return 0;
            }
        }

    }
}
