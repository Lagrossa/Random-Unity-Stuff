# Random-Unity-Stuff
A bunch of random, Unity scripts.



#For-My-Complicated-Projects







I made a plane model and a controller for the plane using new math that I learned about vectors, lerps, and rays. This was a bit of a dive into the deep end since I also researched a bit into Quaternions (which I still don't understand). Documenting this just in case I forget...

A ray is cast from the mouse position on the camera to the "world". The position of the camera is added to the endpoint of the line so that it can properly visualize the direction that the plane will travel in, proportionate to the local vector. I had to translate the plane according to the local space because using world space ended up breaking the direction. I multiplied by Time.DeltaTime to normalize the speed (Although it might move faster with a higher FPS computer... I'm not sure how that's fixed.) and then I also added a speedMultiplier to that normalization because my computer is kinda slow and the plane wasn't moving fast enough for my liking. The Inverse Lerp and Quaternion bit towards the end manages the rotation of the plane. I used Inverse Lerp to get the percent of the screen that the mouse is taking up ( I'm pretty sure this would only work for my computer, I don't know how it would work on a larger monitor because this is based on pixel count... And obviously, a larger monitor has more pixels. I needed this as a percentage specifically so that I could have a modifiable and easy way to interpolate between different values. This decimal would be my "t" value in the Lerp formula. By plugging that value into to another lerp function that scales the rotation of the plane's x-axis between -20 and 20, I'm able to rotate the plane by 20 degrees when the mouse is all the way to the right of the screen, and to rotate it -20 degrees when the mouse is all the way to the left.
