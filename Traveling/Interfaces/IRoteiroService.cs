using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traveling.Models;


namespace Traveling.Interfaces
{
    public interface IRoteiroService
    {
        List<Roteiro> GetRoteiros();
        List<RoteiroItem> GetRoteiroItens(int roteiroID);

    }
}
