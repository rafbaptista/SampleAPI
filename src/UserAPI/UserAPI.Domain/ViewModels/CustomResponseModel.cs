using System.Text.Json.Serialization;

namespace UserAPI.Domain.ViewModels
{
    public class CustomResponseModel
    {
        public CustomResponseModel(int httpStatusCode, string message, string roles, string policies)
        {
            HttpStatusCode = httpStatusCode;
            Message = message;
            AuthorizedRoles = roles?.Split(",") ?? null;
            AuthorizedPolicies = policies?.Split(",") ?? null;
        }

        public int HttpStatusCode { get; set; }
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string[] AuthorizedRoles { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string[] AuthorizedPolicies { get; set; }
    }
}
