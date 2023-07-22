I overcomplicated a little with the logic, but was fun to work in it, I’ve never did something like this from 0.

My logic to solve the problem was:
- I created the sprites from 0 by using pyxelEdit, tried to copy the starred valley style but I’m not an artist. I created 3 T-shirts, pants, shoes and hair, and for each I did the frames for the walking animation.
- PlayerAnimationController: Manages the animations frame by frame with lists of sprites, this data is saved in a ScriptableObject (Up,Down and Horizontal sprites - 4 frames for each animation)
- The shop was not difficult, but I used it with a shopId and sectionId (sections are Body, Pants, Shoes and Hair), the shopId is the same I used for bodyParts in the animation frames. The difficulty here was to save all the data in a list of a class that works as a one to much relation in a database, basically I save the shopId, sectionId and a list of animations by index (groupIndex, partIndex, bodyPartIndex) the group is the set of clothes, every group contains a hair, head, body, arms, legs and shoes set of sprites - partIndex means if is Up, Down or Horizontal type of sprites - bodyPartIndex are hair, head, body, arms, legs and shoes)
- Other difficulty was to make the saveManager, and save the data and filter it by some specific points for the shop and for the animations in the PlayerAnimationController.

I had some problems with my computer so I hadn’t have the whole 2 days, but I think is fair to send what I could work in this couple days.

Thank you so much for the problem to solve, I lately keep working on web and thought I was a little rusty with my unity skills but I’m proud to almost solve the problem even with the problems with my computer.