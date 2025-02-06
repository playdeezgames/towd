local sfx = require "game.sfx"
if os.getenv("LOCAL_LUA_DEBUGGER_VSCODE") == "1" then
  require("lldebugger").start()
end
local json = require "json"
local romfont        = require "romfont"
local status_panel = require "status_panel"
local message_panel = require "message_panel"
local tool_tip = require "tool_tip"
local grid = require "grid"
local character = require "world.character"
local verb_type      = require "world.verb_type"
local directions     = require "game.directions"
local world_initializer = require "world.initializers.world"
local avatar = require "world.avatar"

function love.load(arg)
    sfx.load()
    romfont.set_up()
    grid.set_up()
    status_panel.set_up()
    message_panel.set_up()
    tool_tip.set_up()
    world_initializer.initialize()
    love.keyboard.setKeyRepeat(true)
end

function love.update()
  status_panel.update()
  tool_tip.update()
end

function love.draw()
  grid.draw()
  status_panel.draw()
  message_panel.draw()
  tool_tip.draw()
end

function love.keypressed(key, scancode, isrepeat)
  if key == "up" then
    character.do_verb(avatar.get_character(), verb_type.MOVE, {direction_id = directions.NORTH})
  elseif key == "down" then
    character.do_verb(avatar.get_character(), verb_type.MOVE, {direction_id = directions.SOUTH})
  elseif key == "left" then
    character.do_verb(avatar.get_character(), verb_type.MOVE, {direction_id = directions.WEST})
  elseif key == "right" then
    character.do_verb(avatar.get_character(), verb_type.MOVE, {direction_id = directions.EAST})
  elseif key == "space" then
    character.do_verb(avatar.get_character(), verb_type.ACTION, {})
  end
end