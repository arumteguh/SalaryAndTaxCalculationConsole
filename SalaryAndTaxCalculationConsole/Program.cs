using SalaryAndTaxCalculationConsole;

const string FORMAT_N2 = "N2";

var _IsBIK = true;

var emp = new Employee() {
    Name = "Arum Teguh Prayogo",
    Salary = 7800000,
    Status = "TK0",
    IsJPCompany = true,
    IsJKM = true,
    IsJHT = true,
    IsJPEmployee = true,
    Allowances = { new Allowance() { Name = "Premi Askes", Amount = 120441.66666666666666666666666667, IsBIK = _IsBIK, PaymentSchedule = "MONTHLY"},
    new Allowance() { Name = "Premi Life", Amount = 120441.66666666666666666666666667, IsBIK = _IsBIK, PaymentSchedule = "MONTHLY"},
    new Allowance() { Name = "Medical", Amount = 120441.66666666666666666666666667, IsBIK = _IsBIK, PaymentSchedule = "MONTHLY"},
    }
    //Allowances = { new Allowance() { Name = "Premi Askes", Amount = 120442, IsBIK = true, PaymentSchedule = "MONTHLY"},
    //new Allowance() { Name = "Premi Life", Amount = 120442, IsBIK = true, PaymentSchedule = "MONTHLY"},
    //new Allowance() { Name = "Medical", Amount = 120442, IsBIK = true, PaymentSchedule = "MONTHLY"},
    //}
};



emp.CalculatAnnualSalary();

//var tax = Calculation.Tax(emp.AnnualSalary, emp.Status);

Console.WriteLine("---------- DETAIL ----------");
Console.WriteLine($"Salary: {emp.Salary.ToString(FORMAT_N2)}");

//TODO: Probably change this, to handle the YEARLY Payment Cases?
var BIK = emp.Allowances.Where(x => x.IsBIK).Sum(allowance => allowance.Amount);
var AnnualBIK = Calculation.Annual(BIK, Calculation.CURRENT_MONTH);
var AnnualGrossUp = Calculation.GrossUpNatura(AnnualBIK, emp.AnnualSalary);
var GrossUp = AnnualGrossUp / 12;
Console.WriteLine($"BIK: {BIK.ToString(FORMAT_N2)}");
Console.WriteLine($"GrossUp: {GrossUp.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualSalary: {emp.AnnualSalary.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualBIK: {AnnualBIK.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualGrossUp: {AnnualGrossUp.ToString(FORMAT_N2)}");
var PTKP = Calculation.PTKP[emp.Status];
Console.WriteLine($"Non-Taxable Income(PTKP): {emp.Status} --> {PTKP.ToString(FORMAT_N2)}");

Console.WriteLine("---------- BPJS ----------");
var JKK = Calculation.JKK(emp.Salary);
var JKM  = Calculation.JKM(emp.Salary);
//Q1: Why this JPCompany = 0 in the sheets?
var JPCompany = Calculation.JP(emp.Salary, emp.IsJHT, Calculation.BY_COMPANY);
var BPJSKCompany = Calculation.BPJSK(emp.Salary, emp.IsJPCompany, Calculation.BY_COMPANY);
var BPJSPaidByCompany = JKK + JKM + BPJSKCompany;
var AnnualBPJSPaidByCompany = Calculation.Annual(BPJSPaidByCompany, Calculation.CURRENT_MONTH);
Console.WriteLine($"JKK: {JKK.ToString(FORMAT_N2)}");
Console.WriteLine($"JKM: {JKM.ToString(FORMAT_N2)}");
Console.WriteLine($"JPCompany: {JPCompany.ToString(FORMAT_N2)}");
Console.WriteLine($"BPJSK: {BPJSKCompany.ToString(FORMAT_N2)}");
Console.WriteLine($"BPJSPaidByCompany: {BPJSPaidByCompany.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualBPJSPaidByCompany: {AnnualBPJSPaidByCompany.ToString(FORMAT_N2)}");

