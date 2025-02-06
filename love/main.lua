if os.getenv("LOCAL_LUA_DEBUGGER_VSCODE") == "1" then
  require("lldebugger").start()
end

local back_buffer = require "scene.back_buffer"

function love.load(arg)
  love.keyboard.setKeyRepeat(true)
  back_buffer.init()
end

function love.update()
  back_buffer.update()
end

function love.draw()
  back_buffer.draw()
end

function love.keypressed(key, scancode, isrepeat)
end
