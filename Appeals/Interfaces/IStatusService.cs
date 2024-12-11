﻿using Appeals.Models;

namespace Appeals.Interfaces
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDTO>> GetAllAsync();
        Task<Status> GetByIdAsync(int id);
    }
}
