using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation_CodeFirst.Data;
using CrudOperation_CodeFirst.Models;

namespace CrudOperation_CodeFirst.Services
{
    public class ErrorLogRepo 
    {
        private readonly AppDbContext _Context;
        public ErrorLogRepo(AppDbContext context)
        {
            _Context = context;
        }
        public async Task<ErrorLog> AddError(ErrorLog Data)
        {
            try
            {
                if (Data != null)
                {
                    _Context.ErrorLog.Add(Data);
                    await _Context.SaveChangesAsync();
                    return Data;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
