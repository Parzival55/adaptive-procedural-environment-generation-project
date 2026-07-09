using UnityEngine;

public static class PlanningDebugger
{
    public static void Print(EnvironmentPlan plan)
    {
        Debug.Log("===== Environment Plan =====");

        foreach (PlannedSpace space in plan.Spaces)
        {
            string connections = "";

            foreach (PlannedSpace connected in space.Connections)
            {
                connections += connected.Type + " ";
            }

            Debug.Log($"{space.Type} -> {connections}");
        }

        Debug.Log("============================");
    }
}