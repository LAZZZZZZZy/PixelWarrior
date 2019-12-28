# MobileJoyStick
This project creates a virtual Joystick for the mobile platform since Unity3D supports cross-platform transformation so that it can run in both IOS and Android systems.

## Implementation
* There are two virtual Joysticks, right one for attack and left one for moving.
* When user drag the move Joystick, the player object can move toward the drag direction.
* When user drag the attack Joystick, the player will shot the bullet which moves toward the attack direction.


## Design
* The maginitude between center point of joystick and the drag position will be the moving speed.
* The normalized vector between center point and the drag position will be the direciton.
