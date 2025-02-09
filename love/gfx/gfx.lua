local M = {}
function M.clear(color)
    assert(type(color)=="table","color should be a table")
    love.graphics.clear(color[1],color[2],color[3])
end
return M