# GK1 - Computer Graphics Foundations

This repository tracks the lab tasks and engineering milestones developed during the "Computer Graphics 1" (Grafika Komputerowa 1) university module. The codebase deliberately steps away from massive, automated commercial engines like Unity or Unreal to look directly under the hood at how pixels are drawn, shapes are mathematically transformed in space, and light interacts dynamically with virtual surfaces.

## Key Mechanisms
The codebase serves as a step-by-step implementation of the classic computer graphics rendering pipeline. It focuses on low-level manipulation of graphics context, handling tasks that modern frameworks usually obscure from the developer.

Inside the source files, you will find comprehensive implementations covering:
* Rasterization from Scratch: Writing pixel-level algorithms to draw basic geometric primitives, including lines (Bresenham's algorithm), circles, and complex filled polygons.
* Spatial Mathematics: Applying affine transformations, vector arithmetic, perspective and orthographic projections, and matrix calculations to move 3D coordinates onto a flat 2D screen.
* Rendering & Shading: Handling fundamental illumination models (like ambient, diffuse, and specular reflections) alongside custom viewport logic to control camera angles and field of view.

## Getting Started
Depending on the specific phase of the curriculum, these projects are built using C++ and interface with foundational rendering utilities like OpenGL, FreeGLUT, or GLFW. You will need a C++ compiler and a build automation tool installed to run these projects locally.

1. Clone the project files down to your environment:
   git clone https://github.com/barankiewicz/GK1.git
   cd GK1

2. Create a dedicated build directory and compile the source files using CMake:
   mkdir build
   cd build
   cmake ..
   cmake --build .

3. Execute the compiled binary file generated in your build directory to open the interactive canvas.

## Usage
When you launch any of the compiled lab applications, a dedicated rendering window will open. You can interact with the scene directly using the mouse or specific keyboard hotkeys mapped within the source code. Use these controls to rotate 3D geometries, scale components on the fly, toggle wireframe viewports, or swap shading algorithms instantly to see how the lighting equations behave on screen.

## Contributing
This codebase documents a strict progression through the university graphics curriculum. While pull requests are not actively monitored for merging, detailed feedback on algorithmic efficiency or cross-platform compilation fixes is always welcome in the issues section.
