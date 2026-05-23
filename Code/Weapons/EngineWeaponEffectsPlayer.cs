namespace VSurvival.Code.Weapons;

// Replace these methods with your engine audio/particle APIs.
public sealed class EngineWeaponEffectsPlayer : IWeaponEffectsPlayer
{
    public void PlayMuzzleFlash(string weaponId)
    {
        // Example: spawn muzzle prefab from Prefabs/Effects based on weaponId.
    }

    public void PlayShootSound(string weaponId)
    {
        // Example: play one-shot shot sound from Assets/Sounds.
    }

    public void PlayReloadStartSound(string weaponId)
    {
        // Example: trigger reload start audio marker.
    }

    public void PlayReloadEndSound(string weaponId)
    {
        // Example: trigger reload completion click/chamber sound.
    }
}
