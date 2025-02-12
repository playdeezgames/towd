if os.getenv("LOCAL_LUA_DEBUGGER_VSCODE") == "1" then
  require("lldebugger").start()
end
local stage = require "game.states.stage"

local root = nil
function love.load(arg)
  love.keyboard.setKeyRepeat(true)
  root = stage.new()
  root:load()
end

function love.update(dt)
  assert(root~=nil,"root should not be nil")
  root:update(dt)
end

function love.draw()
  assert(root~=nil,"root should not be nil")
  root:draw()
end

function love.quit()
  assert(root~=nil,"root should not be nil")
  return root:quit()
end

function love.keypressed(key, scancode, isrepeat)
  assert(root~=nil,"root should not be nil")
  root:keypressed(key, scancode, isrepeat)
end

function love.keyreleased(key, scancode)
  assert(root~=nil,"root should not be nil")
  root:keyreleased(key, scancode)
end
