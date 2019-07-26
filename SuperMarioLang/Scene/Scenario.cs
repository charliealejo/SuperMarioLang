using System;
using System.Linq;

namespace SuperMarioLang
{
    public class Scenario
    {
        private readonly Cell[,] scenario;

        public Scenario(string[] scenario, ICellFactory factory)
        {
            this.scenario = new Cell[scenario.Length, scenario.Max(l => l.Length)];
            for (int i = 0; i < scenario.Length; i++)
            {
                for (int j = 0; j < scenario[i].Length; j++)
                {
                    this.scenario[i, j] = factory.Create(i, j, scenario[i][j]);
                }
                for (int j = scenario[i].Length; j < this.scenario.GetLength(1); j++)
                {
                    this.scenario[i, j] = factory.Create(i, j, ' ');
                }
            }
        }

        public Cell InitialPosition
        {
            get { return scenario[0, 0]; }
        }

        public Cell NextPosition(Mario mario)
        {
            int x = mario.X;
            int y = mario.Y;
            var m = mario.Direction;

            if (CheckEndCondition(x, y, m)) return Cell.Endgame;
            if (scenario[x + 1, y].IsFloor() || scenario[x + 1, y].Type == CellType.JUMP)
            {
                if (mario.Direction == Movement.RIGHT &&
                    !scenario[x, y + 1].IsFloor()) return scenario[x, y + 1];
                if (mario.Direction == Movement.LEFT &&
                    !scenario[x, y - 1].IsFloor()) return scenario[x, y - 1];
                if (mario.Direction == Movement.JUMP &&
                    !scenario[x - 1, y].IsFloor()) return scenario[x - 1, y];
                if (mario.Direction == Movement.STOP &&
                    scenario[x + 1, y].Type == CellType.ELEVATOR_START)
                {
                    int elevatorEnd = -1;
                    for (int i = 0; i < scenario.GetLength(0); i++)
                        if (scenario[i, y].Type == CellType.ELEVATOR_END)
                        {
                            elevatorEnd = i;
                            break;
                        }
                    if (elevatorEnd < x)
                    {
                        mario.Direction = Movement.ELEVATOR_UP;
                        return scenario[x - 1, y];
                    }
                    else
                    {
                        mario.Direction = Movement.ELEVATOR_DOWN;
                        return scenario[x + 1, y];
                    }
                }
                throw new Exception($"Mario got stuck at line {x+1}, column {y+1}");
            }
            else if (mario.Direction == Movement.ELEVATOR_UP) return scenario[x - 1, y];
            else return scenario[x + 1, y];
        }

        private bool CheckEndCondition(int x, int y, Movement m)
        {
            return x == scenario.GetLength(0) - 1 ||
                   (x == 0 && m == Movement.JUMP) ||
                   (y == 0 && m == Movement.LEFT && scenario[x + 1, y].IsFloor()) ||
                   (y == scenario.GetLength(1) - 1 && m == Movement.RIGHT && scenario[x + 1, y].IsFloor());
        }
    }
}