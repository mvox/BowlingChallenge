using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class ScoreFrame
    {
        public int? BowlOne;

        public int? BowlTwo;

        public int? FrameTotal;

        public virtual bool IsLastFrame() 
        { 
            return false; 
        }

        public virtual bool IsCompleted() { 
            return BowlOne != null && BowlTwo != null;
        }
    }
}
