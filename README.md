# SimpleUIAnimationPackage
## About
Simple UI Animation Package is an Unity package that implements multiple types of animation like moving, scaling, rotating and text animations.
It is also possible to delay, synchronize and group animations.

## Dependencies:
This package uses LeanTween (https://assetstore.unity.com/packages/tools/animation/leantween-3595) to tween UI element values, and TextMeshPro text elements.

## How to use:

## Simple Animations:

### Simple UI element animations:
Add an "UI Element Animation" component to UI element.

Select which properties you want to animate by checking the apply checkbox in the apropriate submenu.

Define start and end values by either manually wiriting in the values, or by adjusting the element in the scene and clicking the "Set Start" or "Set End" buttons.

Define animation curves to create more complex animations. (Animation curves are set to Linear by default)

### Simple Text element animation
Add an "Text Animation Group" component to TMP_Text element.

Set desired properties.

## Synchornised Animations
### Simple simultaneous or consecutive animations:
Add an "Animate Children Animation Group" component to the parent of UI components you want to synchronise.

Select Simultaneous or Consecutive animation type and the script will animate all the children that have an "Animation Group" Component.

### Complex Synchronised Animation chains:
Add an "Synchronisation Animation Group" component to any GameObject.

Add an entry element and drag and drop "Animation Group" components you wish to animate.

Select Entry or Exit animation.

Select a Sync Option:
  - Wait Until - Waits until the animation component defined in "Wait For" field finishes, 
  - Wait For Seconds - Waits until the number of seconds defined in the "Delay For" field passes,
  - None - Starts the Animation without any delays
