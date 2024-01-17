using SalaryAndTaxCalculationConsole;

const string FORMAT_N2 = "N2";

var _IsBIK = false;

var emp = new Employee()
{
    Name = "Arum Teguh Prayogo",
    Salary = 20000000,
    PTKP = PTKP.Code.K0,
    IsJPCompany = true,
    IsJKM = true,
    IsJHT = true,
    IsJPEmployee = true,
    Allowances = { new Allowance() { Name = "Premi Askes", Amount = 120441.66666666666666666666666667, IsBIK = _IsBIK, PaymentSchedule = "MONTHLY"},
    new Allowance() { Name = "Premi Life", Amount = 120441.66666666666666666666666667, IsBIK = _IsBIK, PaymentSchedule = "MONTHLY"},
    new Allowance() { Name = "Medical", Amount = 120441.66666666666666666666666667, IsBIK = _IsBIK, PaymentSchedule = "MONTHLY"},
    }
};



emp.CalculatAnnualSalary();
emp.GetTer();

//var tax = Calculation.Tax(emp.AnnualSalary, emp.Status);

Console.WriteLine("---------- DETAIL ----------");
Console.WriteLine($"Salary: {emp.Salary.ToString(FORMAT_N2)}");
Console.WriteLine($"Ter: {emp.Ter}");


//TODO: Probably change this, to handle the YEARLY Payment Cases?
var BIK = emp.Allowances.Where(x => x.IsBIK).Sum(allowance => allowance.Amount);
var AnnualBIK = Calculation.Annual(BIK, Calculation.CURRENT_MONTH);
var AnnualGrossUp = Tax.GrossUpNatura(AnnualBIK, emp.AnnualSalary);
var GrossUp = AnnualGrossUp / 12;
Console.WriteLine($"BIK: {BIK.ToString(FORMAT_N2)}");
Console.WriteLine($"GrossUp: {GrossUp.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualSalary: {emp.AnnualSalary.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualBIK: {AnnualBIK.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualGrossUp: {AnnualGrossUp.ToString(FORMAT_N2)}");
var PTKPvalue = PTKP.GetPTKP(emp.PTKP);
Console.WriteLine($"Non-Taxable Income(PTKP): {emp.PTKP} --> {PTKPvalue.ToString(FORMAT_N2)}");

Console.WriteLine("---------- BPJS COMPANY ----------");
var JKK = BPJS.JKK.Calculate(emp.Salary);
var JKM = BPJS.JKM.Calculate(emp.Salary);
var JPCompany = BPJS.JP.Calculate(emp.Salary, emp.IsJHT, BPJS.Constant.BY_COMPANY);
var JHTCompany = BPJS.JHT.Calculate(emp.Salary, BPJS.Constant.BY_COMPANY);
var BPJSKCompany = BPJS.BPJSK.Calculate(emp.Salary, emp.IsJPCompany, BPJS.Constant.BY_COMPANY);
var BPJSPaidByCompany = JKK + JKM + BPJSKCompany + JHTCompany + JPCompany;
var AnnualBPJSPaidByCompany = Calculation.Annual(BPJSPaidByCompany, Calculation.CURRENT_MONTH);
Console.WriteLine($"JKK: {JKK.ToString(FORMAT_N2)}");
Console.WriteLine($"JKM: {JKM.ToString(FORMAT_N2)}");
Console.WriteLine($"JPCompany: {JPCompany.ToString(FORMAT_N2)}");
Console.WriteLine($"JHTCompany: {JHTCompany.ToString(FORMAT_N2)}");
Console.WriteLine($"BPJSK: {BPJSKCompany.ToString(FORMAT_N2)}");
Console.WriteLine($"BPJSPaidByCompany: {BPJSPaidByCompany.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualBPJSPaidByCompany: {AnnualBPJSPaidByCompany.ToString(FORMAT_N2)}");

var GrossSalary = emp.Salary + BIK + GrossUp + BPJSPaidByCompany;
var AnnualGrossSalary = Calculation.Annual(GrossSalary, Calculation.CURRENT_MONTH);
var FunctionalCost = Calculation.FunctionalCost(AnnualGrossSalary, 12);
var FunctionalCostMonthly = FunctionalCost / 12;
Console.WriteLine($"GrossSalary: {GrossSalary.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualGrossSalary: {AnnualGrossSalary.ToString(FORMAT_N2)}");
Console.WriteLine($"FunctionalCost: {FunctionalCost.ToString(FORMAT_N2)}");
Console.WriteLine($"FunctionalCostMonthly: {FunctionalCostMonthly.ToString(FORMAT_N2)}");

