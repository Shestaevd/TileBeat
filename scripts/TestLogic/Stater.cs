using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBeat.scripts.testing
{

    public enum ActionType
    {
        hit,
        prepare,
        none
    }

    public record class Step(int X, int Y, ActionType action, string sprite);

    public class Stater
    {

        public Queue<Step[]> steps;

        private void testlogic()
        {
            steps = new Queue<Step[]>();

            steps.Enqueue(new Step[] { new Step(0, 0, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 1, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 2, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 3, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 4, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 5, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 6, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 7, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 8, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 9, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(1, 1, ActionType.hit, "t2") });
            steps.Enqueue(new Step[] { new Step(0, 1, ActionType.none, "12") });
        }

        public Stater()
        {
            testlogic();
        }

        public Step[] nextSteps()
        {
            Step[] ss;
            var returned = steps.TryDequeue(out ss);

            if (returned) return ss;
            else return null;
        }

    }
}
