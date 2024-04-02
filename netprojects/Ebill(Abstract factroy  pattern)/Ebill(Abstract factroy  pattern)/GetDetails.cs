using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public class GetDetails 
    {
        public  void Final()
        {
            IBillGenerator total = null;

            int id = 0, units = 0;
            string name, address;
           
            Console.WriteLine("-----------Electricity Bill Calculation------------");
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
            Console.WriteLine("\nChoose the site: \n 1.Domestic  \n 2.Commercial  \n 3.Village  \n");
            char site;
            while (!char.TryParse(Console.ReadLine(), out site) && (site != 1 && site != 2 && site != 3))
                Console.WriteLine("Invalid Enter..Please Enter the number between 1 to 3");
            switch (site)
            {
                case '1':
                    Console.WriteLine("\nChoose the commercial sub-division \n 1.Domestic-city \n 2.Domestic-town \n 3.Domestic-village\n");
                    char sitesub;
                    while (!char.TryParse(Console.ReadLine(), out sitesub) && (sitesub != 1 && sitesub != 2 && sitesub != 3))
                        Console.WriteLine("Invalid Enter..Please Enter the number between 1 to 3");
                    switch (sitesub)
                    {
                        case '1':
                            total = new DomesticCityBill(units);
                            break;
                        case '2':
                            total = new DomesticTownBill(units);
                            break;
                        case '3':
                            total = new DomesticVillageBill(units);
                            break;
                        default:
                            Console.WriteLine("Entered incorrect value!..");
                            break;
                    }
                    break;
                case '2':
                    total = new CommercialBillGenerator(units);
                    break;
                case '3':
                    total = new VillageBillGenerator(units);
                    break;
                default:
                    Console.WriteLine("Entered incorrect value!..");
                    break;
            }
            double result = total.CalculateBill(units);
            Console.WriteLine("\n--------Electricity Bill------\n");
            Console.WriteLine("Customer IdNo:    {0}\n", id);
            Console.WriteLine("Customer Name:    {0}\n", name);
            Console.WriteLine("Address:          {0}\n", address);
            Console.WriteLine("Unit Consumed:    {0}\n", units);
            Console.WriteLine("The Total charges is:{0} \n", result);
            Console.WriteLine("\nPlease Enter how much amount do you want to pay?");
            double amt_paying = Convert.ToDouble(Console.ReadLine());
            double bal = result - amt_paying;
            Console.WriteLine("\nYour bill balance is {0}", bal);
            OtherBill others= new OtherBill();
            others.Bill();

        }
       

    }
    }
