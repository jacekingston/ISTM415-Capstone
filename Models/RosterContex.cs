using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrototype.Models
{
    public class RosterContex : DbContext
    {
        public RosterContex(DbContextOptions<RosterContex> options)
        {

        }
    }
}
