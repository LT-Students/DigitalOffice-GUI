using System;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.AuthService;

namespace LT.DigitalOffice.GUI.Helpers
{
    public static class CreateEntityHelper
    {
        public static async Task<string> CreatioHandlerEntity<TReq, TRes>(
            Func<TReq, Task<TRes>> request, 
            TReq body,
            string entityName)
        {
            string messageUser = "";
            bool isSuccessOperation = false;

            try
            {
                var response = await request(body);

                var prop =  response.GetType().GetProperties().ToList().FirstOrDefault(x => x.Name == "Status");
                var a = (int)prop.GetValue(response);
                if ( a == (int)OperationResultStatusType.FullSuccess)
                {
                    messageUser = $"The {entityName} was created successfully!";
                    isSuccessOperation = true;
                }

                // messageUser = $"Something went wrong, please try again later.\nMessage: { response.Errors }";

                return messageUser;
            }
            catch (ApiException<ErrorResponse> ex)
            {
                isSuccessOperation = false;
                messageUser = $"Something went wrong, please try again later.\nMessage: { ex.Result.Message }";
            }

            return messageUser;
        }
    }
}