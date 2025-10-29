 
namespace App2.BLL.ModelVM.ResponseResult
{
    public record class Response<T>(T result,string? ErrorMessage,bool IsHaveErrorOrNot);
    
}
