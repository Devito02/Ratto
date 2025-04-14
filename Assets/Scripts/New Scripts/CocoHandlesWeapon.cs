using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class CocoHandlesWeapon : CharacterHandleWeapon
{


    protected override void HandleInput()
    {
        if (!AbilityAuthorized
                || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
                || (CurrentWeapon == null))
        {
            return;
        }

        bool inputAuthorized = true;
        if (CurrentWeapon != null)
        {
            inputAuthorized = CurrentWeapon.InputAuthorized;
        }

        if (ForceAlwaysShoot)
        {
            ShootStart();
        }

        if (inputAuthorized && ((_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) || (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonDown)))
        {
            ShootStart();
        }

        bool buttonPressed =
            (_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed) ||
            (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonPressed);

        if (inputAuthorized && ContinuousPress && (CurrentWeapon.TriggerMode == Weapon.TriggerModes.Auto) && buttonPressed)
        {
            ShootStart();
        }

        if (inputAuthorized && ContinuousPress && (CurrentWeapon.IsAutoComboWeapon) && buttonPressed)
        {
            ShootStart();
        }

        if (_inputManager.ReloadButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        {
            Reload();
        }

        if (inputAuthorized && ((_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonUp) || (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonUp)))
        {
            ShootStop();
            CurrentWeapon.WeaponInputReleased();
        }

        if ((CurrentWeapon.WeaponState.CurrentState == Weapon.WeaponStates.WeaponDelayBetweenUses)
            && ((_inputManager.ShootAxis == MMInput.ButtonStates.Off) && (_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.Off))
            && !(UseSecondaryAxisThresholdToShoot && (_inputManager.SecondaryMovement.magnitude > _inputManager.Threshold.magnitude)))
        {
            CurrentWeapon.WeaponInputStop();
        }

        if (inputAuthorized && UseSecondaryAxisThresholdToShoot && (_inputManager.SecondaryMovement.magnitude > _inputManager.Threshold.magnitude))
        {
            ShootStart();
        }
    }
}
