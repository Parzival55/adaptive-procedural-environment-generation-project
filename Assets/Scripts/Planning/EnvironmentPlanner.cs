using UnityEngine;

public class EnvironmentPlanner
{
    public EnvironmentPlan CreatePlan(GameplayDefinition gameplay)
    {
        // If no Gameplay Definition has been assigned,
        // create a sensible default.
        if (gameplay == null)
        {
            gameplay = ScriptableObject.CreateInstance<GameplayDefinition>();

            gameplay.GenerateSpawn = true;
            gameplay.GenerateExit = false;
            gameplay.GenerateObjective = false;
            gameplay.GenerateRewardAreas = true;
            gameplay.GenerateHubSpaces = true;
            gameplay.GenerateLandmarks = true;

            gameplay.AllowBranches = true;
            gameplay.AllowLoops = true;

            gameplay.MinimumSpaces = 8;
            gameplay.MaximumSpaces = 12;
        }

        EnvironmentPlan plan = new EnvironmentPlan();

        PlannedSpace previous = null;

        // Spawn
        if (gameplay.GenerateSpawn)
        {
            previous = new PlannedSpace(SpaceType.Spawn);
            plan.Spaces.Add(previous);
        }

        // Standard spaces
        int count = Random.Range(
            gameplay.MinimumSpaces,
            gameplay.MaximumSpaces + 1);

        for (int i = 0; i < count; i++)
        {
            PlannedSpace current =
                new PlannedSpace(SpaceType.Standard);

            if (previous != null)
                previous.Connect(current);

            plan.Spaces.Add(current);

            previous = current;
        }

        // Hub
        PlannedSpace hub = null;

        if (gameplay.GenerateHubSpaces)
        {
            hub = new PlannedSpace(SpaceType.Hub);

            if (previous != null)
                previous.Connect(hub);

            plan.Spaces.Add(hub);

            previous = hub;
        }

        // Reward branch
        if (gameplay.GenerateRewardAreas && hub != null)
        {
            PlannedSpace reward =
                new PlannedSpace(SpaceType.Reward);

            hub.Connect(reward);

            plan.Spaces.Add(reward);
        }

        // Objective
        if (gameplay.GenerateObjective)
        {
            PlannedSpace objective =
                new PlannedSpace(SpaceType.Objective);

            if (previous != null)
                previous.Connect(objective);

            plan.Spaces.Add(objective);

            previous = objective;
        }

        // Exit
        if (gameplay.GenerateExit)
        {
            PlannedSpace exit =
                new PlannedSpace(SpaceType.Exit);

            if (previous != null)
                previous.Connect(exit);

            plan.Spaces.Add(exit);
        }

        return plan;
    }
}