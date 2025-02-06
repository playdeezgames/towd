if os.getenv("LOCAL_LUA_DEBUGGER_VSCODE") == "1" then
  require("lldebugger").start()
end

local back_buffer = require "scene.back_buffer"
local state_machine = require "scene.state_machine"
local grimoire = require "game.grimoire"
local gfx = require "gfx.gfx"
local sfx = require "game.sfx"

function love.load(arg)
  love.keyboard.setKeyRepeat(true)
  gfx.load()
  sfx.load()
  back_buffer.load()
  state_machine.load()
end

function love.update()
  back_buffer.update(state_machine.update)
end

function love.draw()
  back_buffer.draw()
end

local key_table = {
  up=grimoire.COMMAND_UP,
  down=grimoire.COMMAND_DOWN,
  left=grimoire.COMMAND_LEFT,
  right=grimoire.COMMAND_RIGHT,
  space=grimoire.COMMAND_GREEN,
  escape=grimoire.COMMAND_RED,
  ["return"]=grimoire.COMMAND_BLUE
}

function love.keypressed(key, _, isrepeat)
  local command = key_table[key]
  if command ~= nil then
    state_machine.handle_command(command, isrepeat)
  end
end
