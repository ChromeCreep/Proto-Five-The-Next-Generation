using UnityEngine;

public class ProjectDetails : MonoBehaviour
{
	// No Can See You

	// A local-multiplayer game where both players can't see each other, or even themselves.
	// They must shoot in order to create soundwaves, where when they shoot, the enemy becomes visible for them for a limited time.
	// Both players have a limited amount of ammo, and if they both run out, it ends in a draw.

	// There are three stages.
	// For each stage, there is three seconds before they are allowed to shoot.
	// The first, there are no obstacles and both are spawned on the map opposing each other.
	// The second, there is a block in the middle of the stage.
	// The third, there are two walls, akin to the paddles in Pong.
}
