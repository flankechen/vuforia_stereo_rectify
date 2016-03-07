## vuforia stereo rectify

this is sample code of warping background image in vuforia for stereo rectify. [vuforia example](https://developer.vuforia.com/library/articles/Solution/How-To-Access-the-Camera-Image-in-Unity) provide two ways of accessing the camera image. While the first image class example leads low frame raw, we use the second approach, shader program warps the video background directly. The opencv precompute maps, lookup coordinate for warping is passed into the shader program as another texture. The result is cool. I achieve up to 60fps frame rate in my environment.  

## Installation

1. /save_param/ keeps the calibration output files from opencv stereo calibration example like [this](http://docs.opencv.org/2.4/doc/tutorials/calib3d/camera_calibration/camera_calibration.html), we only use the precompute maps calib_para.yml, make sure it's in CV_16S2 formate. change these files to your calibration reslut.
2. I have two cameras with resolution 960*1080. Change this to yours in some of the codes and the vuforia setup xml.
3. /opencv_dll/ is a sample opencv dll project, producing dlls for unity plugin. I use opencv to read the yml files and change the map from CV_16S2 to CV_32FC1. opencv 2.4.11 for win32 is used. recompile the dlls and copy them to the /assects/plugins/ folder.


