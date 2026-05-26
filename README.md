# Grid Builder

> Reusable grid system for Unity. Build rectangle and hexagon grids directly into your scene from a simple editor window — supports XZ and XY planes, and is easy to extend with new grid types.


<img width="900" height="500" alt="Ekran görüntüsü 2026-05-26 180839" src="https://github.com/user-attachments/assets/bcf77b1e-cbf6-45cd-80e2-96df10e81776" />
<img width="900" height="500" alt="Ekran görüntüsü 2026-05-26 180857" src="https://github.com/user-attachments/assets/0e7317e8-e86f-49d0-baa2-a8604a9076e0" />

---

## Features

- Rectangle and hexagon grid layouts
- Hexagon support for both pointy-top and flat-top orientations
- Works on the XZ plane (3D, ground) or the XY plane (2D, upright)
- Editor tool to generate the grid into the scene with one click
- Each tile knows its coordinate and its neighbors (visible in the inspector)
- Clean separation between grid logic and rendering — extend with new grid types without touching existing code

---

## Installation

There are two ways to install Grid Builder. Pick whichever fits your workflow.

### Option 1 — Unity Package Manager ()

1. Open **Window → Package Manager**.
2. Click the **+** button (top-left) and choose **Add package from git URL**.
3. Paste the repository git URL:
   ```
   https://github.com/umitcanyucesoy/GridBuilder-Tool.git
   ```
4. Click **Add**. The package will appear under **Packages** in your Project window.

> Note: packages installed via git URL live under `Packages/` and are **read-only**. If you want to edit the example assets directly, use Option 2, or copy the examples into your own `Assets/` folder.

### Option 2 — Import the .unitypackage


<img width="1258" height="549" alt="Ekran görüntüsü 2026-05-26 181009" src="https://github.com/user-attachments/assets/1841bc68-55a4-4e88-99b4-bf3069922e61" />


1. Go to the [Releases](https://github.com/umitcanyucesoy/GridBuilder-Tool/releases/tag/v1.0.0) page.
2. Download the latest `.unitypackage` file.
3. In Unity, double-click the file (or use **Assets → Import Package → Custom Package**) and import everything.

The files will be added to your `Assets/` folder and are fully editable.


---

## Getting Started

Follow these steps to build your first grid.

### 1. Create a tile prefab

Create a prefab for a single cell (a quad, a hexagon mesh, anything you like) and add the `Tile` component to it.

### 2. Create a Grid Definition

Right-click in the Project window → **Create → Grid System → Rectangle Grid Definition** (or **Hexagon Grid Definition**).

Set its values:
- **Width / Height** — grid size in cells
- **Cell Size** — spacing between cells
- **Plane** — XZ or XY
- **Tile Prefab** — the prefab from step 1
- *(Hexagon only)* **Orientation** — pointy-top or flat-top

### 3. Create a Grid Registry

Right-click → **Create → Grid System → Grid Registry**.

Add an entry for each grid type, giving it a display name (e.g. "Rectangle", "Hex") and assigning the matching Grid Definition.

### 4. Open the tool and build

1. Open **Tools → GridTools**.
2. Assign your **Grid Registry**.
3. Choose a grid type from the dropdown.
4. Adjust the settings if needed.
5. Click **Create Grid**.

The grid is generated into the scene under a single `Grid` root object. Building again clears the previous grid and rebuilds it.



---

## How it works

- **Core** holds the pure grid logic: coordinates, cells, and layout math. No scene dependencies.
- **Layouts** (`RectLayout`, `HexLayout`) handle the geometry — converting coordinates to world positions and finding neighbors.
- **Editor** contains the tool that builds tiles into the scene.

To add a new grid type, create a new layout and a new grid definition — no existing code needs to change.

---
