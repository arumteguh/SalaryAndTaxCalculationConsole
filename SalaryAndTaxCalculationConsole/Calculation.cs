using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SalaryAndTaxCalculationConsole
{
    public class Calculation
    {

        public const double TIER_1_MAX = 60000000;
        public const double TIER_2_MAX = 250000000;
        public const double TIER_3_MAX = 500000000;

        public const double BPJSK_CAP = 12000000;
        public const double JP_COMPANY_CAP = 8754600;
        public const double JP_EMPLOYEE_CAP = 9559600;

        public const double FUNCTIONAL_COST_CAP = 120000000;
        public const string BY_COMPANY = "COMPANY";
        public const string BY_EMPLOYEE = "EMPLOYEE";

        public const int CURRENT_MONTH = 1;

        public static double Tax(double salary)
        {
            double tax = 0;
            double tiertax;

            if (salary < 0) {
                return 0;
            }


            if (salary > TIER_3_MAX)
            {
                tiertax = (salary - TIER_3_MAX) * 0.30;
                tax += tiertax;
                salary = TIER_3_MAX;
                Console.WriteLine($"Tier 4: {tiertax.ToString("N2")}");
            }

            if (salary > TIER_2_MAX)
            {
                tiertax = (salary - TIER_2_MAX) * 0.25;
                tax += tiertax;
                salary = TIER_2_MAX;
                Console.WriteLine($"Tier 3: {tiertax.ToString("N2")}");
            }

            if (salary > TIER_1_MAX)
            {
                tiertax = (salary - TIER_1_MAX) * 0.15;
                tax += tiertax;
                salary = TIER_1_MAX;
                Console.WriteLine($"Tier 2: {tiertax.ToString("N2")}");
            }

            if (salary <= TIER_1_MAX)
            {
                tiertax = salary * 0.05;
                tax += tiertax;
                Console.WriteLine($"Tier 1: {tiertax.ToString("N2")}");
            }

            return tax;
        }

        //Q: Get percentage by PKP or Annual Salary?
        public static double GetGrossUpPercentage(double value)
        {
            if (value > TIER_3_MAX)
            {
                //30%
                return 0.30;
            }

            if (value > TIER_2_MAX)
            {
                //25%
                return 0.25;
            }

            if (value > TIER_1_MAX)
            {
                //15%
                return 0.15;
            }

            if (value <= TIER_1_MAX)
            {
                //5%
                return 0.05;
            }

            return 0;
        }

        public static double BIK(List<Allowance> allowances)
        {
            double grossUp = 0;

            foreach (Allowance allowance in allowances)
            {
                if (!allowance.IsBIK)
                    continue;

                if (allowance.PaymentSchedule == "YEARLY")
                    allowance.Amount = allowance.Amount * 12;


            }

            return grossUp;
        }

        public static double GrossUpNatura(double BIK, double value)
        {
            double percentage = GetGrossUpPercentage(value);
            return BIK / (1 - percentage) - BIK;
        }
        

        public static Dictionary<string, double> PTKP = new Dictionary<string, double>()
        {
            {"TK0", 54000000},
            {"TK1", 58500000},
            {"TK2", 63000000},
            {"TK3", 67500000},
            {"K0", 58500000},
            {"K1", 63000000},
            {"K2", 67500000},
            {"K3", 72000000}
        };

        public static double JKK(double salary)
        {
            //0.24%
            return 0.0024 * salary;
        }

        public static double JKM(double salary)
        {
            //0.3%
            return 0.003 * salary;
        }

        public static double BPJSK(double salary, bool IsJP, string by)
        {
            if (!IsJP)
                return 0;

            if (by == "COMPANY") //4%
                return 0.04 * (salary <= BPJSK_CAP ? salary : BPJSK_CAP);

            if (by == "EMPLOYEE") //1%
                return 0.01 * (salary <= BPJSK_CAP ? salary : BPJSK_CAP);

            return 0;
        }

        public static double JHT(double salary)
        {
            //2%
            return 0.02 * salary;
        }

        public static double JP(double salary, bool IsJHT, string by)
        {
            
            if (!IsJHT) 
                return 0;

            if (by == "COMPANY") //2% (source: https://glints.com/id/lowongan/perhitungan-bpjs-ketenagakerjaan/#:~:text=Nah%2C%20besar%20iuran%20dari%20JP,754.600%2C%20alias%20sisanya%20tidak%20dihitung.)
                return 0.02 * (salary <= JP_COMPANY_CAP ? salary : JP_COMPANY_CAP);

            if (by == "EMPLOYEE") //1%
                return 0.01 * (salary <= JP_EMPLOYEE_CAP ? salary : JP_EMPLOYEE_CAP);

            return 0;
        }


        public static double FunctionalCost(double grossSalary, int currentMonth)
        {
            return grossSalary >= FUNCTIONAL_COST_CAP ? 6000000 : 0.05 * grossSalary * 12 / currentMonth;
        }

        public static double Annual(double amount, int currentMonth)
        {
            return amount * 12 / currentMonth;
        }



    }
}
