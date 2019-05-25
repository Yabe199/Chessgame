using Players.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players.Lib.Services
{
    public class BoardService
    {
        public Board Chessboard { get; set; }

        public BoardService()
        {
            Chessboard = new Board();
        }
    }
}
