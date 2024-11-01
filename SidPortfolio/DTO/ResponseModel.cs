namespace SidPortfolio.DTO
{
    public class ResponseModel<T>
    {
        public T Value { get; set; }
       public  bool IsSuccess {  get; set; }
       public int StatusCode {  get; set; }
    }
}