var GrossSalary = emp.Salary + BIK + GrossUp + BPJSPaidByCompany;
var AnnualGrossSalary = Calculation.Annual(GrossSalary, Calculation.CURRENT_MONTH);
var FunctionalCost = Calculation.FunctionalCost(AnnualGrossSalary, 12);
Console.WriteLine($"GrossSalary: {GrossSalary.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualGrossSalary: {AnnualGrossSalary.ToString(FORMAT_N2)}");
Console.WriteLine($"FunctionalCost: {FunctionalCost.ToString(FORMAT_N2)}");

Console.WriteLine("====================");

var JHT = Calculation.JHT(emp.Salary);
var JPEmployee = Calculation.JP(emp.Salary, emp.IsJHT, Calculation.BY_EMPLOYEE);
var BPJSKEmployee = Calculation.BPJSK(emp.Salary, emp.IsJPEmployee, Calculation.BY_EMPLOYEE);
var BPJSPaidByEmployee = JHT + JPEmployee;
var AnnualBPJSPaidByEmployee = Calculation.Annual(BPJSPaidByEmployee, Calculation.CURRENT_MONTH);
Console.WriteLine($"JHT: {JHT.ToString(FORMAT_N2)}");
Console.WriteLine($"JPEmployee: {JPEmployee.ToString(FORMAT_N2)}");
Console.WriteLine($"BPJSKEmployee: {BPJSKEmployee.ToString(FORMAT_N2)}");
Console.WriteLine($"BPJSPaidByEmployee: {BPJSPaidByEmployee.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualBPJSPaidByEmployee: {AnnualBPJSPaidByEmployee.ToString(FORMAT_N2)}");

var GrossIncome = GrossSalary - BPJSPaidByEmployee;
//var AnnualGrossIncome = Calculation.Annual(GrossIncome, Calculation.CURRENT_MONTH);
//Q2: why the GrossIncome in the sheets is (AnnualGrossSalary - FunctionalCost - AnnualBPJSPaidByEmployee)
var AnnualGrossIncome = AnnualGrossSalary - FunctionalCost - AnnualBPJSPaidByEmployee;
Console.WriteLine($"GrossIncome: {GrossIncome.ToString(FORMAT_N2)}");

Console.WriteLine($"AnnualGrossIncome: {AnnualGrossIncome.ToString(FORMAT_N2)}");


var PKP = AnnualGrossIncome - PTKP;
PKP = Math.Floor(PKP / 1000) * 1000;
Console.WriteLine($"Taxable Income(PKP): {PKP.ToString(FORMAT_N2)}");


var AnnualTax = Calculation.Tax(PKP);
var MonthlyTax = AnnualTax / 12 * Calculation.CURRENT_MONTH;

//Console.WriteLine($"Taxable: {(emp.AnnualSalary - Calculation.PTKP[emp.Status]).ToString(FORMAT_N2)}");
Console.WriteLine($"Tax: {MonthlyTax.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualTax: {AnnualTax.ToString(FORMAT_N2)}");

Console.WriteLine("---------- RESULT ----------");


var TotalTax = MonthlyTax;
var Net = emp.Salary + GrossUp - (BPJSPaidByEmployee + BPJSKEmployee + MonthlyTax);
Console.WriteLine($"Monthly Tax: {MonthlyTax.ToString(FORMAT_N2)}");
Console.WriteLine($"Total Tax: {TotalTax.ToString(FORMAT_N2)}");
Console.WriteLine($"Net: {Net.ToString(FORMAT_N2)}");







public class Employee
{
    public string? Name { get; set; }
    public double Salary { get; set; }

    public double AnnualSalary { get; set; }

    public string? Status { get; set; }

    public bool IsJPCompany { get; set; }

    public bool IsJKM { get; set; }

    public bool IsJHT { get; set; }
    
    public bool IsJPEmployee { get; set; }

    public List<Allowance> Allowances { get; set; } = new List<Allowance>();    
    public Employee() { }

    public void CalculatAnnualSalary()
    {
        this.AnnualSalary = Calculation.Annual(this.Salary, Calculation.CURRENT_MONTH);
    }
}

public class Allowance
{
    public string? Name { get; set; }
    public double Amount { get; set;}

    public bool IsBIK { get; set; }

    public string? PaymentSchedule { get; set; }

    public Allowance() { }
}