namespace VSurvival.Code.Inventory;

public sealed record ItemDefinition(
    string Id,
    string Name,
    bool IsWeapon,
    int MaxStack = 1
);
