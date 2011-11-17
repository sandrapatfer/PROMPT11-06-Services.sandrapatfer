using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServicesConsumer
{
    class Program
    {
        static void ListCountryAvailables()
        {
            var client = new HolidayService.HolidayService2SoapClient("HolidayService2Soap");
            HolidayService.CountryCode[] countries = client.GetCountriesAvailable();
            foreach (var code in countries)
            {
                Console.WriteLine(code.Code);
            }
        }
        static void GetHolidaysAvailableForUS()
        {
            var client = new HolidayService.HolidayService2SoapClient("HolidayService2Soap");
            HolidayService.HolidayCode[] holidays = client.GetHolidaysAvailable(HolidayService.Country.UnitedStates);
            foreach (var holiday in holidays)
            {
                Console.WriteLine(holiday.Code);
            }
        }
        static void GetDefinitionOfProfessor()
        {
            var client = new DictService.DictServiceSoapClient("DictServiceSoap");
            DictService.WordDefinition def = client.Define("professor");
            Console.WriteLine((def.Definitions.Count() > 0) ? def.Definitions[0].WordDefinition : "not found");
        }
        static void Translate()
        {
            var client = new Translator.LanguageServiceClient();
            string txt = client.Translate("C1E6D88CE2967328BBA9BC6C932B9D177247CAE5", "hello world", "en", "pt", null, null);
            Console.WriteLine(txt);
        }

        static void Main(string[] args)
        {
            //ListCountryAvailables();
            //GetHolidaysAvailableForUS();
            //GetDefinitionOfProfessor();
            Translate();
            Console.ReadKey();
        }
    }
}
