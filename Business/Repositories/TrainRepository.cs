using AutoMapper;
using ProjectASP.Business.Repositories.IRepositories;
using ProjectASP.Data;
using ProjectASP.Data.Entities;
using ProjectASP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectASP.Business.Repositories
{
    public class TrainRepository : ITrainRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public TrainRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CreateAsync(TrainDTO objDTO)
        {
            var obj = mapper.Map<TrainDTO, Train>(objDTO);
            db.Train.Add(obj);
            await db.SaveChangesAsync();
        }


        public async Task<Train> GetAsync(string trainName)
        {
            return await db.Train.FirstOrDefaultAsync(u => u.Name.ToLower() == trainName.ToLower());
        }



        public async Task<IEnumerable<TrainDTO>> GetAllAsync()
        {
            var obj = await db.Train.ToListAsync();
            return mapper.Map<IEnumerable<Train>, IEnumerable<TrainDTO>>(obj);
        }

        public async Task<TrainDTO> GetAsync(int id)
        {
            var obj = await db.Train.FirstOrDefaultAsync(u => u.Id == id);
            return mapper.Map<Train, TrainDTO>(obj);
        }

        public async Task<int> RemoveAsync(int id)
        {
            var obj = await db.Train.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                db.Train.Remove(obj);
                return await db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task UpdateAsync(TrainDTO objDTO)
        {
            var objFromDb = await db.Train.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Length = objDTO.Length;
                objFromDb.IsActive = objDTO.IsActive;

                db.Train.Update(objFromDb);
                await db.SaveChangesAsync();
            }
        }
    }
}
