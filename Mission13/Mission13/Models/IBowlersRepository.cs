using System;
using System.Linq;

namespace Mission13.Models
{
    public interface IBowlersRepository
    {
        IQueryable<Bowler> Bowlers { get; }
    }
}
