using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class ScoreFrameFinal : ScoreFrame
    {
        public int? BowlThree;

        public override bool IsLastFrame() 
        {  
            return true; 
        }

        public override bool IsCompleted()
        {
            return BowlOne != null && BowlTwo != null && BowlThree != null;
        }
    }
}
