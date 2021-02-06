namespace UserAPI.Domain.ViewModels
{
    public class ResultViewModel
    {
        public ResultViewModel(object data, bool success,string message)
        {
            Message = message;
            Data = data;
            Success = success;
        }

        public string Message { get; set; }
        public object Data { get; set; }
        public bool Success { get; set; }

        public static ResultViewModel From(object data, bool success, string message)
        {
            return new ResultViewModel(data, success, message);
        }

    }
}
