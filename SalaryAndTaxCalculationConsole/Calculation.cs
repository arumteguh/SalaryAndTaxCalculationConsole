namespace SalaryAndTaxCalculationConsole
{
    public class Calculation
    {
        public class Constant
        {
            public const double FUNCTIONAL_COST_CAP = 120000000;
            public const double FUNCTIONAL_COST_RESULT_DEFAULT = 6000000;
        }

        public const int CURRENT_MONTH = 1;

        public static double Taxes(double salary)
        {
            double tax = 0;
            double tiertax;

            if (salary < 0)
            {
                return 0;
            }


            if (salary > Tax.Constant.TIER_3_MAX)
            {
                tiertax = (salary - Tax.Constant.TIER_3_MAX) * Tax.Constant.TIER_4_PERCENTAGE;
                tax += tiertax;
                salary = Tax.Constant.TIER_3_MAX;
                Console.WriteLine($"Tier 4: {tiertax.ToString("N2")}");
            }

            if (salary > Tax.Constant.TIER_2_MAX)
            {
                tiertax = (salary - Tax.Constant.TIER_2_MAX) * Tax.Constant.TIER_3_PERCENTAGE;
                tax += tiertax;
                salary = Tax.Constant.TIER_2_MAX;
                Console.WriteLine($"Tier 3: {tiertax.ToString("N2")}");
            }

            if (salary > Tax.Constant.TIER_1_MAX)
            {
                tiertax = (salary - Tax.Constant.TIER_1_MAX) * Tax.Constant.TIER_2_PERCENTAGE;
                tax += tiertax;
                salary = Tax.Constant.TIER_1_MAX;
                Console.WriteLine($"Tier 2: {tiertax.ToString("N2")}");
            }

            if (salary <= Tax.Constant.TIER_1_MAX)
            {
                tiertax = salary * Tax.Constant.TIER_1_PERCENTAGE;
                tax += tiertax;
                Console.WriteLine($"Tier 1: {tiertax.ToString("N2")}");
            }

            return tax;
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

        //NOTE: This is Reference to Mas Randy excel calculation
        public static double FunctionalCost(double grossSalary, int currentMonth)
        {
            //return grossSalary >= Constant.FUNCTIONAL_COST_CAP ? Constant.FUNCTIONAL_COST_RESULT_DEFAULT : 0.05 * grossSalary * 12 / currentMonth;
            return grossSalary >= Constant.FUNCTIONAL_COST_CAP ? Constant.FUNCTIONAL_COST_RESULT_DEFAULT : 0.05 * grossSalary;
        }

        public static double Annual(double amount, int currentMonth)
        {
            return amount * 12 / currentMonth;
        }



    }
}
