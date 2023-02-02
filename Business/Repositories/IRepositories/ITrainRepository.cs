
using ProjectASP.Data.Entities;
using ProjectASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectASP.Business.Repositories.IRepositories
{
    public interface ITrainRepository
    {
        Task<IEnumerable<TrainDTO>> GetAllAsync();
        Task<TrainDTO> GetAsync(int id);
        Task<Train> GetAsync(string trainName);
        Task CreateAsync(TrainDTO train);
        Task UpdateAsync(TrainDTO train);
        Task<int> RemoveAsync(int id);

    }
}
