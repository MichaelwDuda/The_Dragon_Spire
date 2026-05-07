# Asset Store Enemy Setup

Use this checklist after importing a Unity Asset Store enemy pack.

1. Find the asset pack's enemy prefab.
2. Duplicate that prefab into `Assets/enemy/`.
3. Rename the duplicate, for example `AssetStoreEnemy`.
4. Set the duplicate prefab tag to `enemy`.
5. Add these gameplay components if they are missing:
   - `Enemy_move`
   - `EnemyHealth`
   - `EnemyAnimationController`
   - `CapsuleCollider` or another collider that fits the model
6. Keep the asset's `Animator` and Animator Controller on the model.
7. Open the Animator Controller and make sure it uses these parameter names:
   - Float: `Speed`
   - Trigger: `Hit`
   - Trigger: `Attack`
   - Trigger: `Die`
8. Create transitions:
   - Idle to Walk/Run when `Speed` is greater than `0.1`
   - Walk/Run to Idle when `Speed` is less than `0.1`
   - Any State to Hit when `Hit` is triggered
   - Any State to Death when `Die` is triggered
9. Select `Enemy Spawner` in `SampleScene`.
10. Replace each wave's `BasicEnemy` prefab with your new duplicated enemy prefab.

The spawner and towers can stay unchanged.
