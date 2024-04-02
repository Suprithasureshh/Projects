using Ebill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Ebill
{
    public class Electricbill: IEbill
    {
        public void GetDetails()
        {
            
            int id = 0, units = 0;
            string name, address;
            Console.WriteLine("Electricity Bill Calculation");
            while (id == 0)
            {
                Console.WriteLine("Enter customer id");
                if (!int.TryParse(Console.ReadLine(), out id)) 
                    Console.WriteLine("Invalid Input");
            }
            Console.WriteLine("Enter customer name");
            name = Console.ReadLine();
            Console.WriteLine("Enter address");
            address = Console.ReadLine();
            while (units == 0)
            {
                Console.WriteLine("Enter the units consumed by the customer");
                if (!int.TryParse(Console.ReadLine(), out units)) 
                    Console.WriteLine("Invalid Input");
            }
            double charges = CalculateCharges(units);
            double value = GetSiteValue(charges);
            Console.WriteLine("Total bill is " + value);
            Console.WriteLine("\n--------Electricity Bill------\n");
            Console.WriteLine("Customer IdNo:    {0}\n", id);
            Console.WriteLine("Customer Name:    {0}\n", name);
            Console.WriteLine("Address:          {0}\n", address);
            Console.WriteLine("Unit Consumed:    {0}\n", units);
            Console.WriteLine("The Total charges is:{0} \n", value);
            char nextbill;
            Console.WriteLine("Do you want to generate another bill");
            Console.WriteLine("If Yes Press 'Y' If No then 'N' ");
            nextbill = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine("");
            if (nextbill == 'Y') GetDetails();
            else Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        private double CalculateCharges(int units)
        {
            double charges = 0;
            if (units <= 100) charges = units * 0;
            else if (units <= 1000) charges = (100 * 0) + (units - 100) * 5;
            else if (units <= 10000) charges = (100 * 0) + (900 * 5) + (units - 1000) * 10;
            else if (units <= 30000) charges = (100 * 0) + (900 * 5) + (9000 * 10) + (units - 10000) * 20;
            else if (units > 30000) charges = (100 * 0) + (900 * 5) + (9000 * 10) + (20000 * 20) + (units - 30000) * 35;
            return charges;
        }
        private double GetSiteValue(double charges)
        {
            Console.WriteLine("Enter the Site:\n1.Domestic\n2.Commercial\n3.Village\n");
            char site;
            while (!char.TryParse(Console.ReadLine(), out site) || (site != '1' && site != '2' && site != '3')) 
                Console.WriteLine("Invalid number. Please select range in the list");
            switch (site)
            {
                case '1':
                    Console.WriteLine("Select your Domestic site: \n1.City\n2.Town\n3.Village\n");
                    char site1;
                    while (!char.TryParse(Console.ReadLine(), out site1) || (site1 != '1' && site1 != '2' && site1 != '3')) 
                        Console.WriteLine("Invalid number. Please select range in the list");
                    if (site1 == '1') return charges * 5;
                    else if (site1 == '2') return charges * 2;
                    else return charges * 0;
                case '2':
                    return charges * 10;
                default:
                    return charges * 0;
            }
        }
    }
}