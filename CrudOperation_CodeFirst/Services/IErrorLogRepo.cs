using CrudOperation_CodeFirst.Models;
using System.Threading.Tasks;

namespace CrudOperation_CodeFirst.Services
{
    public interface IErrorLogRepo
    {
        Task<ErrorLog> AddError(ErrorLog Data);
    }
}