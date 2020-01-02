# PixelWarrior
This is a 2D shooting game that player can kill the monsters to get scores and unlock new things in the store.

# DEMO
![Hnet-image(2)](https://user-images.githubusercontent.com/43282464/71638916-f2fa6980-2c29-11ea-9cdd-dace9ae9199c.gif)

## MobileJoyStick
* It creates two virtual Joysticks to controll the movement and attack action of player.

## Random Map and Monsters
* It creates the map and monsters randomly.
* The monsters attributes are read from csv files.
* The map uses the Berlin algorithm to automatically generate.

## Design
* The maginitude between center point of joystick and the drag position will be the moving speed.
* The normalized vector between center point and the drag position will be the direciton.
