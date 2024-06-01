using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCase.Interfaces
{
    public interface IGetAllRequestsUseCase
    {
        Task<List<Pedidos>> GetAllRequetsAsync(); 
    }
}
