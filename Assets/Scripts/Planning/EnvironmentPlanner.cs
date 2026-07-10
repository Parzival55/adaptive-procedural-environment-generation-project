using System.Collections.Generic;
using UnityEngine;

public class EnvironmentPlanner
{
    public EnvironmentPlan CreatePlan(GameplayDefinition gameplay)
    {
        if (gameplay == null)
        {
            gameplay = ScriptableObject.CreateInstance<GameplayDefinition>();

            gameplay.GenerateSpawn = true;
            gameplay.GenerateExit = false;
            gameplay.GenerateObjective = false;
            gameplay.GenerateRewardAreas = true;
            gameplay.GenerateHubSpaces = true;

            gameplay.MinimumSpaces = 12;
            gameplay.MaximumSpaces = 18;
        }

        EnvironmentPlan plan = new EnvironmentPlan();

        List<PlannedSpace> mainPath = new List<PlannedSpace>();

        // Spawn
        PlannedSpace spawn = new PlannedSpace(SpaceType.Spawn);
        plan.Spaces.Add(spawn);
        mainPath.Add(spawn);

        PlannedSpace previous = spawn;

        int mainPathLength = Random.Range(
            gameplay.MinimumSpaces,
            gameplay.MaximumSpaces + 1);

        int hubIndex = mainPathLength / 2;

        for (int i = 0; i < mainPathLength; i++)
        {
            SpaceType type = SpaceType.Standard;

            if (gameplay.GenerateHubSpaces && i == hubIndex)
                type = SpaceType.Hub;

            PlannedSpace room = new PlannedSpace(type);

            previous.Connect(room);

            plan.Spaces.Add(room);
            mainPath.Add(room);

            previous = room;
        }

        // Exit
        if (gameplay.GenerateExit)
        {
            PlannedSpace exit = new PlannedSpace(SpaceType.Exit);

            previous.Connect(exit);

            plan.Spaces.Add(exit);
        }

        // Reward branches
        if (gameplay.GenerateRewardAreas)
        {
            int branchCount = Mathf.Max(2, mainPathLength / 4);

            for (int i = 0; i < branchCount; i++)
            {
                PlannedSpace parent =
                    mainPath[Random.Range(2, mainPath.Count - 2)];

                PlannedSpace branch =
                    new PlannedSpace(SpaceType.Standard);

                parent.Connect(branch);

                plan.Spaces.Add(branch);

                PlannedSpace reward =
                    new PlannedSpace(SpaceType.Reward);

                branch.Connect(reward);

                plan.Spaces.Add(reward);
            }
        }

        // Extra side rooms
        int sideRooms = Mathf.Max(2, mainPathLength / 3);

        for (int i = 0; i < sideRooms; i++)
        {
            PlannedSpace parent =
                mainPath[Random.Range(1, mainPath.Count - 1)];

            PlannedSpace side =
                new PlannedSpace(SpaceType.Standard);

            parent.Connect(side);

            plan.Spaces.Add(side);
        }

        return plan;
    }
}