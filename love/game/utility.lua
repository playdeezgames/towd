local message_panel = require "message_panel"
local M = {}

function M.send_message(color, ...)
    local args = {...}
    for _, line in ipairs(args) do
      message_panel.write_line(color,line)
    end
end


return M