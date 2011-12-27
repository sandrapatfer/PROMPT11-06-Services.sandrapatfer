using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Service3.Service
{
    [ServiceContract]
    public interface IService3Service
    {
        [OperationContract]
        WordResp WordLookup(WordReq req);
    }

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service3Impl : IService3Service
    {
        public WordResp WordLookup(WordReq req)
        {
            var translator = new TranslatorService.LanguageServiceClient();
            string txt = translator.Translate("C1E6D88CE2967328BBA9BC6C932B9D177247CAE5", req.Word, "en", "pt", null, null);
            if (!string.IsNullOrEmpty(txt))
            {
                var dictionary = new DictService.DictServiceSoapClient("DictServiceSoap");
                var definition = dictionary.Define(txt);
                if (definition != null && definition.Definitions.Count() > 0)
                {
                    return new WordResp() { Exists = true, Description = definition.Definitions[0].WordDefinition };
                }
            }

            return new WordResp() { Exists = false };
        }
    }
}
