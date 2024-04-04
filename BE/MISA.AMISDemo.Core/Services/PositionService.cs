using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{
    public class PositionService : BaseService<Position>, IPositionService
    {
        #region
        IPositionRepository _positionRepository;
        #endregion

        #region Contructor
        public PositionService(IPositionRepository positionRepository) : base(positionRepository)
        {
            _positionRepository = positionRepository;
        }
        #endregion
    }
}
