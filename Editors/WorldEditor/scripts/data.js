var world =
{
    "TileWidth": 8,
    "TileHeight": 8,
    "Avatar": 0,
    "Creatures": {
      "Tagon": {
        "ResourceIdentifier": "dungeon-creatures",
        "ResourceIndex": 0
      }
    },
    "CreatureInstances": {
      "0": {
        "Creature": "Tagon",
        "Room": "home",
        "Column": 2,
        "Row": 4
      }
    },
    "Terrains": {
      "floor": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 3,
        "Role": 0
      },
      "solid": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 0,
        "Role": 1
      },
      "north-wall": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 4,
        "Role": 1
      },
      "east-wall": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 5,
        "Role": 1
      },
      "south-wall": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 6,
        "Role": 1
      },
      "west-wall": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 7,
        "Role": 1
      },
      "ne-corner-inside": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 8,
        "Role": 1
      },
      "se-corner-inside": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 9,
        "Role": 1
      },
      "sw-corner-inside": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 10,
        "Role": 1
      },
      "nw-corner-inside": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 11,
        "Role": 1
      },
      "ne-corner-outside": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 12,
        "Role": 1
      },
      "se-corner-outside": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 13,
        "Role": 1
      },
      "sw-corner-outside": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 14,
        "Role": 1
      },
      "nw-corner-outside": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 15,
        "Role": 1
      },
      "north-doorway": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 16,
        "Role": 0
      },
      "east-doorway": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 17,
        "Role": 0
      },
      "south-doorway": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 18,
        "Role": 0
      },
      "west-doorway": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 19,
        "Role": 0
      },
      "ladder-up": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 20,
        "Role": 0
      },
      "ladder-down": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 21,
        "Role": 0
      },
      "bed": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 22,
        "Role": 1
      },
      "box": {
        "ResourceIdentifier": "dungeon-tiles",
        "ResourceIndex": 23,
        "Role": 1
      }
    },
    "Rooms": {
      "home": {
        "Caption": "Home",
        "Width": 6,
        "Pixels": [
          {
            "Terrain": "nw-corner-inside"
          },
          {
            "Terrain": "north-wall"
          },
          {
            "Terrain": "north-wall"
          },
          {
            "Terrain": "north-wall"
          },
          {
            "Terrain": "north-wall"
          },
          {
            "Terrain": "ne-corner-inside"
          },
          {
            "Terrain": "west-wall"
          },
          {
            "Terrain": "ladder-up",
            "RoleOverride": 2,
            "Teleport": {
              "Room": "loft",
              "Column": 1,
              "Row": 1,
              "Prompt": "Climb ladder?"
            }
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "east-wall"
          },
          {
            "Terrain": "west-wall"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "east-wall"
          },
          {
            "Terrain": "west-wall"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "east-wall"
          },
          {
            "Terrain": "west-wall"
          },
          {
            "Terrain": "bed",
            "RoleOverride": 3
          },
          {
            "Terrain": "floor",
            "CreatureInstance": 0
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "east-wall"
          },
          {
            "Terrain": "sw-corner-inside"
          },
          {
            "Terrain": "south-wall"
          },
          {
            "Terrain": "south-wall"
          },
          {
            "Terrain": "south-doorway",
            "RoleOverride": 2,
            "Teleport": {
              "Room": "hometown",
              "Column": 0,
              "Row": 0,
              "Prompt": "Leave home?"
            }
          },
          {
            "Terrain": "south-wall"
          },
          {
            "Terrain": "se-corner-inside"
          }
        ]
      },
      "loft": {
        "Caption": "Loft",
        "Width": 5,
        "Pixels": [
          {
            "Terrain": "nw-corner-inside"
          },
          {
            "Terrain": "north-wall"
          },
          {
            "Terrain": "north-wall"
          },
          {
            "Terrain": "north-wall"
          },
          {
            "Terrain": "ne-corner-inside"
          },
          {
            "Terrain": "west-wall"
          },
          {
            "Terrain": "ladder-down",
            "RoleOverride": 2,
            "Teleport": {
              "Room": "home",
              "Column": 1,
              "Row": 1,
              "Prompt": "Climb ladder?"
            }
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "east-wall"
          },
          {
            "Terrain": "west-wall"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "east-wall"
          },
          {
            "Terrain": "west-wall"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "box",
            "RoleOverride": 3
          },
          {
            "Terrain": "east-wall"
          },
          {
            "Terrain": "sw-corner-inside"
          },
          {
            "Terrain": "south-wall"
          },
          {
            "Terrain": "south-wall"
          },
          {
            "Terrain": "south-wall"
          },
          {
            "Terrain": "se-corner-inside"
          }
        ]
      },
      "hometown": {
        "Caption": "Town",
        "Width": 8,
        "Pixels": [
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          },
          {
            "Terrain": "floor"
          }
        ]
      }
    }
  };