Console.WriteLine("====================");
Console.WriteLine("---------- BPJS EMPLOYEE ----------");
var JHTEmployee = BPJS.JHT.Calculate(emp.Salary, BPJS.Constant.BY_EMPLOYEE);
var JPEmployee = BPJS.JP.Calculate(emp.Salary, emp.IsJHT, BPJS.Constant.BY_EMPLOYEE);
var BPJSKEmployee = BPJS.BPJSK.Calculate(emp.Salary, emp.IsJPEmployee, BPJS.Constant.BY_EMPLOYEE);
var BPJSPaidByEmployee = JHTEmployee + JPEmployee;
var AnnualBPJSPaidByEmployee = Calculation.Annual(BPJSPaidByEmployee, Calculation.CURRENT_MONTH);
Console.WriteLine($"JHT: {JHTEmployee.ToString(FORMAT_N2)}");
Console.WriteLine($"JPEmployee: {JPEmployee.ToString(FORMAT_N2)}");
Console.WriteLine($"BPJSKEmployee: {BPJSKEmployee.ToString(FORMAT_N2)}");
Console.WriteLine($"BPJSPaidByEmployee: {BPJSPaidByEmployee.ToString(FORMAT_N2)}");
Console.WriteLine($"AnnualBPJSPaidByEmployee: {AnnualBPJSPaidByEmployee.ToString(FORMAT_N2)}");

var GrossIncome = GrossSalary - FunctionalCostMonthly - BPJSPaidByEmployee;
//Q2: why the AnnualGrossIncome in the sheets is (AnnualGrossSalary - FunctionalCost - AnnualBPJSPaidByEmployee)
var AnnualGrossIncome = AnnualGrossSalary - FunctionalCost - AnnualBPJSPaidByEmployee;
Console.WriteLine($"GrossIncome: {GrossIncome.ToString(FORMAT_N2)}");

Console.WriteLine($"AnnualGrossIncome: {AnnualGrossIncome.ToString(FORMAT_N2)}");


var PKP = AnnualGrossIncome - PTKPvalue;
PKP = Math.Floor(PKP / 1000) * 1000;
Console.WriteLine($"Taxable Income(PKP): {PKP.ToString(FORMAT_N2)}");


var AnnualTax = Calculation.Taxes(PKP);
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


Console.WriteLine("---------- TAX TER BELOW ----------");
var TerTuple = Tax.GetTerTuple(emp.Ter);
if (TerTuple != null)
{
    var counter = 1;
    foreach (var row in TerTuple)
    {
        if (GrossSalary >= row.Item1 && GrossSalary <= row.Item2)
        {
            Console.WriteLine($"The salary of {GrossSalary.ToString(FORMAT_N2)} falls into the range {row.Item1.ToString(FORMAT_N2)} to {row.Item2.ToString(FORMAT_N2)} ({counter}) with a percentage of {row.Item3}%");
            var TaxEffective = Tax.TaxTerCalculation(GrossSalary, AnnualTax, row.Item3, 11);
            Console.WriteLine($"Tax Effective: {TaxEffective.ToString(FORMAT_N2)}");
            break;
        }

        counter++;
    }
}

Console.WriteLine("---------- TAX TER PDF PAGE 23 EXAMPLE ----------");
PTKP.Code CodePDF = PTKP.Code.K0;
var TerPDF = Tax.GetTerByPTKP(CodePDF);
double GajiPDF = 10000000;
double TunjanganPDF = 20000000;
double UangLembur = 5000000;
double PremiPDF = 80000;

List<double> BrutoJanuary = new List<double>() { GajiPDF, TunjanganPDF, PremiPDF };
List<double> BrutoFebruary = new List<double>() { GajiPDF, TunjanganPDF, UangLembur, PremiPDF };
double BrutoJanuarySum = BrutoJanuary.Sum();
double BrutoFebruarySum = BrutoFebruary.Sum();

var testSum = BrutoFebruarySum;

var TerTuple2 = Tax.GetTerTuple(TerPDF);
if (TerTuple2 != null)
{
    var counter = 1;
    foreach (var row in TerTuple2)
    {
        if (testSum >= row.Item1 && testSum <= row.Item2)
        {
            Console.WriteLine($"The salary of {testSum.ToString(FORMAT_N2)} falls into the range {row.Item1.ToString(FORMAT_N2)} to {row.Item2.ToString(FORMAT_N2)} ({counter}) with a percentage of {row.Item3}%");
            var TaxEffectivePDF = Tax.TaxTerCalculation(testSum, 0, row.Item3, 11);
            Console.WriteLine($"Tax Effective PDF: {TaxEffectivePDF.ToString(FORMAT_N2)}");
            break;
        }

        counter++;
    }
}




public class Employee
{
    public string? Name { get; set; }
    public double Salary { get; set; }

    public double AnnualSalary { get; set; }

    public PTKP.Code PTKP { get; set; }
    public Tax.Ter Ter { get; set; }
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

    public void GetTer()
    {
        this.Ter = Tax.GetTerByPTKP(this.PTKP);
    }
}

public class Allowance
{
    public string? Name { get; set; }
    public double Amount { get; set; }

    public bool IsBIK { get; set; }

    public string? PaymentSchedule { get; set; }

    public Allowance() { }
}